using System;
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

public partial class Manager_ShopStorage_MaterialStorageAdjust : Mesnac.Web.UI.Page
{
    private PstMaterialChkManager manager = new PstMaterialChkManager();
    private BasFactoryInfoManager facManager = new BasFactoryInfoManager();
    private BasMaterialManager materManager = new BasMaterialManager();
    private PstMaterialChkDetailManager detailManager = new PstMaterialChkDetailManager();
    private BasUserManager userManager = new BasUserManager();
    private BasStorageManager basStorageManager = new BasStorageManager();

    protected PstStorageManager storageManager = new PstStorageManager();
    protected PstStorageDetailManager storageDetailManager = new PstStorageDetailManager();
    protected PstShopStorageManager shopStorageManager = new PstShopStorageManager();
    protected PstShopStorageDetailManager shopStorageDetailManager = new PstShopStorageDetailManager();
    protected PstMaterialStoreoutManager storeoutManager = new PstMaterialStoreoutManager();
    protected PstMaterialStoreoutDetailManager storeoutDetailManager = new PstMaterialStoreoutDetailManager();
    protected PstMaterialAdjustManager adjustManager = new PstMaterialAdjustManager();
    protected PstMaterialAdjustDetailManager adjustDetailManager = new PstMaterialAdjustDetailManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest && !IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["BillNO"]))
            {
                string billNo = Request.QueryString["BillNO"].ToString();
                string barcode = Request.QueryString["Barcode"].ToString();
                string orderID = Request.QueryString["OrderID"].ToString();

                txtBillNo.Text = billNo;
                this.storeDetail.DataSource = adjustDetailManager.GetByOtherBillNo(billNo, barcode, orderID);
                this.storeDetail.DataBind();
            }
            else
            {
                txtBeginTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtEndTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
                cbxChkResultFlag.SelectedItem.Value = "all";
            }
        }
    }

    #region 分页相关方法
    private PageResult<PstMaterialAdjust> GetPageResultData(PageResult<PstMaterialAdjust> pageParams)
    {
        PstMaterialAdjustManager.QueryParams queryParams = new PstMaterialAdjustManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.billNo = txtBillNo.Text;
        queryParams.beginDate = Convert.ToDateTime(txtBeginTime.Text);
        queryParams.endDate = Convert.ToDateTime(txtEndTime.Value);
        queryParams.hrCode = hiddenMakerPerson.Text;
        queryParams.storageType = "1";

        if (cbxChkResultFlag.SelectedItem.Value != "all")
            queryParams.chkResultFlag = cbxChkResultFlag.SelectedItem.Value;

        return adjustManager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<PstMaterialAdjust> pageParams = new PageResult<PstMaterialAdjust>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "AdjustDate DESC";

        PageResult<PstMaterialAdjust> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }

    protected void RowSelect(object sender, StoreReadDataEventArgs e)
    {
        string billNo = e.Parameters["BillNo"];

        this.storeDetail.DataSource = adjustDetailManager.GetByBillNo(billNo);
        this.storeDetail.DataBind();
    }
    #endregion

    #region 增删改查按钮激发的事件
    [Ext.Net.DirectMethod()]
    public string btnBatchLocked_Click()
    {
        string strBillNo = string.Empty;
        string billNo = string.Empty;
        EntityArrayList<PstMaterialAdjust> adjust = null;
        try
        {
            foreach (SelectedRow row in this.rowSelectMuti.SelectedRows)
            {
                strBillNo += "'" + row.RecordID + "', ";
                billNo = row.RecordID;

                adjust = adjustManager.GetListByWhere(PstMaterialAdjust._.BillNo == row.RecordID);
                if (adjust[0].ChkResultFlag == "1")
                    return "false";
            }

            //修改库存量并插入明细数据，首先判断库存数量是否满足出库的数量和重量要求
            string sourceStorageID = adjustManager.GetListByWhere(PstMaterialAdjust._.BillNo == billNo)[0].SourceStorage;
            string targetStorageID = adjustManager.GetListByWhere(PstMaterialAdjust._.BillNo == billNo)[0].TargetStorage;
            EntityArrayList<PstMaterialAdjustDetail> details = adjustDetailManager.GetListByWhere(PstMaterialAdjustDetail._.BillNo == billNo);
            for (int i = 0; i < details.Count; i++)
            {
                PstShopStorage storage = shopStorageManager.GetListByWhere(PstShopStorage._.Barcode == details[i].Barcode && PstShopStorage._.StorageID == sourceStorageID && PstShopStorage._.StoragePlaceID == details[i].SourceStoragePlace && PstShopStorage._.MaterCode == details[i].MaterCode)[0];
                if (storage.Num < details[i].AdjustNum || storage.RealWeight < details[i].AdjustWeight)
                {
                    return "false1";
                }
            }

            adjust[0].ChkResultFlag = "1";
            adjust[0].ChkPerson = this.UserID;
            adjust[0].ChkDate = DateTime.Now;
            adjustManager.Update(adjust[0]);

            for (int i = 0; i < details.Count; i++)
            {
                PstShopStorage storage = shopStorageManager.GetListByWhere(PstShopStorage._.Barcode == details[i].Barcode && PstShopStorage._.StorageID == sourceStorageID && PstShopStorage._.StoragePlaceID == details[i].SourceStoragePlace && PstShopStorage._.MaterCode == details[i].MaterCode)[0];

                if (storage.Num == details[i].AdjustNum)
                    storage.RealWeight = 0;
                else
                    storage.RealWeight = storage.RealWeight - details[i].AdjustWeight;
                storage.Num = storage.Num - details[i].AdjustNum;
                if (storage.Num != 0)
                    storage.PieceWeight = Convert.ToDecimal(String.Format("{0:N3}", storage.RealWeight / storage.Num));
                else
                    storage.PieceWeight = 0;
                shopStorageManager.Update(storage);

                BasStorage basStorage = basStorageManager.GetListByWhere(BasStorage._.StorageID == targetStorageID)[0];
                string newBarcode = string.Empty;
                if (materManager.IsMinorType(storage.MaterCode, "'天然胶'"))
                {
                    //newBarcode = details[i].Barcode.Substring(0, 18);
                    newBarcode = details[i].Barcode;
                }
                else
                    newBarcode = storage.Barcode;
                
                if (basStorage.IsVirtual != "1" && basStorage.StorageType == "0")
                {
    
                    //直接插入，如果是天然胶生成22位新条码，否则为源条码
                    EntityArrayList<PstStorage> storageTarget = storageManager.GetListByWhere(PstStorage._.Barcode == details[i].Barcode && PstStorage._.StorageID == targetStorageID && PstStorage._.StoragePlaceID == details[i].TargetStoragePlace && PstStorage._.MaterCode == details[i].MaterCode);
                    if (storageTarget.Count > 0)
                    {
                        storageTarget[0].Num = storageTarget[0].Num + details[i].AdjustNum;
                        //if (storage.Num == details[i].AdjustNum)
                        //    storageTarget[0].RealWeight = storage.RealWeight;
                        //else
                            storageTarget[0].RealWeight = storageTarget[0].RealWeight + details[i].AdjustWeight;
                        if (storageTarget[0].Num != 0)
                            storageTarget[0].PieceWeight = Convert.ToDecimal(String.Format("{0:N3}", storageTarget[0].RealWeight / storageTarget[0].Num));
                        else
                            storageTarget[0].PieceWeight = 0;
                        storageManager.Update(storageTarget[0]);
                    }
                    else
                    {
                        PstStorage store = new PstStorage();
                        store.Barcode = newBarcode;
                        store.StorageID = targetStorageID;
                        store.StoragePlaceID = details[i].TargetStoragePlace;
                        store.ProductNo = details[i].ProductNo;
                        store.MaterCode = details[i].MaterCode;
                        store.ProcDate = details[i].ProcDate;
                        store.Num = details[i].AdjustNum;
                        store.PieceWeight = details[i].PieceWeight;
                        store.RealWeight = details[i].AdjustWeight;
                        store.RecordDate = DateTime.Now;

                        storageManager.Insert(store);
                    }
                }
                else if (basStorage.IsVirtual != "1" && basStorage.StorageType == "0")
                {
                    EntityArrayList<PstStorage> storageTarget = storageManager.GetListByWhere(PstStorage._.Barcode == details[i].Barcode && PstStorage._.StorageID == targetStorageID && PstStorage._.StoragePlaceID == details[i].TargetStoragePlace && PstStorage._.MaterCode == details[i].MaterCode);
                    if (storageTarget.Count > 0)
                    {
                        storageTarget[0].Num = storageTarget[0].Num + details[i].AdjustNum;
                        if (storage.Num == details[i].AdjustNum)
                            storageTarget[0].RealWeight = storage.RealWeight;
                        else
                            storageTarget[0].RealWeight = storageTarget[0].RealWeight + details[i].AdjustWeight;
                        if (storageTarget[0].Num != 0)
                            storageTarget[0].PieceWeight = Convert.ToDecimal(String.Format("{0:N3}", storageTarget[0].RealWeight / storageTarget[0].Num));
                        else
                            storageTarget[0].PieceWeight = 0;
                        storageManager.Update(storageTarget[0]);
                    }
                    else
                    {
                        PstStorage store = new PstStorage();
                        store.Barcode = details[i].Barcode;
                        store.StorageID = targetStorageID;
                        store.StoragePlaceID = details[i].TargetStoragePlace;
                        store.ProductNo = details[i].ProductNo;
                        store.MaterCode = details[i].MaterCode;
                        store.ProcDate = details[i].ProcDate;
                        store.Num = details[i].AdjustNum;
                        store.PieceWeight = details[i].PieceWeight;
                        store.RealWeight = details[i].AdjustWeight;
                        store.RecordDate = DateTime.Now;
                        //store.Remark = this.userManager.UserID + "调拨入库";

                        storageManager.Insert(store);
                    }
                }

                PstStorageDetail detail = new PstStorageDetail();
                detail.Barcode = storage.Barcode;
                detail.OrderID = storageDetailManager.GetOrderID(detail.Barcode) + 1;
                detail.StorageID = storage.StorageID;
                detail.StoragePlaceID = storage.StoragePlaceID;
                detail.StoreInOut = "O";
                detail.RecordDate = DateTime.Now;
                detail.Num = details[i].AdjustNum;
                detail.PieceWeight = details[i].PieceWeight;
                if (storage.Num == details[i].AdjustNum)
                    detail.Weight = storage.RealWeight;
                else
                    //detail.Weight = storage.PieceWeight * details[i].AdjustNum;
                    detail.Weight = details[i].AdjustWeight;
                detail.InaccountDuration = string.Format("{0:yyyyMM}", adjust[0].AdjustDate);
                detail.InaccountDate = adjust[0].AdjustDate;
                detail.BillType = "1003";
                detail.SourceBillNo = details[i].BillNo;
                detail.SourceOrderID = details[i].OrderID;
                //detail.Remark = this.userManager.UserID + "调拨明细出库";

                storageDetailManager.Insert(detail);

                if (basStorage.IsVirtual != "1" && basStorage.StorageType == "1")
                {
                    PstShopStorageDetail detailIn = new PstShopStorageDetail();
                    detailIn.Barcode = newBarcode;
                    detailIn.OrderID = storageDetailManager.GetOrderID(detail.Barcode) + 1;
                    detailIn.StorageID = targetStorageID;
                    detailIn.StoragePlaceID = details[i].TargetStoragePlace;
                    detailIn.StoreInOut = "I";
                    detailIn.RecordDate = DateTime.Now;
                    detailIn.Num = details[i].AdjustNum;
                    detailIn.PieceWeight = details[i].PieceWeight;
                    if (storage.Num == details[i].AdjustNum)
                        detailIn.Weight = storage.RealWeight;
                    else
                        detailIn.Weight = details[i].AdjustWeight;
                    detailIn.InaccountDuration = string.Format("{0:yyyyMM}", adjust[0].AdjustDate);
                    detailIn.InaccountDate = adjust[0].AdjustDate;
                    detailIn.BillType = "1003";
                    detailIn.SourceBillNo = details[i].BillNo;
                    detailIn.SourceOrderID = details[i].OrderID;
                    detailIn.StorageType = details[i].TypeID;
                    detailIn.ShiftClassID = details[i].ShiftClassID;
                    detailIn.ShiftID = details[i].ShiftID;

                    shopStorageDetailManager.Insert(detailIn);
                }
                else if (basStorage.IsVirtual != "1" && basStorage.StorageType == "0")
                {
                    PstStorageDetail detailIn = new PstStorageDetail();
                    detailIn.Barcode = storage.Barcode;
                    detailIn.OrderID = storageDetailManager.GetOrderID(detail.Barcode) + 1;
                    detailIn.StorageID = targetStorageID;
                    detailIn.StoragePlaceID = details[i].TargetStoragePlace;
                    detailIn.StoreInOut = "I";
                    detailIn.RecordDate = DateTime.Now;
                    detailIn.Num = details[i].AdjustNum;
                    detailIn.PieceWeight = details[i].PieceWeight;
                    if (storage.Num == details[i].AdjustNum)
                        detailIn.Weight = storage.RealWeight;
                    else
                        //detailIn.Weight = storage.PieceWeight * details[i].AdjustNum;
                        detailIn.Weight = details[i].AdjustWeight;
                    detailIn.InaccountDuration = string.Format("{0:yyyyMM}", adjust[0].AdjustDate);
                    detailIn.InaccountDate = adjust[0].AdjustDate;
                    detailIn.BillType = "1003";
                    detailIn.SourceBillNo = details[i].BillNo;
                    detailIn.SourceOrderID = details[i].OrderID;
                    detailIn.StorageType = details[i].TypeID;
                    detailIn.ShiftClassID = details[i].ShiftClassID;
                    detailIn.ShiftID = details[i].ShiftID;
                    //detailIn.Remark = this.userManager.UserID + "调拨明细入库";

                    storageDetailManager.Insert(detailIn);
                }
            }

            pageToolBar.DoRefresh();
            return "调拨成功！";
        }
        catch (Exception ex)
        {
            return "调拨失败！" + ex.ToString();
        }
    }

    [Ext.Net.DirectMethod()]
    public string btnCancelLocked_Click()
    {
        string strBillNo = string.Empty;
        string billNo = string.Empty;
        EntityArrayList<PstMaterialAdjust> adjust = null;

        try
        {
            foreach (SelectedRow row in this.rowSelectMuti.SelectedRows)
            {
                strBillNo += "'" + row.RecordID + "', ";
                billNo = row.RecordID;

                adjust = adjustManager.GetListByWhere(PstMaterialAdjust._.BillNo == billNo);
                if (adjust[0].ChkResultFlag == "0")
                    return "false";
            }

            //修改库存量并插入明细数据，首先判断库存数量是否满足出库的数量和重量要求
            string sourceStorageID = adjustManager.GetListByWhere(PstMaterialAdjust._.BillNo == billNo)[0].SourceStorage;
            string targetStorageID = adjustManager.GetListByWhere(PstMaterialAdjust._.BillNo == billNo)[0].TargetStorage;
            BasStorage basStorage = basStorageManager.GetListByWhere(BasStorage._.StorageID == targetStorageID)[0];
            EntityArrayList<PstMaterialAdjustDetail> details = adjustDetailManager.GetListByWhere(PstMaterialAdjustDetail._.BillNo == billNo);
            if (basStorage.IsVirtual != "1" && basStorage.StorageType == "0")
            {
                for (int i = 0; i < details.Count; i++)
                {
                    PstStorage storage = storageManager.GetListByWhere(PstStorage._.Barcode == details[i].Barcode && PstStorage._.StorageID == targetStorageID && PstStorage._.StoragePlaceID == details[i].TargetStoragePlace && PstStorage._.MaterCode == details[i].MaterCode)[0];

                    if (storage.Num < details[i].AdjustNum || storage.RealWeight < details[i].AdjustWeight)
                    {
                        return "false1";
                    }
                }
            }
            else if (basStorage.IsVirtual != "1" && basStorage.StorageType == "1")
            {
                for (int i = 0; i < details.Count; i++)
                {
                    PstShopStorage storage = shopStorageManager.GetListByWhere(PstShopStorage._.Barcode == details[i].Barcode && PstShopStorage._.StorageID == sourceStorageID && PstShopStorage._.StoragePlaceID == details[i].TargetStoragePlace && PstShopStorage._.MaterCode == details[i].MaterCode)[0];

                    if (storage.Num < details[i].AdjustNum || storage.RealWeight < details[i].AdjustWeight)
                    {
                        return "false1";
                    }
                }
            }

            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required))
            {
                try
                {
                    adjust[0].ChkResultFlag = "0";
                    adjust[0].ChkPerson = this.UserID;
                    adjustManager.Update(adjust[0]);

                    for (int i = 0; i < details.Count; i++)
                    {
                        string shopBarcode = string.Empty;
                        PstShopStorage storage = shopStorageManager.GetListByWhere(PstShopStorage._.Barcode == details[i].Barcode && PstShopStorage._.StorageID == sourceStorageID && PstShopStorage._.StoragePlaceID == details[i].SourceStoragePlace && PstShopStorage._.MaterCode == details[i].MaterCode)[0];

                        storage.RealWeight = storage.RealWeight + details[i].AdjustWeight;
                        storage.Num = storage.Num + details[i].AdjustNum;
                        if (storage.Num != 0)
                            storage.PieceWeight = Convert.ToDecimal(String.Format("{0:N3}", storage.RealWeight / storage.Num));
                        else
                            storage.PieceWeight = 0;
                        shopStorageManager.Update(storage);

                        if (basStorage.IsVirtual != "1" && basStorage.StorageType == "1")
                        {
                            //EntityArrayList<PstShopStorage> storageTarget = shopStorageManager.GetListByWhere(PstShopStorage._.Barcode == details[i].Barcode && PstShopStorage._.StorageID == targetStorageID && PstShopStorage._.StoragePlaceID == details[i].TargetStoragePlace && PstShopStorage._.MaterCode == details[i].MaterCode);
                            PstShopStorage shopStorage = shopStorageManager.getPstShopStorage(details[i].Barcode, targetStorageID, details[i].TargetStoragePlace, details[i].BillNo, details[i].OrderID.ToString());
                            //if (!string.IsNullOrEmpty(storage.Barcode))
                            //{
                            //    shopStorage.Num = shopStorage.Num - details[i].AdjustNum;
                            //    shopStorage.RealWeight = shopStorage.RealWeight - details[i].AdjustWeight;
                            //    if (shopStorage.Num != 0)
                            //        shopStorage.PieceWeight = Convert.ToDecimal(String.Format("{0:N3}", shopStorage.RealWeight / shopStorage.Num));
                            //    else
                            //        shopStorage.PieceWeight = 0;
                            //    shopStorageManager.Update(shopStorage);
                            //}
                            shopBarcode = shopStorage.Barcode;
                            shopStorageManager.Delete(shopStorage);
                        }
                        else if (basStorage.IsVirtual != "1" && basStorage.StorageType == "0")
                        {
                            EntityArrayList<PstStorage> storageTarget = storageManager.GetListByWhere(PstStorage._.Barcode == details[i].Barcode && PstStorage._.StorageID == targetStorageID && PstStorage._.StoragePlaceID == details[i].TargetStoragePlace && PstStorage._.MaterCode == details[i].MaterCode);
                            if (storageTarget.Count > 0)
                            {
                                storageTarget[0].Num = storageTarget[0].Num - details[i].AdjustNum;
                                storageTarget[0].RealWeight = storageTarget[0].RealWeight - details[i].AdjustWeight;
                                if (storageTarget[0].Num != 0)
                                    storageTarget[0].PieceWeight = Convert.ToDecimal(String.Format("{0:N3}", storageTarget[0].RealWeight / storageTarget[0].Num));
                                else
                                    storageTarget[0].PieceWeight = 0;
                                storageManager.Update(storageTarget[0]);
                            }
                        }

                        PstStorageDetail detail = storageDetailManager.GetListByWhere(PstStorageDetail._.Barcode == storage.Barcode && PstStorageDetail._.StorageID == storage.StorageID && PstStorageDetail._.StoragePlaceID == storage.StoragePlaceID && PstStorageDetail._.SourceBillNo == details[i].BillNo && PstStorageDetail._.SourceOrderID == details[i].OrderID)[0];
                        storageDetailManager.Delete(detail);

                        if (basStorage.IsVirtual != "1" && basStorage.StorageType == "1")
                        {
                            PstShopStorageDetail detailIn = shopStorageDetailManager.GetListByWhere(PstShopStorageDetail._.Barcode == shopBarcode && PstShopStorageDetail._.StorageID == targetStorageID && PstShopStorageDetail._.StoragePlaceID == details[i].TargetStoragePlace && PstShopStorageDetail._.SourceBillNo == details[i].BillNo && PstShopStorageDetail._.SourceOrderID == details[i].OrderID)[0];
                            shopStorageDetailManager.Delete(detailIn);
                        }
                        else if (basStorage.IsVirtual != "1" && basStorage.StorageType == "0")
                        {
                            PstStorageDetail detailIn = storageDetailManager.GetListByWhere(PstStorageDetail._.Barcode == storage.Barcode && PstStorageDetail._.StorageID == targetStorageID && PstStorageDetail._.StoragePlaceID == details[i].TargetStoragePlace && PstStorageDetail._.SourceBillNo == details[i].BillNo && PstStorageDetail._.SourceOrderID == details[i].OrderID)[0];
                            storageDetailManager.Delete(detailIn);
                        }
                    }

                    pageToolBar.DoRefresh();

                    scope.Complete();
                    scope.Dispose();

                    return "撤销成功！";
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    return "撤销失败！";
                }
            }

            
        }
        catch(Exception ex)
        {
            return "撤销失败！";
        }
    }

    [Ext.Net.DirectMethod()]
    public string commandcolumn_direct_delete(string billNo)
    {
        try
        {
            PstMaterialAdjust store = adjustManager.GetById(billNo);
            store.DeleteFlag = "1";

            adjustManager.Update(store);
            pageToolBar.DoRefresh();
        }
        catch (Exception e)
        {
            return "删除失败：" + e;
        }
        return "删除成功";
    }

    [Ext.Net.DirectMethod()]
    public string commandcolumn_direct_filed(string billNo)
    {
        //try
        //{
        //    //判断是否可以归档，根据是该数据所对应的明细数据全部全部处理完毕，SendChkFlag标志为1
        //    EntityArrayList<PstMaterialChkDetail> list = detailManager.GetListByWhere(PstMaterialChkDetail._.BillNo == billNo && PstMaterialChkDetail._.SendChkFlag == null && PstMaterialChkDetail._.DeleteFlag == "0");
        //    if (list.Count > 0)
        //    {
        //        return "不能归档：该送检单没有处理完毕，请首先处理该数据对应的明细数据！";
        //    }

        //    PstMaterialChk store = manager.GetById(billNo);
        //    store.FiledFlag = "1";

        //    manager.Update(store);
        //    pageToolBar.DoRefresh();
        //}
        //catch (Exception e)
        //{
        //    return "操作失败：" + e;
        //}
        return "未处理";
    }
        public void txtBeginTime_change(object sender, EventArgs e)
    {
        pageToolBar.DoRefresh();
    }
    public void txtEndTime_change(object sender, EventArgs e)
    {
        pageToolBar.DoRefresh();
    }
    public void cbxChkResultFlag_change(object sender, EventArgs e)
    {
        pageToolBar.DoRefresh();
    }
  
    #endregion

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