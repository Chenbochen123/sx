using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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

using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.Util;
using NPOI.HSSF.Util;
using NPOI.SS.Util;

public partial class Manager_Rubber_Report_WorkerProdReport : System.Web.UI.Page
{
    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查询 = new SysPageAction() { ActionID = 1, ActionName = "ButtonNorthQuery" };
            导出 = new SysPageAction() { ActionID = 2, ActionName = "ButtonNorthExport" };
        }
        public SysPageAction 查询 { get; private set; } //必须为 public
        public SysPageAction 导出 { get; private set; } //必须为 public
    }
    #endregion

    protected SYS_USERManager usermanager = new SYS_USERManager();
    private const string constSelectAllText = "---请选择---";

    /// <summary>
    /// 页面加载
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            InitControls();

            DateFieldNorthBeginDate.SetValue(DateTime.Today.ToString("yyyy-MM-dd"));
            DateFieldNorthEndDate.SetValue(DateTime.Today.ToString("yyyy-MM-dd"));

        }
    }

    private void InitControls()
    {
        IBasWorkShopManager bBasWorkShopManager = new BasWorkShopManager();
        //机台
        string sql = " select equip_Code,equip_name from pmt_equip where equip_Class<'03' ";
        DataSet ds = bBasWorkShopManager.GetBySql(sql).ToDataSet();

        ComboBoxEquip.Items.Clear();
        Ext.Net.ListItem allitem = new Ext.Net.ListItem(constSelectAllText, constSelectAllText);
        ComboBoxEquip.Items.Add(allitem);

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem();
            item.Value = ds.Tables[0].Rows[i]["equip_Code"].ToString();
            item.Text = ds.Tables[0].Rows[i]["equip_name"].ToString();
            ComboBoxEquip.Items.Add(item);
        }
        ComboBoxEquip.Select(0);

        //部门
        sql = " select Dep_Code,Fac_Name from jczl_subfac ";
        ds = bBasWorkShopManager.GetBySql(sql).ToDataSet();

        ComboBoxSubFac.Items.Clear();
        Ext.Net.ListItem allitem1 = new Ext.Net.ListItem(constSelectAllText, constSelectAllText);
        ComboBoxSubFac.Items.Add(allitem1);

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem();
            item.Value = ds.Tables[0].Rows[i]["Dep_Code"].ToString();
            item.Text = ds.Tables[0].Rows[i]["Fac_Name"].ToString();
            ComboBoxSubFac.Items.Add(item);
        }
        ComboBoxSubFac.Select(0);

        //岗位
        sql = " select worktype,work_name from jczl_work  ";
        ds = bBasWorkShopManager.GetBySql(sql).ToDataSet();

        ComboBoxWorkshop.Items.Clear();
        Ext.Net.ListItem allitem2 = new Ext.Net.ListItem(constSelectAllText, constSelectAllText);
        ComboBoxWorkshop.Items.Add(allitem2);

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem();
            item.Value = ds.Tables[0].Rows[i]["worktype"].ToString();
            item.Text = ds.Tables[0].Rows[i]["work_name"].ToString();
            ComboBoxWorkshop.Items.Add(item);
        }
        ComboBoxWorkshop.Select(0);

        //人员
        sql = " select USER_ID,USER_NAME from SYS_USER   ";
        ds = bBasWorkShopManager.GetBySql(sql).ToDataSet();

        cbxUser.Items.Clear();
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem();
            item.Value = ds.Tables[0].Rows[i]["USER_ID"].ToString();
            item.Text = ds.Tables[0].Rows[i]["USER_NAME"].ToString();
            cbxUser.Items.Add(item);
        }
        cbxUser.Select(-1);
    }

    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonNorthQuery_Click(object sender, DirectEventArgs e)
    {
        string beginPlanDate = DateFieldNorthBeginDate.RawText;

        HiddenBeginDate.SetValue(beginPlanDate);

        IQmtRubberLBEquipDataReportParams paras = new QmtRubberLBEquipDataReportParams();
        paras.BeginPlanDate = beginPlanDate;

        IQmtCheckMasterManager bQmtCheckMasterManager = new QmtCheckMasterManager();
        DataSet ds = GetCheckRubberLBEquipDataReportByParas(paras);
        ModelCenter.Fields.Clear();

        foreach (DataColumn dc in ds.Tables[0].Columns)
        {
            ModelCenter.Fields.Add(new ModelField { Name = dc.ColumnName });
        }

        GridPanelCenter.ColumnModel.Columns.Clear();
        foreach (DataColumn dc in ds.Tables[0].Columns)
        {
            Ext.Net.Column cs = new Ext.Net.Column { DataIndex = dc.ColumnName, Text = dc.ColumnName };
            GridPanelCenter.ColumnModel.Columns.Add(cs);
        }

        StoreCenter.DataSource = ds;
        StoreCenter.DataBind();

        GridPanelCenter.Render();

    }
    public DataSet GetCheckRubberLBEquipDataReportByParas(IQmtRubberLBEquipDataReportParams paras)
    {
        IBasWorkShopManager bBasWorkShopManager = new BasWorkShopManager();
        Dictionary<string, object> dict = new Dictionary<string, object>();

        dict.Add("@PlanDate", DateFieldNorthBeginDate.RawText);
        dict.Add("@plandate2", DateFieldNorthEndDate.RawText);
        dict.Add("@depcode", ComboBoxSubFac.Value.ToString().Replace(constSelectAllText, ""));
        dict.Add("@worktype", ComboBoxWorkshop.Value.ToString().Replace(constSelectAllText, ""));
        dict.Add("@Equip_Code", ComboBoxEquip.Value.ToString().Replace(constSelectAllText, ""));
        dict.Add("@user_id", cbxUser.Value);
        return bBasWorkShopManager.GetDataSetByStoreProcedure("Proce_ChanLiangToal", dict);
    }
    /// <summary>
    /// 导出
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonNorthExport_Click(object sender, DirectEventArgs e)
    {
        string fields = e.ExtraParams["fields"];
        string records = e.ExtraParams["records"];
        Newtonsoft.Json.JavaScriptArray jsArrayFields = Newtonsoft.Json.JavaScriptConvert.DeserializeObject(fields) as Newtonsoft.Json.JavaScriptArray;
        Newtonsoft.Json.JavaScriptArray jsArrayRecords = Newtonsoft.Json.JavaScriptConvert.DeserializeObject(records) as Newtonsoft.Json.JavaScriptArray;

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

        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(dt, "员工产量统计");

    }
}