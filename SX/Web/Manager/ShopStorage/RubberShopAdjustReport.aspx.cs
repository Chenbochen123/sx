using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using Ext.Net;
using NBear.Common;
using Mesnac.Business.Implements;
using Mesnac.Entity;
using Mesnac.Data.Components;
using Mesnac.Business.Interface;
using Mesnac.Data.Implements;
public partial class Manager_ShopStorage_RubberShopAdjustReport : Mesnac.Web.UI.Page
{
    protected PstShopAdjustManager shopAdjustManager = new PstShopAdjustManager();
    protected BasUserManager userManager = new BasUserManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest && !IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["BeginTime"]))
            {
                string beginTime = Request.QueryString["BeginTime"].ToString();
                string endTime = Request.QueryString["EndTime"].ToString();
                string barcode = Request.QueryString["Barcode"].ToString();

                txtBeginTime.Text = beginTime;
                txtEndTime.Text = endTime;
                txtBarcode.Text = barcode;
            }
            else
            {
                txtBeginTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtEndTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
                hiddenOperPerson.Text = this.UserID;
                txtOperPerson.Text = userManager.GetListByWhere(BasUser._.WorkBarcode == this.UserID)[0].UserName;
            }
            cbxChejian.Value = "all";
            cbxShift.Value = "all";
        }
    }
    protected void btnSearch_Click(object sender, DirectEventArgs e)
    {
        PstShopAdjustManager.QueryParams queryParams = new PstShopAdjustManager.QueryParams();
        queryParams.storageID = hiddenStorageID.Text;
        queryParams.toStorageID = hiddenToStorageID.Text;
        queryParams.workShopCode = cbxChejian.SelectedItem.Value;
        queryParams.beginDate = Convert.ToDateTime(txtBeginTime.Text);
        queryParams.endDate = Convert.ToDateTime(txtEndTime.Text);
        queryParams.barcode = txtBarcode.Text;
        queryParams.materCode = hiddenMaterCode.Text;
        queryParams.operPerson = hiddenOperPerson.Text;
        queryParams.shiftID = cbxShift.SelectedItem.Value;
        queryParams.deleteFlag = "0";
        queryParams.matertype = "1";

        DataTable dt = shopAdjustManager.GetReportBySql(queryParams).Tables[0];
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
        report.Load(Server.MapPath("RubberShopAdjustReport.frx"));
        //绑定数据源
        report.RegisterData(dt, "RubberShopAdjustReport");
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
            Response.AddHeader("Content-Disposition", "attachment;filename=胶料调拨统计.xlsx");
            strm.Position = 0;
            strm.WriteTo(Response.OutputStream);
            Response.End();
        }
    }
}