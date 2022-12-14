using System;
using System.Collections.Generic;
using Ext.Net;
using Mesnac.Business.Implements;
using Mesnac.Business.Interface;
using Mesnac.Entity;
using NBear.Common;
using Mesnac.Data.Components;
using System.Data;
using Newtonsoft.Json;
using System.Globalization;
using System.Text;

/// <summary>
/// 批报表
/// 孙本强 @ 2013-04-03 13:17:32
/// </summary>
/// <remarks></remarks>
public partial class Manager_Technology_Report_PlanLotReport : Mesnac.Web.UI.Page
{

    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查询 = new SysPageAction() { ActionID = 1, ActionName = "btnSearch" };
            更详细批记录 = new SysPageAction() { ActionID = 2, ActionName = "btnSwitch" };
        }
        public SysPageAction 查询 { get; private set; } //必须为 public
        public SysPageAction 更详细批记录 { get; private set; } //必须为 public
    }
    #endregion

    #region 属性注入
    /// <summary>
    /// 班次
    /// 孙本强 @ 2013-04-03 13:17:32
    /// </summary>
    private IPptClassManager pptClassManager = new PptClassManager();
    /// <summary>
    /// 班组
    /// 孙本强 @ 2013-04-03 13:17:32
    /// </summary>
    private IPptShiftManager pptShiftManager = new PptShiftManager();
    /// <summary>
    /// 车生产信息
    /// 孙本强 @ 2013-04-03 13:17:32
    /// </summary>
    private IPptLotManager pptLotManager = new PptLotManager();

    /// <summary>
    /// 生产计划信息
    /// 孙本强 @ 2013-04-03 13:17:32
    /// </summary>
    private IPptPlanManager pptPlanManager = new PptPlanManager();

    /// <summary>
    /// 物料信息
    /// 孙本强 @ 2013-04-03 13:17:32
    /// </summary>
    private IBasMaterialManager basMaterialManager = new BasMaterialManager();
    #endregion


    #region 页面初始化
    /// <summary>
    /// 默认选择字段
    /// 孙本强 @ 2013-04-03 13:17:32
    /// </summary>
    private const string constSelectAllText = "---请选择---";
    /// <summary>
    /// 初始化ComboBox
    /// 孙本强 @ 2013-04-03 13:17:32
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
    /// <summary>
    /// 页面初始化
    /// 孙本强 @ 2013-04-03 13:17:33
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    /// <remarks></remarks>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (X.IsAjaxRequest)
        {
            return;
        }
        txtBeginTime.Text = DateTime.Now.ToString("yyyy-MM-dd 00:00:00");
        txtEndTime.Text = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd 00:00:00");
        WhereClip where = new WhereClip();
        OrderByClip order = new OrderByClip();
        where = new WhereClip();
        order = new OrderByClip();
        where.And(PptClass._.UseFlag == "1");
        order = PptClass._.ObjID.Asc;
        Ext.Net.ListItem allitem = new Ext.Net.ListItem(constSelectAllText, constSelectAllText);
        txtPptClass.Items.Clear();
        txtPptClass.Items.Add(allitem);
        foreach (PptClass m in pptClassManager.GetListByWhereAndOrder(where, order))
        {
            txtPptClass.Items.Add(new ListItem(m.ClassName, m.ObjID));
        }
        txtPptClass.Text = (txtPptClass.Items[0].Value);

        where = new WhereClip();
        order = new OrderByClip();
        where.And(PptShift._.UseFlag == "1");
        order = PptShift._.ObjID.Asc;
        txtPptShift.Items.Clear();
        txtPptShift.Items.Add(allitem);
        foreach (PptShift m in pptShiftManager.GetListByWhereAndOrder(where, order))
        {
            txtPptShift.Items.Add(new ListItem(m.ShiftName, m.ObjID));
        }
        txtPptShift.Text = (txtPptClass.Items[0].Value);
    }
    /// <summary>
    /// 生成红色Html标示
    /// 孙本强 @ 2013-04-03 13:17:33
    /// </summary>
    /// <param name="ss">The ss.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    private string RedHtml(string ss)
    {
        return "<font color='red'>" + ss + "</font>";
    }
    /// <summary>
    /// 默认Html标示
    /// 孙本强 @ 2013-04-03 13:17:33
    /// </summary>
    /// <param name="ss">The ss.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    private string DefaultHtml(string ss)
    {
        return ss;
    }
    #endregion


    #region 查询显示右侧
    /// <summary>
    /// 获取分页数据集
    /// 孙本强 @ 2013-04-03 13:34:17
    /// </summary>
    /// <param name="pageParams">The page params.</param>
    /// <returns>分页数据集</returns>
    /// <remarks></remarks>
    private PageResult<PptPlan> GetPageResultData(PptPlanManager.QueryParams queryParams)
    {
        int iSearchTimes = 0;
        int.TryParse(hiddenSearchTimes.Text, out iSearchTimes);
        hiddenSearchTimes.Text = (iSearchTimes + 1).ToString();
        if (iSearchTimes == 0)
        {
            return queryParams.pageParams;
        }
        if (string.IsNullOrWhiteSpace(hiddenEquipCode.Text))
        {
            X.Msg.Alert("提示", "请选择机台！").Show();
            return queryParams.pageParams;
        }
        try
        {
            Convert.ToDateTime(txtBeginTime.Text);
        }
        catch
        {
            X.Msg.Alert("提示", "请输入正确的开始时间！").Show();
            return queryParams.pageParams;
        }
        try
        {
            Convert.ToDateTime(txtEndTime.Text);
        }
        catch
        {
            X.Msg.Alert("提示", "请输入正确的结束时间！").Show();
            return queryParams.pageParams;
        }
        queryParams.equipCode = hiddenEquipCode.Text;
        queryParams.planStartDate = txtBeginTime.Text;
        queryParams.planEndDate = txtEndTime.Text;
        queryParams.classID = txtPptClass.Text.Replace(constSelectAllText, string.Empty);
        queryParams.shiftID = txtPptShift.Text.Replace(constSelectAllText, string.Empty);
        queryParams.recipeID = hiddenPmtRecipeID.Text;
        return pptPlanManager.GetPlanLotReportPageDataBySql(queryParams);
    }
    /// <summary>
    /// GridPanel数据绑定
    /// 孙本强 @ 2013-04-03 13:17:33
    /// </summary>
    /// <param name="action">The action.</param>
    /// <param name="extraParams">The extra params.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    [DirectMethod]
    public object GridPanelBindData1(string action, Dictionary<string, object> extraParams)
    {
        if (this._.查询.SeqIdx==0)
        {
            return null;
        }
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PptPlanManager.QueryParams queryParams = new PptPlanManager.QueryParams();
        queryParams.pageParams.PageIndex = prms.Page;
        queryParams.pageParams.PageSize = prms.Limit;
        PageResult<PptPlan> lst = GetPageResultData(queryParams);
        if (lst.DataSet.Tables.Count > 0 && lst.DataSet.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < lst.DataSet.Tables[0].Rows.Count; i++)
            {
                DataRow row = lst.DataSet.Tables[0].Rows[i];
                try
                {
                    row["RecipeMaterialName"] = basMaterialManager.GetListByWhere(BasMaterial._.MaterialCode == row["RecipeMaterialCode"])[0].MaterialName;
                }
                catch { }
            }
        }
        return new { data = lst.DataSet.Tables[0], total = lst.RecordCount };

    }
    /// <summary>
    /// GridPanel数据绑定
    /// 孙本强 @ 2013-04-03 13:17:33
    /// </summary>
    /// <param name="action">The action.</param>
    /// <param name="extraParams">The extra params.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    [DirectMethod]
    public object GridPanelBindData2(string action, Dictionary<string, object> extraParams)
    {
        if (this._.查询.SeqIdx == 0)
        {
            return null;
        }
        if (string.IsNullOrWhiteSpace(hiddenMaterialCode.Text))
        {
            return new { data = new EntityArrayList<PptPlan>(), total = 0 };
        }
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PptPlanManager.QueryParams queryParams = new PptPlanManager.QueryParams();
        queryParams.pageParams.PageIndex = prms.Page;
        queryParams.pageParams.PageSize = prms.Limit;
        queryParams.materialCode = hiddenMaterialCode.Text;
        PageResult<PptPlan> lst = GetPageResultData(queryParams);
        return new { data = lst.DataSet.Tables[0], total = lst.RecordCount };

    }

    protected void RowSelectionModelMaster_SelectionChange(object sender, DirectEventArgs e)
    {
        string PlanID = e.ExtraParams["PlanID"];
        LoadGridPanelDetail(PlanID);
    }

    private void LoadGridPanelDetail(string PlanID)
    {
        if (PlanID != "")
        {
            hiddenPlanID.Text = PlanID;
            DataSet ds = GetDataByParas(PlanID);
            StoreMix.DataSource = ds.Tables[0];
            StoreMix.DataBind();
        }
    }

    public DataSet GetDataByParas(string PlanID)
    {
        IQmtCheckStandDetailManager bQmtCheckStandDetailManager = new QmtCheckStandDetailManager();
        StringBuilder sb = new StringBuilder();
        #region
        sb.AppendLine(@"  select a.Barcode,a.MixID,c.Term_name,a.SetTime,a.TermCode,a.SeTemp,a.SetPower,a.SetEner
                                ,b.Act_name,a.SetRota,a.SetPres,a.ActCode,a.step_time
                          from PptMixingData  a
                          left join Pmt_act b on a.ActCode = b.Act_code
                          left join Pmt_term c on a.TermCode = c.Term_addr ");
        sb.AppendLine(" WHERE 1=1");
        sb.AppendLine(" AND a.Barcode like'" + PlanID + "%'");
        sb.AppendLine(" order by Barcode ,MixID " );

        #endregion

        NBear.Data.CustomSqlSection css = bQmtCheckStandDetailManager.GetBySql(sb.ToString());
        return css.ToDataSet();
    }

    protected void ButtonNorthExport_Click(object sender, DirectEventArgs e)
    {
        string fields = e.ExtraParams["fields"];
        string records = e.ExtraParams["records"];
        Newtonsoft.Json.JavaScriptArray jsArrayFields = Newtonsoft.Json.JavaScriptConvert.DeserializeObject(fields) as Newtonsoft.Json.JavaScriptArray;
        Newtonsoft.Json.JavaScriptArray jsArrayRecords = Newtonsoft.Json.JavaScriptConvert.DeserializeObject(records) as Newtonsoft.Json.JavaScriptArray;

        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        foreach (Newtonsoft.Json.JavaScriptObject jsObjectField in jsArrayFields)
        {
            if (jsObjectField["name"].ToString().ToLower() != "id")
            {
                dt.Columns.Add(new DataColumn(jsObjectField["name"].ToString(), typeof(string)));
            }
        }

        foreach (Newtonsoft.Json.JavaScriptObject jsObjectRecord in jsArrayRecords)
        {
            DataRow dr = dt.NewRow();
            foreach (DataColumn dc in dt.Columns)
            {
                dr[dc.ColumnName] = jsObjectRecord[dc.ColumnName];
            }
            dt.Rows.Add(dr);
        }

        if (dt.Rows.Count == 0)
        {
            X.Msg.Alert("提示", "没有找到符合条件的记录").Show();
            return;
        }
        dt.TableName = "混炼信息";
        ds.Tables.Add(dt);

        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(ds, "混炼信息");
    }

    protected void btnExportSubmit_Click(object sender, EventArgs e)
    {
        DataTable dt = GetDataByParas(hiddenPlanID.Text).Tables[0];
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            bool isshow = false;
            DataColumn dc = dt.Columns[i];
            foreach (ColumnBase cb in this.gridPanelMix.ColumnModel.Columns)
            {
                if ((cb.DataIndex != null) && (cb.DataIndex.ToUpper() == dc.ColumnName.ToUpper()))
                {
                    dc.ColumnName = cb.Text;
                    isshow = true;
                    break;
                }
            }
            if (!isshow)
            {
                dt.Columns.Remove(dc.ColumnName);
                i--;
            }
        }
        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(dt, "混炼信息");
    }

    #endregion
}