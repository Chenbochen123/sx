using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.UI.HtmlControls;

using Ext.Net;

using Mesnac.Business;
using Mesnac.Business.Implements;
using Mesnac.Business.Interface;
using Mesnac.Entity;
using Mesnac.Data.Components;
using Mesnac.Data.Interface;
using Mesnac.Data.Implements;
using Mesnac.Web.UI;

using NBear.Common;

public partial class Manager_RubberQuality_Manage_CheckSummary : BasePage
{
    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            浏览 = new SysPageAction() { ActionID = 0, ActionName = "" };
            查询 = new SysPageAction() { ActionID = 1, ActionName = "ButtonQuery" };
            导出 = new SysPageAction() { ActionID = 2, ActionName = "ButtonExcel" };
        }
        public SysPageAction 浏览 { get; private set; } //必须为 public
        public SysPageAction 查询 { get; private set; } //必须为 public
        public SysPageAction 导出 { get; private set; } //必须为 public
    }
    #endregion

    private ISysCodeManager sysCodeManager = new SysCodeManager();

    QmtCheckMasterService procService = new QmtCheckMasterService();

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

            HtmlGenericControl scriptLink = new HtmlGenericControl("script");
            scriptLink.Attributes.Add("type", "text/javascript");
            scriptLink.Attributes.Add("src", "CheckSummary.js?" + DateTime.Now.Ticks.ToString());
            this.Page.Header.Controls.Add(scriptLink);

            InitControls();

            DateFieldSDate.SetValue(DateTime.Today);
            DateFieldEDate.SetValue(DateTime.Today);

            //StoreMain.LoadProxy();

        }

    }

    /// <summary>
    /// 初始化控件
    /// </summary>
    private void InitControls()
    {
        // 生产车间
        IBasWorkShopManager bBasWorkShopManager = new BasWorkShopManager();
        EntityArrayList<BasWorkShop> mBasWorkShopList = bBasWorkShopManager.GetListByWhereAndOrder(
            BasWorkShop._.DeleteFlag == "0"
            , BasWorkShop._.ObjID.Asc);
        foreach (BasWorkShop mBasWorkShop in mBasWorkShopList)
        {
            ComboBoxWorkShopId.AddItem(mBasWorkShop.WorkShopName, mBasWorkShop.ObjID.ToString());
        }

        // 生产班次
        IPptSetTimeManager bPptSetTimeManager = new PptSetTimeManager();
        EntityArrayList<PptSetTime> mPptSetTimeList = bPptSetTimeManager.GetListByWhereAndOrder(PptSetTime._.UseFlag == "1"
            & PptSetTime._.ProcedureID == 1
            , PptSetTime._.ShiftID.Asc);

        IPptShiftManager bPptShiftManager = new PptShiftManager();
        foreach (PptSetTime mPptSetTime in mPptSetTimeList)
        {
            string shiftName = "";
            PptShift mPptShift = bPptShiftManager.GetById(new object[] { mPptSetTime.ShiftID });
            if (mPptShift != null)
            {
                shiftName = mPptShift.ShiftName;
            }
            ComboBoxShiftId.AddItem(shiftName, mPptSetTime.ShiftID.Value.ToString());
        }

        // 班组
        IPptClassManager bPptClassManager = new PptClassManager();
        EntityArrayList<PptClass> mPptClassList = bPptClassManager.GetListByWhereAndOrder(PptClass._.UseFlag == "1"
           , PptClass._.ObjID.Asc);
        foreach (PptClass mPptClass in mPptClassList)
        {
            ComboBoxShiftClass.AddItem(mPptClass.ClassName, mPptClass.ObjID.ToString());
        }

        //主机手
        IBasMainHanderManager bBasMainHanderManager = new BasMainHanderManager();
        DataSet dsBasMainHander = bBasMainHanderManager.GetMixMainHanderInfo();
        foreach (DataRow drBasMainHander in dsBasMainHander.Tables[0].Rows)
        {
            ComboBoxZJSID.AddItem("[" + drBasMainHander["MainHanderCode"].ToString() + "]" + drBasMainHander["UserName"].ToString(), drBasMainHander["MainHanderCode"].ToString());
        }

        // 检验标准分类
        IQmtCheckStandTypeManager bQmtCheckStandTypeManager = new QmtCheckStandTypeManager();
        EntityArrayList<QmtCheckStandType> mQmtCheckStandTypeList = bQmtCheckStandTypeManager.GetListByWhereAndOrder(
            QmtCheckStandType._.DeleteFlag == "0"
            & QmtCheckStandType._.CheckTypeCode.In(new object[] { 2, 3 })
            , QmtCheckStandType._.ObjID.Asc);

        foreach (QmtCheckStandType mQmtCheckStandType in mQmtCheckStandTypeList)
        {
            ComboBoxStandCode.Items.Add(new Ext.Net.ListItem(mQmtCheckStandType.StandTypeName, mQmtCheckStandType.ObjID.ToString()));
        }

        //配方类型
        EntityArrayList<SysCode> lst = sysCodeManager.GetListByWhere(SysCode._.TypeID == "PmtType");
        IniComboBox(cboType, lst);

    }

    /// </summary>
    private const string constSelectAllText = "---请选择---";
    /// <summary>
    /// <summary>
    /// 初始化ComboBox
    /// </summary>
    /// <param name="cb">The cb.</param>
    /// <param name="lst">The LST.</param>
    /// <remarks></remarks>
    private void IniComboBox(ComboBox cb, EntityArrayList<SysCode> lst)
    {
        Ext.Net.ListItem allitem = new Ext.Net.ListItem(constSelectAllText, constSelectAllText);
        cb.Items.Clear();
        cb.Items.Add(allitem);
        foreach (SysCode m in lst)
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem(m.ItemName, m.ItemCode.ToString());
            cb.Items.Add(item);
        }
        if (cb.Items.Count > 0)
        {
            cb.Text = (cb.Items[0].Value);
        }
    }

    private IQmtCheckMasterSummaryQueryParams GetQmtCheckMasterSummaryQueryParams()
    {
        IQmtCheckMasterSummaryQueryParams paras = new QmtCheckMasterSummaryQueryParams();
        paras.PlanSDate = DateFieldSDate.SelectedDate.ToString("yyyy-MM-dd");
        paras.PlanEDate = DateFieldEDate.SelectedDate.ToString("yyyy-MM-dd");
        paras.WorkShopId = ComboBoxWorkShopId.Value.ToString();
        paras.ShiftId = ComboBoxShiftId.Value.ToString();
        paras.ShiftClass = ComboBoxShiftClass.Value.ToString();
        paras.ZJSID = ComboBoxZJSID.Value.ToString();
        paras.MaterCode = HiddenMaterCode.Value.ToString();
        paras.StandCode = ComboBoxStandCode.Value.ToString();
        paras.JudgeResult = ComboBoxJudgeResult.Value.ToString();
        paras.EquipCode = HiddenEquipCode.Value.ToString();

        return paras;
    }

    /// <summary>
    /// 获取查询数据
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <returns></returns>
    protected void StoreMain_ReadData(object sender, StoreReadDataEventArgs e)
    {
        IQmtCheckMasterManager bQmtCheckMasterManager = new QmtCheckMasterManager();

        IQmtCheckMasterSummaryQueryParams queryParams = GetQmtCheckMasterSummaryQueryParams();

        PageResult<QmtCheckMaster> pageParams = new PageResult<QmtCheckMaster>();
        pageParams.PageIndex = e.Page;
        pageParams.PageSize = e.Limit;
        if (e.Sort.Length > 0)
        {
            pageParams.Orderfld = e.Sort[0].Property;
            if (e.Sort[0].Direction.ToString().ToUpper() == "DESC")
            {
                pageParams.OrderType = 1;
            }
            else
            {
                pageParams.OrderType = 0;
            }
        }
        else
        {
            pageParams.Orderfld = "";
        }

        queryParams.PageParams = pageParams;

        //PageResult<QmtCheckMaster> pageResult = bQmtCheckMasterManager.GetCheckSummaryQueryByParas(queryParams);
        PageResult<QmtCheckMaster> pageResult = GetCheckSummaryQueryByParas(queryParams);

        e.Total = pageResult.RecordCount;
        DataTable dt = pageResult.DataSet.Tables[0];

        StoreMain.DataSource = dt;

        PagingToolbarMain.HideRefresh = false;
    }

    public PageResult<QmtCheckMaster> GetCheckSummaryQueryByParas(IQmtCheckMasterSummaryQueryParams paras)
    {
        Dictionary<string, object> dict = new Dictionary<string, object>();
        dict.Add("@PlanSDate", paras.PlanSDate);
        dict.Add("@PlanEDate", paras.PlanEDate);
        dict.Add("@WorkShopId", paras.WorkShopId);
        dict.Add("@ShiftId", paras.ShiftId);
        dict.Add("@ShiftClass", paras.ShiftClass);
        dict.Add("@ZJSID", paras.ZJSID);
        dict.Add("@StandCode", paras.StandCode);
        dict.Add("@MaterCode", paras.MaterCode);
        dict.Add("@JudgeResult", paras.JudgeResult);
        dict.Add("@EquipCode", paras.EquipCode);
        dict.Add("@CboType", cboType.Value.ToString());

        DataSet ds = procService.GetDataSetByStoreProcedure("ProcQmtSummaryCheckInfo", dict);

        PageResult<QmtCheckMaster> pageParams = paras.PageParams;

        if (pageParams != null && pageParams.PageSize > 0)
        {
            DataTable dt = ds.Tables[0];
            pageParams.RecordCount = dt.Rows.Count;

            if (pageParams.RecordCount > 0)
            {
                ds = new DataSet();
                DataTable dtr = null;
                if (pageParams.Orderfld == ""
                    || pageParams.Orderfld == (new QmtCheckMaster()).GetPropertyMappingColumnNames()[0])
                {
                    dtr = dt.AsEnumerable()
                        .Skip((pageParams.PageIndex - 1) * pageParams.PageSize)
                        .Take(pageParams.PageSize).CopyToDataTable();
                }
                else
                {
                    if (pageParams.OrderType == 1)
                    {
                        dtr = dt.AsEnumerable()
                            .OrderByDescending(x => x.Field<string>(pageParams.Orderfld))
                            .Skip((pageParams.PageIndex - 1) * pageParams.PageSize)
                            .Take(pageParams.PageSize).CopyToDataTable();
                    }
                    else
                    {
                        dtr = dt.AsEnumerable()
                            .OrderBy(x => x.Field<string>(pageParams.Orderfld))
                            .Skip((pageParams.PageIndex - 1) * pageParams.PageSize)
                            .Take(pageParams.PageSize).CopyToDataTable();
                    }
                }
                ds.Tables.Add(dtr);
            }

            pageParams.DataSet = ds;

            return pageParams;

        }
        else
        {
            if (pageParams == null)
            {
                pageParams = new PageResult<QmtCheckMaster>();
            }

            pageParams.DataSet = ds;

            return pageParams;
        }

    }

    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonQuery_Click(object sender, DirectEventArgs e)
    {
        StoreMain.LoadPage(1);
    }

    /// <summary>
    /// 导出
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonExcel_Click(object sender, DirectEventArgs e)
    {
        string recordCount = e.ExtraParams["RecordCount"];

        if (recordCount == "0")
        {
            X.Msg.Alert("提示", "没有找到符合条件的记录").Show();
            return;
        }
        else if (Convert.ToInt32(recordCount) > 65535)
        {
            X.Msg.Alert("提示", "记录总数不能超过65535").Show();
            return;
        }

        IQmtCheckMasterManager bQmtCheckMasterManager = new QmtCheckMasterManager();

        IQmtCheckMasterSummaryQueryParams queryParams = GetQmtCheckMasterSummaryQueryParams();

        //PageResult<QmtCheckMaster> pageResult = bQmtCheckMasterManager.GetCheckSummaryQueryByParas(queryParams);

        PageResult<QmtCheckMaster> pageResult = GetCheckSummaryQueryByParas(queryParams);
        DataTable dt = pageResult.DataSet.Tables[0];

        if (dt.Rows.Count == 0)
        {
            X.Msg.Alert("提示", "没有找到符合条件的记录").Show();
            return;
        }
        Mesnac.Util.Excel.ExcelDownload ed = new Mesnac.Util.Excel.ExcelDownload();
        ed.ExcelFileDown(dt, "检验数据汇总");


    }

}