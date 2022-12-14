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

public partial class Manager_ShopStorage_MaterialShopStoragePrint : Mesnac.Web.UI.Page
{
    protected PstShopStorageManager shopStorageManager = new PstShopStorageManager();
    protected PstShopStorageDetailManager shopStorageDetailManager = new PstShopStorageDetailManager();
    protected PstMaterialInOastManager inOastManager = new PstMaterialInOastManager();

    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查询 = new SysPageAction() { ActionID = 1, ActionName = "btnSearch" };
        }
        public SysPageAction 查询 { get; private set; } //必须为 public
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest && !IsPostBack)
        {
            txtBeginTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtEndTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
    }

    #region 分页相关方法
    private PageResult<PstShopStorageDetail> GetPageResultData(PageResult<PstShopStorageDetail> pageParams)
    {
        PstShopStorageDetailManager.QueryParams queryParams = new PstShopStorageDetailManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.barcode = Server.HtmlEncode(txtBarcode.Text);
        queryParams.materCode = hiddenMaterCode.Text;
        queryParams.beginDate = Convert.ToDateTime(txtBeginTime.Text);
        queryParams.endDate = Convert.ToDateTime(txtEndTime.Value);
        queryParams.storageID = hiddenStorageID.Text;
        queryParams.storagePlaceID = hiddenStoragePlaceID.Text;
        queryParams.isScreenPrinted = cbxIsPrint.Checked;

        return shopStorageDetailManager.GetTablePageDataBySqlPrint(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<PstShopStorageDetail> pageParams = new PageResult<PstShopStorageDetail>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "InaccountDate DESC";

        PageResult<PstShopStorageDetail> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }
    #endregion

    [Ext.Net.DirectMethod()]
    public void commandcolumn_direct_print(string barcode, string storageID, string storagePlaceID, string orderID)
    {
        DataSet ds = shopStorageDetailManager.GetByPrintInfo(barcode, storageID, storagePlaceID, orderID);
        txtStorageID1.Text = ds.Tables[0].Rows[0]["StorageName"].ToString();
        hiddenStorageID.Text = ds.Tables[0].Rows[0]["StorageID"].ToString();
        txtStoragePlaceID1.Text = ds.Tables[0].Rows[0]["StoragePlaceName"].ToString();
        hiddenStoragePlaceID.Text = ds.Tables[0].Rows[0]["StoragePlaceID"].ToString();
        txtBarcode1.Text = ds.Tables[0].Rows[0]["Barcode"].ToString();
        txtMaterCode1.Text = ds.Tables[0].Rows[0]["MaterialName"].ToString();
        hiddenMaterCode.Text = ds.Tables[0].Rows[0]["MaterialCode"].ToString();
        txtWeight1.Text = ds.Tables[0].Rows[0]["Weight"].ToString();
        hiddenOrderID.Text = ds.Tables[0].Rows[0]["OrderID"].ToString();
        hiddenIsPrint.Text = ds.Tables[0].Rows[0]["IsPrint"].ToString();

        this.winSet.Show();
    }

    [Ext.Net.DirectMethod()]
    public void BatchScreenWinBatchPrint()
    {
        this.winBatch.Show();
    }

    [Ext.Net.DirectMethod()]
    public void BatchSaveInStorageTime(string barcodes, string storages, string storageplaces, string matercodes)
    {
        string[] sBarcodeArray = barcodes.Split(',');
        string[] sstorageArray = storages.Split(',');
        string[] sstorageplaceArray = storageplaces.Split(',');
        string[] smatercodeArray = matercodes.Split(',');
        for (int i = 0; i < sBarcodeArray.Length; i++)
        {
            SaveInStorageTime(sBarcodeArray[i].ToString(), sstorageArray[i].ToString(), sstorageplaceArray[i].ToString(), smatercodeArray[0].ToString());
        }
        txtHouseNo2.Text = string.Empty;
        txtMark2.Text = string.Empty;
        txtNum2.Text = string.Empty;
        this.winBatch.Close();
    }

    [Ext.Net.DirectMethod()]
    public void SaveInStorageTime(string barcode, string storageID, string storagePlaceID, string materCode)
    {
        //将打印的数据存到PstMaterialInOast表，首先判断表中是否有该批次的信息，如果有增加补打时间记录；没有插入新数据
        EntityArrayList<PstMaterialInOast> list = inOastManager.GetListByWhere(PstMaterialInOast._.Barcode == barcode && PstMaterialInOast._.StorageID == storageID && PstMaterialInOast._.StoragePlaceID == storagePlaceID);
        if (list.Count > 0)
        {
            list[0].PrintTime = list[0].PrintTime + DateTime.Now.ToString() + ";";

            inOastManager.Update(list[0]);
        }
        else
        {
            PstMaterialInOast inOast = new PstMaterialInOast();
            inOast.Barcode = barcode;
            inOast.StorageID = storageID;
            inOast.StoragePlaceID = storagePlaceID;
            inOast.MaterCode = materCode;
            inOast.InTime = DateTime.Now;
            inOast.PrintTime = DateTime.Now.ToString() + ";";

            inOastManager.Insert(inOast);
        }

        ClearControls();
        this.winSet.Close();
        this.pageToolBar.DoRefresh();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();

        this.winSet.Close();
        this.winBatch.Close();
    }

    protected void ClearControls()
    {
        txtStorageID1.Text = string.Empty;
        hiddenStorageID.Text = string.Empty;
        txtStoragePlaceID1.Text = string.Empty;
        hiddenStoragePlaceID.Text = string.Empty;
        txtBarcode1.Text = string.Empty;
        txtMaterCode1.Text = string.Empty;
        hiddenMaterCode.Text = string.Empty;
        txtNum1.Text = string.Empty;
        txtWeight1.Text = string.Empty;
        hiddenOrderID.Text = string.Empty;
        txtHouseNo1.Text = string.Empty;
        txtMark1.Text = string.Empty;
    }

    public void txtBeginTime_change(object sender, EventArgs e)
    {
        pageToolBar.DoRefresh();
    }
    public void txtEndTime_change(object sender, EventArgs e)
    {
        pageToolBar.DoRefresh();
    }
    public void cbxIsPrint_Change(object sender, EventArgs e)
    {
        pageToolBar.DoRefresh();
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