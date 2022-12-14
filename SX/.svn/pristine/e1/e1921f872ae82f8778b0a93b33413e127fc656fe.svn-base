using System;
using System.Data;
using System.Xml;
using System.Xml.Xsl;
using Ext.Net;
using Mesnac.Business.Implements;
using Mesnac.Data.Interface;
using Mesnac.Data.Implements;
using Mesnac.Entity;
using NBear.Common;
using System.Collections.Generic;
using System.Text;
using Mesnac.Entity;


public partial class Manager_Rubber_Plan : Mesnac.Web.UI.Page
{
    protected PptPlanManager Manager = new PptPlanManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if ( !X.IsAjaxRequest&&!Page.IsPostBack )
        {
            PageInit();
        }
    }

    #region 初始化下拉列表
    private void PageInit()
    {
        DateBeginTime.SelectedDate = DateTime.Now;
        DateEndTime.SelectedDate = DateTime.Now;
        bindList();
    }


    private DataSet getList()
    {


        return GetDataByParas();
       // return GetDataByParas(cboPlan.Value.ToString);
    }

    public System.Data.DataSet GetDataByParas()
    {
        StringBuilder sb = new StringBuilder();
        #region
        sb.AppendLine(@"select Plan_id,plan_date,equip_name,mater_name,real_num,cjname,CJweight from ppt_plan a left join pmt_equip b on a.equip_Code=b.equip_Code ");
        sb.AppendLine("where cjweight>0");
        if (DateBeginTime.SelectedDate != DateTime.MinValue)
        {
            sb.AppendLine("AND plan_date>='" + DateBeginTime.SelectedDate.ToString("yyyy-MM-dd") + "'");
        }
        if (DateEndTime.SelectedDate != DateTime.MinValue)
        {
            sb.AppendLine("AND plan_date<='" + DateEndTime.SelectedDate.ToString("yyyy-MM-dd") + "'");
        }
        #endregion

        NBear.Data.CustomSqlSection css = Manager.GetBySql(sb.ToString());
        return css.ToDataSet();
    }
    private void bindList()
    {
        this.store.DataSource = getList();
        this.store.DataBind();
    }
    #endregion



    #region 按钮事件响应
    protected void btnSearch_Click( object sender , EventArgs e )
    {
        bindList();
    }
    #endregion



}