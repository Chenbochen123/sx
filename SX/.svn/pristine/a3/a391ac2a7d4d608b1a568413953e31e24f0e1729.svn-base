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

using FastReport;
using FastReport.Utils;
using FastReport.Web;
using FastReport.Data;

public partial class Manager_ReportCenter_ShopStorage_ShopConsumeTotal : Mesnac.Web.UI.Page
{
    protected BasUserManager userManager = new BasUserManager();
    protected PstmmshopoutManager shopConsumeManager = new PstmmshopoutManager();
    DataSet ds;

    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查询 = new SysPageAction() { ActionID = 1, ActionName = "btnSearch" };
        }
        public SysPageAction 查询 { get; private set; } //必须为 public
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest && !IsPostBack)
        {
            txtBeginTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtEndTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string beginDate = txtBeginTime.Value.ToString();
        string endDate = Convert.ToDateTime(txtEndTime.Value).AddDays(1).ToString();

        //初始化报表控件
        FastReport.Report report = this.WebReport1.Report;
        report.Load(Server.MapPath("ShopConsumeTotal.frx"));
        //绑定数据源
        ds = shopConsumeManager.GetShopConsumeTotal(beginDate, endDate, cbxMaterialype.Value.ToString(),this.cbxShiftID.SelectedItem.Value,this.hiddenEquipCode.Text.Trim());
        if (ds.Tables[0].Rows.Count > 0)
            btnPrint.Visible = true;
        else
            btnPrint.Visible = false;
        report.RegisterData(ds.Tables[0], "ShopConsumeTotal");
        //report.Refresh();
        WebReport1.Refresh();
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        string beginDate = txtBeginTime.Value.ToString();
        string endDate = Convert.ToDateTime(txtEndTime.Value).AddDays(1).ToString();

        //初始化报表控件
        FastReport.Report report = this.WebReport1.Report;
        report.Load(Server.MapPath("ShopConsumeTotal.frx"));
        //绑定数据源
        DataSet ds = shopConsumeManager.GetShopConsumeTotal(beginDate, endDate, cbxMaterialype.Value.ToString(), this.cbxShiftID.SelectedItem.Value, this.hiddenEquipCode.Text.Trim());
        report.RegisterData(ds.Tables[0], "ShopConsumeTotal");
        //System.Drawing.Printing.PrinterSettings settings = new System.Drawing.Printing.PrinterSettings();
        //settings.ToPage = 2;
        //report.ShowPrintDialog(out settings);
        report.PrintSettings.ShowDialog = false;
        report.Print();
    }
}