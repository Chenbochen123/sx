using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Web.UI.HtmlControls;

using Mesnac.Web.UI;
using Mesnac.Business.Interface;
using Mesnac.Business.Implements;
using Mesnac.Data.Interface;
using Mesnac.Data.Implements;
using Mesnac.Entity;

using Ext.Net;

public partial class Manager_RubberQuality_Manage_CheckRubberQualityMonthReport : BasePage
{
    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            浏览 = new SysPageAction() { ActionID = 0, ActionName = "" };
            查询 = new SysPageAction() { ActionID = 1, ActionName = "ButtonNorthQuery" };
            导出 = new SysPageAction() { ActionID = 2, ActionName = "ButtonNorthExport" };
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
        #region 加载CSS样式
        HtmlGenericControl cssLink = new HtmlGenericControl("link");
        cssLink.Attributes.Add("type", "text/css");
        cssLink.Attributes.Add("href", this.ResolveUrl("~/resources/css/main.css"));
        this.Page.Header.Controls.Add(cssLink);
        #endregion 加载CSS样式

        if (!X.IsAjaxRequest)
        {
            //DateFieldNorthPlanMonth.SetValue(DateTime.Today.ToString("yyyy-MM"));
            DateFieldNorthBeginPlanDate.SetValue(DateTime.Today.ToString("yyyy-MM-01"));
            DateFieldNorthEndPlanDate.SetValue(DateTime.Today.ToString("yyyy-MM-dd"));
            ComboBoxNorthCheckTypeCode.SetValue("2");
        }
    }

    protected void ButtonNorthQuery_Click(object sender, DirectEventArgs e)
    {
        //if (DateFieldNorthPlanMonth.SelectedValue == null)
        //{
        //    X.Msg.Alert("提示", "生产月份不能为空").Show();
        //    return;
        //}

        if (DateFieldNorthBeginPlanDate.SelectedValue == null)
        {
            X.Msg.Alert("提示", "开始生产日期不能为空").Show();
            return;
        }

        if (DateFieldNorthBeginPlanDate.SelectedValue == null)
        {
            X.Msg.Alert("提示", "开始生产日期不能为空").Show();
            return;
        }
        string chejian = "";
        String shift = "";
        if (this.cbxChejian.Value != null)
        {
            if (this.cbxChejian.Value.ToString() == "全部" || this.cbxChejian.Value.ToString() == "0")
            {
                chejian = "";
            }
            else
            {
                chejian = cbxChejian.Value.ToString();
            }

        }

        if (this.cbxShift.Value != null)
        {
            if (this.cbxShift.Value.ToString() == "全部" || this.cbxShift.Value.ToString() == "0")
            {
                shift = "";
            }
            else
            {
                shift = cbxShift.Value.ToString();
            }

        }
        IQmtCheckRubberQualityMonthReportParams paras = new QmtCheckRubberQualityMonthReportParams();

        //paras.PlanMonth = DateFieldNorthPlanMonth.RawText;
        paras.BeginPlanDate = DateFieldNorthBeginPlanDate.RawText;
        paras.EndPlanDate = DateFieldNorthEndPlanDate.RawText;
        paras.CheckTypeCode = ComboBoxNorthCheckTypeCode.Value.ToString();
        paras.WorkBar = chejian;
        paras.ShiftID = shift;
        IQmtCheckMasterManager bQmtCheckMasterManager = new QmtCheckMasterManager();

        DataSet ds = bQmtCheckMasterManager.GetCheckRubberQualityMonthReportByParas(paras);

        if (ds.Tables[0].Rows.Count == 0)
        {
            WebReport1.Report.Clear();
            WebReport1.Report.Refresh();
            WebReport1.Update();
            WebReport1.Refresh();

            X.Msg.Alert("提示", "没有找到符合条件的记录").Show();
            return;
        }

        //初始化报表控件
        FastReport.Report report = this.WebReport1.Report;
        report.Load(Server.MapPath("CheckRubberQualityMonthReport.frx"));
        //绑定数据源

        //if (ds.Tables[0].Rows.Count > 0)
        //    btnPrint.Visible = true;
        //else
        //    btnPrint.Visible = false;
        report.RegisterData(ds.Tables[0], "Table");
        //report.SetParameterValue("PlanMonth", paras.PlanMonth.Replace("-", "年") + "月");
        report.Refresh();
        WebReport1.Update();
        WebReport1.Refresh();

        X.Msg.Alert("提示", "查询完毕").Show();

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
        //String planMonth = DateFieldNorthPlanMonth.RawText;
        //string beginPlanDate = DateFieldNorthBeginPlanDate.RawText;
        //string endPlanDate = DateFieldNorthEndPlanDate.RawText;
        //WebReport1.Report.FileName = "RubberQualityStat(" + beginPlanDate.Replace("-", "") + "-" + endPlanDate.Replace("-", "") + ")";
        //WebReport1.ExportExcel2007();

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
            string fileName = "胶料质量统计";
            if (ComboBoxNorthCheckTypeCode.Value.ToString() == "2")
            {
                fileName += "(检验标准)";
            }
            else if (ComboBoxNorthCheckTypeCode.Value.ToString() == "1")
            {
                fileName += "(考核标准)";
            }
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