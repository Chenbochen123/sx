using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Web.UI.HtmlControls;

using NBear;
using NBear.Common;

using Ext.Net;

using Mesnac.Business.Interface;
using Mesnac.Business.Implements;
using Mesnac.Data.Interface;
using Mesnac.Data.Implements;
using Mesnac.Entity;
using Mesnac.Web.UI;

public partial class Manager_RubberQuality_Manage_CheckRubberQSReport : BasePage
{
    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            浏览 = new SysPageAction() { ActionID = 0, ActionName = "" };
            查询 = new SysPageAction() { ActionID = 1, ActionName = "ButtonNorthQuery" };
            导出 = new SysPageAction() { ActionID = 2, ActionName = "ButtonNorthExcel" };
        }
        public SysPageAction 浏览 { get; private set; } //必须为 public
        public SysPageAction 查询 { get; private set; } //必须为 public
        public SysPageAction 导出 { get; private set; } //必须为 public
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
            #region 加载CSS样式
            HtmlGenericControl cssLink = new HtmlGenericControl("link");
            cssLink.Attributes.Add("type", "text/css");
            cssLink.Attributes.Add("href", this.ResolveUrl("~/resources/css/main.css"));
            this.Page.Header.Controls.Add(cssLink);
            #endregion 加载CSS样式

            #region 加载JS文件
            HtmlGenericControl scriptLink = new HtmlGenericControl("script");
            scriptLink.Attributes.Add("type", "text/javascript");
            scriptLink.Attributes.Add("src", "CheckRubberQSReport.js?" + DateTime.Now.Ticks.ToString());
            this.Page.Header.Controls.Add(scriptLink);
            #endregion 加载JS文件

            InitControls();

            DateFieldNorthCheckPlanSDate.SetValue(DateTime.Today);
            DateFieldNorthCheckPlanEDate.SetValue(DateTime.Today);

            ComboBoxNorthCheckType.SetValueAndFireSelect("2");
        }

    }

    /// <summary>
    /// 初始化控件
    /// </summary>
    private void InitControls()
    {
        // 质检班次
        IPptShiftManager bPptShiftManager = new PptShiftManager();
        EntityArrayList<PptShift> mPptShiftList = bPptShiftManager.GetAllListOrder(PptShift._.ObjID.Asc);
        foreach (PptShift mPptShift in mPptShiftList)
        {
            //ComboBoxNorthCheckShiftId.AddItem(mPptShift.ShiftName, mPptShift.ObjID.ToString());
            ComboBoxNorthCheckShiftId.Items.Add(new Ext.Net.ListItem(mPptShift.ShiftName, mPptShift.ObjID.ToString()));
        }

        // 质检班组
        IPptClassManager bPptClassManager = new PptClassManager();
        EntityArrayList<PptClass> mPptClassList = bPptClassManager.GetAllListOrder(PptClass._.ObjID.Asc);
        foreach (PptClass mPptClass in mPptClassList)
        {
            //ComboBoxNorthCheckShiftClass.AddItem(mPptClass.ClassName, mPptClass.ObjID.ToString());
            ComboBoxNorthCheckShiftClass.Items.Add(new Ext.Net.ListItem(mPptClass.ClassName, mPptClass.ObjID.ToString()));
        }

        // 生产车间
        IBasWorkShopManager bBasWorkShopManager = new BasWorkShopManager();
        EntityArrayList<BasWorkShop> mBasWorkShopList = bBasWorkShopManager.GetListByWhereAndOrder(
            BasWorkShop._.DeleteFlag == "0"
            , BasWorkShop._.ObjID.Asc);
        foreach (BasWorkShop mBasWorkShop in mBasWorkShopList)
        {
            ComboBoxNorthWorkShop.AddItem(mBasWorkShop.WorkShopName, mBasWorkShop.ObjID.ToString());
        }

    }

    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonNorthQuery_Click(object sender, EventArgs e)
    {
        #region 验证查询条件

        #endregion 验证查询条件

        HiddenNorthCheckType.SetValue(ComboBoxNorthCheckType.Value.ToString());

        DataSet ds = ReadDataSet();

        StoreCenterMain.DataSource = ds;
        StoreCenterMain.DataBind();

        WebReport1.Report.Clear();
        WebReport1.Report.Refresh();
        WebReport1.Update();
        WebReport1.Refresh();

        X.Msg.Alert("提示", "查询完毕").Show();
    }


    /// <summary>
    /// 获取查询数据
    /// </summary>
    /// <returns></returns>
    private DataSet ReadDataSet()
    {
        DataSet ds = null;
        string checkType = HiddenNorthCheckType.Value.ToString();
        if (checkType == "2")
        {
            IQmtCheckMasterManager bQmtCheckMasterManager = new QmtCheckMasterManager();
            IQmtCheckRubberQSQueryParams paras = new QmtCheckRubberQSQueryParams();
            paras.CheckPlanSDate = DateFieldNorthCheckPlanSDate.SelectedValue == null ? "" : DateFieldNorthCheckPlanSDate.SelectedDate.ToString("yyyy-MM-dd");
            paras.CheckPlanEDate = DateFieldNorthCheckPlanEDate.SelectedValue == null ? "" : DateFieldNorthCheckPlanEDate.SelectedDate.ToString("yyyy-MM-dd");
            paras.CheckShiftClass = ComboBoxNorthCheckShiftClass.Value.ToString();
            paras.CheckShiftId = ComboBoxNorthCheckShiftId.Value.ToString();
            paras.WorkShopCode = ComboBoxNorthWorkShop.Value.ToString();

            ds = bQmtCheckMasterManager.GetCheckRubberQSQueryByParas(paras);
        }
        else if (checkType == "1")
        {
            IQmtCheckAssessMasterManager bQmtCheckAssessMasterManage = new QmtCheckAssessMasterManager();
            IQmtCheckRubberAssessQSQueryParams paras = new QmtCheckRubberAssessQSQueryParams();
            paras.CheckPlanSDate = DateFieldNorthCheckPlanSDate.SelectedValue == null ? "" : DateFieldNorthCheckPlanSDate.SelectedDate.ToString("yyyy-MM-dd");
            paras.CheckPlanEDate = DateFieldNorthCheckPlanEDate.SelectedValue == null ? "" : DateFieldNorthCheckPlanEDate.SelectedDate.ToString("yyyy-MM-dd");
            paras.CheckShiftClass = ComboBoxNorthCheckShiftClass.Value.ToString();
            paras.CheckShiftId = ComboBoxNorthCheckShiftId.Value.ToString();
            paras.WorkShopCode = ComboBoxNorthWorkShop.Value.ToString();

            ds = bQmtCheckAssessMasterManage.GetCheckRubberAssessQSQueryByParas(paras);
        }

        return ds;
    }

    /// <summary>
    /// 导出Excel
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonNorthExcel_Click(object sender, DirectEventArgs e)
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
        Mesnac.Util.Excel.ExcelDownload ed = new Mesnac.Util.Excel.ExcelDownload();
        ed.ExcelFileDown(dt, "胶料快检报表");

    }

    /// <summary>
    /// 选择导入文件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void RowSelectionModel_SelectionChange(object sender, DirectEventArgs e)
    {
        string checkPlanDate = e.ExtraParams["CheckPlanDate"];
        string shiftCheckId = e.ExtraParams["ShiftCheckId"];
        string shiftCheckGroupID = e.ExtraParams["ShiftCheckGroupID"];
        string shiftCheckName = e.ExtraParams["ShiftCheckName"];
        string shiftCheckGroupName = e.ExtraParams["ShiftCheckGroupName"];
        string workShopCode = e.ExtraParams["WorkShopCode"];
        string workShopName = e.ExtraParams["WorkShopName"];

        DataSet ds = null;
        string checkType = HiddenNorthCheckType.Value.ToString();
        if (checkType == "2")
        {
            IQmtCheckMasterManager bQmtCheckMasterManager = new QmtCheckMasterManager();
            IQmtCheckRubberQSReportParams paras = new QmtCheckRubberQSReportParams();
            paras.CheckPlanDate = checkPlanDate;
            paras.CheckShiftClass = shiftCheckGroupID;
            paras.CheckShiftId = shiftCheckId;
            paras.WorkShopCode = workShopCode;
            ds = bQmtCheckMasterManager.GetCheckRubberQSReportByParas(paras);
        }
        else if (checkType == "1")
        {
            IQmtCheckAssessMasterManager bQmtCheckAssessMasterManager = new QmtCheckAssessMasterManager();
            IQmtCheckRubberAssessQSReportParams paras = new QmtCheckRubberAssessQSReportParams();
            paras.CheckPlanDate = checkPlanDate;
            paras.CheckShiftClass = shiftCheckGroupID;
            paras.CheckShiftId = shiftCheckId;
            paras.WorkShopCode = workShopCode;
            ds = bQmtCheckAssessMasterManager.GetCheckRubberAssessQSReportByParas(paras);
        }

        StoreCenterDetail.DataSource = ds;
        StoreCenterDetail.DataBind();

        //初始化报表控件
        FastReport.Report report = this.WebReport1.Report;
        report.Load(Server.MapPath("CheckRubberQSReport.frx"));
        //绑定数据源

        //if (ds.Tables[0].Rows.Count > 0)
        //    btnPrint.Visible = true;
        //else
        //    btnPrint.Visible = false;
        report.RegisterData(ds.Tables[0], "QmtCheckRubberQSReport");
        report.RegisterData(ds.Tables[1], "QmtCheckRubberQSReportType");
        report.SetParameterValue("CheckClassName", shiftCheckName + "班");
        report.SetParameterValue("CheckShiftName", shiftCheckId);
        report.SetParameterValue("CheckPlanDate", checkPlanDate);
        string reportTitle = "";
        if (checkType == "2")
        {
            reportTitle = "胶料快检报表";
        }
        else if (checkType == "1")
        {
            reportTitle = "胶料快检考核报表";
        }
        report.SetParameterValue("ReportTitle", reportTitle);
        report.Refresh();
        WebReport1.Update();
        WebReport1.Refresh();

    }

    /// <summary>
    /// 导出Report
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonNorthExport_Click(object sender, EventArgs e)
    {
        if (WebReport1.Report.Pages.Count == 0)
        {
            X.Msg.Alert("提示", "请先查询出结果后再导出").Show();
            return;
        }

        string fileName = "";
        string checkType = HiddenNorthCheckType.Value.ToString();
        if (checkType == "2")
        {
            fileName = "胶料快检报表";
        }
        else if (checkType == "1")
        {
            fileName = "胶料快检考核报表";
        }

        // WebReport1.ExportExcel2007();

        // FastReport.Utils.Config.WebMode = true;

        WebReport1.Report.Prepare();

        // Export report to Excel stream

        FastReport.Export.OoXML.Excel2007Export excelExport = new FastReport.Export.OoXML.Excel2007Export();

        using (System.IO.MemoryStream strm = new System.IO.MemoryStream())
        {
            WebReport1.Report.Export(excelExport, strm);

            // Stream the Excel back to the client as an attachment

            Response.ClearContent();

            Response.ClearHeaders();

            Response.Buffer = true;

            Response.ContentType = "Application/Excel";

            Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName + ".xlsx");

            strm.Position = 0;

            strm.WriteTo(Response.OutputStream);

            Response.End();
        }

    }
    protected void RefreshTime(object sender, DirectEventArgs e)
    {
        this.HiddenServerTime.SetValue(DateTime.Now.ToString("HH:mm:ss"));
    }
}