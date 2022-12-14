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
using Mesnac.Entity;


public partial class Manager_ShopStorage_MaterBatchReport : Mesnac.Web.UI.Page
{
    protected PpmRubConsumeManager Manager = new PpmRubConsumeManager();
    protected JCZL_SubFacManager FacManager = new JCZL_SubFacManager();
    protected Pmt_materialManager materManager = new Pmt_materialManager();
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
        bindmater();
        bindfac();
        bindList();
    }


    private void bindfac()
    {
        Fac.Clear();
        WhereClip where = new WhereClip();
        EntityArrayList<JCZL_SubFac> list = FacManager.GetListByWhere(where);
        foreach (JCZL_SubFac main in list)
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem(main.Fac_Name, main.Dep_Code);
            Fac.Items.Add(item);
        }
        Fac.Select(0);
    }

    private void bindmater()
    {
        WhereClip where = new WhereClip();
        EntityArrayList<Pmt_material> list = materManager.GetListByWhere(where);
        foreach (Pmt_material main in list)
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem(main.Mater_name, main.Mater_code);
            Mater.Items.Add(item);
        }
    }

    private void bindList()
    {
        StringBuilder sb = new StringBuilder();
        #region
        sb.AppendLine(@"exec proc_getMaterBatchReport '" + StartDate.SelectedDate.ToString("yyyy-MM-dd") + "','" + EndDate.SelectedDate.ToString("yyyy-MM-dd") + "','" + Fac.SelectedItem.Value + "','" + Mater.SelectedItem.Value + "'");
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

}