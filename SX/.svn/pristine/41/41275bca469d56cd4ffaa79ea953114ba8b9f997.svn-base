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

public partial class Manager_Rubber_Report_PpmScanCalcRate : System.Web.UI.Page
{
    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查询 = new SysPageAction() { ActionID = 1, ActionName = "btnSearch" };
            导出 = new SysPageAction() { ActionID = 2, ActionName = "btnExport" };
        }
        public SysPageAction 查询 { get; private set; } //必须为 public
        public SysPageAction 导出 { get; private set; } //必须为 public
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest && !IsPostBack)
        {
            txtBeginTime.Text = DateTime.Now.ToString("yyyy-MM-") + "01";
            txtEndTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
    }

    protected void btnSearch_Click(object sender, DirectEventArgs e)
    {
        PpmEquipScanRateManager scanRateManager = new PpmEquipScanRateManager();

        DataSet ds;

        if (cbxTotalType.SelectedItem.Value == "1") //按照车间对比
        {
            ds = scanRateManager.GetPpmScanCalcWorkShop(Convert.ToDateTime(txtBeginTime.Text).ToString("yyyy-MM-dd"), Convert.ToDateTime(txtEndTime.Text).ToString("yyyy-MM-dd"), "", hiddenUserID.Text);

            if (ds.Tables[0].Rows.Count == 0)
            {
                WebReport1.Report.Clear();
                WebReport1.Report.Refresh();
                WebReport1.Update();
                WebReport1.Refresh();

                X.Msg.Alert("提示", "没有找到符合条件的记录!").Show();
                return;
            }

            //初始化报表控件
            FastReport.Report report1 = this.WebReport1.Report;
            report1.Load(Server.MapPath("PpmScanCalcWorkShop.frx"));
            //绑定数据源
            report1.RegisterData(ds.Tables[0], "PpmScanCalcWorkShop");
            report1.Refresh();
            WebReport1.Update();
            WebReport1.Refresh();
        }
        if (cbxTotalType.SelectedItem.Value == "2") //按照机台对比
        {
            ds = scanRateManager.GetPpmScanCalcEquipCode(Convert.ToDateTime(txtBeginTime.Text).ToString("yyyy-MM-dd"), Convert.ToDateTime(txtEndTime.Text).ToString("yyyy-MM-dd"), cbxWorkShopCode.SelectedItem.Value);

            if (ds.Tables[0].Rows.Count == 0)
            {
                WebReport1.Report.Clear();
                WebReport1.Report.Refresh();
                WebReport1.Update();
                WebReport1.Refresh();

                X.Msg.Alert("提示", "没有找到符合条件的记录!").Show();
                return;
            }

            //初始化报表控件
            FastReport.Report report2 = this.WebReport1.Report;
            report2.Load(Server.MapPath("PpmScanCalcEquipCode.frx"));
            //绑定数据源
            report2.RegisterData(ds.Tables[0], "PpmScanCalcEquipCode");
            report2.Refresh();
            WebReport1.Update();
            WebReport1.Refresh();
        }
        if (cbxTotalType.SelectedItem.Value == "3") //按照主机手对比
        {
            ds = scanRateManager.GetPpmScanCalcHrCode(Convert.ToDateTime(txtBeginTime.Text).ToString("yyyy-MM-dd"), Convert.ToDateTime(txtEndTime.Text).ToString("yyyy-MM-dd"), cbxWorkShopCode.SelectedItem.Value);

            if (ds.Tables[0].Rows.Count == 0)
            {
                WebReport1.Report.Clear();
                WebReport1.Report.Refresh();
                WebReport1.Update();
                WebReport1.Refresh();

                X.Msg.Alert("提示", "没有找到符合条件的记录!").Show();
                return;
            }

            //初始化报表控件
            FastReport.Report report3 = this.WebReport1.Report;
            report3.Load(Server.MapPath("PpmScanCalcHrCode.frx"));
            //绑定数据源
            report3.RegisterData(ds.Tables[0], "PpmScanCalcHrCode");
            report3.Refresh();
            WebReport1.Update();
            WebReport1.Refresh();
        }
        if (cbxTotalType.SelectedItem.Value == "4") //按照明细查询
        {
            ds = scanRateManager.GetPpmScanCalcDetail(Convert.ToDateTime(txtBeginTime.Text).ToString("yyyy-MM-dd"), Convert.ToDateTime(txtEndTime.Text).ToString("yyyy-MM-dd"), hiddenUserID.Text);

            if (ds.Tables[0].Rows.Count == 0)
            {
                WebReport1.Report.Clear();
                WebReport1.Report.Refresh();
                WebReport1.Update();
                WebReport1.Refresh();

                X.Msg.Alert("提示", "没有找到符合条件的记录!").Show();
                return;
            }

            //初始化报表控件
            FastReport.Report report4 = this.WebReport1.Report;
            report4.Load(Server.MapPath("PpmScanCalcDetail.frx"));
            //绑定数据源
            report4.RegisterData(ds.Tables[0], "PpmScanCalcDetail");
            report4.Refresh();
            WebReport1.Update();
            WebReport1.Refresh();
        }

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
            if (cbxTotalType.SelectedItem.Value=="1")
                Response.AddHeader("Content-Disposition", "attachment;filename=条码扫描率-按车间对比.xlsx");
            if (cbxTotalType.SelectedItem.Value == "2")
                Response.AddHeader("Content-Disposition", "attachment;filename=条码扫描率-按机台对比.xlsx");
            if (cbxTotalType.SelectedItem.Value == "3")
                Response.AddHeader("Content-Disposition", "attachment;filename=条码扫描率-按主机手对比.xlsx");
            if (cbxTotalType.SelectedItem.Value == "4")
                Response.AddHeader("Content-Disposition", "attachment;filename=条码扫描率-明细查询.xlsx");
            strm.Position = 0;
            strm.WriteTo(Response.OutputStream);
            Response.End();
        }

    }
}