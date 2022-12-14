﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;

using Mesnac.Web.UI;
using Mesnac.Business.Interface;
using Mesnac.Business.Implements;
using Mesnac.Data.Interface;
using Mesnac.Data.Implements;
using Mesnac.Entity;
using Ext.Net;

public partial class Manager_Rubber_Report_RubberBackReport : System.Web.UI.Page
{
    protected PpmRubberStorageManager rubberStorageManager = new PpmRubberStorageManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            txtBeginTime.SetValue(DateTime.Today.ToString("yyyy-MM-dd"));
            txtEndTime.SetValue(DateTime.Today.ToString("yyyy-MM-dd"));
            this.cboMaterType.Value = "all";
        }
    }

    protected void btnSearch_Click(object sender, DirectEventArgs e)
    {
        PpmRubberStorageManager.QueryParams queryParams = new PpmRubberStorageManager.QueryParams();
        queryParams.barcode = txtBarcode.Text;
        queryParams.storageID = hiddenStorageID.Text;
        queryParams.storagePlaceID = hiddenToStorageCheckID.Text;

        queryParams.beginDate = Convert.ToDateTime(txtBeginTime.Text);
        queryParams.endDate = Convert.ToDateTime(txtEndTime.Text);
        queryParams.materCode = hiddenMaterCode.Text;
        queryParams.Oper = this.cboMaterType.Value.ToString();
        DataTable dt = rubberStorageManager.GetTableStoreBackReport(queryParams);
        if (dt == null)
        {
            WebReport1.Report.Clear();
            WebReport1.Report.Refresh();
            WebReport1.Update();
            WebReport1.Refresh();
            X.Msg.Alert("提示", "没有找到符合条件的记录!").Show();
            return;
        }
        //初始化报表控件
        FastReport.Report report = this.WebReport1.Report;
        report.Load(Server.MapPath("RubberBackReport.frx"));
        //绑定数据源
        report.RegisterData(dt, "RubberBackReport");
        report.Refresh();
        WebReport1.Update();
        WebReport1.Refresh();

        X.Msg.Alert("提示", "查询完毕!").Show();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (WebReport1.Report.Pages.Count == 0)
        {
            X.Msg.Alert("提示", "请先查询出结果后再导出!").Show();
            return;
        }

        WebReport1.Report.Prepare();

        FastReport.Export.OoXML.Excel2007Export excelExport = new FastReport.Export.OoXML.Excel2007Export();

        using (System.IO.MemoryStream strm = new System.IO.MemoryStream())
        {
            WebReport1.Report.Export(excelExport, strm);
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.ContentType = "Application/Excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=胶料退库统计.xlsx");
            strm.Position = 0;
            strm.WriteTo(Response.OutputStream);
            Response.End();
        }
    }
}