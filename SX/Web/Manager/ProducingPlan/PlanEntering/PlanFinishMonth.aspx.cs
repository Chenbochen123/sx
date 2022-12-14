﻿using System;
using System.Collections.Generic;
using Ext.Net;
using Mesnac.Business.Implements;
using Mesnac.Business.Interface;
using Mesnac.Entity;
using NBear.Common;
using Mesnac.Data.Components;
using System.Data;
using Newtonsoft.Json;
using System.Globalization;
using System.Text;

public partial class Manager_ProducingPlan_PlanEntering_PlanFinishMonth : Mesnac.Web.UI.Page
{
    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查询 = new SysPageAction() { ActionID = 1, ActionName = "btnSearch" };
            导出 = new SysPageAction() { ActionID = 2, ActionName = "btnExport" };
        }
        public SysPageAction 查询 { get; private set; } //必须为 public
        public SysPageAction 导出 { get; private set; } //必须为 public
    }
    #endregion

    #region 属性注入
    private IPptShiftManager pptShiftManager = new PptShiftManager();

    private IJCZL_SubFacManager subFacManager = new JCZL_SubFacManager();

    private IPptPlanManager pptPlanManager = new PptPlanManager();
    #endregion


    #region 页面初始化
    /// <summary>
    /// 页面初始化
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    /// <remarks></remarks>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest && !IsPostBack)
        {
            txtBeginTime.Text = DateTime.Now.ToString("yyyy-MM-dd 00:00:00");
            txtEndTime.Text = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd 00:00:00");

            WhereClip where = new WhereClip();
            OrderByClip order = new OrderByClip();
            where = new WhereClip();
            order = new OrderByClip();
            txtDep.Items.Clear();
            foreach (JCZL_SubFac m in subFacManager.GetListByWhereAndOrder(where, order))
            {
                txtDep.Items.Add(new ListItem(m.Fac_Name, m.Dep_Code));
            }
            txtDep.Select(0);
        }
    }
    #endregion

    #region 查询显示右侧
    /// <summary>
    /// GridPanel数据绑定
    /// </summary>
    /// <param name="action">The action.</param>
    /// <param name="extraParams">The extra params.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic.Add("@plandate", Convert.ToDateTime(txtBeginTime.Text).ToShortDateString());
        dic.Add("@plandate2", Convert.ToDateTime(txtEndTime.Text).ToShortDateString());
        dic.Add("@depcode", txtDep.SelectedItem.Value);
        DataSet ds = pptPlanManager.GetDataSetByStoreProcedure("proc_getplanlist_month", dic);

        return new { data = ds.Tables[0], total = ds.Tables[0].Rows.Count };
    }

    /// <summary>
    /// 导出
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExportSubmit_Click(object sender, EventArgs e)
    {
        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic.Add("@plandate", Convert.ToDateTime(txtBeginTime.Text).ToShortDateString());
        dic.Add("@plandate2", Convert.ToDateTime(txtEndTime.Text).ToShortDateString());
        dic.Add("@depcode", txtDep.SelectedItem.Value);
        DataTable dt = pptPlanManager.GetDataSetByStoreProcedure("proc_getplanlist_month", dic).Tables[0];
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            bool isshow = false;
            DataColumn dc = dt.Columns[i];
            foreach (ColumnBase cb in this.gridPanel1.ColumnModel.Columns)
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
                dt.Columns.Remove(dc.ColumnName);
                i--;
            }
        }
        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(dt, "月计划完成信息");
    }

    #endregion
}