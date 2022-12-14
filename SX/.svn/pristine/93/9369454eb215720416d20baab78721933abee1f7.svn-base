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

public partial class Manager_ProducingPlan_SmallMaterialWeigh_SmallMaterialWeigh_2 : System.Web.UI.Page
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
        //#region 加载CSS样式
        //HtmlGenericControl cssLink = new HtmlGenericControl("link");
        //cssLink.Attributes.Add("type", "text/css");
        //cssLink.Attributes.Add("href", this.ResolveUrl("~/resources/css/main.css"));
        //this.Page.Header.Controls.Add(cssLink);
        //#endregion 加载CSS样式

        if (!X.IsAjaxRequest)
        {
            txtstart.SetValue(DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd"));
            txtend.SetValue(DateTime.Today.ToString("yyyy-MM-dd"));
            ComboBox1.Select("1");
            cbxTotalType.Select("11");
        }
    }

    protected void btnSearch_Click(object sender, DirectEventArgs e)
    {
        if (string.IsNullOrEmpty(hiddenEquipCode.Text))
        {
            X.Msg.Alert("提示", "请选择机台!").Show();
            return;
        }

     
        PpmEquipYieldReportManager yieldReport = new PpmEquipYieldReportManager();

        DataSet ds;
       IPptPlanManager pptPlanManager = new PptPlanManager();
       String sql = " exec proc_equipchanliangrptnew2 '" + DateTime.Parse(txtstart.Text).ToString("yyyy-MM-dd") + "','" + DateTime.Parse(txtend.Text).ToString("yyyy-MM-dd") + "','" + hiddenEquipCode.Text + "','" + cbxTotalType.SelectedItem.Value + "'," + ComboBox1.SelectedItem.Value;


       //X.Msg.Alert("提示", sql).Show(); return;
        ds = pptPlanManager.GetBySql(sql).ToDataSet();

        foreach (DataRow dr in ds.Tables[0].Rows)

        { dr["UserName"] = dr["UserName"] + " " + dr["Mater_Name"]; }

        if (ds.Tables[0].Rows.Count == 0)
        {
            WebReport1.Report.Clear();
            WebReport1.Report.Refresh();
            WebReport1.Update();
            WebReport1.Refresh();

            X.Msg.Alert("提示", "没有找到符合条件的记录!").Show();
            return;
        }
        if (ComboBox1.SelectedItem.Value == "1")
        {
            foreach (DataRow dr in ds.Tables[0].Rows)

            { dr["RealWeight"] = dr["Num"]; }



        }
        //初始化报表控件

        FastReport.Report report = this.WebReport1.Report;

        report.Load(Server.MapPath("table.frx"));
        
        //绑定数据源
        report.RegisterData(ds.Tables[0], "Table");
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
        if(1==2){
        PpmEquipYieldReportManager yieldReport = new PpmEquipYieldReportManager();

        DataSet ds;
        IPptPlanManager pptPlanManager = new PptPlanManager();
        String sql = " exec proc_equipchanliangrptnew2 '" + DateTime.Parse(txtstart.Text).ToString("yyyy-MM-dd") + "','" + DateTime.Parse(txtend.Text).ToString("yyyy-MM-dd") + "','" + hiddenEquipCode.Text + "','" + cbxTotalType.SelectedItem.Value + "'," + ComboBox1.SelectedItem.Value;


        //X.Msg.Alert("提示", sql).Show(); return;
        ds = pptPlanManager.GetBySql(sql).ToDataSet();

        foreach (DataRow dr in ds.Tables[0].Rows)

        { dr["UserName"] = dr["UserName"] + " " + dr["Mater_Name"]; }


        if (ComboBox1.SelectedItem.Value == "1")
        {
            foreach (DataRow dr in ds.Tables[0].Rows)

            { dr["RealWeight"] = dr["Num"]; }



        }




        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(ds, "小料产量查询");

        return;}


        WebReport1.Report.Prepare();

        FastReport.Export.OoXML.Excel2007Export excelExport = new FastReport.Export.OoXML.Excel2007Export();
       
        using (System.IO.MemoryStream strm = new System.IO.MemoryStream())
        {
            WebReport1.Report.Export(excelExport, strm);
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.ContentType = "Application/Excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=小料产量汇总明细.xlsx");
            strm.Position = 0;
            strm.WriteTo(Response.OutputStream);
            Response.End();
        }

    }
}