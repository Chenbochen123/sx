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
using NBear.Common;



public partial class Manager_Rubber_Report_RubberConsume : System.Web.UI.Page
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
    private BasWorkShopManager shopManager = new BasWorkShopManager();
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
           // bindBasEquip();
            txtBeginTime.SetValue(DateTime.Today.ToString("yyyy-MM-dd"));
            txtEndTime.SetValue(DateTime.Today.ToString("yyyy-MM-dd"));
        }
    }
    //private void bindBasEquip()
    //{
    //    WhereClip where = new WhereClip();
    //    EntityArrayList<BasWorkShop> list = shopManager.GetListByWhere(where);
    //    foreach (BasWorkShop main in list)
    //    {
    //        Ext.Net.ListItem item = new Ext.Net.ListItem(main.WorkShop_Name, main.WorkShop_Code);
    //        cbxChejian.Items.Add(item);
    //    }
    //}


    protected void btnSearch_Click(object sender, DirectEventArgs e)
    {
        PpmRubConsumeManager ppmrub = new PpmRubConsumeManager();
        DataTable dt = GetTotalPageDataBySql(this.txtBeginTime.Text.Trim(), this.txtEndTime.Text.Trim(), "", hiddenEquipCode.Text, cboMaterType.SelectedItem.Value, hiddenMaterCode.Text.ToString());
        if (dt==null)
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
        report.Load(Server.MapPath("RubberConsume.frx"));
        //绑定数据源
        report.RegisterData(dt, "RubConsume");
        report.Refresh();
        WebReport1.Update();
        WebReport1.Refresh();

        X.Msg.Alert("提示", "查询完毕!").Show();
    }

    public DataTable GetTotalPageDataBySql(string begindate, string enddate, string chejian, string equipcode, string matertype, string matercode)
    {
        StringBuilder sqlstr = new StringBuilder();
        sqlstr.AppendLine(@"SELECT RIGHT(CONVERT(NVARCHAR(10),Plandate,112),6) AS PlanDate,c.EquipName,CASE  Left(A.Costcode,1) WHEN '4' THEN '母炼胶' WHEN '5' THEN '终炼胶' WHEN '6' THEN '返回胶' ELSE '其他' END AS MinerTypeName,e.MaterialName AS CostMaterName,b.MaterialName
,CONVERT(decimal(18,1),SUM(consumeqty)) AS consumeqty,CONVERT(decimal(18,1),SUM(Consqty)) AS Consqty
 from PpmRubConsume A
                                left join BasMaterial B on A.MaterCode = B.MaterialCode
                                left join BasEquip C on A.EquipCode = C.EquipCode
                                left join BasMaterial E on A.CostCode = E.MaterialCode
                                where A.DeleteFlag = '0' ");
        if (begindate != "")
            sqlstr.AppendLine(" AND A.PlanDate >= '" + begindate + "'");
        if (enddate != "")
            sqlstr.AppendLine(" AND A.PlanDate <= '" + enddate + "'");
        if (!string.IsNullOrEmpty(equipcode))
        {
            sqlstr.AppendLine(" AND A.EquipCode = '" + equipcode + "'");
        }
        if (!string.IsNullOrEmpty(matertype))
        {

            sqlstr.AppendLine(" AND case when Left(A.Matercode,1)  in ('4','5','6') then Left(A.Matercode,1) else '05' end = '" + matertype + "'");
        }
        if (!string.IsNullOrEmpty(matercode))
        {
            sqlstr.AppendLine(" AND A.Costcode = '" + matercode + "'");
        }
        if (!string.IsNullOrEmpty(chejian))
        {
            sqlstr.AppendLine(" AND c.WorkShopCode = '" + chejian + "'");
        }
        sqlstr.AppendLine(" GROUP BY RIGHT(CONVERT(NVARCHAR(10),Plandate,112),6),CASE  Left(A.Costcode,1) WHEN '4' THEN '母炼胶' WHEN '5' THEN '终炼胶' WHEN '6' THEN '返回胶' ELSE '其他' END,c.EquipName,e.MaterialName,b.MaterialName ");
        PpmRubConsumeManager ppmrub = new PpmRubConsumeManager();
        NBear.Data.CustomSqlSection css = ppmrub.GetBySql(sqlstr.ToString());

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
            Response.AddHeader("Content-Disposition", "attachment;filename=胶料消耗汇总.xlsx");
            strm.Position = 0;
            strm.WriteTo(Response.OutputStream);
            Response.End();
        }

    }
}
