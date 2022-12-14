using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mesnac.Entity;
using Ext.Net;
using Mesnac.Business.Implements;
using NBear.Common;
using System.Text.RegularExpressions;
using Mesnac.Data.Components;
using System.Data;
using System.Text;

public partial class Manager_Technology_Manage_FristWeight : Mesnac.Web.UI.Page
{
    protected Ppt_WeighManager manager = new Ppt_WeighManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["Barcode"]))
            {
                hidden_type.Text = Request.QueryString["Barcode"].ToString();
            }
            if (!string.IsNullOrEmpty(Request.QueryString["Plan_Date"]))
            {
                hidden1.Text = Request.QueryString["Plan_Date"].ToString();
            }
            if (!string.IsNullOrEmpty(Request.QueryString["Equip_Code"]))
            {
                hidden2.Text = Request.QueryString["Equip_Code"].ToString();
            }
            if (!string.IsNullOrEmpty(Request.QueryString["Shift_id"]))
            {
                hidden3.Text = Request.QueryString["Shift_id"].ToString();
            }
            bindList();
            bindList1();
        }
    }


    #region 初始化控件
    


    #endregion



    private DataSet getList()
    {

        return GetDataByParas();
    }

    private DataSet getList1()
    {

        return GetDataByParas1();
    }

    public System.Data.DataSet GetDataByParas()
    {
        StringBuilder sb = new StringBuilder();
        #region
        sb.AppendLine(@"select a.*,a.Barcode as Barcode1,a.real_weight+isnull(b.xlbagweight,c.weigh) as checkweight,e.User_name,e.Work_typeName,
case a.Warning_sgn when 0 then '合格' when 1 then '不合格' end Hege,
case weigh_type when '2' then '油' when '1' then '炭黑' when '3' then '胶料' when '4' then '小料' else weigh_type end as weightype
from ppt_weigh a
left join ppt_plan b on a.plan_id=b.plan_id  
 left join V_xlbagweight c on b.mater_Code=c.mater_Code 
 left join Ppt_Lot d on a.Barcode=d.Barcode
 left join ppt_worker e on e.Equip_Code=d.Equip_Code and e.Plan_date=d.Plan_Date and e.Shift_id=d.Shift_Id");
        sb.AppendLine("WHERE 1=1 ");
        sb.AppendLine("AND a.Barcode='" + hidden_type.Text + "'");
      
        #endregion

        NBear.Data.CustomSqlSection css = manager.GetBySql(sb.ToString());
        return css.ToDataSet();
    }

    public System.Data.DataSet GetDataByParas1()
    {
        StringBuilder sb1 = new StringBuilder();
        #region
        sb1.AppendLine(@"select distinct t.User_name,t.Work_typeName from ppt_worker t
left join ppt_lot t1 on 1=1");
        sb1.AppendLine("WHERE 1=1 ");
        sb1.AppendLine("AND t.Equip_Code='" + hidden2.Text + "'");
        sb1.AppendLine("AND t.Plan_Date='" + hidden1.Text + "'");
        sb1.AppendLine("AND t.Shift_Id='" + hidden3.Text + "'");

        #endregion

        NBear.Data.CustomSqlSection css = manager.GetBySql(sb1.ToString());
        return css.ToDataSet();
    }

    private void bindList()
    {
        this.store.DataSource = getList();
        this.store.DataBind();
    }
    private void bindList1()
    {
        this.store1.DataSource = getList1();
        this.store1.DataBind();
    }

    #region 按钮事件响应
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindList();
        bindList1();
    }
  
    #endregion

}