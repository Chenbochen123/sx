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

public partial class Manager_Storage_MaterialStoreInNew : Mesnac.Web.UI.Page
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
                this.storeDetail.DataSource = detailManager.GetByOtherBillNo(billNo, barcode, orderID);
                this.storeDetail.DataBind();
            }
            else
            {
                txtBeginTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtEndTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
                cbxFiledFlag.SelectedItem.Value = "all";
            }
        }
    }

    #region 分页相关方法
    private PageResult<PstMaterialStorein> GetPageResultData(PageResult<PstMaterialStorein> pageParams)
    {
        PstMaterialStoreinManager.QueryParams queryParams = new PstMaterialStoreinManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.billNo = txtBillNo.Text;
        queryParams.factoryID = hiddenFactoryID.Text;
        queryParams.beginDate = Convert.ToDateTime(txtBeginTime.Text);
        queryParams.endDate = Convert.ToDateTime(txtEndTime.Value);
        if (cbxFiledFlag.SelectedItem.Value != "all")
            queryParams.filedFlag = cbxFiledFlag.SelectedItem.Value;
        queryParams.hrCode = hiddenMakerPerson.Text;
        queryParams.deleteFlag = "0";

        return GetTablePageDataBySql(queryParams);
    }
    public PageResult<PstMaterialStorein> GetTablePageDataBySql(PstMaterialStoreinManager.QueryParams queryParams)
    {
        PageResult<PstMaterialStorein> pageParams = queryParams.pageParams;
        StringBuilder sqlstr = new StringBuilder();
        sqlstr.AppendLine(@"select Barcode,E.Batch, InputNum, InputWeight,  EB.StorageName, EC.StoragePlaceName, E.SourceBillNo, E.NoticeNo,
A.BillNo, BillType, C.FacName FactoryName, B.UserName, InputDate, convert(bit, LockedFlag) LockedFlag, convert(bit, FiledFlag) FiledFlag, ChkPerson, A.Remark 
                               ,ED.Materialname from
 PstMaterialStoreinDetail E
                            left join PstMaterialStorein A on A.BillNo = E.BillNo
                                left join BasUser B on A.MakerPerson = B.WorkBarcode
                                left join BasFactoryInfo C on A.FactoryID = C.ObjID
           
                             left join BasStorage EB on A.StorageID = EB.StorageID
                            left join BasStoragePlace EC on E.StoragePlaceID = EC.StoragePlaceID
                            left join BasMaterial ED on E.MaterCode = ED.MaterialCode

                                where 1 = 1");
        if (!string.IsNullOrEmpty(queryParams.billNo))
        {
            sqlstr.AppendLine(" AND A.BillNo like '%" + queryParams.billNo + "%'");
        }
        if (!string.IsNullOrEmpty(queryParams.billType))
        {
            sqlstr.AppendLine(" AND A.BillType = '" + queryParams.billType + "'");
        }
        if (!string.IsNullOrEmpty(queryParams.factoryID))
        {
            sqlstr.AppendLine(" AND A.FactoryID = '" + queryParams.factoryID + "'");
        }
        if (queryParams.beginDate != DateTime.MinValue)
            sqlstr.AppendLine(" AND A.InputDate >= '" + queryParams.beginDate.ToString() + "'");
        if (queryParams.endDate != DateTime.MinValue)
            sqlstr.AppendLine(" AND A.InputDate <= '" + queryParams.endDate.AddDays(1).ToString() + "'");
        if (!string.IsNullOrEmpty(queryParams.filedFlag))
        {
            sqlstr.AppendLine(" AND A.FiledFlag = '" + queryParams.filedFlag + "'");
        }
        if (!string.IsNullOrEmpty(queryParams.hrCode))
        {
            sqlstr.AppendLine(" AND B.HRCode = '" + queryParams.hrCode + "'");
        }
        if (!string.IsNullOrEmpty(queryParams.deleteFlag))
        {
            sqlstr.AppendLine(" AND A.DeleteFlag = '" + queryParams.deleteFlag + "'");
        }

        if ((!string.IsNullOrEmpty(TextBarcode.Text)) || (!string.IsNullOrEmpty(TextMaterial.Text)))
        {
            String sql = @" and A.BillNo in (select BillNo from PstMaterialStoreinDetail left join basmaterial on  PstMaterialStoreinDetail.matercode = basmaterial.materialcode
 where barcode like '%"
              + TextBarcode.Text + "%' and  basmaterial.materialname like '%" + TextMaterial.Text + "%')";
            sqlstr.AppendLine(sql);
        }

        if (!string.IsNullOrEmpty(TextField1.Text))
        {
            String sql = @" and A.BillNo in (select BillNo from PstMaterialStoreinDetail left join basstorage on left( PstMaterialStoreinDetail.storageplaceid ,len( PstMaterialStoreinDetail.storageplaceid)-3 )= basstorage.storageid
 where  basstorage.storagename like '%" + TextField1.Text + "%')";
            sqlstr.AppendLine(sql);
        }


        //TextMaterial.Text = sqlstr.ToString(); ;
        pageParams.QueryStr = sqlstr.ToString();
        if (pageParams.PageSize < 0)
        {
            NBear.Data.CustomSqlSection css = manager.GetBySql(sqlstr.ToString());
            pageParams.DataSet = css.ToDataSet();
            return pageParams;
        }
        else
        {
            return manager.GetPageDataBySql(pageParams);
        }
    }
    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<PstMaterialStorein> pageParams = new PageResult<PstMaterialStorein>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "InputDate DESC";

        PageResult<PstMaterialStorein> lst = GetPageResultData(pageParams);
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

        this.storeDetail.DataSource = GetByBillNo(billNo);
        this.storeDetail.DataBind();
    }

    public DataSet GetByBillNo(string BillNo)
    {
        string sql = @"select A.BillNo, Barcode, ProductNo, E.StorageID, B.StorageName, A.StoragePlaceID, C.StoragePlaceName, A.Batch,
                            A.MaterCode, D.MaterialName, ProcDate, InputNum, InputWeight, A.RecordDate, A.Remark, A.PieceWeight, A.SourceBillNo, A.SourceOrderID, A.NoticeNo
                            from PstMaterialStoreinDetail A
                            left join PstMaterialStorein E on A.BillNo = E.BillNo
                            left join BasStorage B on E.StorageID = B.StorageID
                            left join BasStoragePlace C on A.StoragePlaceID = C.StoragePlaceID
                            left join BasMaterial D on A.MaterCode = D.MaterialCode
                            where A.DeleteFlag = '0' and A.BillNo = '" + BillNo + "'  and D.MaterialName like '%" + TextMaterial.Text + "%'";
        //TextMaterial.Text = sql;
        return detailManager.GetBySql(sql).ToDataSet();
    }
    #endregion

    #region 增删改查按钮激发的事件
  

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
            PstMaterialStorein store = manager.GetById(billNo);
            store.DeleteFlag = "1";

            manager.Update(store);
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
        string strBillNo = string.Empty;
        string billNo = string.Empty;
        foreach (SelectedRow row in this.rowSelectMuti.SelectedRows)
        {
            strBillNo += "'" + row.RecordID + "', ";
            billNo = row.RecordID;
            EntityArrayList<PstMaterialStorein> chk = manager.GetListByWhere(PstMaterialStorein._.BillNo == row.RecordID);
            if (chk[0].ChkResultFlag == "1")
                return "该单据已经审核入库";
            //判断是否在库房期间内
            DataSet ds = storageManager.GetDuration(chk[0].StorageID);
            if (chk[0].InputDate < Convert.ToDateTime(ds.Tables[0].Rows[0][5].ToString()))
            {
                return "false1";
            }

            //检验合格后将数据放到库存中心明细信息表和主表
            EntityArrayList<PstMaterialStoreinDetail> storeinDetails = detailManager.GetListByWhere(PstMaterialStoreinDetail._.BillNo == row.RecordID);
            //首先将送检单中的数据减少，如果入库数量不足，弹出提示，不能入库
            for (int i = 0; i < storeinDetails.Count; i++)
            {
                //if (!string.IsNullOrEmpty(storeinDetails[i].SourceBillNo))
                //{
                //    PstMaterialChkDetail chkDetail = chkdetailManager.GetListByWhere(PstMaterialChkDetail._.BillNo == storeinDetails[i].SourceBillNo && PstMaterialChkDetail._.Barcode == storeinDetails[i].Barcode && PstMaterialChkDetail._.OrderID == storeinDetails[i].SourceOrderID)[0];
                //    if ((chkDetail.PassNum - chkDetail.StoreInNum) < storeinDetails[i].InputNum)
                //    {
                //        return "false";
                //    }
                //}
            }
            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required))
            {
                try
                {
                    for (int i = 0; i < storeinDetails.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(storeinDetails[i].SourceBillNo))
                        {
                            PstMaterialChkDetail chkDetail = chkdetailManager.GetListByWhere(PstMaterialChkDetail._.BillNo == storeinDetails[i].SourceBillNo && PstMaterialChkDetail._.Barcode == storeinDetails[i].Barcode && PstMaterialChkDetail._.OrderID == storeinDetails[i].SourceOrderID)[0];

                            chkDetail.StoreInNum = chkDetail.StoreInNum + storeinDetails[i].InputNum;
                            chkDetail.StoreInWeight = chkDetail.StoreInWeight + storeinDetails[i].InputWeight;

                            chkdetailManager.Update(chkDetail);

                            string Stockin = "0";
                            EntityArrayList<PstMaterialChkDetail> chkDetails1 = chkdetailManager.GetListByWhere(PstMaterialChkDetail._.BillNo == storeinDetails[i].SourceBillNo && PstMaterialChkDetail._.DeleteFlag == "0");
                            foreach (PstMaterialChkDetail chk1 in chkDetails1)
                            {
                                if (chk1.PassNum >= chk1.StoreInNum)
                                {
                                    Stockin = "0";
                                    break;
                                }
                                Stockin = "1";
                            }
                            PstMaterialChk chkStore = chkManager.GetById(storeinDetails[i].SourceBillNo);
                            chkStore.StockInFlag = Stockin;
                            chkManager.Update(chkStore);
                        }
                    }

                    //插入主信息
                    for (int i = 0; i < storeinDetails.Count; i++)
                    {
                        PstStorage store = new PstStorage();
                        store.Barcode = storeinDetails[i].Barcode;
                        store.ProductNo = storeinDetails[i].ProductNo;
                        store.StorageID = chk[0].StorageID;
                        store.StoragePlaceID = storeinDetails[i].StoragePlaceID;
                        store.MaterCode = storeinDetails[i].MaterCode;
                        store.ProcDate = storeinDetails[i].ProcDate;
                        store.FactoryID = chk[0].FactoryID;
                        store.ProductPlace = storeinDetails[i].ProductPlace;

                        PstStorage storage = pstStorageManager.getPstStorage(store.Barcode, store.StorageID, store.StoragePlaceID, store.MaterCode);
                        if (storage != null)
                        {
                            storage.Num += storeinDetails[i].InputNum;
                            storage.RealWeight += storeinDetails[i].InputWeight;
                            if (storage.Num != 0)
                                storage.PieceWeight = Convert.ToDecimal(String.Format("{0:N3}", storage.RealWeight / storage.Num));
                            else
                                storage.PieceWeight = 0;

                            pstStorageManager.Update(storage);
                        }
                        else
                        {
                            store.Num = storeinDetails[i].InputNum;
                            store.PieceWeight = storeinDetails[i].PieceWeight;
                            store.RealWeight = storeinDetails[i].InputWeight;
                            if (store.Num != 0)
                                store.PieceWeight = Convert.ToDecimal(String.Format("{0:N3}", store.RealWeight / store.Num));
                            else
                                store.PieceWeight = 0;
                            store.RecordDate = DateTime.Now;

                            pstStorageManager.Insert(store);
                        }

                        //插入明细数据
                        PstStorageDetail storeDetail = new PstStorageDetail();
                        storeDetail.Barcode = storeinDetails[i].Barcode;
                        if (storage != null || (storage != null && storage.RealWeight != 0))
                            storeDetail.OrderID = pstStorageDetailManager.GetOrderID(storeDetail.Barcode) + 1;
                        else
                            storeDetail.OrderID = 1;
                        storeDetail.StorageID = chk[0].StorageID;
                        storeDetail.StoragePlaceID = storeinDetails[i].StoragePlaceID;
                        storeDetail.StoreInOut = "I";
                        storeDetail.RecordDate = DateTime.Now;
                        storeDetail.Num = storeinDetails[i].InputNum;
                        storeDetail.PieceWeight = storeinDetails[i].PieceWeight;
                        storeDetail.Weight = storeinDetails[i].InputWeight;
                        //if (Convert.ToDateTime(ds.Tables[0].Rows[0][5].ToString()).Day == 1)
                        storeDetail.InaccountDuration = string.Format("{0:yyyyMM}", chk[0].InputDate);
                        storeDetail.InaccountDate = chk[0].InputDate;
                        storeDetail.BillType = "1001";
                        storeDetail.SourceBillNo = storeinDetails[i].BillNo;
                        storeDetail.SourceOrderID = storeinDetails[i].OrderID;

                        pstStorageDetailManager.Insert(storeDetail);
                        scope.Complete();
                        scope.Dispose();
                    }
                }
                catch
                {
                    scope.Dispose();
                    return "审核失败！";
                }
            }
        }

        bool result = manager.UpdateChkResultFlag(strBillNo.Remove(strBillNo.Length - 2, 1), userManager.UserID);

        if (userCtrlManager.GetItemCtrl("SapInterfaceCtrl") == "1")
        {
            try
            {
                WebReference.Service ws = new WebReference.Service();
                ws.Url = userCtrlManager.GetItemCtrl("WebServiceIP");
                if (string.IsNullOrEmpty(txtSAPBillNo.Text))
                {
                    ws.ToSapStoreinInfo(billNo, "0", "0");
                }
                else
                {
                    ws.ToSapAdjustInfo(billNo, txtSAPBillNo.Text, "0", "0");
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
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
        string strBillNo = string.Empty;
        string billNo = string.Empty;
        foreach (SelectedRow row in this.rowSelectMuti.SelectedRows)
        {
            strBillNo += "'" + row.RecordID + "', ";
            billNo = row.RecordID;
            EntityArrayList<PstMaterialStorein> chk = manager.GetListByWhere(PstMaterialStorein._.BillNo == row.RecordID);
            if (chk[0].ChkResultFlag == "0")
                return "该单据未审核，不能撤销!";
            //判断是否在库房期间内
            DataSet ds = storageManager.GetDuration(chk[0].StorageID);
            if (chk[0].InputDate < Convert.ToDateTime(ds.Tables[0].Rows[0][5].ToString()))
            {
                return "false1";
            }
            //判断库房数据是否已经使用，使用不能撤销
            EntityArrayList<PstStorageDetail> lst = pstStorageDetailManager.GetListByWhere(PstStorageDetail._.SourceBillNo == row.RecordID && PstStorageDetail._.StoreInOut == "O");
            if (lst.Count > 0)
            {
                return "false2";
            }

            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required))
            {
                try
                {
                    //检验合格后将数据放到库存中心明细信息表和主表
                    EntityArrayList<PstMaterialStoreinDetail> storeinDetails = detailManager.GetListByWhere(PstMaterialStoreinDetail._.BillNo == row.RecordID);
                    //首先判断库存数量是否大于等于入库数量
                    for (int i = 0; i < storeinDetails.Count; i++)
                    {
                        PstStorage storage = pstStorageManager.GetListByWhere(PstStorage._.StorageID == chk[0].StorageID && PstStorage._.StoragePlaceID == storeinDetails[i].StoragePlaceID && PstStorage._.Barcode == storeinDetails[i].Barcode)[0];
                        if (storage.RealWeight < storeinDetails[i].InputWeight)
                        {
                            return "false";
                        }
                    }
                    for (int i = 0; i < storeinDetails.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(storeinDetails[i].SourceBillNo))
                        {
                            PstMaterialChkDetail chkDetail = chkdetailManager.GetListByWhere(PstMaterialChkDetail._.BillNo == storeinDetails[i].SourceBillNo && PstMaterialChkDetail._.Barcode == storeinDetails[i].Barcode && PstMaterialChkDetail._.OrderID == storeinDetails[i].SourceOrderID)[0];

                            chkDetail.StoreInNum = chkDetail.StoreInNum - storeinDetails[i].InputNum;
                            chkDetail.StoreInWeight = chkDetail.StoreInWeight - storeinDetails[i].InputWeight;

                            chkdetailManager.Update(chkDetail);

                            PstMaterialChk chkStore = chkManager.GetById(storeinDetails[i].SourceBillNo);
                            chkStore.StockInFlag = "0";
                            chkManager.Update(chkStore);
                        }
                    }

                    //库存主信息和明细信息修改
                    for (int i = 0; i < storeinDetails.Count; i++)
                    {
                        PstStorage store = new PstStorage();
                        store.Barcode = storeinDetails[i].Barcode;
                        store.ProductNo = storeinDetails[i].ProductNo;
                        store.StorageID = chk[0].StorageID;
                        store.StoragePlaceID = storeinDetails[i].StoragePlaceID;
                        store.MaterCode = storeinDetails[i].MaterCode;
                        store.ProcDate = storeinDetails[i].ProcDate;
                        store.FactoryID = chk[0].FactoryID;

                        PstStorage storage = pstStorageManager.getPstStorage(store.Barcode, store.StorageID, store.StoragePlaceID, store.MaterCode);
                        if (storage != null)
                        {
                            storage.Num -= storeinDetails[i].InputNum;
                            storage.RealWeight -= storeinDetails[i].InputWeight;
                            if (storage.Num != 0)
                                storage.PieceWeight = Convert.ToDecimal(String.Format("{0:N3}", storage.RealWeight / storage.Num));
                            else
                                storage.PieceWeight = 0;

                            pstStorageManager.Update(storage);
                        }

                        //删除明细数据
                        PstStorageDetail storeDetail = pstStorageDetailManager.GetListByWhere(PstStorageDetail._.Barcode == storeinDetails[i].Barcode && PstStorageDetail._.SourceBillNo == storeinDetails[i].BillNo && PstStorageDetail._.SourceOrderID == storeinDetails[i].OrderID)[0];

                        pstStorageDetailManager.Delete(storeDetail);
                    }

                    bool result = manager.CancelChkResult(strBillNo.Remove(strBillNo.Length - 2, 1), userManager.UserID);
                    scope.Complete();
                    scope.Dispose();

                    if (userCtrlManager.GetItemCtrl("SapInterfaceCtrl") == "1")
                    {
                        try
                        {
                            WebReference.Service ws = new WebReference.Service();
                            ws.Url = userCtrlManager.GetItemCtrl("WebServiceIP");
                            if (string.IsNullOrEmpty(txtSAPBillNo.Text))
                            {
                                ws.ToSapStoreinInfo(billNo, "1", "0");
                            }
                            else
                            {
                                ws.ToSapAdjustInfo(billNo, txtSAPBillNo.Text, "1", "0");
                            }
                        }
                        catch (Exception ex)
                        {
                            return ex.ToString();
                        }
                    }

                    pageToolBar.DoRefresh();
                    return "撤销审核成功！";
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    return "撤销审核失败！";
                }
            }
        }

        return "您没有选择任何行，请选择！";
    }

    [Ext.Net.DirectMethod()]
    public string commandcolumn_direct_filed(string billNo)
    {
        try
        {
         
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
    public void txtBeginTime_change(object sender, EventArgs e)
    {
        pageToolBar.DoRefresh();
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