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
using System.Text;

public partial class Manager_Storage_MaterialStorageAdjust : Mesnac.Web.UI.Page
{
    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查询 = new SysPageAction() { ActionID = 1, ActionName = "btnSearch" };
            //审核 = new SysPageAction() { ActionID = 2, ActionName = "btnLock" };
            //反审核 = new SysPageAction() { ActionID = 3, ActionName = "btnUnLock" };
            //导出 = new SysPageAction() { ActionID = 4, ActionName = "btnExport" };
            //添加 = new SysPageAction() { ActionID = 5, ActionName = "btnAdd" };
        }
        public SysPageAction 查询 { get; private set; } //必须为 public
        //public SysPageAction 审核 { get; private set; } //必须为 public
        //public SysPageAction 反审核 { get; private set; } //必须为 public
        //public SysPageAction 导出 { get; private set; } //必须为 public
        //public SysPageAction 添加 { get; private set; } //必须为 public
    }
    #endregion

    private PstMaterialChkManager manager = new PstMaterialChkManager();
    private BasFactoryInfoManager facManager = new BasFactoryInfoManager();
    private BasMaterialManager materManager = new BasMaterialManager();
    private PstMaterialChkDetailManager detailManager = new PstMaterialChkDetailManager();
    private BasUserManager userManager = new BasUserManager();
    private BasStorageManager basStorageManager = new BasStorageManager();

    private PstStorageManager storageManager = new PstStorageManager();
    private PstStorageDetailManager storageDetailManager = new PstStorageDetailManager();
    private PstShopStorageManager shopStorageManager = new PstShopStorageManager();
    private PstShopStorageDetailManager shopStorageDetailManager = new PstShopStorageDetailManager();
    private PstMaterialStoreoutManager storeoutManager = new PstMaterialStoreoutManager();
    private PstMaterialStoreoutDetailManager storeoutDetailManager = new PstMaterialStoreoutDetailManager();
    private PstMaterialAdjustManager adjustManager = new PstMaterialAdjustManager();
    private PstMaterialAdjustDetailManager adjustDetailManager = new PstMaterialAdjustDetailManager();
    private SysUserCtrlManager userCtrlManager = new SysUserCtrlManager();

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
                //this.storeDetail.DataSource = adjustDetailManager.GetByOtherBillNo(billNo, barcode, orderID);
                //this.storeDetail.DataBind();
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
        queryParams.storageType = "0";

        if (cbxChkResultFlag.SelectedItem.Value != "all")
            queryParams.chkResultFlag = cbxChkResultFlag.SelectedItem.Value;

        return GetTablePageDataBySql(queryParams);
    }
    public PageResult<PstMaterialAdjust> GetTablePageDataBySql(PstMaterialAdjustManager.QueryParams queryParams)
    {
        PageResult<PstMaterialAdjust> pageParams = queryParams.pageParams;
        StringBuilder sqlstr = new StringBuilder();
        sqlstr.AppendLine(@"select  P.Barcode, P.ProductNo,  A.BillNo, A.AdjustDate, A.SourceStorage , B.StorageName SourceStorageName, 
	                                A.TargetStorage, C.StorageName TargetStorageName, A.MakerPerson, D.UserName,
 PB.StoragePlaceName SourceStoragePlaceName,PD.MaterialName   , P.ProcDate,
	                                    P.AdjustNum, P.PieceWeight, P.AdjustWeight,
	                                PC.StoragePlaceName TargetStoragePlaceName,
	                                A.ChkResultFlag, A.Remark
                                from PstMaterialAdjustDetail P 
                                         left join     PstMaterialAdjust A on P.billNo=A.billNo
                                left join BasStorage B on A.SourceStorage = B.StorageID
                                left join BasStorage C on A.TargetStorage = C.StorageID
                                left join BasUser D on A.MakerPerson = D.WorkBarcode

 left join BasStoragePlace PB on P.SourceStoragePlace = PB.StoragePlaceID
                                    left join BasStoragePlace PC on P.TargetStoragePlace = PC.StoragePlaceID
                                    left join BasMaterial PD on P.MaterCode = PD.MaterialCode

                                where A.DeleteFlag = '0'");
        if (!string.IsNullOrEmpty(queryParams.storageType))
        {
            sqlstr.AppendLine(" AND B.StorageType = '" + queryParams.storageType + "'");
        }
        if (!string.IsNullOrEmpty(queryParams.billNo))
        {
            sqlstr.AppendLine(" AND A.BillNo = '" + queryParams.billNo + "'");
        }
        if (queryParams.beginDate != DateTime.MinValue)
            sqlstr.AppendLine(" AND A.AdjustDate >= '" + queryParams.beginDate.ToString() + "'");
        if (queryParams.endDate != DateTime.MinValue)
            sqlstr.AppendLine(" AND A.AdjustDate <= '" + queryParams.endDate.AddDays(1).ToString() + "'");
        if (!string.IsNullOrEmpty(queryParams.chkResultFlag))
        {
            sqlstr.AppendLine(" AND A.ChkResultFlag = '" + queryParams.chkResultFlag + "'");
        }
        if (!string.IsNullOrEmpty(queryParams.hrCode))
        {
            sqlstr.AppendLine(" AND D.HRCode = '" + queryParams.hrCode + "'");
        }
//        if ((!string.IsNullOrEmpty(TextBarcode.Text)) || (!string.IsNullOrEmpty(TextMaterial.Text)))
//        { String sql = @" and A.BillNo in (select BillNo from PstMaterialAdjustdetail left join basmaterial on  PstMaterialAdjustdetail.matercode = basmaterial.materialcode
// where barcode like '%"
//            + TextBarcode.Text + "%' and  basmaterial.materialname like '%" + TextMaterial.Text + "%')";
//        sqlstr.AppendLine(sql);
//        }

        if (!string.IsNullOrEmpty(TextBarcode.Text))
        {
            sqlstr.AppendLine(" AND  P.barcode  like '%" + TextBarcode.Text + "%'");
        }
        if (!string.IsNullOrEmpty(TextMaterial.Text))
        {
            sqlstr.AppendLine(" AND PD.MaterialName  like '%" + TextMaterial.Text + "%'");
        }


        sqlstr.AppendLine(" AND  B.StorageName  like '%" + TextField1.Text + "%'");
        pageParams.QueryStr = sqlstr.ToString();
        //pageParams.PageSize = 200;

        //if (pageParams.PageSize == 10) pageParams.PageSize = 15;
        if (pageParams.PageSize < 0)
        {
            NBear.Data.CustomSqlSection css = adjustManager.GetBySql(sqlstr.ToString());
            Session["DSAdjust"] = css.ToDataSet();
            pageParams.DataSet = css.ToDataSet();
            return pageParams;
        }
        else
        {
            return adjustManager.GetPageDataBySql(pageParams);
        }
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

        //this.storeDetail.DataSource = GetByBillNo(billNo);
        //this.storeDetail.DataBind();
    }

    public DataSet GetByBillNo(string BillNo)
    {
        return adjustDetailManager.GetBySql(@"select A.BillNo, A.Barcode, A.ProductNo, A.SourceStoragePlace, B.StoragePlaceName SourceStoragePlaceName,
	                                    A.TargetStoragePlace, C.StoragePlaceName TargetStoragePlaceName, A.MaterCode, D.MaterialName, A.ProcDate,
	                                    A.AdjustNum, A.PieceWeight, A.AdjustWeight, A.Remark
                                    from PstMaterialAdjustDetail A
                                    left join BasStoragePlace B on A.SourceStoragePlace = B.StoragePlaceID
                                    left join BasStoragePlace C on A.TargetStoragePlace = C.StoragePlaceID
                                    left join BasMaterial D on A.MaterCode = D.MaterialCode
                                    where A.DeleteFlag = '0'
                                    and BillNo = '" + BillNo + "' and D.MaterialName like '%" + TextMaterial.Text + "%'").ToDataSet();
    }
    #endregion

    #region 增删改查按钮激发的事件
    [Ext.Net.DirectMethod()]
    public string btnBatchLocked_Click()
    {
        string strBillNo = string.Empty;
        string billNo = string.Empty;
        string sourceStorageID = string.Empty;
        string targetStorageID = string.Empty;
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
            sourceStorageID = adjustManager.GetListByWhere(PstMaterialAdjust._.BillNo == billNo)[0].SourceStorage;
            targetStorageID = adjustManager.GetListByWhere(PstMaterialAdjust._.BillNo == billNo)[0].TargetStorage;
            EntityArrayList<PstMaterialAdjustDetail> details = adjustDetailManager.GetListByWhere(PstMaterialAdjustDetail._.BillNo == billNo);

            for (int i = 0; i < details.Count; i++)
            {
                PstStorage storage = storageManager.GetListByWhere(PstStorage._.Barcode == details[i].Barcode && PstStorage._.StorageID == sourceStorageID && PstStorage._.StoragePlaceID == details[i].SourceStoragePlace && PstStorage._.MaterCode == details[i].MaterCode)[0];
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
                PstStorage storage = storageManager.GetListByWhere(PstStorage._.Barcode == details[i].Barcode && PstStorage._.StorageID == sourceStorageID && PstStorage._.StoragePlaceID == details[i].SourceStoragePlace && PstStorage._.MaterCode == details[i].MaterCode)[0];

                if (storage.Num == details[i].AdjustNum)
                    storage.RealWeight = 0;
                else
                    storage.RealWeight = storage.RealWeight - details[i].AdjustWeight;
                storage.Num = storage.Num - details[i].AdjustNum;
                if (storage.Num != 0)
                    storage.PieceWeight = Convert.ToDecimal(String.Format("{0:N3}", storage.RealWeight / storage.Num));
                else
                    storage.PieceWeight = 0;
                storageManager.Update(storage);

                BasStorage basStorage = basStorageManager.GetListByWhere(BasStorage._.StorageID == targetStorageID)[0];
                string newBarcode = string.Empty;
                //if (materManager.IsMinorType(storage.MaterCode, "'天然胶'"))
                //    newBarcode = shopStorageManager.GetNewBarcode(details[i].Barcode, targetStorageID, details[i].TargetStoragePlace);
                //else
                newBarcode = storage.Barcode;
                
                if (basStorage.IsVirtual != "1" && basStorage.StorageType == "1")
                {
                    //直接插入，如果是天然胶生成22位新条码，否则为源条码
                    EntityArrayList<PstShopStorage> storageTarget = shopStorageManager.GetListByWhere(PstShopStorage._.Barcode == details[i].Barcode && PstShopStorage._.StorageID == targetStorageID && PstShopStorage._.StoragePlaceID == details[i].TargetStoragePlace && PstShopStorage._.MaterCode == details[i].MaterCode);
                    if (storageTarget.Count > 0)
                    {
                        storageTarget[0].Num = storageTarget[0].Num + details[i].AdjustNum;
                        if (storage.Num == details[i].AdjustNum)
                            storageTarget[0].RealWeight = storage.RealWeight;
                        else
                            storageTarget[0].RealWeight = storageTarget[0].RealWeight + details[i].AdjustWeight;
                        storageTarget[0].PieceWeight = details[i].PieceWeight;//Convert.ToDecimal(String.Format("{0:N3}", storageTarget[0].RealWeight / storageTarget[0].Num));
                        shopStorageManager.Update(storageTarget[0]);
                    }
                    else
                    {
                        PstShopStorage store = new PstShopStorage();
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

                        shopStorageManager.Insert(store);
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
                    detailIn.OrderID = shopStorageDetailManager.GetOrderID(detail.Barcode) + 1;
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

            if (userCtrlManager.GetItemCtrl("SapInterfaceCtrl") == "1")
            {
                try
                {
                    if (!adjustManager.IsSameStorageType(sourceStorageID, targetStorageID))
                    {
                        WebReference.Service ws = new WebReference.Service();
                        ws.Url = userCtrlManager.GetItemCtrl("WebServiceIP");
                        ws.ToSapAdjustInfo(billNo, txtSAPBillNo.Text, "0", "0");
                    }
                }
                catch
                { }
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
        string sourceStorageID = string.Empty;
        string targetStorageID = string.Empty;
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
            sourceStorageID = adjustManager.GetListByWhere(PstMaterialAdjust._.BillNo == billNo)[0].SourceStorage;
            targetStorageID = adjustManager.GetListByWhere(PstMaterialAdjust._.BillNo == billNo)[0].TargetStorage;
            BasStorage basStorage = basStorageManager.GetListByWhere(BasStorage._.StorageID == targetStorageID)[0];
            EntityArrayList<PstMaterialAdjustDetail> details = adjustDetailManager.GetListByWhere(PstMaterialAdjustDetail._.BillNo == billNo);
            if (basStorage.IsVirtual != "1" && basStorage.StorageType == "1")
            {
                for (int i = 0; i < details.Count; i++)
                {
                    PstShopStorage storage = shopStorageManager.getPstShopStorage(details[i].Barcode, targetStorageID, details[i].TargetStoragePlace, details[i].BillNo, details[i].OrderID.ToString());

                    if (storage.Num < details[i].AdjustNum || storage.RealWeight < details[i].AdjustWeight)
                    {
                        return "false1";
                    }
                }
            }
            else if (basStorage.IsVirtual != "1" && basStorage.StorageType == "0")
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
                        PstStorage storage = storageManager.GetListByWhere(PstStorage._.Barcode == details[i].Barcode && PstStorage._.StorageID == sourceStorageID && PstStorage._.StoragePlaceID == details[i].SourceStoragePlace && PstStorage._.MaterCode == details[i].MaterCode)[0];

                        storage.RealWeight = storage.RealWeight + details[i].AdjustWeight;
                        storage.Num = storage.Num + details[i].AdjustNum;
                        if (storage.Num != 0)
                            storage.PieceWeight = Convert.ToDecimal(String.Format("{0:N3}", storage.RealWeight / storage.Num));
                        else
                            storage.PieceWeight = 0;
                        storageManager.Update(storage);

                        if (basStorage.IsVirtual != "1" && basStorage.StorageType == "1")
                        {
                            //EntityArrayList<PstShopStorage> storageTarget = shopStorageManager.GetListByWhere(PstShopStorage._.Barcode == details[i].Barcode && PstShopStorage._.StorageID == targetStorageID && PstShopStorage._.StoragePlaceID == details[i].TargetStoragePlace && PstShopStorage._.MaterCode == details[i].MaterCode);
                            PstShopStorage shopStorage = shopStorageManager.getPstShopStorage(details[i].Barcode, targetStorageID, details[i].TargetStoragePlace, details[i].BillNo, details[i].OrderID.ToString());
                            shopBarcode = shopStorage.Barcode;
                            if (shopBarcode.Length == 22)
                            {
                                shopStorageManager.Delete(shopStorage);
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(storage.Barcode))
                                {
                                    shopStorage.Num = shopStorage.Num - details[i].AdjustNum;
                                    shopStorage.RealWeight = shopStorage.RealWeight - details[i].AdjustWeight;
                                    if (shopStorage.Num != 0)
                                        shopStorage.PieceWeight = Convert.ToDecimal(String.Format("{0:N3}", shopStorage.RealWeight / shopStorage.Num));
                                    else
                                        shopStorage.PieceWeight = 0;
                                    shopStorageManager.Update(shopStorage);
                                }
                            }
                            
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

                    if (userCtrlManager.GetItemCtrl("SapInterfaceCtrl") == "1")
                    {
                        try
                        {
                            if (!adjustManager.IsSameStorageType(sourceStorageID, targetStorageID))
                            {
                                WebReference.Service ws = new WebReference.Service();
                                ws.Url = userCtrlManager.GetItemCtrl("WebServiceIP");
                                ws.ToSapAdjustInfo(billNo, txtSAPBillNo.Text, "1", "0");
                            }
                        }
                        catch
                        { }
                    }

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

    protected void btnExportSubmit_Click(object sender, EventArgs e)
    {
        //DataSet ds = adjustDetailManager.GetDetailInfo(Convert.ToDateTime(txtBeginTime.Text).ToString("yyyy-MM-dd"));
        StringBuilder sqlstr = new StringBuilder();
        sqlstr.AppendLine(@"select 'B'+P.Barcode as '条码号', A.BillNo as '领料单号',A.AdjustDate as '领料日期', B.StorageName   as '源库房', 
	                                C.StorageName   as '目标库房',  D.UserName,
 PB.StoragePlaceName '源库位',PD.MaterialName  '物料名称' , P.ProcDate as '生产日期',
	                                    P.AdjustNum as '数量', P.PieceWeight as '均重', P.AdjustWeight as '总重',
	                                PC.StoragePlaceName '目标库位'
                                from PstMaterialAdjustDetail P 
                                         left join     PstMaterialAdjust A on P.billNo=A.billNo
                                left join BasStorage B on A.SourceStorage = B.StorageID
                                left join BasStorage C on A.TargetStorage = C.StorageID
                                left join BasUser D on A.MakerPerson = D.WorkBarcode

 left join BasStoragePlace PB on P.SourceStoragePlace = PB.StoragePlaceID
                                    left join BasStoragePlace PC on P.TargetStoragePlace = PC.StoragePlaceID
                                    left join BasMaterial PD on P.MaterCode = PD.MaterialCode

                                where A.DeleteFlag = '0'");
       
            sqlstr.AppendLine(" AND B.StorageType = '0'");
       
        if (!string.IsNullOrEmpty(txtBillNo.Text))
        {
            sqlstr.AppendLine(" AND A.BillNo = '" + txtBillNo.Text + "'");
        }
        if (Convert.ToDateTime(txtBeginTime.Text) != DateTime.MinValue)
            sqlstr.AppendLine(" AND A.AdjustDate >= '" + Convert.ToDateTime(txtBeginTime.Text) + "'");
        if (Convert.ToDateTime(txtEndTime.Value) != DateTime.MinValue)
            sqlstr.AppendLine(" AND A.AdjustDate <= '" + Convert.ToDateTime(txtEndTime.Value).AddDays(1).ToString() + "'");
          String chkResultFlag="";
        if (cbxChkResultFlag.SelectedItem.Value != "all")
        chkResultFlag = cbxChkResultFlag.SelectedItem.Value;
        if (!string.IsNullOrEmpty(chkResultFlag))
        {
            sqlstr.AppendLine(" AND A.ChkResultFlag = '" + chkResultFlag + "'");
        }
        if (!string.IsNullOrEmpty(hiddenMakerPerson.Text))
        {
            sqlstr.AppendLine(" AND D.HRCode = '" + hiddenMakerPerson.Text + "'");
        }
//        if ((!string.IsNullOrEmpty(TextBarcode.Text)) || (!string.IsNullOrEmpty(TextMaterial.Text)))
//        {
//            String sql = @" and A.BillNo in (select BillNo from PstMaterialAdjustdetail left join basmaterial on  PstMaterialAdjustdetail.matercode = basmaterial.materialcode
// where barcode like '%"
//              + TextBarcode.Text + "%' and  basmaterial.materialname like '%" + TextMaterial.Text + "%')";
//            sqlstr.AppendLine(sql);
//        }

        if (!string.IsNullOrEmpty(TextBarcode.Text))
        {
            sqlstr.AppendLine(" AND  P.barcode  like '%" + TextBarcode.Text + "%'");
        }
        if (!string.IsNullOrEmpty(TextMaterial.Text))
        {
            sqlstr.AppendLine(" AND PD.MaterialName  like '%" + TextMaterial.Text + "%'");
        }


        sqlstr.AppendLine(" AND  B.StorageName  like '%" + TextField1.Text + "%'");


        DataSet ds = adjustManager.GetBySql(sqlstr.ToString()).ToDataSet();





        HttpResponse resp = Page.Response;
        resp.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
        resp.ContentType = "application/ms-excel";

        resp.AddHeader("Content-Disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("领料明细表", System.Text.Encoding.UTF8) + ".xls");
        this.EnableViewState = false;

        string colHeaders = "", Is_item = "";
        int i = 0;

        DataTable dt = ds.Tables[0];
        DataRow[] myRow = dt.Select();
        for (i = 0; i < dt.Columns.Count; i++)
        {
            colHeaders += dt.Columns[i].Caption.ToString() + "\t";
        }
        colHeaders += "\n";

        resp.Write(colHeaders);
        foreach (DataRow row in myRow)
        {
            for (i = 0; i < dt.Columns.Count; i++)
            {
                Is_item += row[i].ToString() + "\t";
            }
            Is_item += "\n";
            resp.Write(Is_item);
            Is_item = "";
        }
        resp.End();
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