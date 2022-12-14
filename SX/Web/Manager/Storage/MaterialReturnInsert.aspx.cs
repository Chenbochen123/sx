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

public partial class Manager_Storage_MaterialReturnInsert : Mesnac.Web.UI.Page
{
    protected PstMaterialChkManager chkManager = new PstMaterialChkManager();
    protected PstMaterialChkDetailManager chkdetailManager = new PstMaterialChkDetailManager();
    protected BasUserManager userManager = new BasUserManager();
    protected BasMaterialManager materManager = new BasMaterialManager();
    protected BasFactoryInfoManager facManager = new BasFactoryInfoManager();

    protected BasStorageManager basStorageManager = new BasStorageManager();
    protected BasStoragePlaceManager basStoragePlaceManager = new BasStoragePlaceManager();
    protected PstStorageManager storageManager = new PstStorageManager();
    protected PstStorageDetailManager storageDetailManager = new PstStorageDetailManager();
    protected PstMaterialStoreinManager storeInManager = new PstMaterialStoreinManager();
    protected PstMaterialStoreinDetailManager storeinDetailManager = new PstMaterialStoreinDetailManager();
    protected PstMaterialReturnManager returnManager = new PstMaterialReturnManager();
    protected PstMaterialReturnDetailManager returnDetailManager = new PstMaterialReturnDetailManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest && !IsPostBack)
        {
            //txtBillNo.Text = storeInManager.GetBillNo();
            txtBeginTime.Text = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
            txtEndTime.Text = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            txtReturnDate.Text = DateTime.Now.ToString();
            txtMakerPerson.Text = userManager.GetListByWhere(BasUser._.WorkBarcode == this.UserID)[0].UserName;
            this.Session["TempStoreInDetail"] = null;
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

    private PageResult<PstMaterialReturnDetail> GetPageResultData1(PageResult<PstMaterialReturnDetail> pageParams)
    {
        PstMaterialReturnDetailManager.QueryParams queryParams = new PstMaterialReturnDetailManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.billNo = hiddenBillNo.Text;

        return returnDetailManager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindDetail(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<PstMaterialReturnDetail> pageParams = new PageResult<PstMaterialReturnDetail>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "Barcode asc";

        PageResult<PstMaterialReturnDetail> lst = GetPageResultData1(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }

    private PageResult<PstStorage> GetPageResultData(PageResult<PstStorage> pageParams)
    {
        PstStorageManager.QueryParams queryParams = new PstStorageManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.storageID = hiddenStorageID.Text;
        queryParams.storagePlaceID = hiddenStoragePlaceID.Text;
        queryParams.materCode = hiddenMaterCode.Text;
        if (!string.IsNullOrEmpty(txtBeginTime.Text))
            queryParams.beginDate = Convert.ToDateTime(txtBeginTime.Text);
        if (!string.IsNullOrEmpty(txtEndTime.Text))
            queryParams.endDate = Convert.ToDateTime(txtEndTime.Text);
        queryParams.IsEmptyWeight = "0";//cbxEmptyFlag.SelectedItem.Value;
        queryParams.factoryID = hiddenFactoryID2.Text;

        return storageManager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<PstStorage> pageParams = new PageResult<PstStorage>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "Barcode ASC";

        PageResult<PstStorage> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }

    public void BindData(string billNo)
    {
        PstMaterialReturn returnBill = returnManager.GetById(Request.QueryString["BillNo"].ToString());
        txtBillNo.Text = returnBill.BillNo;
        hiddenBillNo.Text = returnBill.BillNo;
        //txtSendChkNo.Text = storeInBill.SendChkNo;
        //txtStorageName.Text = basStorageManager.GetStorageName(storeInBill.StorageID);
        hiddenStorageID.Text = returnBill.StorageID;
        txtFactoryID.Text = facManager.GetById(returnBill.ReturnFactory).FacName;
        hiddenFactoryID.Text = returnBill.ReturnFactory.ToString();
        txtReturnDate.Text = returnBill.ReturnDate.ToString();
        txtReturnReason.Text = returnBill.ReturnReason;
        txtStorageName.Text = basStorageManager.GetStorageName(returnBill.StorageID);
        hiddenStorageID.Text = returnBill.StorageID;
        txtRemark.Text = returnBill.Remark;
        //btnSave.Disabled = false;
        DataSet ds = returnDetailManager.GetByBillNo(billNo);
        //EntityArrayList<PstMaterialStoreinDetail> storeinDetail = storeinDetailManager.GetListByWhere(PstMaterialStoreinDetail._.BillNo == billNo);
        //txtStorageName.Text = storageManager.GetStorageName(storeinDetail[0].StorageID);
        //hiddenStorageID.Text = storeinDetail[0].StorageID;
        //txtStoragePlaceName.Text = storagePlaceManager.GetStoragePlaceName(storeinDetail[0].StorageID, storeinDetail[0].StoragePlaceID);
        //hiddenStoragePlaceID.Text = storeinDetail[0].StoragePlaceID;

        this.store.DataSource = ds;
        this.store.DataBind();
    }

    private List<PstMaterialStoreinDetail> CurrentData
    {
        get
        {
            return this.Session["TempStoreInDetail"] as List<PstMaterialStoreinDetail>;
        }
    }

    public string GetMaterName(string materCode)
    {
        return materManager.GetMaterName(materCode);
    }

    [Ext.Net.DirectMethod()]
    public void commandcolumndetail_direct_edit(string barcode, string orderID)
    {
        PstMaterialReturnDetail returnDetail = returnDetailManager.GetListByWhere(PstMaterialReturnDetail._.BillNo == hiddenBillNo.Text && PstMaterialReturnDetail._.Barcode == barcode && PstMaterialReturnDetail._.OrderID == orderID)[0];

        hiddenBarcode.Text = returnDetail.Barcode;
        hiddenOrderID.Text = returnDetail.OrderID.ToString();
        txtBarcode2.Text = returnDetail.Barcode;
        txtProductNo2.Text = returnDetail.ProductNo;
        txtMaterialName2.Text = materManager.GetMaterName(returnDetail.MaterCode);
        hiddenMaterCode.Text = returnDetail.MaterCode;
        //txtStorageName2.Text = basStorageManager.GetStorageName(returnDetail.SourceStorageID);
        //hiddenStorageID.Text = returnDetail.SourceStorageID;
        txtStoragePlaceName2.Text = basStoragePlaceManager.GetListByWhere(BasStoragePlace._.StoragePlaceID == returnDetail.StoragePlaceID)[0].StoragePlaceName;
        hiddenStoragePlaceID.Text = returnDetail.StoragePlaceID;
        txtProcDate2.Text = returnDetail.ProcDate.ToString();
        txtReturnNum2.Text = returnDetail.ReturnNum.ToString();
        hiddenOpenEdit.Text = "1";
        txtPieceWeight2.Text = returnDetail.PieceWeight.ToString();
        txtReturnWeight2.Text = returnDetail.ReturnWeight.ToString();
        //txtRemark2.Text = returnDetail.Remark;

        this.winModifyDetail.Show();
    }

    protected void btnModifyDetailSave_Click(object sender, EventArgs e)
    {
        try
        {
            PstStorage storage = storageManager.GetListByWhere(PstStorage._.StorageID == hiddenStorageID.Text && PstStorage._.StoragePlaceID == hiddenStoragePlaceID.Text && PstStorage._.Barcode == hiddenBarcode.Text && PstStorage._.MaterCode == hiddenMaterCode.Text)[0];
            //判断其他单据是否有相同的条码和物料
            if (storage.Num < Convert.ToInt32(txtReturnNum2.Text))
            {
                X.MessageBox.Alert("提示", "退货数量多于库存数量(" + storage.Num.ToString() + ")，请检查！").Show();
                return;
            }
            SaveModifyDetail();
        }
        catch (Exception ex)
        {
            X.MessageBox.Alert("操作", "更新失败：" + ex).Show();
        }
    }

    public string SaveModifyDetail()
    {
        PstMaterialReturnDetail storeInDetail = returnDetailManager.GetListByWhere(PstMaterialReturnDetail._.BillNo == hiddenBillNo.Text && PstMaterialReturnDetail._.Barcode == hiddenBarcode.Text && PstMaterialReturnDetail._.OrderID == hiddenOrderID.Text)[0];

        storeInDetail.ReturnNum = Convert.ToInt32(txtReturnNum2.Text);
        storeInDetail.ReturnWeight = Convert.ToDecimal(txtReturnWeight2.Text);
        storeInDetail.RecordDate = DateTime.Now;

        returnDetailManager.Update(storeInDetail);
        txtReturnNum2.Text = string.Empty;
        pageToolBar.DoRefresh();
        this.winModifyDetail.Close();
        return "true";
    }

    [Ext.Net.DirectMethod()]
    public void DeleteChkDetail(string barcode, string orderID)
    {
        PstMaterialReturnDetail returnDetail = returnDetailManager.GetListByWhere(PstMaterialReturnDetail._.BillNo == hiddenBillNo.Text && PstMaterialReturnDetail._.Barcode == barcode && PstMaterialReturnDetail._.OrderID == orderID)[0];

        returnDetailManager.Delete(returnDetail);
    }

    protected void AddStoreIn(object sender, DirectEventArgs e)
    {
        EntityArrayList<PstMaterialReturnDetail> returnDetail = returnDetailManager.GetListByWhere(PstMaterialReturnDetail._.BillNo == hiddenBillNo.Text);

        ChangeRecords<PstStorageTemp> pstStorages = new StoreDataHandler(e.ExtraParams["data"]).BatchObjectData<PstStorageTemp>();
        foreach (PstStorageTemp pstStorage in pstStorages.Updated)
        {
            if (pstStorage.Num < pstStorage.NewNum)
            {
                X.MessageBox.Alert("提示", "物料(" + pstStorage.MaterialName + ")库存数量为" + pstStorage.Num + ",您输入的数量超过库存数量").Show();
                return;
            }
        }

        hiddenStorageID.Text = pstStorages.Updated[0].StorageID;
        txtStorageName.Text = pstStorages.Updated[0].StorageName;

        for (int i = 0; i < pstStorages.Updated.Count; i++)
        {
            PstStorage storage = storageManager.GetListByWhere(PstStorage._.Barcode == pstStorages.Updated[i].Barcode && PstStorage._.StorageID == pstStorages.Updated[i].StorageID && PstStorage._.StoragePlaceID == pstStorages.Updated[i].StoragePlaceID)[0];
            for (int j = 0; j < returnDetail.Count; j++)
            {
                if (storage.Barcode == returnDetail[j].Barcode)
                {
                    X.MessageBox.Alert("操作", "操作失败，已存在条码为" + storage.Barcode + "的数据，不能插入重复数据！").Show();
                    return;
                }
            }

            txtFactoryID.Text = facManager.GetById(storage.FactoryID).FacName;
            hiddenFactoryID.Text = storage.FactoryID.ToString();

            PstMaterialReturnDetail returnDetailEntity = new PstMaterialReturnDetail();
            returnDetailEntity.BillNo = hiddenBillNo.Text;
            returnDetailEntity.Barcode = storage.Barcode;
            returnDetailEntity.OrderID = i + 1;
            returnDetailEntity.ProductNo = storage.ProductNo;
            returnDetailEntity.MaterCode = storage.MaterCode;
            returnDetailEntity.ProcDate = storage.ProcDate;
            //returnDetailEntity.SourceStorageID = storage.StorageID;
            returnDetailEntity.StoragePlaceID = storage.StoragePlaceID;
            returnDetailEntity.ReturnNum = pstStorages.Updated[i].NewNum;
            returnDetailEntity.PieceWeight = storage.PieceWeight;
            if (pstStorages.Updated[i].Num == pstStorages.Updated[i].NewNum)
                returnDetailEntity.ReturnWeight = storage.RealWeight;
            else
                returnDetailEntity.ReturnWeight = pstStorages.Updated[i].NewNum * storage.PieceWeight;
            returnDetailEntity.RecordDate = DateTime.Now;
            returnDetailEntity.DeleteFlag = "0";
            returnDetailManager.Insert(returnDetailEntity);
        }

        pageToolBar.DoRefresh();
        this.winAdd.Close();
    }    

    [Ext.Net.DirectMethod()]
    public string btnSave_Click()
    {
        EntityArrayList<PstMaterialReturnDetail> returnDetailCounts = returnDetailManager.GetListByWhere(PstMaterialReturnDetail._.BillNo == hiddenBillNo.Text && PstMaterialReturnDetail._.DeleteFlag == "0");
        if (returnDetailCounts.Count == 0)
        {
            return "false";
        }

        try
        {
            if (!string.IsNullOrEmpty(Request.QueryString["BillNo"]))
            {
                PstMaterialReturn returnInfo = returnManager.GetById(Request.QueryString["BillNo"].ToString());
                returnInfo.ReturnFactory = Convert.ToInt32(hiddenFactoryID.Text);
                returnInfo.ReturnDate = Convert.ToDateTime(txtReturnDate.Text);
                returnInfo.ReturnReason = txtReturnReason.Text;
                returnInfo.MakerPerson = userManager.UserID;
                returnInfo.Remark = txtRemark.Text;

                returnManager.Update(returnInfo);
            }
            else
            {
                //插入主信息
                PstMaterialReturn returnInfo = new PstMaterialReturn();
                returnInfo.BillNo = returnManager.GetBillNo();
                if (Convert.ToDateTime(txtReturnDate.Text).ToShortDateString() == DateTime.Now.ToShortDateString())
                    returnInfo.ReturnDate = DateTime.Now;
                else
                    returnInfo.ReturnDate = Convert.ToDateTime(txtReturnDate.Text);
                returnInfo.ReturnReason = txtReturnReason.Text;
                returnInfo.ReturnFactory = Convert.ToInt32(hiddenFactoryID.Text);
                returnInfo.MakerPerson = userManager.UserID;
                returnInfo.StorageID = hiddenStorageID.Text;
                returnInfo.InaccountDuration = string.Format("{0:yyyyMM}", returnInfo.ReturnDate);
                returnInfo.ChkResultFlag = "0";
                returnInfo.DeleteFlag = "0";
                returnInfo.Remark = txtRemark.Text;

                returnManager.Insert(returnInfo);

                returnDetailManager.Update(new PropertyItem[] { PstMaterialReturnDetail._.BillNo }, new object[] { returnInfo.BillNo }, PstMaterialReturnDetail._.BillNo == returnDetailCounts[0].BillNo);
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
        //txtStorageName.Text = string.Empty;
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

    //protected void RowSelect(object sender, DirectEventArgs e)
    //{
    //    string billNo = e.ExtraParams["BillNo"];

    //    this.storeDetail.DataSource = chkdetailManager.GetNoPassByBillNo(billNo);
    //    this.storeDetail.DataBind();
    //}

    public void btnCancel_Click(object sender, DirectEventArgs e)
    {
        this.winModifyDetail.Close();
        this.winAdd.Close();
    }

    public void txtReturn2_Change(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(txtReturnNum2.Text) && !string.IsNullOrEmpty(txtPieceWeight2.Text))
            {
                if (hiddenOpenEdit.Text == "1")
                    hiddenOpenEdit.Text = "0";
                else
                    txtReturnWeight2.Text = Convert.ToString(Convert.ToInt32(txtReturnNum2.Text) * Convert.ToDecimal(txtPieceWeight2.Text));
            }
        }
        catch
        { }
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