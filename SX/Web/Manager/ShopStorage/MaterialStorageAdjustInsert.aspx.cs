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

public partial class Manager_ShopStorage_MaterialStorageAdjustInsert : Mesnac.Web.UI.Page
{
    protected BasUserManager userManager = new BasUserManager();
    protected BasDeptManager deptManager = new BasDeptManager();
    protected BasMaterialManager materialManager = new BasMaterialManager();
    protected BasStorageManager basStorageManager = new BasStorageManager();
    protected BasStoragePlaceManager basStoragePlaceManager = new BasStoragePlaceManager();
    protected PstStorageManager storageManager = new PstStorageManager();
    protected PstStorageDetailManager storageDetailManager = new PstStorageDetailManager();
    protected PstShopStorageManager ShopStorageManager = new PstShopStorageManager();
    protected PstShopStorageDetailManager ShopStorageDetailManager = new PstShopStorageDetailManager();
    protected PstMaterialStoreoutManager storeOutManager = new PstMaterialStoreoutManager();
    protected PstMaterialStoreoutDetailManager storeOutDetailManager = new PstMaterialStoreoutDetailManager();
    protected PptShiftTimeManager shiftTimeManager = new PptShiftTimeManager();

    protected PstMaterialAdjustManager adjustManager = new PstMaterialAdjustManager();
    protected PstMaterialAdjustDetailManager adjustDetailManager = new PstMaterialAdjustDetailManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest && !IsPostBack)
        {
            cbxEmptyFlag.SelectedItem.Value = "0";
            txtAdjustDate1.Text = Convert.ToString(DateTime.Now);
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

    private PageResult<PstMaterialAdjustDetail> GetPageResultData1(PageResult<PstMaterialAdjustDetail> pageParams)
    {
        PstMaterialAdjustDetailManager.QueryParams queryParams = new PstMaterialAdjustDetailManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.billNo = hiddenBillNo.Text;

        return adjustDetailManager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindDataDetail(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<PstMaterialAdjustDetail> pageParams = new PageResult<PstMaterialAdjustDetail>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "OrderID ASC";

        PageResult<PstMaterialAdjustDetail> lst = GetPageResultData1(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }

    private PageResult<PstShopStorage> GetPageResultData(PageResult<PstShopStorage> pageParams)
    {
        PstShopStorageManager.QueryParams queryParams = new PstShopStorageManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.storageID = hiddenStorageID.Text;
        queryParams.storagePlaceID = hiddenStoragePlaceID.Text;
        queryParams.materCode = hiddenMaterCode.Text;
        if (!string.IsNullOrEmpty(txtBeginTime.Text))
            queryParams.beginDate = Convert.ToDateTime(txtBeginTime.Text);
        if (!string.IsNullOrEmpty(txtEndTime.Text))
            queryParams.endDate = Convert.ToDateTime(txtEndTime.Text);
        queryParams.IsEmptyWeight = cbxEmptyFlag.SelectedItem.Value;

        return ShopStorageManager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<PstShopStorage> pageParams = new PageResult<PstShopStorage>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "Barcode ASC";

        PageResult<PstShopStorage> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }

    public void BindData(string billNo)
    {
        PstMaterialAdjust storeOut = adjustManager.GetById(Request.QueryString["BillNo"].ToString());
        txtBillNo1.Text = storeOut.BillNo;
        hiddenBillNo.Text = storeOut.BillNo;
        txtAdjustDate1.Text = storeOut.AdjustDate.ToString();
        txtTargetStorage1.Text = basStorageManager.GetStorageName(storeOut.TargetStorage);
        hiddenTargetStorageID.Text = storeOut.TargetStorage;
        txtMakerPerson1.Text = userManager.GetListByWhere(BasUser._.WorkBarcode == this.UserID)[0].UserName;
        txtSourceStorage1.Text = basStorageManager.GetStorageName(storeOut.SourceStorage);
        hiddenSourceStorageID.Text = storeOut.SourceStorage;
        txtRemark1.Text = storeOut.Remark;
        GenStorageOut.Disabled = false;
        EntityArrayList<PstMaterialAdjustDetail> adjustDetail = adjustDetailManager.GetListByWhere(PstMaterialAdjustDetail._.BillNo == billNo);

        this.store1.DataSource = adjustDetail;
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

    protected void AddStoreIn(object sender, DirectEventArgs e)
    {
        EntityArrayList<PstMaterialAdjustDetail> adjustDetail = adjustDetailManager.GetListByWhere(PstMaterialAdjustDetail._.BillNo == hiddenBillNo.Text);

        ChangeRecords<PstStorageTemp> pstStorages = new StoreDataHandler(e.ExtraParams["data"]).BatchObjectData<PstStorageTemp>();
        if (pstStorages.Updated.Count == 0)
        {
            X.MessageBox.Alert("提示", "您没有为选择项设置重量，请双击设置！").Show();
            return;
        }
        foreach (PstStorageTemp pstStorage in pstStorages.Updated)
        {
            if (pstStorage.RealWeight < pstStorage.NewWeight)
            {
                X.MessageBox.Alert("提示", "物料(" + pstStorage.MaterialName + ")库存重量为" + pstStorage.RealWeight + ",您输入的重量超过库存重量").Show();
                return;
            }
        }

        hiddenSourceStorageID.Text = pstStorages.Updated[0].StorageID;
        hiddenSourceStoragePlaceID.Text = pstStorages.Updated[0].StoragePlaceID;
        txtSourceStorage1.Text = pstStorages.Updated[0].StorageName;

        for (int i = 0; i < pstStorages.Updated.Count; i++)
        {
            PstShopStorage storage = ShopStorageManager.GetListByWhere(PstShopStorage._.Barcode == pstStorages.Updated[i].Barcode && PstShopStorage._.StorageID == hiddenSourceStorageID.Text && PstShopStorage._.StoragePlaceID == hiddenSourceStoragePlaceID.Text)[0];
            for (int j = 0; j < adjustDetail.Count; j++)
            {
                if (storage.Barcode == adjustDetail[j].Barcode)
                {
                    X.MessageBox.Alert("操作", "操作失败，已存在条码为" + storage.Barcode + "的数据，不能插入重复数据！").Show();
                    return;
                }
            }

            PstMaterialAdjustDetail adjustDetailEntity = new PstMaterialAdjustDetail();
            adjustDetailEntity.BillNo = hiddenBillNo.Text;
            adjustDetailEntity.Barcode = storage.Barcode;
            adjustDetailEntity.OrderID = i + 1;
            adjustDetailEntity.ProductNo = storage.ProductNo;
            adjustDetailEntity.SourceStoragePlace = storage.StoragePlaceID;
            //adjustDetailEntity.TargetStoragePlace = storage.StoragePlaceID;
            adjustDetailEntity.MaterCode = storage.MaterCode;
            adjustDetailEntity.ProcDate = storage.ProcDate;
            adjustDetailEntity.AdjustWeight = pstStorages.Updated[i].NewWeight;
            adjustDetailEntity.PieceWeight = storage.PieceWeight;
            if (pstStorages.Updated[i].RealWeight == pstStorages.Updated[i].NewWeight)
                adjustDetailEntity.AdjustNum = storage.Num;
            else
                adjustDetailEntity.AdjustNum = Convert.ToInt32(pstStorages.Updated[i].NewWeight / storage.PieceWeight);
            if (adjustDetailEntity.AdjustNum>0)
                adjustDetailEntity.PieceWeight = Convert.ToDecimal(String.Format("{0:N3}", adjustDetailEntity.AdjustWeight / adjustDetailEntity.AdjustNum));
            adjustDetailEntity.DeleteFlag = "0";
            adjustDetailEntity.RecordDate = DateTime.Now;
            DataSet shiftDS = shiftTimeManager.GetShiftDS("1", string.Empty);
            //string storageType = basStoragePlaceManager.GetStorageType(adjustDetailEntity.TargetStoragePlace);
            adjustDetailEntity.ShiftClassID = shiftDS.Tables[0].Rows[0]["ShiftClassID"].ToString();
            adjustDetailEntity.ShiftID = shiftDS.Tables[0].Rows[0]["ShiftID"].ToString();

            adjustDetailManager.Insert(adjustDetailEntity);

        }

        PagingToolbar2.DoRefresh();  
        this.winAdd.Close();
    }

    [Ext.Net.DirectMethod()]
    public void DeleteStorage(string barcode)
    {
        PstMaterialAdjustDetail detail = adjustDetailManager.GetListByWhere(PstMaterialAdjustDetail._.BillNo == hiddenBillNo.Text && PstMaterialAdjustDetail._.Barcode == barcode)[0];

        adjustDetailManager.Delete(detail);

        PagingToolbar2.DoRefresh();
    }

    [Ext.Net.DirectMethod()]
    public string btnSave_Click()
    {
        EntityArrayList<PstMaterialAdjustDetail> adjustDetailCounts = adjustDetailManager.GetListByWhere(PstMaterialAdjustDetail._.BillNo == hiddenBillNo.Text && PstMaterialAdjustDetail._.DeleteFlag == "0");
        if (adjustDetailCounts.Count == 0)
        {
            return "false";
        }

        for (int i = 0; i < adjustDetailCounts.Count; i++)
        {
            PstShopStorage storage = ShopStorageManager.GetListByWhere(PstShopStorage._.Barcode == adjustDetailCounts[i].Barcode && PstShopStorage._.StorageID == hiddenSourceStorageID.Text && PstShopStorage._.StoragePlaceID == adjustDetailCounts[i].SourceStoragePlace)[0];
            if (storage.Num < adjustDetailCounts[i].AdjustNum || storage.RealWeight < adjustDetailCounts[i].AdjustWeight)
            {
                return "false1";
            }
        }

        EntityArrayList<BasStoragePlace> storagePlace = basStoragePlaceManager.GetListByWhere(BasStoragePlace._.StorageID == hiddenTargetStorageID.Text && BasStoragePlace._.DeleteFlag == "0");
        int IsHave = 0;
        for (int i = 0; i < storagePlace.Count; i++)
        {
            for (int j = 0; j < adjustDetailCounts.Count; j++)
            {
                if (adjustDetailCounts[j].TargetStoragePlace == storagePlace[i].StoragePlaceID)
                {
                    IsHave = 1;
                }
            }
        }

        if (IsHave == 0)
        {
            return "false2";
        }

        if (!string.IsNullOrEmpty(Request.QueryString["BillNo"]))
        {
            PstMaterialAdjust adjust = adjustManager.GetListByWhere(PstMaterialAdjust._.BillNo == Request.QueryString["BillNo"].ToString())[0];
            //storeOut.DeptCode = hiddenDeptCode.Text;
            adjust.TargetStorage = hiddenTargetStorageID.Text;
            adjust.AdjustDate = Convert.ToDateTime(txtAdjustDate1.Text);
            if (Convert.ToDateTime(txtAdjustDate1.Text).ToShortDateString() == DateTime.Now.ToShortDateString())
                adjust.AdjustDate = DateTime.Now;
            else
                adjust.AdjustDate = Convert.ToDateTime(txtAdjustDate1.Text);
            adjust.SourceInaccountDuration = string.Format("{0:yyyyMM}", adjust.AdjustDate);
            adjust.TargetInaccountDuration = string.Format("{0:yyyyMM}", adjust.AdjustDate);
            adjust.MakerPerson = this.UserID;
            adjust.Remark = txtRemark1.Text;

            adjustManager.Update(adjust);

            if (basStoragePlaceManager.GetStorageType(string.Empty, adjust.TargetStorage) == "1")
            {
                EntityArrayList<PstMaterialAdjustDetail> details = adjustDetailManager.GetListByWhere(PstMaterialAdjustDetail._.BillNo == adjust.BillNo);
                for (int i = 0; i < details.Count; i++)
                {
                    details[i].TypeID = "1";
                    adjustDetailManager.Update(details[i]);
                }
            }
        }
        else
        {
            PstMaterialAdjust adjust = new PstMaterialAdjust();
            if (!string.IsNullOrEmpty(Request.QueryString["BillNo"]))
                adjust.BillNo = hiddenBillNo.Text;
            else
                adjust.BillNo = adjustManager.GetBillNo();
            adjust.SourceStorage = hiddenSourceStorageID.Text;
            adjust.TargetStorage = hiddenTargetStorageID.Text;
            adjust.MakerPerson = this.UserID;
            if (Convert.ToDateTime(txtAdjustDate1.Text).ToShortDateString() == DateTime.Now.ToShortDateString())
                adjust.AdjustDate = DateTime.Now;
            else
                adjust.AdjustDate = Convert.ToDateTime(txtAdjustDate1.Text);
            adjust.SourceInaccountDuration = string.Format("{0:yyyyMM}", adjust.AdjustDate);
            adjust.TargetInaccountDuration = string.Format("{0:yyyyMM}", adjust.AdjustDate);
            adjust.ChkResultFlag = "0";
            adjust.DeleteFlag = "0";
            adjust.Remark = userManager.UserID + "添加";
            adjustManager.Insert(adjust);

            adjustDetailManager.Update(new PropertyItem[] { PstMaterialAdjustDetail._.BillNo }, new object[] { adjust.BillNo }, PstMaterialAdjustDetail._.BillNo == adjustDetailCounts[0].BillNo);

            EntityArrayList<PstMaterialAdjustDetail> details = adjustDetailManager.GetListByWhere(PstMaterialAdjustDetail._.BillNo == adjust.BillNo);
            BasStoragePlace place = basStoragePlaceManager.GetListByWhere(BasStoragePlace._.StorageID == hiddenTargetStorageID.Text && BasStoragePlace._.DefaultFlag == "1")[0];
            for (int i = 0; i < details.Count; i++)
            {
                if (string.IsNullOrEmpty(details[i].TargetStoragePlace))
                {
                    details[i].TargetStoragePlace = place.StoragePlaceID;
                    adjustDetailManager.Update(details[i]);
                }
            }
        }

        txtTargetStorage1.Text = string.Empty;
        txtSourceStorage1.Text = string.Empty;
        hiddenSourceStorageID.Text = string.Empty;
        hiddenTargetStorageID.Text = string.Empty;

        store1.Data = null;
        store1.DataBind();

        return "执行成功";
    }

    [Ext.Net.DirectMethod()]
    public void commandcolumndetail_direct_edit(string barcode)
    {
        PstMaterialAdjustDetail adjustDetail = adjustDetailManager.GetListByWhere(PstMaterialAdjustDetail._.BillNo == hiddenBillNo.Text && PstMaterialAdjustDetail._.Barcode == barcode)[0];

        txtBarcode2.Text = adjustDetail.Barcode;
        hiddenOrderID.Text = adjustDetail.OrderID.ToString();
        txtProductNo2.Text = adjustDetail.ProductNo;
        string sourceStorageID = basStoragePlaceManager.GetListByWhere(BasStoragePlace._.StoragePlaceID == adjustDetail.SourceStoragePlace)[0].StorageID;
        txtSourceStoragePlaceName2.Text = basStoragePlaceManager.GetStoragePlaceName(sourceStorageID, adjustDetail.SourceStoragePlace);
        hiddenStoragePlaceID.Text = adjustDetail.SourceStoragePlace;//txtTargetStoragePlaceName2.Text = string.Empty;
        if (!string.IsNullOrEmpty(adjustDetail.TargetStoragePlace))
        {
            string targetStorageID = basStoragePlaceManager.GetListByWhere(BasStoragePlace._.StoragePlaceID == adjustDetail.TargetStoragePlace)[0].StorageID;
            txtTargetStoragePlaceName2.Text = basStoragePlaceManager.GetStoragePlaceName(targetStorageID, adjustDetail.TargetStoragePlace);
            hiddenTargetStoragePlaceID.Text = adjustDetail.TargetStoragePlace;
        }
        txtMaterialName2.Text = materialManager.GetMaterName(adjustDetail.MaterCode);
        //hiddenMaterCode.Text = adjustDetail.MaterCode;
        txtProcDate2.Text = adjustDetail.ProcDate.ToString();
        txtAdjustNum2.Text = adjustDetail.AdjustNum.ToString();
        txtPieceWeight2.Text = adjustDetail.PieceWeight.ToString();
        txtAdjustWeight2.Text = adjustDetail.AdjustWeight.ToString();
        txtRemark2.Text = adjustDetail.Remark;
        cbxShiftClassID.Value = adjustDetail.ShiftClassID;

        this.winModifyDetail.Show();
    }

    protected void btnModifyDetailSave_Click(object sender, EventArgs e)
    {
        try
        {
            PstMaterialAdjustDetail adjustDetail = adjustDetailManager.GetListByWhere(PstMaterialAdjustDetail._.BillNo == hiddenBillNo.Text && PstMaterialAdjustDetail._.Barcode == txtBarcode2.Text && PstMaterialAdjustDetail._.OrderID == hiddenOrderID.Text)[0];

            adjustDetail.TargetStoragePlace = hiddenTargetStoragePlaceID.Text;
            adjustDetail.AdjustNum = Convert.ToInt32(txtAdjustNum2.Text);
            adjustDetail.AdjustWeight = Convert.ToDecimal(txtAdjustWeight2.Text);
            DataSet shiftDS = shiftTimeManager.GetShiftDS("1", cbxShiftClassID.Value.ToString());

            adjustDetail.TypeID = basStoragePlaceManager.GetStorageType(adjustDetail.TargetStoragePlace, string.Empty);

            adjustDetail.ShiftClassID = cbxShiftClassID.Value.ToString();
            adjustDetail.ShiftID = shiftDS.Tables[0].Rows[0]["ShiftID"].ToString();

            adjustDetailManager.Update(adjustDetail);
            txtAdjustNum2.Text = string.Empty;
            PagingToolbar2.DoRefresh();
            this.winModifyDetail.Close();
        }
        catch (Exception ex)
        {
            X.MessageBox.Alert("操作", "更新失败：" + ex).Show();
        }
    }

    public void txtAdjustNum2_Change(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(txtAdjustNum2.Text) && !string.IsNullOrEmpty(txtPieceWeight2.Text) && hiddenFocus.Text == "txtAdjustNum2")
            {
                txtAdjustWeight2.Text = Convert.ToString(Convert.ToInt32(txtAdjustNum2.Text) * Convert.ToDecimal(txtPieceWeight2.Text));
            }
        }
        catch
        { }
    }

    public void txtAdjustWeight2_Change(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(txtAdjustWeight2.Text) && !string.IsNullOrEmpty(txtPieceWeight2.Text) && hiddenFocus.Text == "txtAdjustWeight2")
            {
                txtAdjustNum2.Text = Math.Round(Convert.ToDecimal(txtAdjustWeight2.Text) / Convert.ToDecimal(txtPieceWeight2.Text), 0).ToString();
            }
        }
        catch
        { }
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