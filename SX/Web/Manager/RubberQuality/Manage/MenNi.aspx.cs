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


public partial class Manager_RubberQuality_Manage_MenNi : Mesnac.Web.UI.Page
{
    protected Ppt_ClassUserManager Manager = new Ppt_ClassUserManager();
    protected BasWorkManager WorkManager = new BasWorkManager();
    protected Pmt_equipManager equipManager = new Pmt_equipManager();
    protected Pmt_materialManager materManager = new Pmt_materialManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest && !Page.IsPostBack)
        {
            PageInit();
        }
    }

    #region 初始化下拉列表
    private void PageInit()
    {
        txtdate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        txtenddate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        equip();
     //   mater();
        bindList();
    }
    private void equip()
    {
        WhereClip where = new WhereClip();
        EntityArrayList<Pmt_equip> list = equipManager.GetListByWhere(where);
        foreach (Pmt_equip main in list)
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem(main.Equip_name, main.Equip_code);
            cbxequip.Items.Add(item);
        }
    }
    //private void mater()
    //{
    //    WhereClip where = new WhereClip();
    //    EntityArrayList<Pmt_material> list = materManager.GetListByWhere(Pmt_material._.Mkind_code == 3 || Pmt_material._.Mkind_code == 4 || Pmt_material._.Mkind_code == 5);
    //    foreach (Pmt_material main in list)
    //    {
    //        Ext.Net.ListItem item = new Ext.Net.ListItem(main.Mater_name, main.Mater_code);
    //        cbxmater.Items.Add(item);
    //    }
    //}
    private DataSet getList()
    {
        return GetDataByParas();
    }

    public System.Data.DataSet GetDataByParas()
    {
        StringBuilder sb = new StringBuilder();
        #region
        sb.AppendLine(@"select plandate [生产日期],planshift [生产班次],equipname [生产机台],matername [物料名称],testkind [检验类型],checkdate [检验日期],checkshift [检验班次],
      serialid [车次],item_name [实验项目],check_Value [检验值],case juge_result when 0 then '不合格' when 1 then '合格' else '无标准' end as [判定] from Qmt_QrigGaoTie_MN
   where plandate>='" + txtdate.SelectedDate.ToString("yyyy-MM-dd") + "' and plandate<='" + txtenddate.SelectedDate.ToString("yyyy-MM-dd") + "'");
        if (!string.IsNullOrEmpty(cbxequip.SelectedItem.Value))
        {
            sb.AppendLine("AND equipCode='" + cbxequip.SelectedItem.Value + "'");
        }
        if (!string.IsNullOrEmpty(cbxShift.SelectedItem.Value))
        {
            sb.AppendLine("AND planshift='" + cbxShift.SelectedItem.Value + "'");
        }
        if (!string.IsNullOrEmpty(txMaterialName.Text.Trim()))
        {
            sb.AppendLine("AND matername='" + txMaterialName.Text.Trim() + "'");
        }
        #endregion

        NBear.Data.CustomSqlSection css = Manager.GetBySql(sb.ToString());
        Session["data"] = css.ToDataSet().Tables[0];
        return css.ToDataSet();
    }
    private void bindList()
    {
        this.store.DataSource = getList();
        this.store.DataBind();
    }
    #endregion



    #region 按钮事件响应
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindList();
    }

   
    #endregion

    protected void btnExportSubmit_Click(object sender, EventArgs e)
    {
        if ((DataTable)Session["data"] == null)
        {
            X.Msg.Alert("提示", "未查询出数据！").Show();
        }
        else
        {
            DataTable dt = (DataTable)Session["data"];
            //for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
            //{
            //    bool isshow = false;
            //    DataColumn dc = ds.Tables[0].Columns[i];
            //    foreach (ColumnBase cb in this.GridPanelCenter.ColumnModel.Columns)
            //    {
            //        if ((cb.DataIndex != null) && (cb.DataIndex.ToUpper() == dc.ColumnName.ToUpper()))
            //        {
            //            dc.ColumnName = cb.Text;
            //            isshow = true;
            //            break;
            //        }
            //    }
            //    if (!isshow)
            //    {
            //        ds.Tables[0].Columns.Remove(dc.ColumnName);
            //        i--;
            //    }
            //}
            new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(dt, "报警记录查询记录");
        }
    }


}