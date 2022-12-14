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


public partial class Manager_Equipment_SparePart_Dianhao : Mesnac.Web.UI.Page
{
    protected PptAlarmManager Manager = new PptAlarmManager();
    protected BasEquipManager BasEquipManager = new BasEquipManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        if ( !X.IsAjaxRequest&&!Page.IsPostBack )
        {
            bindBasEquip();
            PageInit();
        }
    }

    private void PageInit()
    {
        cbxequip.Select(0);
        daDateTime.Text = DateTime.Now.AddDays(1 - DateTime.Now.Day).Date.ToString("yyyy-MM-dd");
        daDateTimeEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");
       
    }
    private void bindBasEquip()
    {
        WhereClip where = new WhereClip();
        EntityArrayList<BasEquip> list = BasEquipManager.GetListByWhere(BasEquip._.EquipType=="01");
        foreach (BasEquip main in list)
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem(main.EquipName, main.EquipCode);
            cbxequip.Items.Add(item);
        }
    }

    [DirectMethod]
    public string chartMainBindData()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine(@"exec Cost_DayEquipDian '" + daDateTime.SelectedDate.ToString("yyyy-MM-dd") + "','" + daDateTimeEnd.SelectedDate.ToString("yyyy-MM-dd") + "','"+cbxequip.SelectedItem.Value+"'");

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
        StoreCenter.DataSource = ds;
        StoreCenter.DataBind();
        GridPanelCenter.Render();


        string jiaban = Stringjiaban(ds.Tables[0]);
        string yiban = Stringyiban(ds.Tables[0]);
        string bingban = Stringbingban(ds.Tables[0]);
        string jizhun = Stringjizhun(ds.Tables[0]);

        return jiaban + "@" + yiban + "@" + bingban + "@" + jizhun;
    }
    private string Stringjiaban(DataTable dt)
    {
        StringBuilder jsonBuilder = new StringBuilder();
        jsonBuilder.Append("[");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            jsonBuilder.Append("\"");
            jsonBuilder.Append(dt.Rows[i]["甲班"].ToString());
            jsonBuilder.Append("\"");
            jsonBuilder.Append(",");
        }
        jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
        jsonBuilder.Append("]");
        return jsonBuilder.ToString();
    }
    private string Stringyiban(DataTable dt)
    {
        StringBuilder jsonBuilder = new StringBuilder();
        jsonBuilder.Append("[");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            jsonBuilder.Append("\"");
            jsonBuilder.Append(dt.Rows[i]["乙班"].ToString());
            jsonBuilder.Append("\"");
            jsonBuilder.Append(",");
        }
        jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
        jsonBuilder.Append("]");
        return jsonBuilder.ToString();
    }
    private string Stringbingban(DataTable dt)
    {
        StringBuilder jsonBuilder = new StringBuilder();
        jsonBuilder.Append("[");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            jsonBuilder.Append("\"");
            jsonBuilder.Append(dt.Rows[i]["丙班"].ToString());
            jsonBuilder.Append("\"");
            jsonBuilder.Append(",");
        }
        jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
        jsonBuilder.Append("]");
        return jsonBuilder.ToString();
    }
    private string Stringjizhun(DataTable dt)
    {
        StringBuilder jsonBuilder = new StringBuilder();
        jsonBuilder.Append("[");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            jsonBuilder.Append("\"");
            jsonBuilder.Append(dt.Rows[i]["基准"].ToString());
            jsonBuilder.Append("\"");
            jsonBuilder.Append(",");
        }
        jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
        jsonBuilder.Append("]");
        return jsonBuilder.ToString();
    }




    //protected void btnSearch_Click( object sender , EventArgs e )
    //{
    //    bindList();
    //}
    //protected void btnExportSubmit_Click(object sender, EventArgs e)
    //{
    //    DataSet ds = getList();
    //    //huiw,2013-10-28添加，判断不为空时才导出excel
    //    if (ds == null || ds.Tables[0].Rows.Count == 0)
    //    {
    //        X.Msg.Alert("提示", "未查询出数据！").Show();
    //    }
    //    else
    //    {
    //        for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
    //        {
    //            bool isshow = false;
    //            DataColumn dc = ds.Tables[0].Columns[i];
    //            foreach (ColumnBase cb in this.GridPanelCenter.ColumnModel.Columns)
    //            {
    //                if ((cb.DataIndex != null) && (cb.DataIndex.ToUpper() == dc.ColumnName.ToUpper()))
    //                {
    //                    dc.ColumnName = cb.Text;
    //                    isshow = true;
    //                    break;
    //                }
    //            }
    //            if (!isshow)
    //            {
    //                ds.Tables[0].Columns.Remove(dc.ColumnName);
    //                i--;
    //            }
    //        }
    //        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(ds, "吨胶消耗日计量");
    //    }
    //}


    
}