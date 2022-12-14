using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using Ext.Net;

using Mesnac.Business.Interface;
using Mesnac.Business.Implements;
using Mesnac.Data.Interface;
using Mesnac.Data.Implements;
using Mesnac.Entity;
using Mesnac.Web.UI;

using NBear.Common;
using System.Text;

public partial class Manager_Technology_QmtPassInfo : BasePage
{
    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            浏览 = new SysPageAction() { ActionID = 0, ActionName = "" };
            查询 = new SysPageAction() { ActionID = 1, ActionName = "ButtonNorthQuery" };
            放行 = new SysPageAction() { ActionID = 2, ActionName = "ButtonNorthPass" };
            掺用 = new SysPageAction() { ActionID = 3, ActionName = "Button1" };
            审核 = new SysPageAction() { ActionID = 4, ActionName = "Button2" };
            撤销审核 = new SysPageAction() { ActionID = 5, ActionName = "Button3" };
        }
        public SysPageAction 浏览 { get; private set; } //必须为 public
        public SysPageAction 查询 { get; private set; } //必须为 public
        public SysPageAction 放行 { get; private set; } //必须为 public
        public SysPageAction 掺用 { get; private set; } //必须为 public
        public SysPageAction 审核 { get; private set; } //必须为 public
        public SysPageAction 撤销审核 { get; private set; } //必须为 public
    }
    #endregion

    /// <summary>
    /// 页面加载
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            HtmlGenericControl cssLink = new HtmlGenericControl("link");
            cssLink.Attributes.Add("type", "text/css");
            cssLink.Attributes.Add("rel", "stylesheet");
            cssLink.Attributes.Add("href", this.ResolveUrl("~/resources/css/main.css"));
            this.Page.Header.Controls.Add(cssLink);

            cssLink = new HtmlGenericControl("link");
            cssLink.Attributes.Add("type", "text/css");
            cssLink.Attributes.Add("rel", "stylesheet");
            cssLink.Attributes.Add("href", this.ResolveUrl("~/resources/css/ext-chinese-font.css"));
            this.Page.Header.Controls.Add(cssLink);

            //HtmlGenericControl scriptLink = new HtmlGenericControl("script");
            //scriptLink.Attributes.Add("type", "text/javascript");
            //scriptLink.Attributes.Add("src", ".js?" + DateTime.Now.Ticks.ToString());
            //this.Page.Header.Controls.Add(scriptLink);

            InitControls();

            //ComboBoxQuerySearchType.SetValue("1");
            DateFieldQueryBeginDate.SetValue(DateTime.Today.ToString("yyyy-MM-dd"));
            DateFieldQueryEndDate.SetValue(DateTime.Today.ToString("yyyy-MM-dd"));
        }
    }

    /// <summary>
    /// 初始化控件
    /// </summary>
    private void InitControls()
    {
        // 班次 
        IPptShiftManager bPptShiftManager = new PptShiftManager();
        EntityArrayList<PptShift> mPptShiftList = bPptShiftManager.GetListByWhereAndOrder(PptShift._.UseFlag == 1
            , PptShift._.ObjID.Asc);
        foreach (PptShift mPptShift in mPptShiftList)
        {
            ComboBoxQueryShift.AddItem(mPptShift.ShiftName, mPptShift.ObjID.ToString());
            //ComboBoxQueryShift.Items.Add(new Ext.Net.ListItem() { Text = mPptShift.ShiftName, Value = mPptShift.ObjID.ToString() });
        }
        string sql = "select * from dbo.CJItem";
        DataSet ds = bPptShiftManager.GetBySql(sql).ToDataSet();
        foreach (DataRow dr in ds.Tables[0].Rows)
        {

            ComboBox2.AddItem(dr["Name"].ToString(), dr["ID"].ToString());
          }
        sql = @"
select * from basmaterial
where left(materialcode,1)>='4' and deleteflag ='0'
order by materialname";
        ds = bPptShiftManager.GetBySql(sql).ToDataSet();
        foreach (DataRow dr in ds.Tables[0].Rows)
        {

            ComboBox1.AddItem(dr["MaterialName"].ToString(), dr["MaterialCode"].ToString());
        }

        // 
    }

    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonNorthQuery_Click(object sender, DirectEventArgs e)
    {
        DataSet ds = GetMainDataSet();

        StoreMain.DataSource = ds;
        StoreMain.DataBind();

        if (ds.Tables[0].Rows.Count == 0)
        {
            X.Msg.Alert("提示", "未找到符合条件的记录").Show();
            return;
        }
        else
        {
            //X.Msg.Alert("提示", "查询完毕").Show();

        }
    }

    /// <summary>
    /// 获取查询结果
    /// </summary>
    /// <returns></returns>
    private DataSet GetMainDataSet()
    {
        string beginPlanDate = "";
        string endPlanDate = "";
        string beginPassDate = "";
        string endPassDate = "";
        if (RadioGroupQuerySearchType.CheckedItems[0].InputValue == "1")
        {
            // 按生产日期
            beginPlanDate = DateFieldQueryBeginDate.RawText;
            endPlanDate = DateFieldQueryEndDate.RawText;
        }
        else if (RadioGroupQuerySearchType.CheckedItems[0].InputValue == "2")
        {
            // 按放行日期
            beginPassDate = DateFieldQueryBeginDate.RawText;
            endPassDate = DateFieldQueryEndDate.RawText;
        }
        string passFlag = ComboBoxQueryPassFlag.Value.ToString();
        string shiftId = ComboBoxQueryShift.Value.ToString();
        string zjsID = TextFieldQueryZJS.RawText.Trim();
        string barcode = TextFieldQueryBarcode.RawText.Trim();
        string llBarcode = TextFieldQueryLLBarcode.RawText.Trim();

        IQmtPassInfoQueryParams paras = new QmtPassInfoQueryParams();
        paras.BeginPlanDate = beginPlanDate;
        paras.EndPlanDate = endPlanDate;
        paras.BeginPassDate = beginPassDate;
        paras.EndPassDate = endPassDate;

        paras.PassFlag = passFlag;
        paras.ShiftId = shiftId;
        paras.ZJSID = zjsID;
        paras.Barcode = barcode;
        paras.LLBarcode = llBarcode;

        IQmtPassInfoManager bQmtPassInfoManager = new QmtPassInfoManager();
        DataSet ds = GetDataInfoByQueryParams(paras); //
        return ds;
    }
    public DataSet GetDataInfoByQueryParams(IQmtPassInfoQueryParams queryParams)
    {
        StringBuilder sb = new StringBuilder();
        #region

        sb.AppendLine("Select A.*");
        sb.AppendLine(", B.EquipName, C.ShiftName, D.ClassName,F.materialname as CMaterialName,G.Name,E.Weight,Case E.AuditFlag when 1 then '是' else   ( Case E.AuditFlag  when 0 then '否'  else '' end )  end as AuditFlag");
        sb.AppendLine("From PptShiftConfig A");
        sb.AppendLine("Left Join BasEquip B On A.EquipCode = B.EquipCode");
        sb.AppendLine("Left Join PptShift C On A.ShiftID = C.ObjID");
        sb.AppendLine("Left Join PptClass D On A.ClassID = D.ObjID");
        sb.AppendLine("Left Join CJInfo E On A.Barcode = E.Barcode");
        sb.AppendLine("Left Join Basmaterial F On E.toMaterialCode = F.MaterialCode");
        sb.AppendLine("Left Join CJItem G On E.CJItem = G.ID");
        sb.AppendLine("WHERE 1 = 1 And A.MaterialCode > '5' And A.CheckFlag = '2'");
        if (!string.IsNullOrEmpty(queryParams.BeginPlanDate))
        {
            sb.AppendFormat("And A.PlanDate >= '{0}'", queryParams.BeginPlanDate);
            sb.AppendLine();
        }
        if (!string.IsNullOrEmpty(queryParams.EndPlanDate))
        {
            sb.AppendFormat("And DATEADD(DAY, -1, A.PlanDate) < '{0}'", queryParams.EndPlanDate);
            sb.AppendLine();
        }
        if (!string.IsNullOrEmpty(queryParams.BeginPassDate))
        {
            sb.AppendFormat("And A.PassTime >= '{0}'", queryParams.BeginPassDate);
            sb.AppendLine();
        }
        if (!string.IsNullOrEmpty(queryParams.EndPassDate))
        {
            sb.AppendFormat("And DATEADD(DAY, -1, A.PassTime) < '{0}'", queryParams.EndPassDate);
            sb.AppendLine();
        }
        if (!string.IsNullOrEmpty(queryParams.PassFlag))
        {
            sb.AppendFormat("And ISNULL(A.PassFlag, '0') = '{0}'", queryParams.PassFlag);
            sb.AppendLine();
        }
        if (!string.IsNullOrEmpty(queryParams.ShiftId))
        {
            sb.AppendFormat("And A.ShiftId = '{0}'", queryParams.ShiftId);
            sb.AppendLine();
        }
        if (!string.IsNullOrEmpty(queryParams.ZJSID))
        {
            sb.AppendFormat("And A.ZJSID = '{0}'", queryParams.ZJSID);
            sb.AppendLine();
        }
        if (!string.IsNullOrEmpty(queryParams.Barcode))
        {
            sb.AppendFormat("And A.Barcode = '{0}'", queryParams.Barcode);
            sb.AppendLine();
        }
        if (!string.IsNullOrEmpty(queryParams.LLBarcode))
        {
            sb.AppendFormat("And A.LLBarcode = '{0}'", queryParams.LLBarcode);
            sb.AppendLine();
        }


        if (queryParams.PageResult != null && string.IsNullOrEmpty(queryParams.PageResult.Orderfld) == false)
        {
            sb.AppendLine("ORDER BY " + queryParams.PageResult.Orderfld);
        }
        else
        {
            sb.AppendLine("ORDER BY A.Barcode");
        }
        #endregion
        IPptShiftManager bPptShiftManager = new PptShiftManager();
        NBear.Data.CustomSqlSection css = bPptShiftManager.GetBySql(sb.ToString());
        return css.ToDataSet();

    }
    /// <summary>
    /// 打开放行窗口
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonNorthPass_Click(object sender, DirectEventArgs e)
    {
        if (SelectionModelMain.SelectedRows.Count == 0)
        {
            X.Msg.Alert("提示", "请选择要放行的记录").Show();
            return;
        }

        string barcode = SelectionModelMain.SelectedRecordID;
        TextFieldEditBarcode.SetValue(barcode);
        TextFieldEditPassMemo.SetValue("");
        WindowPass.Show();

    }
    protected void ButtonCY_Click(object sender, DirectEventArgs e)
    {
        if (SelectionModelMain.SelectedRows.Count == 0)
        {
            X.Msg.Alert("提示", "请选择要放行的记录").Show();
            return;
        }

        string barcode = SelectionModelMain.SelectedRecordID;
        TextBarcode.SetValue(barcode);
        TextWeight.SetValue("");
        WindowCY.Show();

    }
    protected void ButtonSH_Click(object sender, DirectEventArgs e)
    {
        if (SelectionModelMain.SelectedRows.Count == 0)
        {
            X.Msg.Alert("提示", "请选择要放行的记录").Show();
            return;
        }
        IPptShiftManager bPptShiftManager = new PptShiftManager();
        string barcode = SelectionModelMain.SelectedRecordID;
        String sql = "update CJInfo set Auditflag ='1'  where barcode = '" + barcode + "'";
        DataSet dt = bPptShiftManager.GetBySql(sql).ToDataSet();
        AppendWebLog("不合格胶放行-掺用审核", barcode);

        DataSet ds = GetMainDataSet();
        StoreMain.DataSource = ds;
        StoreMain.DataBind();



        X.Msg.Alert("提示", "审核成功").Show();

    }
    protected void ButtonCSH_Click(object sender, DirectEventArgs e)
    {
        if (SelectionModelMain.SelectedRows.Count == 0)
        {
            X.Msg.Alert("提示", "请选择要放行的记录").Show();
            return;
        }
        IPptShiftManager bPptShiftManager = new PptShiftManager();
        string barcode = SelectionModelMain.SelectedRecordID;
        String sql = "update CJInfo set Auditflag ='0'  where barcode = '" + barcode + "'";
        DataSet dt = bPptShiftManager.GetBySql(sql).ToDataSet();
        AppendWebLog("不合格胶放行-撤销掺用审核", barcode);

        DataSet ds = GetMainDataSet();
        StoreMain.DataSource = ds;
        StoreMain.DataBind();



        X.Msg.Alert("提示", "撤销审核成功").Show();
    }
    /// <summary>
    /// 放行
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonEditAccept_Click(object sender, DirectEventArgs e)
    {
        string barcode = TextFieldEditBarcode.RawText.Trim();
        string passMemo = TextFieldEditPassMemo.RawText.Trim();
        string passUserId = this.UserID;
        string passUserName = "";
        IBasUserManager bBasUserManager = new BasUserManager();
        EntityArrayList<BasUser> mBasUserList = bBasUserManager.GetListByWhereAndOrder(
            BasUser._.WorkBarcode == this.UserID
            , BasUser._.DeleteFlag.Asc);
        if (mBasUserList.Count > 0)
        {
            passUserName = mBasUserList[0].UserName;
        }

        IQmtPassInfoManager bQmtPassInfoManager = new QmtPassInfoManager();
        bQmtPassInfoManager.Pass(barcode, passUserId, passUserName, passMemo);

        System.Text.StringBuilder methodResult = new System.Text.StringBuilder();
        methodResult.AppendFormat("Barcode={0},PassUserId={1},PassUserName={2},PassMemo={3}"
            , new object[] { barcode, passUserId, passUserName, passMemo });

        AppendWebLog("不合格胶放行-放行", methodResult.ToString(), 2);

        DataSet ds = GetMainDataSet();
        StoreMain.DataSource = ds;
        StoreMain.DataBind();

        WindowPass.Close();

        X.Msg.Alert("提示", "放行成功").Show();
    }


    protected void ButtonEditCY_Click(object sender, DirectEventArgs e)
    {
        IPptShiftManager bPptShiftManager = new PptShiftManager();
        string barcode = TextBarcode.RawText.Trim();
        string Weight = TextWeight.RawText.Trim();
        string passUserId = this.UserID;
        Double dw = 0;
        try {dw=Double.Parse(Weight); }
        catch(Exception ex)
        {  X.Msg.Alert("提示", "请输入合法重量").Show(); return;}


        String sql = "delete from CJInfo where barcode = '" + barcode + "'";
        DataSet dt = bPptShiftManager.GetBySql(sql).ToDataSet();
        sql = "insert into CJInfo values ('" + barcode + "','" + ComboBox2.SelectedItem.Value + "','" +"  "+ "','" + dw + "',0)";
        //X.Msg.Alert("提示",sql).Show(); return; ComboBox1.SelectedItem.Value 
        dt = bPptShiftManager.GetBySql(sql).ToDataSet();




        DataSet ds = GetMainDataSet();
        StoreMain.DataSource = ds;
        StoreMain.DataBind();

        WindowCY.Close();

        X.Msg.Alert("提示", "掺用成功").Show();
    }
}