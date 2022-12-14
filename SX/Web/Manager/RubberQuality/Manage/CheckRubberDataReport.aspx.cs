﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using Ext.Net;

using Mesnac.Business.Interface;
using Mesnac.Business.Implements;
using Mesnac.Data.Interface;
using Mesnac.Data.Implements;
using Mesnac.Entity;
using Mesnac.Web.UI;

using NBear.Common;

using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.Util;
using NPOI.HSSF.Util;
using NPOI.SS.Util;

public partial class Manager_RubberQuality_Manage_CheckRubberDataReport : System.Web.UI.Page
{
    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查询 = new SysPageAction() { ActionID = 1, ActionName = "ButtonNorthQuery" };
            导出 = new SysPageAction() { ActionID = 2, ActionName = "ButtonNorthExport" };
        }
        public SysPageAction 查询 { get; private set; } //必须为 public
        public SysPageAction 导出 { get; private set; } //必须为 public
    }
    #endregion

    /// <summary>
    /// 页面加载
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            #region 加载CSS样式
            HtmlGenericControl cssLink = new HtmlGenericControl("link");
            cssLink.Attributes.Add("type", "text/css");
            cssLink.Attributes.Add("href", this.ResolveUrl("~/resources/css/main.css"));
            this.Page.Header.Controls.Add(cssLink);

            cssLink = new System.Web.UI.HtmlControls.HtmlGenericControl("link");
            cssLink.Attributes.Add("type", "text/css");
            cssLink.Attributes.Add("rel", "stylesheet");
            cssLink.Attributes.Add("href", this.ResolveUrl("~/resources/css/ext-chinese-font.css"));
            this.Page.Header.Controls.Add(cssLink);
            #endregion 加载CSS样式

            InitControls();

            DateFieldNorthBeginDate.SetValue(DateTime.Today.ToString("yyyy-MM-dd"));
            DateFieldNorthEndDate.SetValue(DateTime.Today.ToString("yyyy-MM-dd"));
            if (ComboBoxNorthItemType.Items.Count > 0)
            {
                ComboBoxNorthItemType.Select(0);
            }
        }
    }

    private void InitControls()
    {
        IBasWorkShopManager bBasWorkShopManager = new BasWorkShopManager();

        // 机台类型
        string sql = "  select * from qmt_dayreport";
        DataSet ds = bBasWorkShopManager.GetBySql(sql).ToDataSet();
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            ComboBoxNorthItemType.Items.Add(new Ext.Net.ListItem { Text = dr[1].ToString(), Value = dr[0].ToString() });
        }
    }

    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonNorthQuery_Click(object sender, DirectEventArgs e)
    {
        string beginPlanDate = DateFieldNorthBeginDate.RawText;
        string itemType = ComboBoxNorthItemType.SelectedItem.Text;

        HiddenBeginDate.SetValue(beginPlanDate);

        IQmtRubberLBEquipDataReportParams paras = new QmtRubberLBEquipDataReportParams();
        paras.BeginPlanDate = beginPlanDate;
        paras.ItemType = itemType;

        IQmtCheckMasterManager bQmtCheckMasterManager = new QmtCheckMasterManager();
        DataSet ds = GetCheckRubberLBEquipDataReportByParas(paras);
        ModelCenter.Fields.Clear();

        foreach (DataColumn dc in ds.Tables[0].Columns)
        {
            ModelCenter.Fields.Add(new ModelField { Name = dc.ColumnName });
        }

        GridPanelCenter.ColumnModel.Columns.Clear();
        foreach (DataColumn dc in ds.Tables[0].Columns)
        {
            Ext.Net.Column cs = new Ext.Net.Column { DataIndex = dc.ColumnName, Text = dc.ColumnName };
            cs.Renderer = new Renderer { Fn = "change" };
            GridPanelCenter.ColumnModel.Columns.Add(cs);
        }

        StoreCenter.DataSource = ds;
        StoreCenter.DataBind();

        GridPanelCenter.Render();
    }
    public DataSet GetCheckRubberLBEquipDataReportByParas(IQmtRubberLBEquipDataReportParams paras)
    {
        IBasWorkShopManager bBasWorkShopManager = new BasWorkShopManager();
        Dictionary<string, object> dict = new Dictionary<string, object>();

        dict.Add("@datex", paras.BeginPlanDate);
        dict.Add("@date1x", DateFieldNorthEndDate.RawText);
        dict.Add("@lind", paras.ItemType);
        dict.Add("@MaterName", txMaterialName.Text.Trim());
        return bBasWorkShopManager.GetDataSetByStoreProcedure("P_pmtCheckValue", dict);
    }
    /// <summary>
    /// 导出
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonNorthExport_Click(object sender, DirectEventArgs e)
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

        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(dt, "检验数据查询");
    }
}