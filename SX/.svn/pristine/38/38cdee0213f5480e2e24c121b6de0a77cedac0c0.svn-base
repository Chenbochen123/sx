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

public partial class Manager_Storage_MaterialReturnIn : Mesnac.Web.UI.Page
{
    protected PstMaterialStoreinManager manager = new PstMaterialStoreinManager();
    protected PstMaterialStoreinDetailManager detailManager = new PstMaterialStoreinDetailManager();
    protected PstMaterialChkManager chkManager = new PstMaterialChkManager();
    protected PstMaterialChkDetailManager chkdetailManager = new PstMaterialChkDetailManager();
    protected BasMaterialManager materManager = new BasMaterialManager();
    protected BasUserManager userManager = new BasUserManager();
    protected BasStorageManager storageManager = new BasStorageManager();
    protected PstStorageManager pstStorageManager = new PstStorageManager();
    protected PstStorageDetailManager pstStorageDetailManager = new PstStorageDetailManager();
    protected PstMaterialReturninManager returninManager = new PstMaterialReturninManager();
    protected PstMaterialReturninDetailManager returninDetailManager = new PstMaterialReturninDetailManager();
    protected SysUserCtrlManager userCtrlManager = new SysUserCtrlManager();

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
                this.storeDetail.DataSource = returninDetailManager.GetByOtherBillNo(billNo, barcode, orderID);
                this.storeDetail.DataBind();
            }
            else
            {
                txtBeginTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtEndTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
                cbxFiledFlag.SelectedItem.Value = "0";
            }
        }
    }

    #region 分页相关方法
    private PageResult<PstMaterialReturnin> GetPageResultData(PageResult<PstMaterialReturnin> pageParams)
    {
        PstMaterialReturninManager.QueryParams queryParams = new PstMaterialReturninManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.billNo = txtBillNo.Text;
        queryParams.factoryID = hiddenFactoryID.Text;
        queryParams.beginDate = Convert.ToDateTime(txtBeginTime.Text);
        queryParams.endDate = Convert.ToDateTime(txtEndTime.Value);
        if (cbxFiledFlag.SelectedItem.Value != "all")
            queryParams.filedFlag = cbxFiledFlag.SelectedItem.Value;
        queryParams.deleteFlag = "0";

        return returninManager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<PstMaterialReturnin> pageParams = new PageResult<PstMaterialReturnin>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "ReturninDate DESC";

        PageResult<PstMaterialReturnin> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }

    protected void RowSelect(object sender, StoreReadDataEventArgs e)
    {
        string billNo = e.Parameters["BillNo"];

        this.storeDetail.DataSource = returninDetailManager.GetByBillNo(billNo);
        this.storeDetail.DataBind();
    }
    #endregion

    #region 增删改查按钮激发的事件
    [Ext.Net.DirectMethod()]
    public string commandcolumn_direct_delete(string billNo)
    {
        try
        {
            PstMaterialReturnin store = returninManager.GetById(billNo);
            store.DeleteFlag = "1";

            returninManager.Update(store);
            pageToolBar.DoRefresh();
        }
        catch (Exception e)
        {
            return "删除失败：" + e;
        }
        return "删除成功";
    }

    [Ext.Net.DirectMethod()]
    public string btnBatchChk_Click()
    {
        string SapUserCtrl = userCtrlManager.GetItemCtrl("SapInterfaceCtrl");
        if (SapUserCtrl == "1" && string.IsNullOrEmpty(txtSAPBillNo.Text))
        {
            return "SAP单号不能为空！";
        }
        string strBillNo = string.Empty;
        string billNo = string.Empty;
        foreach (SelectedRow row in this.rowSelectMuti.SelectedRows)
        {
            strBillNo += "'" + row.RecordID + "', ";
            billNo = row.RecordID;
            EntityArrayList<PstMaterialReturnin> chk = returninManager.GetListByWhere(PstMaterialReturnin._.BillNo == row.RecordID);
            if (chk[0].ChkResultFlag == "1")
                return "该单据已经审核入库";
            //判断是否在库房期间内
            DataSet ds = storageManager.GetDuration(chk[0].StorageID);
            if (chk[0].ReturninDate < Convert.ToDateTime(ds.Tables[0].Rows[0][5].ToString()))
            {
                return "false1";
            }

            //检验合格后将数据放到库存中心明细信息表和主表
            EntityArrayList<PstMaterialReturninDetail> returninDetails = returninDetailManager.GetListByWhere(PstMaterialReturninDetail._.BillNo == row.RecordID);

            //插入主信息
            for (int i = 0; i < returninDetails.Count; i++)
            {
                PstStorage store = new PstStorage();
                store.Barcode = returninDetails[i].Barcode;
                store.ProductNo = returninDetails[i].ProductNo;
                store.StorageID = chk[0].StorageID;
                store.StoragePlaceID = returninDetails[i].StoragePlaceID;
                store.MaterCode = returninDetails[i].MaterCode;
                store.ProcDate = returninDetails[i].ProcDate;
                store.FactoryID = chk[0].FactoryID;

                PstStorage storage = pstStorageManager.getPstStorage(store.Barcode, store.StorageID, store.StoragePlaceID, store.MaterCode);
                if (storage != null)
                {
                    storage.Num += returninDetails[i].ReturninNum;
                    storage.RealWeight += returninDetails[i].ReturninWeight;
                    if (storage.Num != 0)
                        storage.PieceWeight = Convert.ToDecimal(String.Format("{0:N3}", storage.RealWeight / storage.Num));
                    else
                        storage.PieceWeight = 0;

                    pstStorageManager.Update(storage);
                }
                else
                {
                    store.Num = returninDetails[i].ReturninNum;
                    store.PieceWeight = returninDetails[i].PieceWeight;
                    store.RealWeight = returninDetails[i].ReturninWeight;
                    store.PieceWeight = Convert.ToDecimal(String.Format("{0:N3}", store.RealWeight / store.Num));
                    store.RecordDate = DateTime.Now;

                    pstStorageManager.Insert(store);
                }

                //插入明细数据
                PstStorageDetail storeDetail = new PstStorageDetail();
                storeDetail.Barcode = returninDetails[i].Barcode;
                if (storage != null)
                    storeDetail.OrderID = pstStorageDetailManager.GetOrderID(storeDetail.Barcode) + 1;
                else
                    storeDetail.OrderID = 1;
                storeDetail.StorageID = chk[0].StorageID;
                storeDetail.StoragePlaceID = returninDetails[i].StoragePlaceID;
                storeDetail.StoreInOut = "I";
                storeDetail.RecordDate = DateTime.Now;
                storeDetail.Num = returninDetails[i].ReturninNum;
                storeDetail.PieceWeight = returninDetails[i].PieceWeight;
                storeDetail.Weight = returninDetails[i].ReturninWeight;
                //if (Convert.ToDateTime(ds.Tables[0].Rows[0][5].ToString()).Day == 1)
                storeDetail.InaccountDuration = string.Format("{0:yyyyMM}", chk[0].ReturninDate);
                storeDetail.InaccountDate = chk[0].ReturninDate;
                storeDetail.BillType = "1005";
                storeDetail.SourceBillNo = returninDetails[i].BillNo;
                storeDetail.SourceOrderID = returninDetails[i].OrderID;

                pstStorageDetailManager.Insert(storeDetail);
            }
        }

        bool result = returninManager.UpdateChkResultFlag(strBillNo.Remove(strBillNo.Length - 2, 1), userManager.UserID);

        if (SapUserCtrl == "1")
        {
            try
            {
                WebReference.Service ws = new WebReference.Service();
                ws.Url = userCtrlManager.GetItemCtrl("WebServiceIP");
                ws.ToSapReturninInfo(billNo, txtSAPBillNo.Text, "0");
            }
            catch
            { }
        }

        if (result == true)
        {
            pageToolBar.DoRefresh();
            return "审核成功！";
        }
        else
        {
            return "审核失败！";
        }
    }

    [Ext.Net.DirectMethod()]
    public string btnCancelChk_Click()
    {
        string SapUserCtrl = userCtrlManager.GetItemCtrl("SapInterfaceCtrl");
        if (SapUserCtrl == "1" && string.IsNullOrEmpty(txtSAPBillNo.Text))
        {
            return "SAP单号不能为空！";
        }
        string strBillNo = string.Empty;
        string billNo = string.Empty;
        foreach (SelectedRow row in this.rowSelectMuti.SelectedRows)
        {
            strBillNo += "'" + row.RecordID + "', ";
            billNo = row.RecordID;
            EntityArrayList<PstMaterialReturnin> chk = returninManager.GetListByWhere(PstMaterialReturnin._.BillNo == row.RecordID);
            if (chk[0].ChkResultFlag == "0")
                return "该单据未审核，不能撤销！";
            //判断是否在库房期间内
            DataSet ds = storageManager.GetDuration(chk[0].StorageID);
            if (chk[0].ReturninDate < Convert.ToDateTime(ds.Tables[0].Rows[0][5].ToString()))
            {
                return "false1";
            }

            //判断库存是否充足
            EntityArrayList<PstMaterialReturninDetail> returninDetails1 = returninDetailManager.GetListByWhere(PstMaterialReturninDetail._.BillNo == row.RecordID);
            for (int i = 0; i < returninDetails1.Count; i++)
            {

                PstStorage storage = pstStorageManager.GetListByWhere(PstStorage._.Barcode == returninDetails1[0].Barcode && PstStorage._.StorageID == chk[0].StorageID && PstStorage._.StoragePlaceID == returninDetails1[0].StoragePlaceID)[0];
                if (storage.Num < returninDetails1[i].ReturninNum || storage.RealWeight < returninDetails1[i].ReturninWeight)
                {
                    return "false";
                }
            }

            //检验合格后将数据放到库存中心明细信息表和主表
            EntityArrayList<PstMaterialReturninDetail> returninDetails = returninDetailManager.GetListByWhere(PstMaterialReturninDetail._.BillNo == row.RecordID);

            //修改主信息
            for (int i = 0; i < returninDetails.Count; i++)
            {
                PstStorage storage = pstStorageManager.getPstStorage(returninDetails[i].Barcode, chk[0].StorageID, returninDetails[i].StoragePlaceID, returninDetails[i].MaterCode);
                if (storage != null)
                {
                    storage.Num -= returninDetails[i].ReturninNum;
                    storage.RealWeight -= returninDetails[i].ReturninWeight;
                    if (storage.Num != 0)
                        storage.PieceWeight = Convert.ToDecimal(String.Format("{0:N3}", storage.RealWeight / storage.Num));
                    else
                        storage.PieceWeight = 0;

                    pstStorageManager.Update(storage);
                }

                //删除明细数据
                PstStorageDetail storeDetail = pstStorageDetailManager.GetListByWhere(PstStorageDetail._.Barcode == returninDetails[i].Barcode && PstStorageDetail._.StorageID == chk[0].StorageID && PstStorageDetail._.StoragePlaceID == returninDetails[i].StoragePlaceID && PstStorageDetail._.SourceBillNo == returninDetails[i].BillNo && PstStorageDetail._.SourceOrderID == returninDetails[i].OrderID)[0];
                pstStorageDetailManager.Delete(storeDetail);
            }
        }

        bool result = returninManager.CancelChkResult(strBillNo.Remove(strBillNo.Length - 2, 1), userManager.UserID);

        if (SapUserCtrl == "1")
        {
            try
            {
                WebReference.Service ws = new WebReference.Service();
                ws.Url = userCtrlManager.GetItemCtrl("WebServiceIP");
                ws.ToSapStoreoutInfo(billNo, txtSAPBillNo.Text, "0");
            }
            catch
            { }
        }

        if (result == true)
        {
            pageToolBar.DoRefresh();
            return "撤销成功！";
        }
        else
        {
            return "撤销失败！";
        }
    }

    [Ext.Net.DirectMethod()]
    public string commandcolumn_direct_filed(string billNo)
    {
        try
        {
            ////判断是否可以归档，根据是该数据所对应的明细数据全部全部处理完毕，SendChkFlag标志为1
            //EntityArrayList<PstMaterialReturninDetail> list = detailManager.GetListByWhere(PstMaterialReturninDetail._.BillNo == billNo && PstMaterialReturninDetail._.SendChkFlag == null && PstMaterialReturninDetail._.DeleteFlag == "0");
            //if (list.Count > 0)
            //{
            //    return "不能归档：该送检单没有处理完毕，请首先处理该数据对应的明细数据！";
            //}

            //PstMaterialReturnin store = manager.GetById(billNo);
            //store.FiledFlag = "1";

            //manager.Update(store);
            //pageToolBar.DoRefresh();
        }
        catch (Exception e)
        {
            return "操作失败：" + e;
        }
        return "已归档";
    }
        public void txtBeginTime_change(object sender, EventArgs e)
    {
        pageToolBar.DoRefresh();
    }
    public void txtEndTime_change(object sender, EventArgs e)
    {
        pageToolBar.DoRefresh();
    }
    public void cbxFiledFlag_change(object sender, EventArgs e)
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