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

public partial class Manager_Storage_MaterialStoreOutInsert : Mesnac.Web.UI.Page
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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest && !IsPostBack)
        {
            cbxEmptyFlag.SelectedItem.Value = "0";
            //txtBillNo1.Text = storeOutManager.GetBillNo();
            txtOutputDate1.Text = Convert.ToString(DateTime.Now);
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

    private PageResult<PstMaterialStoreoutDetail> GetPageResultData1(PageResult<PstMaterialStoreoutDetail> pageParams)
    {
        PstMaterialStoreoutDetailManager.QueryParams queryParams = new PstMaterialStoreoutDetailManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.billNo = hiddenBillNo.Text;

        return storeOutDetailManager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindDataDetail(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<PstMaterialStoreoutDetail> pageParams = new PageResult<PstMaterialStoreoutDetail>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "RecordDate DESC";

        PageResult<PstMaterialStoreoutDetail> lst = GetPageResultData1(pageParams);
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
        queryParams.IsEmptyWeight = cbxEmptyFlag.SelectedItem.Value;

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
        PstMaterialStoreout storeOut = storeOutManager.GetById(Request.QueryString["BillNo"].ToString());
        txtBillNo1.Text = storeOut.BillNo;
        hiddenBillNo.Text = storeOut.BillNo;
        txtFactoryID1.Text = deptManager.GetListByWhere(BasDept._.DepCode == storeOut.DeptCode)[0].DepName;
        hiddenFactoryID.Text = storeOut.DeptCode;
        txtOutputDate1.Text = storeOut.OutputDate.ToString();
        txtStorageName.Text = basStorageManager.GetStorageName(storeOut.StorageID);
        hiddenStorageID.Text = storeOut.StorageID;
        //txtRemark1.Text = storeOutBill.Remark;
        //GenStorageOut.Disabled = false;
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

    protected void AddStoreIn(object sender, DirectEventArgs e)
    {
        EntityArrayList<PstMaterialStoreoutDetail> storeoutDetail = storeOutDetailManager.GetListByWhere(PstMaterialStoreoutDetail._.BillNo == hiddenBillNo.Text);
        
        ChangeRecords<PstStorageTemp> pstStorages = new StoreDataHandler(e.ExtraParams["data"]).BatchObjectData<PstStorageTemp>();
        
        foreach (PstStorageTemp pstStorage in pstStorages.Updated)
        {
            if (pstStorage.Num < pstStorage.NewNum)
            {
                X.MessageBox.Alert("提示", "物料(" + pstStorage.MaterialName + ")库存数量为" + pstStorage.Num + ",您输入的数量超过库存数量").Show();
                return;
            }
        }

        for (int i = 0; i < pstStorages.Updated.Count; i++)
        {
            PstStorage storage = storageManager.GetListByWhere(PstStorage._.Barcode == pstStorages.Updated[i].Barcode && PstStorage._.StorageID == pstStorages.Updated[i].StorageID && PstStorage._.StoragePlaceID == pstStorages.Updated[i].StoragePlaceID)[0];
            for (int j = 0; j < storeoutDetail.Count; j++)
            {
                if (storage.Barcode == storeoutDetail[j].Barcode)
                {
                    X.MessageBox.Alert("操作", "操作失败，已存在条码为" + storage.Barcode + "的数据，不能插入重复数据！").Show();
                    return;
                }
            }

            if (!string.IsNullOrEmpty(hiddenStorageID.Text))
            {
                if (hiddenStorageID.Text != storage.StorageID)
                {
                    string startStorage = basStorageManager.GetStorageName(hiddenStorageID.Text);
                    string endStorage = basStorageManager.GetStorageName(storage.StorageID);
                    X.MessageBox.Alert("操作", "操作失败，您选择的库房(" + endStorage + ")和上次选择的库房(" + startStorage + ")不同，必须从同一库房出库！").Show();
                    return;
                }
            }

            EntityArrayList<PstMaterialStoreoutDetail> list = storeOutDetailManager.GetListByWhere(PstMaterialStoreoutDetail._.BillNo == hiddenBillNo.Text);

            PstMaterialStoreoutDetail storeoutDetailEntity = new PstMaterialStoreoutDetail();
            storeoutDetailEntity.BillNo = hiddenBillNo.Text;
            storeoutDetailEntity.Barcode = storage.Barcode;
            storeoutDetailEntity.OrderID = list.Count + 1;
            storeoutDetailEntity.ProductNo = storage.ProductNo;
            hiddenStorageID.Text = storage.StorageID;
            storeoutDetailEntity.StoragePlaceID = storage.StoragePlaceID;
            storeoutDetailEntity.MaterCode = storage.MaterCode;
            storeoutDetailEntity.ProcDate = storage.ProcDate;
            storeoutDetailEntity.OutputNum = pstStorages.Updated[i].NewNum;
            storeoutDetailEntity.PieceWeight = storage.PieceWeight;
            if (pstStorages.Updated[i].Num == pstStorages.Updated[i].NewNum)
                storeoutDetailEntity.OutputWeight = storage.RealWeight;
            else
                storeoutDetailEntity.OutputWeight = pstStorages.Updated[i].NewNum * storage.PieceWeight;
            storeoutDetailEntity.RecordDate = DateTime.Now;
            storeoutDetailEntity.DeleteFlag = "0";

            storeOutDetailManager.Insert(storeoutDetailEntity);

            //PstStorage storageChange = storageManager.GetListByWhere(PstStorage._.Barcode == pstStorages.Updated[i].Barcode && PstStorage._.StorageID == storage.StorageID && PstStorage._.StoragePlaceID == storage.StoragePlaceID)[0];
            //if (pstStorages.Updated[i].Num == pstStorages.Updated[i].NewNum)
            //{
            //    storageChange.Num = 0;
            //    storageChange.RealWeight = 0;
            //}
            //else
            //{
            //    storageChange.Num = storageChange.Num - pstStorages.Updated[i].NewNum;
            //    storageChange.RealWeight = storageChange.RealWeight - storageChange.PieceWeight * pstStorages.Updated[i].NewNum;
            //}
            //storageManager.Update(storageChange);

            //PstStorageDetail detail = new PstStorageDetail();
            //detail.Barcode = storage.Barcode;
            //detail.OrderID = storageDetailManager.GetOrderID(storage.Barcode);
            //detail.StoreInOut = "O";
            //detail.InputDate = DateTime.Now;
            //detail.Num = pstStorages.Updated[i].NewNum;
            //detail.PieceWeight = storage.PieceWeight;
            //if (pstStorages.Updated[i].Num == pstStorages.Updated[i].NewNum)
            //    detail.Weight = storage.RealWeight;
            //else
            //    detail.Weight = storageChange.PieceWeight * pstStorages.Updated[i].NewNum;
            //detail.Remark = this.UserID + "出库";

            //storageDetailManager.Insert(detail);
        }

        PagingToolbar2.DoRefresh();  
        this.winAdd.Close();
    }

    [Ext.Net.DirectMethod()]
    public void DeleteStorage(string barcode)
    {
        PstMaterialStoreoutDetail detail = storeOutDetailManager.GetListByWhere(PstMaterialStoreoutDetail._.BillNo == hiddenBillNo.Text && PstMaterialStoreoutDetail._.Barcode == barcode)[0];

        storeOutDetailManager.Delete(detail);

        PagingToolbar2.DoRefresh();
    }

    [Ext.Net.DirectMethod()]
    public string btnSave_Click()
    {
        EntityArrayList<PstMaterialStoreoutDetail> storeoutDetailCounts = storeOutDetailManager.GetListByWhere(PstMaterialStoreoutDetail._.BillNo == hiddenBillNo.Text && PstMaterialStoreoutDetail._.DeleteFlag == "0");
        if (storeoutDetailCounts.Count == 0)
        {
            return "false";
        }

        if (!string.IsNullOrEmpty(Request.QueryString["BillNo"]))
        {
            PstMaterialStoreout storeOut = storeOutManager.GetListByWhere(PstMaterialStoreout._.BillNo == Request.QueryString["BillNo"].ToString())[0];
            storeOut.DeptCode = hiddenFactoryID.Text;
            if (Convert.ToDateTime(txtOutputDate1.Text).ToShortDateString() == DateTime.Now.ToShortDateString())
                storeOut.OutputDate = DateTime.Now;
            else
                storeOut.OutputDate = Convert.ToDateTime(txtOutputDate1.Text);
            storeOut.MakerPerson = this.UserID;
            storeOut.Remark = txtRemark1.Text;

            storeOutManager.Update(storeOut);
        }
        else
        {
            PstMaterialStoreout storeOut = new PstMaterialStoreout();
            if (!string.IsNullOrEmpty(Request.QueryString["BillNo"]))
                storeOut.BillNo = hiddenBillNo.Text;
            else
                storeOut.BillNo = storeOutManager.GetBillNo();
            storeOut.StorageID = hiddenStorageID.Text;
            storeOut.BillType = "1";
            storeOut.RecordDate = DateTime.Now;
            if (Convert.ToDateTime(txtOutputDate1.Text).ToShortDateString() == DateTime.Now.ToShortDateString())
                storeOut.OutputDate = DateTime.Now;
            else
                storeOut.OutputDate = Convert.ToDateTime(txtOutputDate1.Text);

            storeOut.DeptCode = hiddenFactoryID.Text;
            storeOut.LockedFlag = "0";
            storeOut.FiledFlag = "0";
            storeOut.MakerPerson = this.UserID;
            storeOut.DeleteFlag = "0";
            storeOut.Remark = userManager.UserID + "添加";
            storeOutManager.Insert(storeOut);

            storeOutDetailManager.Update(new PropertyItem[] { PstMaterialStoreoutDetail._.BillNo }, new object[] { storeOut.BillNo }, PstMaterialStoreoutDetail._.BillNo == storeoutDetailCounts[0].BillNo);
        }

        txtFactoryID1.Text = string.Empty;
        hiddenFactoryID.Text = string.Empty;
        txtRemark1.Text = string.Empty;
        txtOutputDate1.Text = DateTime.Now.ToString();

        store1.Data = null;
        store1.DataBind();

        return "执行成功";
    }

    [Ext.Net.DirectMethod()]
    public void commandcolumndetail_direct_edit(string barcode)
    {
        PstMaterialStoreoutDetail storeoutDetail = storeOutDetailManager.GetListByWhere(PstMaterialStoreoutDetail._.BillNo == hiddenBillNo.Text && PstMaterialStoreoutDetail._.Barcode == barcode)[0];

        txtBarcode2.Text = storeoutDetail.Barcode;
        txtProductNo2.Text = storeoutDetail.ProductNo;
        txtMaterialName2.Text = materialManager.GetMaterName(storeoutDetail.MaterCode);
        hiddenMaterCode.Text = storeoutDetail.MaterCode;
        txtProcDate2.Text = storeoutDetail.ProcDate.ToString();
        txtInputNum2.Text = storeoutDetail.OutputNum.ToString();
        hiddenOpenEdit.Text = "1";
        txtPieceWeight2.Text = storeoutDetail.PieceWeight.ToString();
        txtInputWeight2.Text = storeoutDetail.OutputWeight.ToString();
        txtRemark2.Text = storeoutDetail.Remark;

        this.winModifyDetail.Show();
    }

    protected void btnModifyDetailSave_Click(object sender, EventArgs e)
    {
        try
        {
            PstMaterialStoreoutDetail storeoutDetail = storeOutDetailManager.GetListByWhere(PstMaterialStoreoutDetail._.BillNo == hiddenBillNo.Text && PstMaterialStoreoutDetail._.Barcode == txtBarcode2.Text)[0];

            PstStorage storage = storageManager.GetListByWhere(PstStorage._.Barcode == storeoutDetail.Barcode && PstStorage._.StorageID == hiddenStorageID.Text && PstStorage._.StoragePlaceID == storeoutDetail.StoragePlaceID)[0];
            if (Convert.ToInt32(txtInputNum2.Text) > storage.Num)
            {
                X.MessageBox.Alert("操作", "您输入的数量超过了库存数(" + storage.Num + ")，请重新设置").Show();
                return;
            }

            storeoutDetail.OutputNum = Convert.ToInt32(txtInputNum2.Text);
            storeoutDetail.OutputWeight = Convert.ToDecimal(txtInputWeight2.Text);

            storeOutDetailManager.Update(storeoutDetail);
            txtInputNum2.Text = string.Empty;
            PagingToolbar2.DoRefresh();
            this.winModifyDetail.Close();
        }
        catch (Exception ex)
        {
            X.MessageBox.Alert("操作", "更新失败：" + ex).Show();
        }
    }

    public void txtStorein2_Change(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(txtInputNum2.Text) && !string.IsNullOrEmpty(txtPieceWeight2.Text))
            {
                if (hiddenOpenEdit.Text == "1")
                    hiddenOpenEdit.Text = "0";
                else
                    txtInputWeight2.Text = Convert.ToString(Convert.ToInt32(txtInputNum2.Text) * Convert.ToDecimal(txtPieceWeight2.Text));
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