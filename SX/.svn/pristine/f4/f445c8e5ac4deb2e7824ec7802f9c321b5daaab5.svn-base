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

public partial class Manager_Storage_MaterialStoreOut : Mesnac.Web.UI.Page
{
    private PstMaterialChkManager manager = new PstMaterialChkManager();
    private BasFactoryInfoManager facManager = new BasFactoryInfoManager();
    private BasMaterialManager materManager = new BasMaterialManager();
    private PstMaterialChkDetailManager detailManager = new PstMaterialChkDetailManager();
    private BasUserManager userManager = new BasUserManager();
    private BasStorageManager stManager = new BasStorageManager();
    protected SysUserCtrlManager userCtrlManager = new SysUserCtrlManager();

    protected PstStorageManager storageManager = new PstStorageManager();
    protected PstStorageDetailManager storageDetailManager = new PstStorageDetailManager();
    protected PstMaterialStoreoutManager storeoutManager = new PstMaterialStoreoutManager();
    protected PstMaterialStoreoutDetailManager storeoutDetailManager = new PstMaterialStoreoutDetailManager();

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
                this.storeDetail.DataSource = storeoutDetailManager.GetByOtherBillNo(billNo, barcode, orderID);
                this.storeDetail.DataBind();
            }
            else
            {
                txtBeginTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtEndTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
                cbxLockedFlag.SelectedItem.Value = "0";
            }
        }
    }

    #region 分页相关方法
    private PageResult<PstMaterialStoreout> GetPageResultData(PageResult<PstMaterialStoreout> pageParams)
    {
        PstMaterialStoreoutManager.QueryParams queryParams = new PstMaterialStoreoutManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.billNo = txtBillNo.Text;
        queryParams.beginDate = Convert.ToDateTime(txtBeginTime.Text);
        queryParams.endDate = Convert.ToDateTime(txtEndTime.Value);
        queryParams.hrCode = hiddenMakerPerson.Text;

        if (cbxLockedFlag.SelectedItem.Value != "all")
            queryParams.lockedFlag = cbxLockedFlag.SelectedItem.Value;
        //queryParams.deleteFlag = "0";

        return storeoutManager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<PstMaterialStoreout> pageParams = new PageResult<PstMaterialStoreout>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "OutputDate DESC";

        PageResult<PstMaterialStoreout> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }

    protected void RowSelect(object sender, StoreReadDataEventArgs e)
    {
        string billNo = e.Parameters["BillNo"];

        this.storeDetail.DataSource = storeoutDetailManager.GetByBillNo(billNo);
        this.storeDetail.DataBind();
    }
    #endregion

    #region 增删改查按钮激发的事件
    [Ext.Net.DirectMethod()]
    public string btnBatchLocked_Click()
    {
        string SapUserCtrl = userCtrlManager.GetItemCtrl("SapInterfaceCtrl");
        if (SapUserCtrl == "1" && string.IsNullOrEmpty(txtSAPBillNo.Text))
        {
            return "SAP单号不能为空！";
        }
        string strBillNo = string.Empty;
        string billNo = string.Empty;
        EntityArrayList<PstMaterialStoreout> chk = null;
        foreach (SelectedRow row in this.rowSelectMuti.SelectedRows)
        {
            strBillNo += "'" + row.RecordID + "', ";
            billNo = row.RecordID;

            chk = storeoutManager.GetListByWhere(PstMaterialStoreout._.BillNo == row.RecordID);
            if (chk[0].LockedFlag == "1")
                return "该单据已经审核出库";

            //判断是否在库房期间内
            DataSet ds = stManager.GetDuration(chk[0].StorageID);
            if (chk[0].OutputDate < Convert.ToDateTime(ds.Tables[0].Rows[0][5].ToString()))
            {
                return "false1";
            }
        }        

        //修改库存量并插入明细数据，首先判断库存数量是否满足出库的数量和重量要求
        string storageID = storeoutManager.GetListByWhere(PstMaterialStoreout._.BillNo == billNo)[0].StorageID;
        EntityArrayList<PstMaterialStoreoutDetail> details = storeoutDetailManager.GetListByWhere(PstMaterialStoreoutDetail._.BillNo == billNo);

        for (int i = 0; i < details.Count; i++)
        {
            PstStorage storage = storageManager.GetListByWhere(PstStorage._.Barcode == details[i].Barcode && PstStorage._.StorageID == storageID && PstStorage._.StoragePlaceID == details[i].StoragePlaceID && PstStorage._.MaterCode == details[i].MaterCode)[0];
            if (storage.Num < details[i].OutputNum || storage.RealWeight < details[i].OutputWeight)
            {
                return "false";
            }
        }
        for (int i = 0; i < details.Count; i++)
        {
            PstStorage storage = storageManager.GetListByWhere(PstStorage._.Barcode == details[i].Barcode && PstStorage._.StorageID == storageID && PstStorage._.StoragePlaceID == details[i].StoragePlaceID && PstStorage._.MaterCode == details[i].MaterCode)[0];

            storage.Num = storage.Num - details[i].OutputNum;
            if (storage.Num == details[i].OutputNum)
                storage.RealWeight = 0;
            else
                storage.RealWeight = storage.RealWeight - details[i].OutputWeight;
            if (storage.Num != 0)
                storage.PieceWeight = Convert.ToDecimal(String.Format("{0:N3}", storage.RealWeight / storage.Num));
            else
                storage.PieceWeight = 0;
            storageManager.Update(storage);

            PstStorageDetail detail = new PstStorageDetail();
            detail.Barcode = storage.Barcode;
            detail.OrderID = storageDetailManager.GetOrderID(detail.Barcode) + 1;
            detail.StorageID = storage.StorageID;
            detail.StoragePlaceID = storage.StoragePlaceID;
            detail.StoreInOut = "O";
            detail.RecordDate = DateTime.Now;
            detail.Num = details[i].OutputNum;
            detail.PieceWeight = details[i].PieceWeight;
            if (storage.Num == details[i].OutputNum)
                detail.Weight = storage.RealWeight;
            else
                detail.Weight = details[i].OutputWeight;
            detail.InaccountDuration = string.Format("{0:yyyyMM}", chk[0].OutputDate);
            detail.InaccountDate = chk[0].OutputDate;
            detail.BillType = "1002";
            detail.SourceBillNo = details[i].BillNo;
            detail.SourceOrderID = details[i].OrderID;
            //detail.Remark = this.userManager.UserID + "出库";

            storageDetailManager.Insert(detail);
        }

        bool result = storeoutManager.UpdateLockedFlag(strBillNo.Remove(strBillNo.Length - 2, 1), this.UserID);

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
            return "出库成功！";
        }
        else
        {
            return "出库失败！";
        }
    }

    [Ext.Net.DirectMethod()]
    public string btnCancelLocked_Click()
    {
        string SapUserCtrl = userCtrlManager.GetItemCtrl("SapInterfaceCtrl");
        if (SapUserCtrl == "1" && string.IsNullOrEmpty(txtSAPBillNo.Text))
        {
            return "SAP单号不能为空！";
        }
        string strBillNo = string.Empty;
        string billNo = string.Empty;
        EntityArrayList<PstMaterialStoreout> chk = null;
        foreach (SelectedRow row in this.rowSelectMuti.SelectedRows)
        {
            strBillNo += "'" + row.RecordID + "', ";
            billNo = row.RecordID;

            chk = storeoutManager.GetListByWhere(PstMaterialStoreout._.BillNo == row.RecordID);
            if (chk[0].LockedFlag == "0")
                return "该单据还未出库，不能撤销审核!";

            //判断是否在库房期间内
            DataSet ds = stManager.GetDuration(chk[0].StorageID);
            if (chk[0].OutputDate < Convert.ToDateTime(ds.Tables[0].Rows[0][5].ToString()))
            {
                return "false1";
            }
        }        

        //增加库存量并删除库存明细数据
        string storageID = storeoutManager.GetListByWhere(PstMaterialStoreout._.BillNo == billNo)[0].StorageID;
        EntityArrayList<PstMaterialStoreoutDetail> details = storeoutDetailManager.GetListByWhere(PstMaterialStoreoutDetail._.BillNo == billNo);

        for (int i = 0; i < details.Count; i++)
        {
            PstStorage storage = storageManager.GetListByWhere(PstStorage._.Barcode == details[i].Barcode && PstStorage._.StorageID == storageID && PstStorage._.StoragePlaceID == details[i].StoragePlaceID && PstStorage._.MaterCode == details[i].MaterCode)[0];

            storage.Num = storage.Num + details[i].OutputNum;
            storage.RealWeight = storage.RealWeight + details[i].OutputWeight;
            if (storage.Num != 0)
                storage.PieceWeight = Convert.ToDecimal(String.Format("{0:N3}", storage.RealWeight / storage.Num));
            else
                storage.PieceWeight = 0;
            storageManager.Update(storage);

            PstStorageDetail detail = storageDetailManager.GetListByWhere(PstStorageDetail._.Barcode == details[i].Barcode && PstStorageDetail._.StorageID == storageID && PstStorageDetail._.StoragePlaceID == details[i].StoragePlaceID && PstStorageDetail._.SourceBillNo == details[i].BillNo && PstStorageDetail._.SourceOrderID == details[i].OrderID)[0];

            storageDetailManager.Delete(detail);
        }

        bool result = storeoutManager.CancelLocked(strBillNo.Remove(strBillNo.Length - 2, 1), this.UserID);

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
            return "撤销成功！";
        }
        else
        {
            return "撤销失败！";
        }
    }

    [Ext.Net.DirectMethod()]
    public string commandcolumn_direct_delete(string billNo)
    {
        try
        {
            PstMaterialStoreout store = storeoutManager.GetById(billNo);
            store.DeleteFlag = "1";

            storeoutManager.Update(store);
            pageToolBar.DoRefresh();
        }
        catch (Exception e)
        {
            return "删除失败：" + e;
        }
        return "删除成功！";
    }

    [Ext.Net.DirectMethod()]
    public string commandcolumn_direct_filed(string billNo)
    {
        try
        {
            //判断是否可以归档，根据是该数据所对应的明细数据全部全部处理完毕，SendChkFlag标志为1
            EntityArrayList<PstMaterialStoreout> list = storeoutManager.GetListByWhere(PstMaterialStoreout._.BillNo == billNo && PstMaterialStoreout._.DeleteFlag == "0");
            if (list[0].ChkResultFlag != "1")
            {
                return "该单据没有审核，不能归档！";
            }

            list[0].FiledFlag = "1";

            storeoutManager.Update(list[0]);
            pageToolBar.DoRefresh();
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
    public void cbxLockedFlag_change(object sender, EventArgs e)
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