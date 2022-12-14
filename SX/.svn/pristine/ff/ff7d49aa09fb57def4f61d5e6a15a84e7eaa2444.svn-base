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


public partial class Manager_RubberQuality_Manage_GetRubBuhege2 : Mesnac.Web.UI.Page
{
    protected PpmRubConsumeManager Manager = new PpmRubConsumeManager();
   // protected PptShiftConfigManager Manager = new PptShiftConfigManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if ( !X.IsAjaxRequest&&!Page.IsPostBack )
        {
            StartDate.SelectedDate = DateTime.Now.AddDays(-7);
            EndDate.SelectedDate = DateTime.Now;
          //  cbxType.SelectedItem.Value = "1";
            PageInit();
        }
    }

    #region 检验类型 需要改变颜色的列和需要隐藏的列
    private string[] strArray1 = { "ML合格", "MH合格", "T10合格", "T50合格", "T90合格", "MU合格", "焦烧合格", "js合格", "yd合格", "md合格", "CU合格", "抽出合格", "T30合格", "T60合格", "密度合格", "分散度合格" };
    private string[] strArray2 = { "ML处理", "MH处理", "T10处理", "T50处理", "T90处理", "MU处理", "焦烧处理", "js处理", "yd处理", "md处理", "CU处理", "抽出处理", "T30处理", "T60处理", "密度处理", "分散度处理方式", "Md处理" };
    #endregion 

    #region 初始化下拉列表
    private void PageInit()
    {
        //bindList();
    }
    private void bindList()
    {
        DataSet ds = getList();
        ModelCenter.Fields.Clear();

        foreach (DataColumn dc in ds.Tables[0].Columns)
        {
            ModelCenter.Fields.Add(new ModelField { Name = dc.ColumnName });
        }

        GridPanelCenter.ColumnModel.Columns.Clear();
        foreach (DataColumn dc in ds.Tables[0].Columns)
        {
            Ext.Net.Column cs = new Ext.Net.Column { DataIndex = dc.ColumnName, Text = dc.ColumnName };
            for (int i = 0; i < strArray1.Length; i++)
            {
                if (strArray1[i] == dc.ColumnName)
                {
                    cs.Renderer = new Renderer { Fn = "change" };
                }
            }
            for (int i = 0; i < strArray2.Length; i++)
            {
                if (strArray2[i] == dc.ColumnName)
                {
                    cs.Hidden = true;
                }
            }
            GridPanelCenter.ColumnModel.Columns.Add(cs);
        }


        store.DataSource = ds;
        store.DataBind();
        GridPanelCenter.Render();
    }
    private DataSet getList()
    {
        return GetDataByParas();
    }

    public System.Data.DataSet GetDataByParas()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine(@"exec proc_GetRubBuhege '" + StartDate.SelectedDate.ToString("yyyy-MM-dd") + "','" + EndDate.SelectedDate.ToString("yyyy-MM-dd") + "'");

        NBear.Data.CustomSqlSection css = Manager.GetBySql(sb.ToString());
        return css.ToDataSet();
    }
   



    //private void bindList()
    //{
    //    StringBuilder sb = new StringBuilder();
    //    #region
    //    sb.AppendLine(@"exec proc_GetRubBuhege '" + StartDate.SelectedDate.ToString("yyyy-MM-dd") + "','" + EndDate.SelectedDate.ToString("yyyy-MM-dd") + "','" + cbxType.SelectedItem.Value + "'");
    //    #endregion

    //    DataSet ds = Manager.GetBySql(sb.ToString()).ToDataSet();

    //    ModelCenter.Fields.Clear();

    //    foreach (DataColumn dc in ds.Tables[0].Columns)
    //    {
    //        ModelCenter.Fields.Add(new ModelField { Name = dc.ColumnName });
    //    }

    //    GridPanelCenter.ColumnModel.Columns.Clear();
    //    foreach (DataColumn dc in ds.Tables[0].Columns)
    //    {
    //        Ext.Net.Column cs = new Ext.Net.Column { DataIndex = dc.ColumnName, Text = dc.ColumnName };
           
    //        GridPanelCenter.ColumnModel.Columns.Add(cs);
    //    }


    //    store.DataSource = ds;
    //    store.DataBind();
    //}
    #endregion



    #region 按钮事件响应
    protected void btnSearch_Click( object sender , EventArgs e )
    {
        bindList();
    }
    #endregion

}