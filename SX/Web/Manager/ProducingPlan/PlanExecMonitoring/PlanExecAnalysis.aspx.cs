﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using Mesnac.Business.Implements;
using Mesnac.Business.Interface;
using Mesnac.Entity;
using NBear.Common;
using Mesnac.Data.Components;
using System.Data;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
public partial class Manager_ProducingPlan_PlanExecMonitoring_PlanExecAnalysis : Mesnac.Web.UI.Page
{
    #region 属性注入
    private IPmtConfigManager pmtConfigManager = new PmtConfigManager();
    private IPptShiftConfigManager pptShiftConfigManager = new PptShiftConfigManager();
    private IPptShiftManager pptShiftManaer = new PptShiftManager();
    private IPptPlanManager pptPlanManager = new PptPlanManager();
    private IPptClassManager pptClassManager = new PptClassManager();
    private Ext.Net.MessageBox msg = new Ext.Net.MessageBox();//弹出框
    #endregion

    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查询 = new SysPageAction() { ActionID = 1, ActionName = "btnSearch" };
            导出 = new SysPageAction() { ActionID = 4, ActionName = "btnExcel" };
        }
        public SysPageAction 查询 { get; private set; } //必须为 public
        public SysPageAction 导出 { get; private set; } //必须为 public
    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest && !IsPostBack)
        {
            txtStratShiftDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtEndShiftDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            cbxChejian.Value = "all";
            FillShift();
        }
    }

    #region 页面初始化
    private void FillShift()
    {
        EntityArrayList<PptShift> lstShift = pptShiftManaer.GetListByWhere(PptShift._.UseFlag == 1);
        if (lstShift.Count >= 0)
        {
            foreach (PptShift shift in lstShift)
            {
                Ext.Net.ListItem item = new Ext.Net.ListItem();
                item.Text = shift.ShiftName;
                item.Value = shift.ObjID.ToString();
                cboShift.Items.Add(item);
            }
        }
        //EntityArrayList<PptClass> lstClass = pptClassManager.GetListByWhere(PptClass._.UseFlag==1);
        //if (lstClass.Count >= 0)
        //{
        //    foreach (PptClass classes in lstClass)
        //    {
        //        Ext.Net.ListItem item = new Ext.Net.ListItem();
        //        item.Text = classes.ClassName;
        //        item.Value = classes.ObjID.ToString();
        //        cboClass.Items.Add(item);
        //    }
        //}
    }
    #endregion

    #region 分页相关方法
    private PageResult<PptPlan> GetPageResultData(PageResult<PptPlan> pageParams)
    {
        PptPlanManager.QueryParams queryParams = new PptPlanManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.planStartDate = Convert.ToDateTime(txtStratShiftDate.Text).ToString("yyyy-MM-dd");
        queryParams.planEndDate = Convert.ToDateTime(txtEndShiftDate.Text).ToString("yyyy-MM-dd");
        if (this.txtEquipCode.Text != null)
        {
            queryParams.equipCode = this.hidden_select_equip_code.Text;
        }
        if (this.cboMaterName.SelectedItem.Value != null)
        {
            queryParams.materialCode = this.cboMaterName.SelectedItem.Value;
        }
        if (this.cboShift.SelectedItem.Value != null)
        {
            queryParams.shiftID = this.cboShift.SelectedItem.Value;
        }
        if (this.cbxclass.SelectedItem.Value != null)
        {
            queryParams.classID = this.cbxclass.SelectedItem.Value;
        }
        queryParams.workShopCode = cbxChejian.SelectedItem.Value;
        //if(this.cboClass.SelectedItem.Value!=null)
        //{
        //    queryParams.classID = this.cboClass.SelectedItem.Value;
        //}
        queryParams.deleteFlag = hidden_delete_flag.Text;
        return pptPlanManager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        if (this._.查询.SeqIdx == 0)
        {
            return null;
        }
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<PptPlan> pageParams = new PageResult<PptPlan>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "PlanDate ASC";

        PageResult<PptPlan> lst = GetPageResultData(pageParams);
        
        DataTable data = lst.DataSet.Tables[0];
        this.Session["ExportPlanAnalysis"] = lst;
        int total = lst.RecordCount;
        return new { data, total };
    }
    #endregion

    #region 打印
    /// <summary>
    /// 打印调用方法
    /// sunyj 2013年3月29日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExportSubmit_Click(object sender, EventArgs e)
    {
        if (this.Session["ExportPlanAnalysis"] == null)
        { 
            return;
        }
        PageResult<PptPlan> lst = this.Session["ExportPlanAnalysis"] as PageResult<PptPlan>;
        if (lst.DataSet.Tables[0].Rows.Count <= 0)
        {
            this.Session["ExportPlanAnalysis"] = null;
            return;
        }
        for (int i = 0; i < lst.DataSet.Tables[0].Columns.Count; i++)
        {
            bool isshow = false;
            DataColumn dc = lst.DataSet.Tables[0].Columns[i];
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
                lst.DataSet.Tables[0].Columns.Remove(dc.ColumnName);
                i--;
            }
        }
        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(lst.DataSet, "计划分析");
    }
    #endregion
    protected void MaterNameRefresh(object sender, StoreReadDataEventArgs e)
    {
        EntityArrayList<PptPlan> pptPlans = new EntityArrayList<PptPlan>();
        NBear.Common.WhereClip where = PptPlan._.PlanDate >= Convert.ToDateTime(this.txtStratShiftDate.Text).ToString("yyyy-MM-dd") & PptPlan._.PlanDate <= Convert.ToDateTime(this.txtEndShiftDate.Text).ToString("yyyy-MM-dd");// (this.cboEquipCode.SelectedItem.Value.Equals("-1") ? (PptPlan._.PlanDate ==this.txtStratShiftDate.Text) : (PptPlan._.RecipeEquipCode == this.cboEquipCode.SelectedItem.Value & PptPlan._.PlanDate== this.txtStratShiftDate.Text));
        if (!string.IsNullOrWhiteSpace(cboShift.Text))
        {
            where = where & PptPlan._.ShiftID == Convert.ToInt32(this.cboShift.SelectedItem.Value);
        }
        if (!string.IsNullOrWhiteSpace(txtEquipCode.Text))
        {
            where = where & PptPlan._.RecipeEquipCode == this.hidden_select_equip_code.Text;
        }
        OrderByClip order = new OrderByClip("RecipeMaterialName");
        pptPlans = pptPlanManager.GetListByWhereAndOrder(where, order);
        List<object> data = new List<object>();
        foreach (PptPlan plan in pptPlans.Distinct())
        {
            string RecipeMaterialCode = plan.RecipeMaterialCode;
            string RecipeMaterialName = plan.RecipeMaterialName;
            string PlanNo = plan.PlanID;
            data.Add(new { Id = RecipeMaterialCode, Name = RecipeMaterialName, PlanID = PlanNo });
        }
        this.MaterNameStore.DataSource = data;
        this.MaterNameStore.DataBind();
    }
}