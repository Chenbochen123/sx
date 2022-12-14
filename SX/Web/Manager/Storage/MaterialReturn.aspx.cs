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

public partial class Manager_Storage_MaterialReturn : Mesnac.Web.UI.Page
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
    protected PstMaterialReturnManager returnManager = new PstMaterialReturnManager();
    protected PstMaterialReturnDetailManager returnDetailManager = new PstMaterialReturnDetailManager();

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
                this.storeDetail.DataSource = returnDetailManager.GetByOtherBillNo(billNo, barcode, orderID);
                this.storeDetail.DataBind();
            }
            else
            {
                txtBeginTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtEndTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }
    }

    #region 分页相关方法
    private PageResult<PstMaterialReturn> GetPageResultData(PageResult<PstMaterialReturn> pageParams)
    {
        PstMaterialReturnManager.QueryParams queryParams = new PstMaterialReturnManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.billNo = txtBillNo.Text;
        queryParams.factoryID = hiddenFactoryID.Text;
        queryParams.beginDate = Convert.ToDateTime(txtBeginTime.Text);
        queryParams.endDate = Convert.ToDateTime(txtEndTime.Value);
        queryParams.deleteFlag = "0";
        queryParams.hrCode = hiddenMakerPerson.Text;

        return returnManager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<PstMaterialReturn> pageParams = new PageResult<PstMaterialReturn>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "BillNo DESC";

        PageResult<PstMaterialReturn> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }

    private PageResult<PstMaterialChk> GetPageResultData1(PageResult<PstMaterialChk> pageParams)
    {
        PstMaterialChkManager.QueryParams queryParams = new PstMaterialChkManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.stockInFlag = "0";
        queryParams.sendChkFlag = "1";
        queryParams.deleteFlag = "0";
        queryParams.chkResultFlag = "1";

        return chkManager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData1(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<PstMaterialChk> pageParams = new PageResult<PstMaterialChk>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "BillNo ASC";

        PageResult<PstMaterialChk> lst = GetPageResultData1(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }

    protected void RowSelect(object sender, StoreReadDataEventArgs e)
    {
        string billNo = e.Parameters["BillNo"];

        this.storeDetail.DataSource = returnDetailManager.GetByBillNo(billNo);
        this.storeDetail.DataBind();
    }
    #endregion

    #region 增删改查按钮激发的事件
    //protected void btnAdd_Click(object sender, EventArgs e)
    //{
    //    //txtNoticeNo2.Text = string.Empty;
    //    //txtFactoryID2.Text = string.Empty;
    //    ////txtSendDate2.Text = DateTime.Now.ToString("yyyy-MM-dd");
    //    //txtRemark2.Text = string.Empty;

    //    this.winAdd.Show();
    //}

    //[Ext.Net.DirectMethod()]
    //public string btnAddSave_Click()
    //{
    //    try
    //    {
    //        //判断是否属于当前期间
    //        string dt = storageManager.IsStoreIn(hiddenStorageID.Text);
    //        if (Convert.ToDateTime(txtInputDate2.Text) < Convert.ToDateTime(dt))
    //        {
    //            return "您选择的入库日期已经结存";
    //        }
    //        string strBillNo = string.Empty;
    //        foreach (SelectedRow row in this.RowSelectionMulti1.SelectedRows)
    //        {
    //            PstMaterialChk chkStore = chkManager.GetById(row.RecordID);
    //            PstMaterialStorein store = new PstMaterialStorein();
    //            store.BillNo = manager.GetBillNo();//生成入库单号规则：RK+年月日+三位随机号 如SJ130225001
    //            store.SendChkNo = chkStore.BillNo;
    //            store.BillType = "1";
    //            store.FactoryID = chkStore.FactoryID;
    //            store.InputPerson = userManager.UserID;
    //            store.InputDate = Convert.ToDateTime(txtInputDate2.Text);
    //            store.MakerPerson = userManager.UserID;
    //            store.FiledFlag = "0";
    //            store.Remark = txtRemark2.Text;

    //            manager.Insert(store);

    //            //修改送检单中的StockInFlag标记为1
    //            chkManager.UpdateStockInFlag(row.RecordID);

    //            DataSet ds = detailManager.GetFromChkdetail(row.RecordID);
    //            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
    //            {
    //                PstMaterialStoreinDetail detailStore = new PstMaterialStoreinDetail();
    //                detailStore.BillNo = store.BillNo;
    //                detailStore.Barcode = ds.Tables[0].Rows[i][1].ToString();
    //                detailStore.OrderID = i + 1;
    //                detailStore.StorageID = hiddenStorageID.Text;
    //                detailStore.StoragePlaceID = hiddenStoragePlaceID.Text;
    //                detailStore.MaterCode = ds.Tables[0].Rows[i][2].ToString();
    //                detailStore.ProcDate = Convert.ToDateTime(ds.Tables[0].Rows[i][3].ToString());
    //                detailStore.InputNum = Convert.ToInt32(ds.Tables[0].Rows[i][4].ToString());
    //                detailStore.PieceWeight = Convert.ToDecimal(ds.Tables[0].Rows[i][5].ToString());
    //                detailStore.InputWeight = Convert.ToDecimal(ds.Tables[0].Rows[i][6].ToString());
    //                detailStore.InputDate = Convert.ToDateTime(txtInputDate2.Text);

    //                detailManager.Insert(detailStore);
    //            }
    //        }

    //        pageToolBar.DoRefresh();
    //        this.winAdd.Close();
    //        return "入库单信息添加成功";
    //    }
    //    catch (Exception ex)
    //    {
    //        return "入库单信息添加失败";
    //    }
    //}

    //[Ext.Net.DirectMethod()]
    //public void commandcolumn_direct_edit(string BillNo)
    //{
    //    PstMaterialStorein store = manager.GetById(BillNo);
    //    txtBillNo1.Text = store.BillNo;
    //    txtSendChkNo11.Text = store.SendChkNo;
    //    txtStorageName1.Text = detailManager.GetByBillNo(BillNo).Tables[0].Rows[0][3].ToString();
    //    txtStoragePlaceName1.Text = detailManager.GetByBillNo(BillNo).Tables[0].Rows[0][5].ToString();
    //    hiddenStorageID.Text = detailManager.GetByBillNo(BillNo).Tables[0].Rows[0][2].ToString();
    //    hiddenStoragePlaceID.Text = detailManager.GetByBillNo(BillNo).Tables[0].Rows[0][4].ToString();
    //    txtInputDate1.Text = store.InputDate.ToString();
    //    txtRemark1.Text = store.Remark;

    //    this.winModify.Show();
    //}

    protected void btnModify_Click(object sender, EventArgs e)
    {
        try
        {
            PstMaterialStorein store = manager.GetById(txtBillNo1.Text);

            store.InputDate = Convert.ToDateTime(txtInputDate1.Text);
            store.Remark = txtRemark1.Text;

            manager.Update(store);

            detailManager.UpdateStorage(txtBillNo1.Text, hiddenStorageID.Text, hiddenStoragePlaceID.Text);

            pageToolBar.DoRefresh();
            this.winModify.Close();
            X.MessageBox.Alert("操作", "更新成功").Show();
        }
        catch (Exception ex)
        {
            X.MessageBox.Alert("操作", "更新失败：" + ex).Show();
        }
    }

    [Ext.Net.DirectMethod()]
    public string commandcolumn_direct_delete(string billNo)
    {
        try
        {
            PstMaterialReturn returnInfo = returnManager.GetById(billNo);
            returnInfo.DeleteFlag = "1";

            returnManager.Update(returnInfo);
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
        try
        {
            string strBillNo = string.Empty;
            foreach (SelectedRow row in this.rowSelectMuti.SelectedRows)
            {
                strBillNo += "'" + row.RecordID + "', ";
                EntityArrayList<PstMaterialReturn> chk = returnManager.GetListByWhere(PstMaterialReturn._.BillNo == row.RecordID);
                if (chk[0].ChkResultFlag == "1")
                    return "该单据已经审核退货";
                //判断是否在库房期间内
                DataSet ds = storageManager.GetDuration(chk[0].StorageID);
                if (chk[0].ReturnDate < Convert.ToDateTime(ds.Tables[0].Rows[0][5].ToString()))
                {
                    return "false1";
                }

                EntityArrayList<PstMaterialReturnDetail> returnDetails = returnDetailManager.GetListByWhere(PstMaterialReturnDetail._.BillNo == row.RecordID);
                for (int i = 0; i < returnDetails.Count; i++)
                {

                    PstStorage storage = pstStorageManager.GetListByWhere(PstStorage._.Barcode == returnDetails[0].Barcode && PstStorage._.StorageID == chk[0].StorageID && PstStorage._.StoragePlaceID == returnDetails[0].StoragePlaceID)[0];
                    if (storage.Num < returnDetails[i].ReturnNum)
                    {
                        return "false";
                    }
                }

                PstMaterialReturn returnInfo = returnManager.GetListByWhere(PstMaterialReturn._.BillNo == row.RecordID)[0];

                returnInfo.ChkDate = DateTime.Now;
                returnInfo.ChkPerson = this.UserID;
                returnInfo.ChkResultFlag = "1";

                returnManager.Update(returnInfo);

                //修改库存主信息
                for (int i = 0; i < returnDetails.Count; i++)
                {
                    PstStorage storage = pstStorageManager.getPstStorage(returnDetails[i].Barcode, chk[0].StorageID, returnDetails[i].StoragePlaceID, returnDetails[i].MaterCode);
                    storage.Num -= returnDetails[i].ReturnNum;
                    storage.RealWeight -= returnDetails[i].ReturnWeight;
                    if (storage.Num != 0)
                        storage.PieceWeight = Convert.ToDecimal(String.Format("{0:N3}", storage.RealWeight / storage.Num));
                    else
                        storage.PieceWeight = 0;

                    pstStorageManager.Update(storage);

                    //插入明细数据
                    PstStorageDetail storeDetail = new PstStorageDetail();
                    storeDetail.Barcode = returnDetails[i].Barcode;
                    if (storage != null)
                        storeDetail.OrderID = pstStorageDetailManager.GetOrderID(storeDetail.Barcode) + 1;
                    else
                        storeDetail.OrderID = 1;
                    storeDetail.StorageID = chk[0].StorageID;
                    storeDetail.StoragePlaceID = returnDetails[i].StoragePlaceID;
                    storeDetail.StoreInOut = "O";
                    storeDetail.RecordDate = DateTime.Now;
                    storeDetail.Num = returnDetails[i].ReturnNum;
                    storeDetail.PieceWeight = returnDetails[i].PieceWeight;
                    storeDetail.Weight = returnDetails[i].ReturnWeight;
                    //if (Convert.ToDateTime(ds.Tables[0].Rows[0][5].ToString()).Day == 1)
                    storeDetail.InaccountDuration = string.Format("{0:yyyyMM}", chk[0].ReturnDate);
                    storeDetail.InaccountDate = chk[0].ReturnDate;
                    storeDetail.BillType = "1004";
                    storeDetail.SourceBillNo = returnDetails[i].BillNo;
                    storeDetail.SourceOrderID = returnDetails[i].OrderID;

                    pstStorageDetailManager.Insert(storeDetail);
                }
            }

            pageToolBar.DoRefresh();
            return "审核成功！";
        }
        catch
        {
            return "审核失败！";
        }
    }

    [Ext.Net.DirectMethod()]
    public string btnCancelChk_Click()
    {
        try
        {
            string strBillNo = string.Empty;
            foreach (SelectedRow row in this.rowSelectMuti.SelectedRows)
            {
                strBillNo += "'" + row.RecordID + "', ";
                EntityArrayList<PstMaterialReturn> chk = returnManager.GetListByWhere(PstMaterialReturn._.BillNo == row.RecordID);
                if (chk[0].ChkResultFlag == "0")
                    return "该单据未审核，不能撤销！";
                //判断是否在库房期间内
                DataSet ds = storageManager.GetDuration(chk[0].StorageID);
                if (chk[0].ReturnDate < Convert.ToDateTime(ds.Tables[0].Rows[0][5].ToString()))
                {
                    return "false1";
                }

                EntityArrayList<PstMaterialReturnDetail> returnDetails = returnDetailManager.GetListByWhere(PstMaterialReturnDetail._.BillNo == row.RecordID);
                PstMaterialReturn returnInfo = returnManager.GetListByWhere(PstMaterialReturn._.BillNo == row.RecordID)[0];

                //returnInfo.ChkDate = DateTime.Now;
                returnInfo.ChkPerson = this.UserID;
                returnInfo.ChkResultFlag = "0";

                returnManager.Update(returnInfo);

                //修改库存主信息
                for (int i = 0; i < returnDetails.Count; i++)
                {
                    PstStorage storage = pstStorageManager.getPstStorage(returnDetails[i].Barcode, chk[0].StorageID, returnDetails[i].StoragePlaceID, returnDetails[i].MaterCode);
                    storage.Num += returnDetails[i].ReturnNum;
                    storage.RealWeight += returnDetails[i].ReturnWeight;
                    if (storage.Num != 0)
                        storage.PieceWeight = Convert.ToDecimal(String.Format("{0:N3}", storage.RealWeight / storage.Num));
                    else
                        storage.PieceWeight = 0;

                    pstStorageManager.Update(storage);

                    //删除明细数据
                    PstStorageDetail storeDetail = pstStorageDetailManager.GetListByWhere(PstStorageDetail._.Barcode == returnDetails[i].Barcode && PstStorageDetail._.StorageID == chk[0].StorageID && PstStorageDetail._.StoragePlaceID == returnDetails[i].StoragePlaceID && PstStorageDetail._.SourceBillNo == returnDetails[i].BillNo && PstStorageDetail._.SourceOrderID == returnDetails[i].OrderID)[0];
                    pstStorageDetailManager.Delete(storeDetail);
                }
            }

            pageToolBar.DoRefresh();
            return "撤销成功！";
        }
        catch
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
            //EntityArrayList<PstMaterialStoreinDetail> list = detailManager.GetListByWhere(PstMaterialStoreinDetail._.BillNo == billNo && PstMaterialStoreinDetail._.SendChkFlag == null && PstMaterialStoreinDetail._.DeleteFlag == "0");
            //if (list.Count > 0)
            //{
            //    return "不能归档：该送检单没有处理完毕，请首先处理该数据对应的明细数据！";
            //}

            //PstMaterialStorein store = manager.GetById(billNo);
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

    public void btnCancel_Click(object sender, DirectEventArgs e)
    {
        //this.winAdd.Close();
        this.winModify.Close();
    }

    public void txtBeginTime_Change(object sender, EventArgs e)
    {
        this.pageToolBar.DoRefresh();
    }
    public void txtEndTime_change(object sender, EventArgs e)
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