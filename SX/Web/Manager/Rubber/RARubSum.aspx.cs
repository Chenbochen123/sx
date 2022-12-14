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


public partial class Manager_Rubber_RARubSum : Mesnac.Web.UI.Page
{
    protected PpmRubConsumeManager Manager = new PpmRubConsumeManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if ( !X.IsAjaxRequest&&!Page.IsPostBack )
        {
            StartDate.SelectedDate = DateTime.Now.AddDays(-7);
            EndDate.SelectedDate = DateTime.Now;
            PageInit();
        }
    }

    #region 初始化下拉列表
    private void PageInit()
    {
        bindList();
    }

    private void bindList()
    {
        StringBuilder sb = new StringBuilder();
        #region
        sb.AppendLine(@"exec proc_RaRubSum '" + StartDate.SelectedDate.ToString("yyyy-MM-dd") + "','" + EndDate.SelectedDate.ToString("yyyy-MM-dd") + "'");
        #endregion

        DataSet ds = Manager.GetBySql(sb.ToString()).ToDataSet();

        ModelCenter.Fields.Clear();

        foreach (DataColumn dc in ds.Tables[0].Columns)
        {
            ModelCenter.Fields.Add(new ModelField { Name = dc.ColumnName });
        }

        GridPanelCenter.ColumnModel.Columns.Clear();
        foreach (DataColumn dc in ds.Tables[0].Columns)
        {
            Ext.Net.Column cs = new Ext.Net.Column { DataIndex = dc.ColumnName, Text = dc.ColumnName };
           
            GridPanelCenter.ColumnModel.Columns.Add(cs);
        }


        store.DataSource = ds;
        store.DataBind();
    }
    #endregion



    #region 按钮事件响应
    protected void btnSearch_Click( object sender , EventArgs e )
    {
        bindList();
    }
    #endregion


    protected void btnExportSubmit_Click(object sender, EventArgs e)
    {
        StringBuilder sb = new StringBuilder();
        #region
        sb.AppendLine(@"exec proc_RaRubSum '" + StartDate.SelectedDate.ToString("yyyy-MM-dd") + "','" + EndDate.SelectedDate.ToString("yyyy-MM-dd") + "'");
        #endregion

        DataSet ds = Manager.GetBySql(sb.ToString()).ToDataSet();

        ModelCenter.Fields.Clear();

        foreach (DataColumn dc in ds.Tables[0].Columns)
        {
            ModelCenter.Fields.Add(new ModelField { Name = dc.ColumnName });
        }

        GridPanelCenter.ColumnModel.Columns.Clear();
        foreach (DataColumn dc in ds.Tables[0].Columns)
        {
            Ext.Net.Column cs = new Ext.Net.Column { DataIndex = dc.ColumnName, Text = dc.ColumnName };

            GridPanelCenter.ColumnModel.Columns.Add(cs);
        }


        store.DataSource = ds;
        store.DataBind();
        //huiw,2013-10-28添加，判断不为空时才导出excel
        if (ds == null || ds.Tables[0].Rows.Count == 0)
        {
            X.Msg.Alert("提示", "未查询出数据！").Show();
        }
        else
        {
            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
            {
                bool isshow = false;
                DataColumn dc = ds.Tables[0].Columns[i];
                foreach (ColumnBase cb in this.GridPanelCenter.ColumnModel.Columns)
                {
                    if ((cb.DataIndex != null) && (cb.DataIndex.ToUpper() == dc.ColumnName.ToUpper()))
                    {
                        dc.ColumnName = cb.Text;
                        isshow = true;
                        break;
                    }
                }
                if (!isshow)
                {
                    ds.Tables[0].Columns.Remove(dc.ColumnName);
                    i--;
                }
            }
            new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(ds, "合片胶消耗统计");
        }
    }


}