﻿using System;
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


public partial class Manager_Rubber_RubberRealStorage : Mesnac.Web.UI.Page
{
    protected PptShiftConfigManager Manager = new PptShiftConfigManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest && !Page.IsPostBack)
        {
            PageInit();
        }
    }
    private string[] strArray1 = { "状态"};

    #region 初始化下拉列表
    private void PageInit()
    {
        bindList();
    }


    private DataSet getList()
    {


        return GetDataByParas();
    }

    public System.Data.DataSet GetDataByParas()
    {
        StringBuilder sb = new StringBuilder();
        string fac = "";
        if (cbofac.Text != "全部" && !string.IsNullOrEmpty(cbofac.Text))
        {
            fac = cbofac.SelectedItem.Value;
        }
        else if (string.IsNullOrEmpty(cbofac.Text))
        { fac = "999"; }
        else { fac = "01,07"; }
        #region
        sb.AppendLine(@"exec proc_RubFIFOReportDep '" + "" + fac + "'");

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
          //  GridPanelCenter.ColumnModel.Columns.Add(new Column { DataIndex = dc.ColumnName, Text = dc.ColumnName });

            Ext.Net.Column cs = new Ext.Net.Column { DataIndex = dc.ColumnName, Text = dc.ColumnName };
            for (int i = 0; i < strArray1.Length; i++)
            {
                if (strArray1[i] == dc.ColumnName)
                {
                    cs.Renderer = new Renderer { Fn = "change" };
                }
            }
            GridPanelCenter.ColumnModel.Columns.Add(cs);
        }


        StoreCenter.DataSource = ds;
        StoreCenter.DataBind();
        GridPanelCenter.Render();
    }
    #endregion

    #region 按钮事件响应
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindList();
    }

    protected void btnExportSubmit_Click(object sender, DirectEventArgs e)
    {
        string fields = e.ExtraParams["fields"];
        string records = e.ExtraParams["records"];
        Newtonsoft.Json.JavaScriptArray jsArrayFields = Newtonsoft.Json.JavaScriptConvert.DeserializeObject(fields) as Newtonsoft.Json.JavaScriptArray;
        Newtonsoft.Json.JavaScriptArray jsArrayRecords = Newtonsoft.Json.JavaScriptConvert.DeserializeObject(records) as Newtonsoft.Json.JavaScriptArray;

        DataTable dt = new DataTable();

        foreach (Newtonsoft.Json.JavaScriptObject jsObjectField in jsArrayFields)
        {
            if (jsObjectField["name"].ToString().ToLower() != "id")
            {
                dt.Columns.Add(new DataColumn(jsObjectField["name"].ToString(), typeof(string)));
            }
        }

        foreach (Newtonsoft.Json.JavaScriptObject jsObjectRecord in jsArrayRecords)
        {
            DataRow dr = dt.NewRow();
            foreach (DataColumn dc in dt.Columns)
            {
                dr[dc.ColumnName] = jsObjectRecord[dc.ColumnName];
            }
            dt.Rows.Add(dr);
        }

        if (dt.Rows.Count == 0)
        {
            X.Msg.Alert("提示", "没有找到符合条件的记录").Show();
            return;
        }

        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(dt, "车间胶料实时库存表");
    }

    #endregion

}