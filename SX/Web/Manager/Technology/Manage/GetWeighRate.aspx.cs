﻿using System;
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

public partial class Manager_Technology_Manage_GetWeighRate : Mesnac.Web.UI.Page
{
    protected EQM_EnergyManageManager manager = new EQM_EnergyManageManager();
    protected JCZL_SubFacManager facManager = new JCZL_SubFacManager();
    protected Pmt_equipManager equipManager = new Pmt_equipManager();
    protected Pmt_materialManager materialManager = new Pmt_materialManager();
    protected Pmt_recipetypeManager recipeManager = new Pmt_recipetypeManager();
    protected Ppt_ShiftClassManager ClassManager = new Ppt_ShiftClassManager();
    protected SYS_USERManager userManager = new SYS_USERManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            dStartDate.SetValue(DateTime.Now);
            dEndDate.SetValue(DateTime.Now.AddDays(1));
           // bindEquip();
            bindDep();
            binduser();
           
        }
    }


    #region 初始化控件

    private void bindDep()
    {
        cbxDep.Clear();
        WhereClip where = new WhereClip();
        EntityArrayList<JCZL_SubFac> list = facManager.GetListByWhereAndOrder(where, JCZL_SubFac._.Fac_Id.Asc);
        foreach (JCZL_SubFac type in list)
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem(type.Fac_Name, type.Dep_Code);
            cbxDep.Items.Add(item);
        }
    }

    private void binduser()
    {
        WhereClip where = new WhereClip();
        EntityArrayList<SYS_USER> list = userManager.GetListByWhere(SYS_USER._.Work_type == "1" || SYS_USER._.Work_type == "2");
        foreach (SYS_USER type in list)
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem(type.Real_name, type.USER_ID);
            cbxuser.Items.Add(item);
        }
    }

    #endregion



    private DataSet getList()
    {

        return GetDataByParas();
    }


    public System.Data.DataSet GetDataByParas()
    {
        StringBuilder sb = new StringBuilder();
        #region
        string dep = "";
        if (cbxDep.SelectedItem.Value != null)
        {
            dep = cbxDep.SelectedItem.Value.ToString();
        }
        string equip = "";
        if (cbxEquip.Text!="")
        {
            equip = hiddenRecipeEquipCode.Text;
        }
        string class2 = "";
        if (cbxclass.Text != null && cbxclass.Text !="")
        { class2 = cbxclass.SelectedItem.Value.ToString(); }


        var user = "";
        foreach (Ext.Net.ListItem a in this.cbxuser.SelectedItems)
        {
            if (user == "")
            {
                user = a.Value;
            }
            else
            {
                user = user + "," + a.Value;
            }
        }

        string mater = "";
        if (cbxmater.Text != "")
        {
            mater = hiddenmater.Text;
        }

        sb.AppendLine(@"exec proc_GetWeighRate " + "'" + dStartDate.SelectedDate.ToString("yyyy-MM-dd") + "'," + "'" + dEndDate.SelectedDate.ToString("yyyy-MM-dd") + "'," + "'" + dep + "'," + "'" + cbxType.SelectedItem.Text + "'," + "'" + equip + "','" + class2 + "','" + user + "','" + mater + "'");
        #endregion
        NBear.Data.CustomSqlSection css = manager.GetBySql(sb.ToString());
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
            GridPanelCenter.ColumnModel.Columns.Add(new Column { DataIndex = dc.ColumnName, Text = dc.ColumnName });
        }


        StoreCenter.DataSource = ds;
        StoreCenter.DataBind();
        GridPanelCenter.Render();
        //第二个panel
        Model1.Fields.Clear();

        foreach (DataColumn dc1 in ds.Tables[1].Columns)
        {
            Model1.Fields.Add(new ModelField { Name = dc1.ColumnName });
        }

        GridPanel1.ColumnModel.Columns.Clear();
        foreach (DataColumn dc1 in ds.Tables[1].Columns)
        {
            GridPanel1.ColumnModel.Columns.Add(new Column { DataIndex = dc1.ColumnName, Text = dc1.ColumnName });
        }

        Store1.DataSource = ds.Tables[1];
        Store1.DataBind();
        GridPanel1.Render();

    }




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

        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(dt, "合格率统计导出");
    }


    #endregion


    #region 信息列表事件响应
    #endregion
}