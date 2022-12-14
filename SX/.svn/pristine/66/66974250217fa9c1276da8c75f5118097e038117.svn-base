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

public partial class Manager_Rubber_Report_ReturnRubberDayReport : Mesnac.Web.UI.Page
{
    protected PpmReturnRubberManager returnManager = new PpmReturnRubberManager();
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
            txtPlanDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            //string sql = "select * from BasDept";
            //DataSet ds2 = returnManager.GetBySql(sql).ToDataSet();
            //Ext.Net.ListItem li = new Ext.Net.ListItem("全部", "全部");
            //cbxChejian.Items.Add(li);
            //foreach (DataRow dr in ds2.Tables[0].Rows)
            //{
            //  li = new Ext.Net.ListItem(dr["DepName"].ToString(), dr["DepCode"].ToString());

            //    cbxChejian.Items.Add(li);
            
            //}




         cbxChejian.SelectedItem.Index = 0;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        
        string planDate = txtPlanDate.Value.ToString();

        //初始化报表控件
        FastReport.Report report = this.WebReport1.Report;
        report.Load(Server.MapPath("ReturnRubberDayReport.frx"));
        //绑定数据源 returnManager
        ds = GetDayReport(planDate, cbxChejian.SelectedItem.Value);
        if (ds.Tables[0].Rows.Count > 0)
            btnPrint.Visible = true;
        else
            btnPrint.Visible = false;
        report.RegisterData(ds.Tables[0], "ReturnRubberDayReport");
        //report.Refresh();
        WebReport1.Refresh();
    }


    public DataSet GetDayReport(string PlanDate, string workShopCode)
    {
        string sql = @"select '轮胎' + D.WorkShopName + '胶料返回率日报表' WorkShopName, '(" + PlanDate.Substring(0, 10) + @")' PlanDate,  case A.MadeLine when '一线' then '1' when '二线' then '2' 
	                            when '三线' then '3' when '四线' then '4' when '五线' then '5' 
	                            when '内二' then '6' when '内三' then '7' else A.MadeLine end MadeLineID,
	                            A.MadeLine, B.MaterialName, A.ShiftClassID, C.ClassName + '班' ClassName, A.ReturnWeight
                            from PpmReturnRubber A
	                            left join BasMaterial B on A.MaterCode = B.MaterialCode
	                            left join PptClass C on A.ShiftClassID = C.ObjID
                                left join BasWorkShop D on A.WorkShopCode = D.ObjID
                            where A.MadeLine is not null and PlanDate = '" + PlanDate + "'";
        if (!string.IsNullOrEmpty(workShopCode)&&(workShopCode!="全部") )
        {
            sql += " and A.Cust_Name = '" + workShopCode + "'";
        }
        return returnManager.GetBySql(sql).ToDataSet();
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        string planDate = txtPlanDate.Value.ToString();

        //初始化报表控件
        FastReport.Report report = this.WebReport1.Report;
        report.Load(Server.MapPath("ReturnRubberDayReport.frx"));
        //绑定数据源
        DataSet ds = GetDayReport(planDate, cbxChejian.SelectedItem.Value);
        report.RegisterData(ds.Tables[0], "ReturnRubberDayReport");
        
        report.PrintSettings.ShowDialog = false;
        report.Print();
    }
}