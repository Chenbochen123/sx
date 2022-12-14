using System;
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
using System.Text;

public partial class Manager_Rubber_Report_RubberStoreDaySum : System.Web.UI.Page
{
    protected Ppm_rubDaySumManager Manager = new Ppm_rubDaySumManager();
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!X.IsAjaxRequest)
        {
            txtBeginTime.SetValue(DateTime.Today.ToString("yyyy-MM-dd"));
        }

    }
    protected void btnSearch_Click(object sender, DirectEventArgs e)
    {
        Ppm_rubDaySumManager.QueryParams queryParams = new Ppm_rubDaySumManager.QueryParams();

        queryParams.plan_date = Convert.ToDateTime(txtBeginTime.Text).ToString("yyyy-MM-dd");
        DataTable dt = GetTableStoreDaySum(queryParams);
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
        report.Load(Server.MapPath("RubberStoreDaySum.frx"));
        //绑定数据源
        report.RegisterData(dt, "RubberStoreDaySum");
        report.Refresh();
        WebReport1.Update();
        WebReport1.Refresh();

        X.Msg.Alert("提示", "查询完毕!").Show();
    }

    public DataTable GetTableStoreDaySum(Ppm_rubDaySumManager.QueryParams queryParams)
    {

        StringBuilder sqlstr = new StringBuilder();
        sqlstr.AppendLine(@"select t.*,t1.Mater_name From ppm_rubdaysum t
left join Pmt_material t1 on t1.Mater_code=t.mater_Code
                                where 1 = 1");
            sqlstr.AppendLine(" AND t.plan_date = '" + queryParams.plan_date + "'");
            NBear.Data.CustomSqlSection css = Manager.GetBySql(sqlstr.ToString());
            return css.ToDataSet().Tables[0];

        
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
            Response.AddHeader("Content-Disposition", "attachment;filename=产量信息报表.xlsx");
            strm.Position = 0;
            strm.WriteTo(Response.OutputStream);
            Response.End();
        }
    }
}