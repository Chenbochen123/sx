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


public partial class Manager_Rubber_ShiftConfig : Mesnac.Web.UI.Page
{
    protected PptShiftConfigManager Manager = new PptShiftConfigManager();

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
        DateBeginTime.SelectedDate = DateTime.Now.AddDays(-15);
        DateEndTime.SelectedDate = DateTime.Now;
        bindList();
      //  InitControls();
    }
    // private void InitControls()
    //{
    //    BasWorkShopManager bBasWorkShopManager = new BasWorkShopManager();

    //    // 部门
    //    string sql = " select  * from  Pmt_material where Mkind_code <> 1";
    //    DataSet ds = bBasWorkShopManager.GetBySql(sql).ToDataSet();
    //    foreach (DataRow dr in ds.Tables[0].Rows)
    //    {
    //        cbxmater.Items.Add(new Ext.Net.ListItem { Text = dr[3].ToString(), Value = dr[0].ToString() });
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
        sb.AppendLine(@"select t.Barcode,t.Plan_date,t1.ShiftName,t2.ClassName,Mater_Name,Shelf_num,real_weight-used_weigh weight,shelf_num-used_num as leftnum, t3.WorkShop_Code,t.Mater_code
from ppt_shiftconfig t 
left join PptShift t1 on t1.ObjID=t.Shift_id 
left join PptClass t2 on t2.ObjID=t.Shift_class 
left join Pmt_equip t3 on t3.Equip_code = t.Equip_code  where  used_flag<2");
       // sb.AppendLine("WHERE 1=1");
        if (DateBeginTime.SelectedDate != DateTime.MinValue)
        {
            sb.AppendLine("AND plan_date>='" + DateBeginTime.SelectedDate.ToString("yyyy-MM-dd") + "'");
        }
        if (DateEndTime.SelectedDate != DateTime.MinValue)
        {
            sb.AppendLine("AND plan_date<='" + DateEndTime.SelectedDate.ToString("yyyy-MM-dd") + "'");
        }
        if (!string.IsNullOrEmpty(hiddenMaterialCode.Text))
        {
            sb.AppendLine("AND Mater_code ='" + hiddenMaterialCode.Text + "'");
        }
        //if (!string.IsNullOrEmpty(cboWorkshop.Text))
        //{
        //    sb.AppendLine("AND t3.WorkShop_Code='" + cboWorkshop.SelectedItem.Value + "'");
        //}
        if (cboType.SelectedItem.Value=="4")
        {
            sb.AppendLine("AND t.Mater_code like'" + "4%" + "'");
        }
        if (cboType.SelectedItem.Value == "5")
        {
            sb.AppendLine("AND t.Mater_code like'" + "5%" + "'");
        }
        if (cboType.SelectedItem.Value == "2")
        {
            sb.AppendLine("AND t.Mater_code like'" + "2%" + "'");
        }
      
        #endregion

        NBear.Data.CustomSqlSection css = Manager.GetBySql(sb.ToString());
        return css.ToDataSet();
    }
    private void bindList()
    {
        this.store.DataSource = getList();
        this.store.DataBind();
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
                foreach (ColumnBase cb in this.pnlList.ColumnModel.Columns)
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
            new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(ds, "未使用物料表");
        }
    }

    #endregion


    protected void StoreNow_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Rubber/StoreNow.aspx", true);
    }
}