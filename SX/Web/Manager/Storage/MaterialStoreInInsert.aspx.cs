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

public partial class Manager_Storage_MaterialStoreInInsert : Mesnac.Web.UI.Page
{
    protected PstMaterialChkManager chkManager = new PstMaterialChkManager();
    protected PstMaterialChkDetailManager chkdetailManager = new PstMaterialChkDetailManager();
    protected BasUserManager userManager = new BasUserManager();
    protected BasMaterialManager materManager = new BasMaterialManager();
    protected BasFactoryInfoManager facManager = new BasFactoryInfoManager();

    protected BasStorageManager storageManager = new BasStorageManager();
    protected BasStoragePlaceManager storagePlaceManager = new BasStoragePlaceManager();
    protected BasProductPlaceManager placeManager = new BasProductPlaceManager();
    protected PstMaterialStoreinManager storeInManager = new PstMaterialStoreinManager();
    protected PstMaterialStoreinDetailManager storeinDetailManager = new PstMaterialStoreinDetailManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest && !IsPostBack)
        {
            txtBeginTime.Text = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
            txtEndTime.Text = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            txtInPutDate.Text = DateTime.Now.ToString();
            txtMakerPerson.Text = userManager.GetListByWhere(BasUser._.WorkBarcode == this.UserID)[0].UserName;
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

    private PageResult<PstMaterialStoreinDetail> GetPageResultData1(PageResult<PstMaterialStoreinDetail> pageParams)
    {
        PstMaterialStoreinDetailManager.QueryParams queryParams = new PstMaterialStoreinDetailManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.billNo = hiddenBillNo.Text;

        return storeinDetailManager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindDetail(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<PstMaterialStoreinDetail> pageParams = new PageResult<PstMaterialStoreinDetail>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "RecordDate desc";

        PageResult<PstMaterialStoreinDetail> lst = GetPageResultData1(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }

    private PageResult<PstMaterialChk> GetPageResultData(PageResult<PstMaterialChk> pageParams)
    {
        PstMaterialChkManager.QueryParams queryParams = new PstMaterialChkManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.billNo = txtBillNo1.Text;
        queryParams.beginDate = Convert.ToDateTime(txtBeginTime.Text);
        queryParams.endDate = Convert.ToDateTime(txtEndTime.Value);
        queryParams.factoryID = hiddenFactoryID2.Text;
        queryParams.sendChkFlag = "1";
        queryParams.stockInFlag = "0";
        queryParams.deleteFlag = "0";

        return chkManager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<PstMaterialChk> pageParams = new PageResult<PstMaterialChk>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "BillNo ASC";

        PageResult<PstMaterialChk> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }

    public void BindData(string billNo)
    {
        PstMaterialStorein storeInBill = storeInManager.GetById(Request.QueryString["BillNo"].ToString());
        txtBillNo.Text = storeInBill.BillNo;
        hiddenBillNo.Text = storeInBill.BillNo;
        //txtSendChkNo.Text = storeInBill.SendChkNo;
        txtStorageName.Text = storageManager.GetStorageName(storeInBill.StorageID);
        hiddenStorageID.Text = storeInBill.StorageID;
        txtFactoryID.Text = facManager.GetById(storeInBill.FactoryID).FacName;
        hiddenFactoryID.Text = storeInBill.FactoryID.ToString();
        txtInPutDate.Text = storeInBill.InputDate.ToString();
        txtRemark.Text = storeInBill.Remark;
        btnSave.Disabled = false;
        DataSet ds = storeinDetailManager.GetByBillNo(billNo);
        EntityArrayList<PstMaterialStoreinDetail> storeinDetail = storeinDetailManager.GetListByWhere(PstMaterialStoreinDetail._.BillNo == billNo);
        //txtStorageName.Text = storageManager.GetStorageName(storeinDetail[0].StorageID);
        //hiddenStorageID.Text = storeinDetail[0].StorageID;
        //txtStoragePlaceName.Text = storagePlaceManager.GetStoragePlaceName(storeinDetail[0].StorageID, storeinDetail[0].StoragePlaceID);
        hiddenStoragePlaceID.Text = storeinDetail[0].StoragePlaceID;

        this.store.DataSource = ds;
        this.store.DataBind();
    }

    public string GetMaterName(string materCode)
    {
        return materManager.GetMaterName(materCode);
    }

    protected void btnAddDetail_Click(object sender, EventArgs e)
    {
        //EntityArrayList<PstMaterialStoreinDetail> storeinDetails = storeinDetailManager.GetListByWhere(PstMaterialStoreinDetail._.BillNo == hiddenBillNo.Text && PstMaterialStoreinDetail._.Barcode == txtBarcode3.Text);
        //if (storeinDetails.Count > 0)
        //{
        //    X.MessageBox.Alert("操作", "列表中已存在" + txtBarcode3.Text + "的条码,不能重复！").Show();
        //    return;
        //}
        EntityArrayList<PstMaterialStoreinDetail> details = storeinDetailManager.GetListByWhere(PstMaterialStoreinDetail._.Barcode == txtBarcode3.Text && PstMaterialStoreinDetail._.DeleteFlag == "0");
        foreach (PstMaterialStoreinDetail storeinDetail1 in details)
        {
            if (storeinDetail1.MaterCode != hiddenMaterCode.Text)
            {
                X.MessageBox.Alert("操作", "其他单据已存在" + txtBarcode2.Text + "的条码，物料为" + materManager.GetMaterName(details[0].MaterCode)).Show();
                return;
            }
        }

        EntityArrayList<PstMaterialStoreinDetail> storeinDetailCount = storeinDetailManager.GetListByWhere(PstMaterialStoreinDetail._.BillNo == hiddenBillNo.Text && PstMaterialStoreinDetail._.DeleteFlag == "0");

        PstMaterialStoreinDetail storeinDetail = new PstMaterialStoreinDetail();
        storeinDetail.BillNo = hiddenBillNo.Text;
        storeinDetail.Barcode = txtBarcode3.Text;
        storeinDetail.ProductNo = txtProductNo3.Text;
        storeinDetail.OrderID = storeinDetailCount.Count + 1;
        //storeinDetail.StorageID = hiddenStorageID.Text;
        storeinDetail.StoragePlaceID = hiddenStoragePlaceID.Text;
        storeinDetail.MaterCode = hiddenMaterCode.Text;
        storeinDetail.ProcDate = Convert.ToDateTime(txtProcDate3.Text);
        storeinDetail.InputNum = Convert.ToInt32(txtInputNum3.Text);
        storeinDetail.PieceWeight = Convert.ToDecimal(txtPieceWeight3.Text);
        storeinDetail.InputWeight = Convert.ToDecimal(txtInputWeight3.Text);
        storeinDetail.RecordDate = DateTime.Now;
        //storeinDetail.SourcePlace = "0,手工录入";
        storeinDetail.DeleteFlag = "0";
        storeinDetail.Remark = txtRemark3.Text;
        storeinDetail.ProductPlace = cbxProductPlace.Text;

        //保存产地
        if (!string.IsNullOrEmpty(cbxProductPlace.Text))
            SaveProductPlace(hiddenFactoryID.Text, cbxProductPlace.Text);

        storeinDetailManager.Insert(storeinDetail);
        pageToolBar.DoRefresh();

        txtBarcode3.Text = string.Empty;
        txtProductNo3.Text = string.Empty;
        txtMaterialName3.Text = string.Empty;
        //txtStoragePlaceName3.Text = string.Empty;
        //hiddenStoragePlaceID.Text = string.Empty;
        txtProcDate3.Text = Convert.ToString(DateTime.Now);
        txtInputNum3.Text = string.Empty;
        txtPieceWeight3.Text = string.Empty;
        txtInputWeight3.Text = string.Empty;
        txtRemark3.Text = string.Empty;
        txtBarcode3.Focus();
    }

    public void SaveProductPlace(string factoryID, string productPlace)
    {
        //保存产地
        Double doubleVal;
        if (!Double.TryParse(productPlace, out doubleVal))
        {
            DataSet ds = placeManager.GetProductPlace(factoryID);
            List<string> arrList = new List<string>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                arrList.Add(ds.Tables[0].Rows[i][2].ToString());
            }
            if (!arrList.Contains(productPlace))
            {
                BasProductPlace basProductPlace = new BasProductPlace();
                basProductPlace.FactoryID = Convert.ToInt32(factoryID);
                basProductPlace.ProductPlace = productPlace;
                placeManager.Insert(basProductPlace);
            }
        }
    }

    [Ext.Net.DirectMethod()]
    public void commandcolumndetail_direct_edit(string barcode)
    {
        PstMaterialStoreinDetail storeInDetail = storeinDetailManager.GetListByWhere(PstMaterialStoreinDetail._.BillNo == hiddenBillNo.Text && PstMaterialStoreinDetail._.Barcode == barcode)[0];

        hiddenBarcode.Text = storeInDetail.Barcode;
        txtBarcode2.Text = storeInDetail.Barcode;
        txtProductNo2.Text = storeInDetail.ProductNo;
        txtMaterialName2.Text = materManager.GetMaterName(storeInDetail.MaterCode);
        if (string.IsNullOrEmpty(hiddenStorageID.Text))
        {
            X.MessageBox.Alert("提示", "请首先在主信息中选择库房").Show();
            return;
        }
        if (string.IsNullOrEmpty(storeInDetail.StoragePlaceID))
        {
            BasStoragePlace place = storagePlaceManager.GetListByWhere(BasStoragePlace._.StorageID == hiddenStorageID.Text && BasStoragePlace._.DefaultFlag == "1")[0];
            txtStoragePlaceName2.Text = place.StoragePlaceName;
            hiddenStoragePlaceID.Text = place.StoragePlaceID;
        }
        else
        {
            txtStoragePlaceName2.Text = storagePlaceManager.GetStoragePlaceName(hiddenStorageID.Text, storeInDetail.StoragePlaceID);
            hiddenStoragePlaceID.Text = storeInDetail.StoragePlaceID;
        }
        hiddenMaterCode.Text = storeInDetail.MaterCode;
        txtProcDate2.Text = storeInDetail.ProcDate.ToString();
        txtInputNum2.Text = storeInDetail.InputNum.ToString();
        txtPieceWeight2.Text = storeInDetail.PieceWeight.ToString();
        txtInputWeight2.Text = storeInDetail.InputWeight.ToString();
        txtRemark2.Text = storeInDetail.Remark;
        placeRefresh(this, null);
        cbxProductPlace2.Text = storeInDetail.ProductPlace;

        if (!string.IsNullOrEmpty(storeInDetail.SourceBillNo))
        {
            string factoryID = chkManager.GetFactoryID(storeInDetail.SourceBillNo);
            hiddenFactoryID2.Text = factoryID;
        }
        else
        {
            EntityArrayList<BasFactoryInfo> facInfo = facManager.GetListByWhere(BasFactoryInfo._.ERPCode == txtBarcode2.Text.Substring(14, 4));
            if (facInfo.Count == 0)
            {
                X.MessageBox.Alert("操作", "没有厂家信息，请检查！").Show();
                return;
            }
            hiddenFactoryID2.Text = facInfo[0].ObjID.ToString();
        }

        this.winModifyDetail.Show();
    }

    protected void btnModifyDetailSave_Click(object sender, EventArgs e)
    {
        try
        {
            EntityArrayList<PstMaterialStoreinDetail> storeInDetails = storeinDetailManager.GetListByWhere(PstMaterialStoreinDetail._.BillNo == hiddenBillNo.Text && PstMaterialStoreinDetail._.DeleteFlag == "0");
            PstMaterialStoreinDetail storeInDetail = storeinDetailManager.GetListByWhere(PstMaterialStoreinDetail._.BillNo == hiddenBillNo.Text && PstMaterialStoreinDetail._.Barcode == hiddenBarcode.Text)[0];

            //foreach (PstMaterialStoreinDetail p in storeInDetails)
            //{
            //    if (hiddenBarcode.Text != txtBarcode2.Text && p.Barcode == txtBarcode2.Text)
            //    {
            //        X.MessageBox.Alert("操作", "列表中已存在" + txtBarcode2.Text + "的条码,不能重复！").Show();
            //        return;
            //    }
            //}
            //判断其他单据是否有相同的条码和物料
            EntityArrayList<PstMaterialStoreinDetail> details = storeinDetailManager.GetListByWhere(PstMaterialStoreinDetail._.Barcode == txtBarcode2.Text && PstMaterialStoreinDetail._.DeleteFlag == "0");
            foreach (PstMaterialStoreinDetail storeinDetail1 in details)
            {
                if (storeinDetail1.MaterCode != hiddenMaterCode.Text && details.Count > 2)
                {
                    X.MessageBox.Alert("操作", "已存在" + txtBarcode2.Text + "的条码，物料为" + materManager.GetMaterName(details[0].MaterCode)).Show();
                    return;
                }
            }

            //if (storeInDetail.SourcePlace.Split(',')[0] == "1")
            //{
            //    PstMaterialChkDetail chkDetail = chkdetailManager.GetListByWhere(PstMaterialChkDetail._.BillNo == storeInDetail.SourcePlace.Split(',')[1] && PstMaterialChkDetail._.Barcode == storeInDetail.SourcePlace.Split(',')[2])[0];
            //    //if (chkDetail.StoreInNum == storeInDetail.InputNum)
            //    //{
            //    //    if (Convert.ToInt32(txtInputNum2.Text) > chkDetail.PassNum)
            //    //    {
            //    //        X.MessageBox.Confirm("操作", "修改数量应该小于等于送检合格的数量" + chkDetail.PassNum + "！", SaveModifyDetail());
            //    //    }
            //    //    else
            //    //        SaveModifyDetail();
            //    //}
            //    //else
            //    //{
            //    //    if (Convert.ToInt32(txtInputNum2.Text) > chkDetail.PassNum - chkDetail.StoreInNum)
            //    //    {
            //    //        X.MessageBox.Confirm("操作", "修改数量应该小于等于送检合格的数量" + (chkDetail.PassNum - chkDetail.StoreInNum) + "！", SaveModifyDetail());
            //    //    }
            //    //    else
            //    //        SaveModifyDetail();
            //    //}
            //    SaveModifyDetail();
            //}
            SaveModifyDetail();
        }
        catch (Exception ex)
        {
            X.MessageBox.Alert("操作", "更新失败：" + ex).Show();
        }
    }

    public string SaveModifyDetail()
    {
        PstMaterialStoreinDetail storeInDetail = storeinDetailManager.GetListByWhere(PstMaterialStoreinDetail._.BillNo == hiddenBillNo.Text && PstMaterialStoreinDetail._.Barcode == hiddenBarcode.Text)[0];

        storeInDetail.Barcode = hiddenBarcode.Text;
        storeInDetail.ProductNo = txtProductNo2.Text;
        //storeInDetail.StorageID = hiddenStorageID.Text;
        storeInDetail.StoragePlaceID = hiddenStoragePlaceID.Text;
        storeInDetail.MaterCode = hiddenMaterCode.Text;
        storeInDetail.ProcDate = Convert.ToDateTime(txtProcDate2.Text);
        int Num = Convert.ToInt32(storeInDetail.InputNum);
        storeInDetail.InputNum = Convert.ToInt32(txtInputNum2.Text);
        storeInDetail.PieceWeight = Convert.ToDecimal(txtPieceWeight2.Text);
        storeInDetail.InputWeight = Convert.ToDecimal(txtInputWeight2.Text);
        storeInDetail.RecordDate = DateTime.Now;
        storeInDetail.Remark = txtRemark2.Text;
        storeInDetail.ProductPlace = cbxProductPlace2.Text;

        //保存产地
        if (!string.IsNullOrEmpty(cbxProductPlace2.Text))
            SaveProductPlace(hiddenFactoryID2.Text, cbxProductPlace2.Text);

        storeinDetailManager.Update(storeInDetail);
        if (txtBarcode2.Text != hiddenBarcode.Text)
            storeinDetailManager.Update(new PropertyItem[] { PstMaterialStoreinDetail._.Barcode }, new object[] { txtBarcode2.Text }, PstMaterialStoreinDetail._.BillNo == hiddenBillNo.Text && PstMaterialStoreinDetail._.Barcode == hiddenBarcode.Text);

        //if (storeInDetail.SourcePlace.Split(',')[0] == "1")
        //{
        //    PstMaterialChkDetail chkDetail = chkdetailManager.GetListByWhere(PstMaterialChkDetail._.BillNo == storeInDetail.SourcePlace.Split(',')[1] && PstMaterialChkDetail._.Barcode == storeInDetail.SourcePlace.Split(',')[2])[0];
        //    if (chkDetail.StoreInNum == Num)
        //    {
        //        chkDetail.StoreInNum = storeInDetail.InputNum;
        //        chkDetail.StoreInWeight = storeInDetail.InputWeight;
        //    }
        //    else
        //    {
        //        chkDetail.StoreInNum = chkDetail.StoreInNum + storeInDetail.InputNum;
        //        chkDetail.StoreInWeight = chkDetail.StoreInWeight + storeInDetail.InputWeight;
        //    }
        //    chkdetailManager.Update(chkDetail);

        //    string Stockin = "0";
        //    EntityArrayList<PstMaterialChkDetail> chkDetails1 = chkdetailManager.GetListByWhere(PstMaterialChkDetail._.BillNo == storeInDetail.SourcePlace.Split(',')[1] && PstMaterialChkDetail._.DeleteFlag == "0");
        //    foreach (PstMaterialChkDetail chk in chkDetails1)
        //    {
        //        if (chk.PassNum >= chk.StoreInNum)
        //        {
        //            Stockin = "0";
        //            break;
        //        }
        //        Stockin = "1";
        //    }

        //    PstMaterialChk chkStore = chkManager.GetById(storeInDetail.SourcePlace.Split(',')[1]);
        //    chkStore.StockInFlag = Stockin;
        //    chkManager.Update(chkStore);
        //}
        pageToolBar.DoRefresh();
        this.winModifyDetail.Close();
        return "true";
    }

    [Ext.Net.DirectMethod()]
    public void DeleteChkDetail(string barcode)
    {
        PstMaterialStoreinDetail storeInDetail = storeinDetailManager.GetListByWhere(PstMaterialStoreinDetail._.BillNo == hiddenBillNo.Text && PstMaterialStoreinDetail._.Barcode == barcode)[0];

        //if (!string.IsNullOrEmpty(storeInDetail.SourceBillNo))
        //{
        //    PstMaterialChkDetail chkDetail = chkdetailManager.GetListByWhere(PstMaterialChkDetail._.BillNo == storeInDetail.SourceBillNo && PstMaterialChkDetail._.Barcode == storeInDetail.Barcode)[0];
        //    if (chkDetail.StoreInNum == storeInDetail.InputNum)
        //    {
        //        chkDetail.StoreInNum = 0;
        //        chkDetail.StoreInWeight = 0;
        //    }
        //    else
        //    {
        //        chkDetail.StoreInNum = chkDetail.StoreInNum - storeInDetail.InputNum;
        //        chkDetail.StoreInWeight = chkDetail.StoreInWeight - storeInDetail.InputWeight;
        //    }
        //    chkdetailManager.Update(chkDetail);
        //}

        storeinDetailManager.Delete(storeInDetail);
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtStorageName.Text))
        {
            X.MessageBox.Alert("提示", "请首先在主信息中选择库房").Show();
            return;
        }
        txtBarcode3.Text = string.Empty;
        txtProductNo3.Text = string.Empty;
        txtMaterialName3.Text = string.Empty;
        BasStoragePlace place = storagePlaceManager.GetListByWhere(BasStoragePlace._.StorageID == hiddenStorageID.Text && BasStoragePlace._.DefaultFlag == "1")[0];
        txtStoragePlaceName3.Text = place.StoragePlaceName;
        hiddenStoragePlaceID.Text = place.StoragePlaceID;
        txtProcDate3.Text = DateTime.Now.ToString("yyyy-MM-dd");
        txtInputNum3.Text = string.Empty;
        txtPieceWeight3.Text = string.Empty;
        txtInputWeight3.Text = string.Empty;
        txtRemark3.Text = string.Empty;

        this.winAddDetail.Show();
    }

    [Ext.Net.DirectMethod()]
    public void btnAddSave_Click(string billNo, string barcodes, string orders)
    {
        string[] barcodeArr = barcodes.Split(new char[] { ',' });
        string[] orderArr = orders.Split(new char[] { ',' });
        for (int i = 0; i < barcodeArr.Length; i++)
        {
            PstMaterialStoreinDetail storeInDetail = new PstMaterialStoreinDetail();
            PstMaterialChkDetail chkStoreDetail = chkdetailManager.GetEntity(billNo, barcodeArr[i], orderArr[i]);

            EntityArrayList<PstMaterialStoreinDetail> storeInDetails = storeinDetailManager.GetListByWhere(PstMaterialStoreinDetail._.BillNo == hiddenBillNo.Text && PstMaterialStoreinDetail._.DeleteFlag == "0");
            foreach (PstMaterialStoreinDetail p in storeInDetails)
            {
                if (p.Barcode == chkStoreDetail.Barcode && p.OrderID == chkStoreDetail.OrderID)
                {
                    X.MessageBox.Alert("操作", "列表中已存在" + txtBarcode2.Text + "的条码,不能重复！").Show();
                    return;
                }
            }

            //判断其他单据是否有相同的条码和物料
            EntityArrayList<PstMaterialStoreinDetail> details = storeinDetailManager.GetListByWhere(PstMaterialStoreinDetail._.Barcode == txtBarcode2.Text && PstMaterialStoreinDetail._.DeleteFlag == "0");
            foreach (PstMaterialStoreinDetail storeinDetail1 in details)
            {
                if (storeinDetail1.MaterCode != chkStoreDetail.MaterCode)
                {
                    X.MessageBox.Alert("操作", "其他单据已存在" + txtBarcode2.Text + "的条码，物料为" + materManager.GetMaterName(details[0].MaterCode)).Show();
                    return;
                }
            }

            PstMaterialChk chkStore = chkManager.GetListByWhere(PstMaterialChk._.BillNo == billNo)[0];
            hiddenFactoryID.Text = chkStore.FactoryID.ToString();
            txtFactoryID.Text = facManager.GetById(chkStore.FactoryID).FacName;

            storeInDetail.BillNo = hiddenBillNo.Text;
            storeInDetail.Barcode = chkStoreDetail.Barcode;
            storeInDetail.ProductNo = chkStoreDetail.ProductNo;
            //int orderID = 0;
            //for (int j = 0; j < storeInDetails.Count; j++)
            //{
            //    if (storeInDetails[j].OrderID > orderID)
            //        orderID = storeInDetails[j].OrderID;
            //}
            //orderID++;
            //storeInDetail.OrderID = orderID;
            storeInDetail.OrderID = chkStoreDetail.OrderID;
            storeInDetail.StoragePlaceID = hiddenStoragePlaceID.Text;
            storeInDetail.MaterCode = chkStoreDetail.MaterCode;
            storeInDetail.ProcDate = chkStoreDetail.ProcDate;
            storeInDetail.InputNum = chkStoreDetail.PassNum - chkStoreDetail.StoreInNum;
            storeInDetail.PieceWeight = chkStoreDetail.PieceWeight;
            storeInDetail.InputWeight = chkStoreDetail.PassWeight - chkStoreDetail.StoreInWeight;
            storeInDetail.RecordDate = DateTime.Now;
            storeInDetail.SourceBillNo = billNo;
            storeInDetail.SourceOrderID = Convert.ToInt32(orderArr[i]);
            storeInDetail.NoticeNo = chkStoreDetail.NoticeNo;
            //storeInDetail.SourcePlace = "1," + billNo + "," + barcodeArr[i] + "," + orderArr[i];
            storeInDetail.DeleteFlag = "0";
            storeInDetail.Remark = chkStoreDetail.Remark;
            if (!string.IsNullOrEmpty(chkStoreDetail.LLBarcode))
                storeInDetail.LLBarcode = chkStoreDetail.LLBarcode;
            else
                storeInDetail.LLBarcode = chkdetailManager.GetLLBarcode(storeInDetail.Barcode);

            storeinDetailManager.Insert(storeInDetail);

            //chkStoreDetail.StoreInNum = chkStoreDetail.PassNum;
            //chkStoreDetail.StoreInWeight = chkStoreDetail.PassWeight;
            //chkdetailManager.Update(chkStoreDetail);
        }
        //string Stockin = "0";
        //foreach (string bc in barcodeArr)
        //{
        //    PstMaterialChkDetail chkStoreDetail = chkdetailManager.GetEntity(billNo, bc);
        //    if (chkStoreDetail.PassNum != chkStoreDetail.StoreInNum)
        //    {
        //        Stockin = "0";
        //        return;
        //    }
        //    Stockin = "1";
        //}
        //if (Stockin == "1")
        //{
        //    PstMaterialChk chkStore = chkManager.GetById(billNo);
        //    chkStore.StockInFlag = "1";
        //    chkManager.Update(chkStore);
        //}

        pageToolBar.DoRefresh();
        this.winAdd.Close();
    }

    [Ext.Net.DirectMethod()]
    public string btnSave_Click()
    {
        EntityArrayList<PstMaterialStoreinDetail> storeinDetailCounts = storeinDetailManager.GetListByWhere(PstMaterialStoreinDetail._.BillNo == hiddenBillNo.Text && PstMaterialStoreinDetail._.DeleteFlag == "0");
        if (storeinDetailCounts.Count == 0)
        {
            return "false";
        }
        //foreach (PstMaterialStoreinDetail p in storeinDetailCounts)
        //{
        //    if (string.IsNullOrEmpty(p.StoragePlaceID))
        //        return "false1";
        //}
        try
        {
            if (!string.IsNullOrEmpty(Request.QueryString["BillNo"]))
            {
                PstMaterialStorein store = storeInManager.GetById(Request.QueryString["BillNo"].ToString());
                //store.SendChkNo = txtSendChkNo.Text;
                store.FactoryID = Convert.ToInt32(hiddenFactoryID.Text);
                store.InputDate = Convert.ToDateTime(txtInPutDate.Text);
                store.InputPerson = userManager.UserID;
                store.MakerPerson = userManager.UserID;
                store.Remark = txtRemark.Text;

                storeInManager.Update(store);
            }
            else
            {
                //插入主信息
                PstMaterialStorein store = new PstMaterialStorein();
                store.BillNo = storeInManager.GetBillNo();
                //store.SendChkNo = txtSendChkNo.Text;
                store.BillType = "1";
                store.FactoryID = Convert.ToInt32(hiddenFactoryID.Text);
                store.StorageID = hiddenStorageID.Text;
                store.InputPerson = userManager.UserID;
                if (Convert.ToDateTime(txtInPutDate.Text).ToShortDateString() == DateTime.Now.ToShortDateString())
                    store.InputDate = DateTime.Now;
                else
                    store.InputDate = Convert.ToDateTime(txtInPutDate.Text);
                store.MakerPerson = userManager.UserID;
                store.LockedFlag = "0";
                store.ChkResultFlag = "0";
                store.FiledFlag = "0";
                store.DeleteFlag = "0";
                store.Remark = txtRemark.Text;

                storeInManager.Insert(store);

                storeinDetailManager.Update(new PropertyItem[] { PstMaterialStoreinDetail._.BillNo }, new object[] { store.BillNo }, PstMaterialStoreinDetail._.BillNo == storeinDetailCounts[0].BillNo);

                EntityArrayList<PstMaterialStoreinDetail> details = storeinDetailManager.GetListByWhere(PstMaterialStoreinDetail._.BillNo == store.BillNo);
                BasStoragePlace place = storagePlaceManager.GetListByWhere(BasStoragePlace._.StorageID == hiddenStorageID.Text && BasStoragePlace._.DefaultFlag == "1")[0];
                for (int i = 0; i < details.Count; i++)
                {
                    if (string.IsNullOrEmpty(details[i].StoragePlaceID))
                    {
                        details[i].StoragePlaceID = place.StoragePlaceID;
                        storeinDetailManager.Update(details[i]);
                    }
                }
            }

            //将控件清空还原
            BackControl();
            return "添加成功！";
        }
        catch (Exception ex)
        {
            return "添加失败：" + ex.ToString();
        }
    }

    public void BackControl()
    {
        //txtBillNo.Text = storeInManager.GetBillNo();
        //txtSendChkNo.Text = string.Empty;
        txtStorageName.Text = string.Empty;
        hiddenStorageID.Text = string.Empty;
        //txtStoragePlaceName.Text = string.Empty;
        hiddenStoragePlaceID.Text = string.Empty;
        txtFactoryID.Text = string.Empty;
        txtRemark.Text = string.Empty;

        store.Data = null;
        store.DataBind();
    }

    protected void btnInport_Click(object sender, EventArgs e)
    {
        //txtNoticeNo2.Text = string.Empty;
        //txtFactoryID2.Text = string.Empty;
        ////txtSendDate2.Text = DateTime.Now.ToString("yyyy-MM-dd");
        //txtRemark2.Text = string.Empty;

        this.winAdd.Show();
    }

    protected void RowSelect(object sender, DirectEventArgs e)
    {
        string billNo = e.ExtraParams["BillNo"];

        this.storeDetail.DataSource = chkdetailManager.GetByBillNo(billNo, "NoInput");
        this.storeDetail.DataBind();
    }

    public void btnCancel_Click(object sender, DirectEventArgs e)
    {
        this.winAddDetail.Close();
        this.winModifyDetail.Close();
        this.winAdd.Close();
    }

    public void txtBarcode3_Change(object sender, EventArgs e)
    {
        if (txtBarcode3.Text.Length == 21)
        {
            txtProductNo3.Text = txtBarcode3.Text;

            EntityArrayList<BasMaterial> materInfo = materManager.GetListByWhere(BasMaterial._.ERPCode == txtBarcode3.Text.Substring(0, 9));
            if (materInfo.Count == 0)
            {
                X.MessageBox.Alert("操作", "没有物料信息，请检查！").Show();
                return;
            }
            txtMaterialName3.Text = materInfo[0].MaterialName;
            hiddenMaterCode.Text = materInfo[0].MaterialCode;

            EntityArrayList<BasFactoryInfo> facInfo = facManager.GetListByWhere(BasFactoryInfo._.ERPCode == txtBarcode3.Text.Substring(15, 6));
            if (facInfo.Count == 0)
            {
                X.MessageBox.Alert("操作", "没有厂家信息，请检查！").Show();
                return;
            }
            //if (!string.IsNullOrEmpty(hiddenFactoryID.Text))
            //{
            //    if (hiddenFactoryID.Text != facInfo[0].ObjID.ToString())
            //    {
            //        X.MessageBox.Alert("操作", "录入条码的厂家和上个条码所在的厂家不一致，请检查！").Show();
            //        return;
            //    }
            //}
            txtFactoryID.Text = facInfo[0].FacName;
            hiddenFactoryID.Text = facInfo[0].ObjID.ToString();

            placeRefresh(this, null);
        }
    }

    protected void placeRefresh(object sender, StoreReadDataEventArgs e)
    {
        DataSet ds = placeManager.GetProductPlace(hiddenFactoryID.Text);
        this.storePlace.DataSource = ds;
        this.storePlace.DataBind();
        //cbxProductPlace
    }

    public void txtStorein1_Change(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(txtInputNum3.Text) && !string.IsNullOrEmpty(txtInputWeight3.Text) && hiddenFocus.Text == "txtInputNum3")
                txtPieceWeight3.Text = Math.Round(Convert.ToDecimal(txtInputWeight3.Text) / Convert.ToInt32(txtInputNum3.Text), 3).ToString();
        }
        catch { }
    }

    public void txtStorein2_Change(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(txtPieceWeight3.Text) && !string.IsNullOrEmpty(txtInputWeight3.Text) && hiddenFocus.Text == "txtPieceWeight3")
            {
                txtInputNum3.Text = Math.Round(Convert.ToDecimal(txtInputWeight3.Text) / Convert.ToDecimal(txtPieceWeight3.Text), 0).ToString();
            }
        }
        catch { }
    }

    public void txtStorein3_Change(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(txtInputNum2.Text) && !string.IsNullOrEmpty(txtInputWeight2.Text) && hiddenFocus.Text == "txtInputNum2")
                txtPieceWeight2.Text = Math.Round(Convert.ToDecimal(txtInputWeight2.Text) / Convert.ToInt32(txtInputNum2.Text), 3).ToString();
        }
        catch{ }
    }

    public void txtStorein4_Change(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(txtPieceWeight2.Text) && !string.IsNullOrEmpty(txtInputWeight2.Text) && hiddenFocus.Text == "txtPieceWeight2")
                txtInputNum2.Text = Math.Round(Convert.ToDecimal(txtInputWeight2.Text) / Convert.ToDecimal(txtPieceWeight2.Text), 0).ToString();
        }
        catch{ }
    }

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
}