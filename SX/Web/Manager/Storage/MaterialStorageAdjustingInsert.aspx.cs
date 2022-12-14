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

public partial class Manager_Storage_MaterialStorageAdjustingInsert : Mesnac.Web.UI.Page
{
    protected BasUserManager userManager = new BasUserManager();
    protected BasDeptManager deptManager = new BasDeptManager();
    protected BasMaterialManager materialManager = new BasMaterialManager();
    protected BasStorageManager basStorageManager = new BasStorageManager();
    protected BasStoragePlaceManager basStoragePlaceManager = new BasStoragePlaceManager();
    protected PstStorageManager storageManager = new PstStorageManager();
    protected PstStorageDetailManager storageDetailManager = new PstStorageDetailManager();
    protected PstMaterialStoreoutManager storeOutManager = new PstMaterialStoreoutManager();
    protected PstMaterialStoreoutDetailManager storeOutDetailManager = new PstMaterialStoreoutDetailManager();
    protected PstMaterialAdjustingManager adjustingManager = new PstMaterialAdjustingManager();
    protected PstMaterialAdjustingDetailManager adjustingDetailManager = new PstMaterialAdjustingDetailManager();
    protected PstMaterialInventoryManager inventoryManager = new PstMaterialInventoryManager();
    protected PstMaterialInventoryDetailManager inventoryDetailManager = new PstMaterialInventoryDetailManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest && !IsPostBack)
        {
            txtAdjustingDate1.Text = Convert.ToString(DateTime.Now);
            txtAdjustingDate1.MinDate = DateTime.Now;
            txtMakerPerson1.Text = userManager.GetListByWhere(BasUser._.WorkBarcode == this.UserID)[0].UserName;
            if (!string.IsNullOrEmpty(Request.QueryString["BillNo"]))
            {
                BindData(Request.QueryString["BillNo"].ToString());
            }
            else
            {
                hiddenBillNo.Text = Guid.NewGuid().ToString();
            }
        }
    }

    #region 分页相关方法

    private PageResult<PstMaterialAdjustingDetail> GetPageResultData1(PageResult<PstMaterialAdjustingDetail> pageParams)
    {
        PstMaterialAdjustingDetailManager.QueryParams queryParams = new PstMaterialAdjustingDetailManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.billNo = hiddenBillNo.Text;

        return adjustingDetailManager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindDataDetail(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<PstMaterialAdjustingDetail> pageParams = new PageResult<PstMaterialAdjustingDetail>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "RecordDate DESC";

        PageResult<PstMaterialAdjustingDetail> lst = GetPageResultData1(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }

    private PageResult<PstMaterialInventory> GetPageResultData(PageResult<PstMaterialInventory> pageParams)
    {
        PstMaterialInventoryManager.QueryParams queryParams = new PstMaterialInventoryManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.billNo = txtBillNo.Text;
        queryParams.storageID = hiddenStorageID.Text;
        if (!string.IsNullOrEmpty(txtBeginTime.Text))
            queryParams.beginDate = Convert.ToDateTime(txtBeginTime.Text);
        if (!string.IsNullOrEmpty(txtEndTime.Text))
            queryParams.endDate = Convert.ToDateTime(txtEndTime.Text);

        return inventoryManager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<PstMaterialInventory> pageParams = new PageResult<PstMaterialInventory>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "InventoryDate desc";

        PageResult<PstMaterialInventory> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }

    public void BindData(string billNo)
    {
        PstMaterialAdjusting adjusting = adjustingManager.GetById(Request.QueryString["BillNo"].ToString());
        txtBillNo1.Text = adjusting.BillNo;
        hiddenBillNo.Text = adjusting.BillNo;
        txtInventoryNo1.Text = adjusting.InventoryNo;
        txtAdjustingDate1.Text = adjusting.AdjustingDate.ToString();
        txtStorageName1.Text = basStorageManager.GetStorageName(adjusting.StorageID);
        hiddenStorageID.Text = adjusting.StorageID;
        txtRemark1.Text = adjusting.Remark;
        GenAdjusting.Disabled = false;
        EntityArrayList<PstMaterialStoreoutDetail> storeoutDetail = storeOutDetailManager.GetListByWhere(PstMaterialStoreoutDetail._.BillNo == billNo);

        this.store1.DataSource = storeoutDetail;
        this.store1.DataBind();
    }

    #endregion

    protected void btnInport_Click(object sender, EventArgs e)
    {
        this.winAdd.Show();
    }

    public void btnCancel_Click(object sender, DirectEventArgs e)
    {
        this.winAdd.Close();
        this.winModifyDetail.Close();
    }

    [Ext.Net.DirectMethod()]
    public void btnAddSave_Click(string billNo)
    {
        PstMaterialInventory inventory = inventoryManager.GetById(billNo);
        EntityArrayList<PstMaterialInventoryDetail> inventoryDetails = inventoryDetailManager.GetListByWhere(PstMaterialInventoryDetail._.BillNo == billNo);

        //给调整单主信息赋值
        txtStorageName1.Text = basStorageManager.GetStorageName(inventory.StorageID);
        hiddenStorageID.Text = inventory.StorageID;
        txtInventoryNo1.Text = inventory.BillNo;

        //将盘点单明细数据中盘盈和盘亏的数据插入调整单明细信息中
        for (int i = 0; i < inventoryDetails.Count; i++)
        {
            if (inventoryDetails[i].ProfitLossFlag != "0")
            {
                PstMaterialAdjustingDetail adjustingDetail = new PstMaterialAdjustingDetail();
                adjustingDetail.BillNo = hiddenBillNo.Text;
                adjustingDetail.Barcode = inventoryDetails[i].Barcode;
                adjustingDetail.OrderID = i + 1;
                adjustingDetail.StorageID = inventoryDetails[i].StorageID;
                adjustingDetail.StoragePlaceID = inventoryDetails[i].StoragePlaceID;
                adjustingDetail.RecordDate = DateTime.Now;
                adjustingDetail.MaterCode = inventoryDetails[i].MaterCode;
                adjustingDetail.DecreaseOrAddFlag = inventoryDetails[i].ProfitLossFlag;
                adjustingDetail.AdjustingNum = inventoryDetails[i].DiffNum;
                adjustingDetail.PieceWeight = inventoryDetails[i].PieceWeight;
                adjustingDetail.AdjustingWeight = inventoryDetails[i].DiffWeight;

                adjustingDetailManager.Insert(adjustingDetail);
            }
        }

        PagingToolbar2.DoRefresh();
        this.winAdd.Close();
    }

    [Ext.Net.DirectMethod()]
    public void DeleteStorage(string barcode, string orderID)
    {
        PstMaterialAdjustingDetail detail = adjustingDetailManager.GetListByWhere(PstMaterialAdjustingDetail._.BillNo == hiddenBillNo.Text && PstMaterialAdjustingDetail._.Barcode == barcode && PstMaterialAdjustingDetail._.OrderID == orderID)[0];

        adjustingDetailManager.Delete(detail);
        PagingToolbar2.DoRefresh();
    }

    [Ext.Net.DirectMethod()]
    public string btnSave_Click()
    {
        EntityArrayList<PstMaterialAdjustingDetail> adjustingDetailCounts = adjustingDetailManager.GetListByWhere(PstMaterialAdjustingDetail._.BillNo == hiddenBillNo.Text);
        if (adjustingDetailCounts.Count == 0)
        {
            return "false";
        }

        if (!string.IsNullOrEmpty(Request.QueryString["BillNo"]))
        {
            PstMaterialAdjusting adjusting = adjustingManager.GetListByWhere(PstMaterialAdjusting._.BillNo == Request.QueryString["BillNo"].ToString())[0];
            if (Convert.ToDateTime(txtAdjustingDate1.Text).ToShortDateString() == DateTime.Now.ToShortDateString())
                adjusting.AdjustingDate = DateTime.Now;
            else
                adjusting.AdjustingDate = Convert.ToDateTime(txtAdjustingDate1.Text);
            adjusting.MakerPerson = this.UserID;
            adjusting.Remark = txtRemark1.Text;

            adjustingManager.Update(adjusting);
        }
        else
        {
            PstMaterialAdjusting adjusting = new PstMaterialAdjusting();
            if (!string.IsNullOrEmpty(Request.QueryString["BillNo"]))
                adjusting.BillNo = hiddenBillNo.Text;
            else
                adjusting.BillNo = adjustingManager.GetBillNo();
            adjusting.InventoryNo = txtInventoryNo1.Text;
            adjusting.StorageID = hiddenStorageID.Text;
            if (Convert.ToDateTime(txtAdjustingDate1.Text).ToShortDateString() == DateTime.Now.ToShortDateString())
                adjusting.AdjustingDate = DateTime.Now;
            else
                adjusting.AdjustingDate = Convert.ToDateTime(txtAdjustingDate1.Text);
            adjusting.InaccountDuration = string.Format("{0:yyyyMM}", adjusting.AdjustingDate);
            adjusting.ChkResultFlag = "0";
            adjusting.MakerPerson = this.UserID;
            adjusting.DeleteFlag = "0";
            adjusting.Remark = userManager.UserID + "添加";
            adjustingManager.Insert(adjusting);

            adjustingDetailManager.Update(new PropertyItem[] { PstMaterialAdjustingDetail._.BillNo }, new object[] { adjusting.BillNo }, PstMaterialAdjustingDetail._.BillNo == adjustingDetailCounts[0].BillNo);
        }

        txtStorageName1.Text = string.Empty;
        txtInventoryNo1.Text = string.Empty;
        txtAdjustingDate1.Text = DateTime.Now.ToString();
        txtRemark1.Text = string.Empty;

        store1.Data = null;
        store1.DataBind();

        return "执行成功";
    }

    [Ext.Net.DirectMethod()]
    public void commandcolumndetail_direct_edit(string barcode, string orderID)
    {
        PstMaterialAdjustingDetail adjustingDetail = adjustingDetailManager.GetListByWhere(PstMaterialAdjustingDetail._.BillNo == hiddenBillNo.Text && PstMaterialAdjustingDetail._.Barcode == barcode && PstMaterialAdjustingDetail._.OrderID == orderID)[0];

        txtBarcode2.Text = adjustingDetail.Barcode;
        hiddenOrderID.Text = orderID;
        //txtProductNo2.Text = storeoutDetail.ProductNo;
        txtStoragePlaceName2.Text = basStoragePlaceManager.GetListByWhere(BasStoragePlace._.StoragePlaceID == adjustingDetail.StoragePlaceID)[0].StoragePlaceName;
        hiddenStoragePlaceID.Text = adjustingDetail.StoragePlaceID;
        txtMaterialName2.Text = materialManager.GetMaterName(adjustingDetail.MaterCode);
        hiddenMaterCode.Text = adjustingDetail.MaterCode;
        ckxDecreaseOrAdd2.SelectedItem.Value = adjustingDetail.DecreaseOrAddFlag;
        txtAdjustingNum2.Text = adjustingDetail.AdjustingNum.ToString();
        txtAdjustingWeight2.Text = adjustingDetail.AdjustingWeight.ToString();

        this.winModifyDetail.Show();
    }

    protected void btnModifyDetailSave_Click(object sender, EventArgs e)
    {
        try
        {
            PstMaterialAdjustingDetail adjustingDetail = adjustingDetailManager.GetListByWhere(PstMaterialAdjustingDetail._.BillNo == hiddenBillNo.Text && PstMaterialAdjustingDetail._.Barcode == txtBarcode2.Text && PstMaterialAdjustingDetail._.OrderID == hiddenOrderID.Text)[0];

            adjustingDetail.DecreaseOrAddFlag = ckxDecreaseOrAdd2.SelectedItem.Value;
            adjustingDetail.AdjustingNum = Convert.ToInt32(txtAdjustingNum2.Text);
            adjustingDetail.AdjustingWeight = Convert.ToDecimal(txtAdjustingWeight2.Text);

            adjustingDetailManager.Update(adjustingDetail);
            PagingToolbar2.DoRefresh();
            this.winModifyDetail.Close();
        }
        catch (Exception ex)
        {
            X.MessageBox.Alert("操作", "更新失败：" + ex).Show();
        }
    }

    #region 校验方法
    protected void CheckField(object sender, RemoteValidationEventArgs e)
    {
        TextField field = (TextField)sender;

        if (field.Text != "")
        {
            e.Success = true;
        }
        else
        {
            e.Success = false;
            e.ErrorMessage = "此属性必须填写！";
        }
    }

    protected void CheckCombo(object sender, RemoteValidationEventArgs e)
    {
        ComboBox combo = (ComboBox)sender;

        if (combo.SelectedItem.Value != "")
        {
            e.Success = true;
        }
        else
        {
            e.Success = false;
            e.ErrorMessage = "此属性必须选择！";
        }
    }
    #endregion
}