﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using Ext.Net;
using NBear.Common;
using Mesnac.Business.Implements;
using Mesnac.Entity;
using Mesnac.Data.Components;
using Mesnac.Business.Interface;
using Mesnac.Data.Implements;
using System.Text;
public partial class Manager_Rubber_RubberConsume : System.Web.UI.Page
{
    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查询 = new SysPageAction() { ActionID = 1, ActionName = "btnSearch" };
            //历史查询 = new SysPageAction() { ActionID = 2, ActionName = "btn_history_search" };
            //导出 = new SysPageAction() { ActionID = 3, ActionName = "btnExport" };
            //修改 = new SysPageAction() { ActionID = 4, ActionName = "Edit" };
            //删除 = new SysPageAction() { ActionID = 5, ActionName = "Delete" };
            //恢复 = new SysPageAction() { ActionID = 6, ActionName = "Recover" };
            //添加 = new SysPageAction() { ActionID = 7, ActionName = "btn_add" };
        }
        public SysPageAction 查询 { get; private set; } //必须为 public
        //public SysPageAction 历史查询 { get; private set; } //必须为 public
        //public SysPageAction 导出 { get; private set; } //必须为 public
        //public SysPageAction 修改 { get; private set; } //必须为 public
        //public SysPageAction 删除 { get; private set; } //必须为 public
        //public SysPageAction 恢复 { get; private set; } //必须为 public
        //public SysPageAction 添加 { get; private set; } //必须为 public
    }
    #endregion

    private BasMaterialManager materManager = new BasMaterialManager();
    private BasEquipManager equipManager = new BasEquipManager();
    private PpmRubConsumeManager ppmManager = new PpmRubConsumeManager();
    private BasUserManager userManager = new BasUserManager();
    private BasMaterialMinorTypeManager minorTypeManager = new BasMaterialMinorTypeManager();
    private PstStorageManager storageManager = new PstStorageManager();
    private PptShiftTimeManager shiftTimeManager = new PptShiftTimeManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest && !IsPostBack)
        {
            txtBeginTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtEndTime.Text = DateTime.Now.ToString("yyyy-MM-dd");

            //bindMaterType();
        }
    }

    #region 分页相关方法
    private PageResult<PpmRubConsume> GetPageResultData(PageResult<PpmRubConsume> pageParams)
    {
        PpmRubConsumeManager.QueryParams queryParams = new PpmRubConsumeManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.beginDate = Convert.ToDateTime(txtBeginTime.Text);
        queryParams.endDate = Convert.ToDateTime(txtEndTime.Text);
        queryParams.equipCode = hiddenEquipCode.Text;
        queryParams.materType = cboMaterType.SelectedItem.Value;
        queryParams.materCode = hiddenMaterCode.Text;
        queryParams.chejian = "";
        if (cbxShiftID.SelectedItem.Value != "all")
            queryParams.shiftID = cbxShiftID.SelectedItem.Value;
        return GetTablePageDataBySql(queryParams);
        //return ppmManager.GetTablePageDataBySql(queryParams);
    }
    public PageResult<PpmRubConsume> GetTablePageDataBySql(PpmRubConsumeManager.QueryParams queryParams)
    {
        PageResult<PpmRubConsume> pageParams = queryParams.pageParams;
        StringBuilder sqlstr = new StringBuilder();
        sqlstr.AppendLine(@"select Id, PlanDate, CostCode, E.MaterialName CostMaterName, MaterCode, B.MaterialName, A.EquipCode, C.EquipName, ShiftID, ShiftClassID, ConsumeQty, BalanceQty,
	                                ConsQty,SurPlus,HandFlag,ConsRate=(SurPlus/(case ConsumeQty when 0 then 999999 else ConsumeQty end))*100,
	                                Mater_Kind=substring(MaterCode,2,2), CASE  Left(A.CostCode,1) WHEN '4' THEN '母炼胶' WHEN '5' THEN '终炼胶' WHEN '6' THEN '返回胶' ELSE '其他' END as MinorTypeName, RecordDate
                                from PpmRubConsume A
                                left join BasMaterial B on A.MaterCode = B.MaterialCode
                                left join BasEquip C on A.EquipCode = C.EquipCode
                                left join BasMaterial E on A.CostCode = E.MaterialCode
                                where A.DeleteFlag = '0'  ");
        if (queryParams.beginDate != DateTime.MinValue)
            sqlstr.AppendLine(" AND A.PlanDate >= '" + queryParams.beginDate.ToString() + "'");
        if (queryParams.endDate != DateTime.MinValue)
            sqlstr.AppendLine(" AND A.PlanDate <= '" + queryParams.endDate.AddDays(1).ToString() + "'");
        if (!string.IsNullOrEmpty(queryParams.equipCode))
        {
            sqlstr.AppendLine(" AND A.EquipCode = '" + queryParams.equipCode + "'");
        }
        if (!string.IsNullOrEmpty(queryParams.materType))
        {
            if (queryParams.materType == "05")
            {
                sqlstr.AppendLine(" AND  Left(A.CostCode,1) not in ('4','5','6')");
            }
            else
            {
                sqlstr.AppendLine(" AND Left(A.CostCode,1) = '" + queryParams.materType + "'");
            }

        }
        if (!string.IsNullOrEmpty(ComboBox1.SelectedItem.Value))
        {   sqlstr.AppendLine(" AND Left(A.MaterCode,1) = '" + ComboBox1.SelectedItem.Value + "' ");};
        if (!string.IsNullOrEmpty(queryParams.chejian))
        {
            sqlstr.AppendLine(" AND c.WorkShopCode = '" + queryParams.chejian + "'");
        }

        if (!string.IsNullOrEmpty(queryParams.materCode))
        {
            sqlstr.AppendLine(" AND MaterCode = '" + queryParams.materCode + "'");
        }
        if (!string.IsNullOrEmpty(queryParams.shiftID))
        {
            sqlstr.AppendLine(" AND A.ShiftID = '" + queryParams.shiftID + "'");
        }
        pageParams.QueryStr = sqlstr.ToString();
        if (pageParams.PageSize < 0)
        {
            NBear.Data.CustomSqlSection css = ppmManager.GetBySql(sqlstr.ToString());
            pageParams.DataSet = css.ToDataSet();
            return pageParams;
        }
        else
        {
            return ppmManager.GetPageDataBySql(pageParams);
        }
    }
    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<PpmRubConsume> pageParams = new PageResult<PpmRubConsume>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "RecordDate DESC";

        PageResult<PpmRubConsume> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }



    #endregion

    #region 增删改查按钮激发的事件


    protected void btnExportSubmit_Click(object sender, EventArgs e)
    {
        PageResult<PpmRubConsume> pageParams = new PageResult<PpmRubConsume>();
        pageParams.PageSize = -100;
        PageResult<PpmRubConsume> lst = GetPageResultData(pageParams);
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
        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(lst.DataSet, "胶料消耗报表");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
       
        this.winModify.Close();
    }

    [Ext.Net.DirectMethod()]
    public void commandcolumn_direct_edit(string ShopoutID)
    {
        PpmRubConsume shopout = ppmManager.GetById(Convert.ToInt32(ShopoutID));
        ShopOutid2.Text = shopout.Id.ToString();
        txtBarcode2.Text = materManager.GetMaterName(shopout.Costcode);
        if (!string.IsNullOrEmpty(shopout.Matercode))
            txtMaterName2.Text = materManager.GetMaterName(shopout.Matercode);
        if (shopout.Shiftid.ToString() == "1")
        {
            cbxShiftID2.Text = "早";
            cbxShiftID2.Value = "1";
        }
        else if (shopout.Shiftid.ToString() == "2")
        {
            cbxShiftID2.Text = "中";
            cbxShiftID2.Value = "2";
        }
        else if (shopout.Shiftid.ToString() == "3")
        {
            cbxShiftID2.Text = "夜";
            cbxShiftID2.Value = "3";
        }
        if (shopout.ShiftClassId.ToString() == "1")
        {
            cbxShiftID2.Text = "甲";
            cbxShiftID2.Value = "1";
        }
        else if (shopout.ShiftClassId.ToString() == "2")
        {
            cbxShiftID2.Text = "乙";
            cbxShiftID2.Value = "2";
        }
        else if (shopout.ShiftClassId.ToString() == "3")
        {
            cbxShiftID2.Text = "丙";
            cbxShiftID2.Value = "3";
        }
        txtEquipName2.Text = equipManager.GetListByWhere(BasEquip._.EquipCode == shopout.Equipcode)[0].EquipName;
        hiddenEquipCode.Text = shopout.Equipcode;
       
        txtRealWeight2.Text = shopout.consumeqty.ToString();

        this.winModify.Show();
    }

    protected void btnModify_Click(object sender, EventArgs e)
    {
        PpmRubConsume shopout = ppmManager.GetById(Convert.ToInt32(ShopOutid2.Text));
        shopout.Shiftid = Convert.ToInt16(cbxShiftID2.SelectedItem.Value);
        shopout.Equipcode = hiddenEquipCode.Text;
        shopout.consumeqty = Convert.ToDecimal(txtRealWeight2.Text);
        shopout.Balanceqty = Convert.ToDecimal(txtRealWeight2.Text);
        shopout.Consqty = Convert.ToDecimal(txtRealWeight2.Text);
        shopout.HandFlag = "1";

        ppmManager.Update(shopout);
        hiddenEquipCode.Text = string.Empty;
        hiddenMaterCode.Text = string.Empty;
        pageToolBar.DoRefresh();
        this.winModify.Close();
        X.MessageBox.Alert("操作", "更新成功").Show();
    }

    [Ext.Net.DirectMethod()]
    public string commandcolumn_direct_delete(string ShopoutID)
    {
        try
        {
            PpmRubConsume shopout = ppmManager.GetById(ShopoutID);
            shopout.Deleteflag = "1";
            ppmManager.Update(shopout);

            pageToolBar.DoRefresh();

            return "删除成功";
        }
        catch (Exception e)
        {
            return "删除失败：" + e;
        }
    }
    #endregion

  
}