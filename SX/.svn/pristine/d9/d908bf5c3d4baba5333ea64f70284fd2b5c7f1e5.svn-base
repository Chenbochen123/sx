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

public partial class Manager_Storage_MaterialStorageAdjusting : Mesnac.Web.UI.Page
{
    private PstMaterialChkManager manager = new PstMaterialChkManager();
    private BasFactoryInfoManager facManager = new BasFactoryInfoManager();
    private BasMaterialManager materManager = new BasMaterialManager();
    private PstMaterialChkDetailManager detailManager = new PstMaterialChkDetailManager();
    private BasUserManager userManager = new BasUserManager();
    private BasStorageManager stManager = new BasStorageManager();
    protected PstStorageManager storageManager = new PstStorageManager();
    protected PstStorageDetailManager storageDetailManager = new PstStorageDetailManager();
    protected PstMaterialStoreoutManager storeoutManager = new PstMaterialStoreoutManager();
    protected PstMaterialStoreoutDetailManager storeoutDetailManager = new PstMaterialStoreoutDetailManager();
    protected PstMaterialInventoryManager inventoryManager = new PstMaterialInventoryManager();
    protected PstMaterialInventoryDetailManager inventoryDetailManager = new PstMaterialInventoryDetailManager();
    protected PstMaterialAdjustingManager adjustingManager = new PstMaterialAdjustingManager();
    protected PstMaterialAdjustingDetailManager adjustingDetailManager = new PstMaterialAdjustingDetailManager();

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
                this.storeDetail.DataSource = adjustingDetailManager.GetByOtherBillNo(billNo, barcode, orderID);
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
    private PageResult<PstMaterialAdjusting> GetPageResultData(PageResult<PstMaterialAdjusting> pageParams)
    {
        PstMaterialAdjustingManager.QueryParams queryParams = new PstMaterialAdjustingManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.billNo = txtBillNo.Text;
        queryParams.beginDate = Convert.ToDateTime(txtBeginTime.Text);
        queryParams.endDate = Convert.ToDateTime(txtEndTime.Value);
        queryParams.storageID = hiddenStorageID.Text;
        if (cbxChkResultFlag.SelectedItem.Value != "all")
            queryParams.chkResultFlag = cbxChkResultFlag.SelectedItem.Value;
        queryParams.hrCode = hiddenMakerPerson.Text;
        //queryParams.deleteFlag = "0";

        return adjustingManager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<PstMaterialAdjusting> pageParams = new PageResult<PstMaterialAdjusting>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "AdjustingDate DESC";

        PageResult<PstMaterialAdjusting> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }

    protected void RowSelect(object sender, StoreReadDataEventArgs e)
    {
        string billNo = e.Parameters["BillNo"];

        this.storeDetail.DataSource = adjustingDetailManager.GetByBillNo(billNo);
        this.storeDetail.DataBind();
    }
    #endregion

    #region 增删改查按钮激发的事件
    [Ext.Net.DirectMethod()]
    public string btnBatchResult_Click()
    {
        string strBillNo = string.Empty;
        string billNo = string.Empty;
        EntityArrayList<PstMaterialAdjusting> adjusting = null;
        foreach (SelectedRow row in this.rowSelectMuti.SelectedRows)
        {
            strBillNo += "'" + row.RecordID + "', ";
            billNo = row.RecordID;

            adjusting = adjustingManager.GetListByWhere(PstMaterialAdjusting._.BillNo == row.RecordID);
            if (adjusting[0].ChkResultFlag == "1")
                return "该单据已经审核调整";

            //判断是否在库房期间内
            DataSet ds = stManager.GetDuration(adjusting[0].StorageID);
            if (adjusting[0].AdjustingDate < Convert.ToDateTime(ds.Tables[0].Rows[0][5].ToString()))
            {
                return "false1";
            }
        }        

        //修改库存量并插入明细数据，首先判断库存数量是否满足调整的数量和重量要求
        string storageID = adjustingManager.GetListByWhere(PstMaterialAdjusting._.BillNo == billNo)[0].StorageID;
        EntityArrayList<PstMaterialAdjustingDetail> details = adjustingDetailManager.GetListByWhere(PstMaterialAdjustingDetail._.BillNo == billNo);

        for (int i = 0; i < details.Count; i++)
        {
            PstStorage storage = storageManager.GetListByWhere(PstStorage._.Barcode == details[i].Barcode && PstStorage._.StorageID == storageID && PstStorage._.StoragePlaceID == details[i].StoragePlaceID && PstStorage._.MaterCode == details[i].MaterCode)[0];
            if (details[i].DecreaseOrAddFlag == "2" && (storage.Num < details[i].AdjustingNum || storage.RealWeight < details[i].AdjustingWeight))
            {
                return "false";
            }
        }
        for (int i = 0; i < details.Count; i++)
        {
            PstStorage storage = storageManager.GetListByWhere(PstStorage._.Barcode == details[i].Barcode && PstStorage._.StorageID == storageID && PstStorage._.StoragePlaceID == details[i].StoragePlaceID && PstStorage._.MaterCode == details[i].MaterCode)[0];

            int DecreaseOrAdd = details[i].DecreaseOrAddFlag == "1" ? 1 : -1;
            storage.Num = storage.Num + details[i].AdjustingNum * DecreaseOrAdd;
            storage.RealWeight = storage.RealWeight + details[i].AdjustingWeight * DecreaseOrAdd;
            storage.PieceWeight = storage.RealWeight / storage.Num;
            storageManager.Update(storage);

            PstStorageDetail detail = new PstStorageDetail();
            detail.Barcode = storage.Barcode;
            detail.OrderID = storageDetailManager.GetOrderID(detail.Barcode) + 1;
            detail.StorageID = storage.StorageID;
            detail.StoragePlaceID = storage.StoragePlaceID;
            detail.StoreInOut = details[i].DecreaseOrAddFlag == "1" ? "I" : "O";
            detail.RecordDate = DateTime.Now;
            detail.Num = details[i].AdjustingNum;
            detail.PieceWeight = details[i].PieceWeight;
            detail.Weight = details[i].AdjustingWeight;
            detail.InaccountDuration = string.Format("{0:yyyyMM}", adjusting[0].AdjustingDate);
            detail.InaccountDate = adjusting[0].AdjustingDate;
            detail.BillType = "1006";
            //detail.Remark = this.userManager.UserID + "出库";

            storageDetailManager.Insert(detail);
        }

        bool result = adjustingManager.UpdateChkResultFlag(strBillNo.Remove(strBillNo.Length - 2, 1), this.UserID);
        if (result == true)
        {
            pageToolBar.DoRefresh();
            return "调整成功！";
        }
        else
        {
            return "调整失败！";
        }
    }

    [Ext.Net.DirectMethod()]
    public string commandcolumn_direct_delete(string billNo)
    {
        try
        {
            PstMaterialAdjusting adjusting = adjustingManager.GetById(billNo);
            adjusting.DeleteFlag = "1";

            adjustingManager.Update(adjusting);
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
        try
        {
            //判断是否可以归档，根据是该数据所对应的明细数据全部全部处理完毕，SendChkFlag标志为1
            EntityArrayList<PstMaterialChkDetail> list = detailManager.GetListByWhere(PstMaterialChkDetail._.BillNo == billNo && PstMaterialChkDetail._.SendChkFlag == null && PstMaterialChkDetail._.DeleteFlag == "0");
            if (list.Count > 0)
            {
                return "不能归档：该单据没有处理完毕，请首先处理该数据对应的明细数据！";
            }

            PstMaterialChk store = manager.GetById(billNo);
            store.FiledFlag = "1";

            manager.Update(store);
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