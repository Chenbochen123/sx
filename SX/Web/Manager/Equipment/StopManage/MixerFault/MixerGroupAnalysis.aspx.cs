using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using System.Data;
using Mesnac.Business.Implements;

public partial class Manager_Equipment_StopManage_MixerFault_MixerGroupAnalysis : System.Web.UI.Page
{
    protected EqmMixerFaultManager manager = new EqmMixerFaultManager();//业务对象
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            this.txt_fault_type.Select(0);
            this.txt_count.Select(0);
        }
    }

    #region 图表刷新方法

    /// <summary>
    /// 图表刷新
    /// dongbo 2013-08-15
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void MyData_Refresh(object sender, StoreReadDataEventArgs e)
    {
    }
    /// <summary>
    /// 图表刷新
    /// </summary>
    private void BindData(string columnName , DateTime faultBeginDate , DateTime faultEndDate , int count)
    {
        DataSet ds = manager.GetChartGroupAnalysis(columnName, faultBeginDate, faultEndDate, count);
        if (ds != null && ds.Tables.Count > 0)
        {
            DataTable dtresult = ds.Tables[0];
            if (dtresult != null && dtresult.Rows.Count > 0)
            {
                this.Store1.DataSource = dtresult.DefaultView;
                this.Store1.DataBind();
            }
            else
            {
                X.Msg.Alert("提示","此条件下无相应数据!").Show();
            }
        }

    }

    /// <summary>
    /// 点击查询触发事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_search_click(object sender, EventArgs e)
    {
        this.Store1.RemoveAll();
        Panel1.Title = "密炼机故障统计分析【按" + this.txt_fault_type.Text + "统计】";
        string faultType = this.txt_fault_type.Value.ToString();
        DateTime faultBeginDate = Convert.ToDateTime(this.txt_fault_begin_date.Text);
        DateTime faultEndDate = Convert.ToDateTime(this.txt_fault_end_date.Text);
        int count = Convert.ToInt32(this.txt_count.Value);
        this.BindData(faultType, faultBeginDate, faultEndDate, count);
    }
    #endregion
}