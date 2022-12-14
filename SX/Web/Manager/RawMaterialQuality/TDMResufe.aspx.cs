using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Web.UI.HtmlControls;

using NBear;
using NBear.Common;

using Ext.Net;

using Mesnac.Business.Interface;
using Mesnac.Business.Implements;
using Mesnac.Data.Interface;
using Mesnac.Data.Implements;
using Mesnac.Entity;
using Mesnac.Web.UI;
using NPOI.SS.UserModel;
using System.IO;
using NPOI.HSSF.UserModel;
using System.Text;
public partial class Manager_RawMaterialQuality_TDMResufe : System.Web.UI.Page
{
    #region 属性注入

    protected IQmcCheckDataManager checkDataManager = new QmcCheckDataManager();
  
    protected Ext.Net.MessageBox msg = new Ext.Net.MessageBox();//弹出框
    #endregion

    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
        }

    }
    #endregion

    #region 页面初始化
    /// <summary>
    /// 页面加载
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {

            StartDate.SetValue(DateTime.Today.ToString("yyyy-MM-dd"));
            StartDate.SetRawValue(DateTime.Today.ToString("yyyy-MM-dd"));

            EndDate.SetValue(DateTime.Today.ToString("yyyy-MM-dd"));
            EndDate.SetRawValue(DateTime.Today.ToString("yyyy-MM-dd"));
            InitData();
        }
    }



    /// <summary>
    /// 初始化查询数据
    /// </summary>
    private void InitData()
    {
       
        StoreCenterMaster.RemoveAll();
        IQmcCheckDataQueryParams paras = new QmcCheckDataQueryParams();
        paras.RecordStat = "1";//只显示已提交的检测记录


        DataSet ds = GetReportDataSetByParams(paras);

        StoreCenterMaster.DataSource = ds;
        StoreCenterMaster.DataBind();
    }

    public DataSet GetReportDataSetByParams(IQmcCheckDataQueryParams paras)
    {

        String sql = "select * from TDMRefuse where  Recordtime >= '" + DateTime.Today.ToString("yyyy-MM-dd") + "' order by Recordtime "; ;
        return checkDataManager.GetBySql(sql).ToDataSet();
    }
    #endregion

    #region 页面方法


    #endregion

    #region 增删改按钮激发的事件
    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
       
        StoreCenterMaster.RemoveAll();




        String sql = "select * from TDMRefuse where 1=1 ";
        if (!String.IsNullOrEmpty(TextFieldNorthBarcode.Text))
        { sql = sql + " and barcode like '%" + TextFieldNorthBarcode.Text + "%' "; }

        if (!String.IsNullOrEmpty(StartDate.Text))
        { sql = sql + " and Recordtime >= '" + DateTime.Parse(StartDate.Text).ToString("yyyy-MM-dd") + "' "; }

        if (!String.IsNullOrEmpty(EndDate.Text))
        { sql = sql + " and Recordtime <= '" + DateTime.Parse(EndDate.Text).AddDays(1).ToString("yyyy-MM-dd") + "' "; }

      
        sql = sql + " order by recordtime";
        DataSet ds = checkDataManager.GetBySql(sql).ToDataSet();
        StoreCenterMaster.DataSource = ds;
        StoreCenterMaster.DataBind();
    }




    /// <summary>
    /// 选中
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CheckboxSelectionModelCenterMaster_SelectionChange(object sender, DirectEventArgs e)
    {
     
    }



    #endregion
}