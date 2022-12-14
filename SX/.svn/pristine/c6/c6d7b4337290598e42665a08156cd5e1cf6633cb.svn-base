using System;
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

public partial class Manager_RubberQuality_Manage_getManulweight : Mesnac.Web.UI.Page
{
    protected Ppt_LotManager manager = new Ppt_LotManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest && !Page.IsPostBack)
        {
            cbxType.SelectedItem.Value = "1";
            dStartDate.SetValue(DateTime.Now);
            dEndDate.SetValue(DateTime.Now.AddDays(1));
        }
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
        store2.DataSource = ds;
        store2.DataBind();
        GridPanelCenter.Render();
    }
    private DataSet getList()
    {
        return GetDataByParas();
    }

    public System.Data.DataSet GetDataByParas()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine(@"exec proc_getManulweight '" + dStartDate.SelectedDate.ToString("yyyy-MM-dd") + "','" + dEndDate.SelectedDate.ToString("yyyy-MM-dd") + "','" + cbxType.SelectedItem.Value + "'");

        NBear.Data.CustomSqlSection css = manager.GetBySql(sb.ToString());
        return css.ToDataSet();
    }
    protected void RowSelect(object sender, StoreReadDataEventArgs e)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine(@" select Mater_name,Set_weight,Real_weight,case weigh_state when 0 then'手动' when 1 then '自动' end state  from Ppt_Weigh ");


        string barcode2 = "";
        foreach (SelectedRow row in this.rowSelectMuti.SelectedRows)
        {
            barcode2 = row.RecordID;
        }
        if (!string.IsNullOrEmpty(barcode2))
        {
            sb.AppendLine(@"  where barcode='" + barcode2 + "'");
        }

        NBear.Data.CustomSqlSection css = manager.GetBySql(sb.ToString());
        this.store1.DataSource = css.ToDataSet();
        this.store1.DataBind();
    }
   
    #region 按钮事件响应
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindList();
    }
 
    #endregion


}