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


public partial class Manager_ProducingPlan_MLFL : Mesnac.Web.UI.Page
{
    protected PptAlarmManager Manager = new PptAlarmManager();
    protected BasEquipManager BasEquipManager = new BasEquipManager();

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
        daDateTime.SelectedDate = DateTime.Now;
        daDateTimeEnd.SelectedDate = DateTime.Now;
        bindList();
    }
    
    private DataSet getList()
    {
        return GetDataByParas();
    }

    public System.Data.DataSet GetDataByParas()
    {
        StringBuilder sb = new StringBuilder();
        #region
        sb.AppendLine(@"exec DZSP_MuJiaoFanlianlv '" + daDateTime.SelectedDate.ToShortDateString() + "','" + daDateTimeEnd.SelectedDate.ToShortDateString() + "'");

        #endregion

        NBear.Data.CustomSqlSection css = Manager.GetBySql(sb.ToString());
        return css.ToDataSet();
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

            GridPanelCenter.ColumnModel.Columns.Add(cs);
        }


        StoreCenter.DataSource = ds;
        StoreCenter.DataBind();
        GridPanelCenter.Render();
    }
    #endregion



    #region 按钮事件响应
    protected void btnSearch_Click( object sender , EventArgs e )
    {
        bindList();
    }
    protected void btnExportSubmit_Click(object sender, EventArgs e)
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

            GridPanelCenter.ColumnModel.Columns.Add(cs);
        }


        StoreCenter.DataSource = ds;
        StoreCenter.DataBind();
        GridPanelCenter.Render();
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
            new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(ds, "母炼胶返炼查询");
        }
    }
  
    #endregion

    
}