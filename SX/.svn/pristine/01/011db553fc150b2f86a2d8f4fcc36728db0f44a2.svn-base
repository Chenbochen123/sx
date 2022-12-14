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

public partial class Manager_ShopStorage_MaterialShopRubberPrint : Mesnac.Web.UI.Page
{
    protected PstShopStorageManager shopStorageManager = new PstShopStorageManager();
    protected PstShopStorageDetailManager shopStorageDetailManager = new PstShopStorageDetailManager();
    protected PstMaterialInOastManager inOastManager = new PstMaterialInOastManager();
    protected PstMaterialRubberSplitManager splitManager = new PstMaterialRubberSplitManager();
    protected BasUserManager userManager = new BasUserManager();

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
            cbxIsPrint.Value = "0";
            cbxChejian.Value = "all";
            hiddenOperPerson.Text = this.UserID;
            txtOperPerson.Text = userManager.GetListByWhere(BasUser._.WorkBarcode == this.UserID)[0].UserName;
        }
    }

    #region 分页相关方法
    private PageResult<PstMaterialRubberSplit> GetPageResultData(PageResult<PstMaterialRubberSplit> pageParams)
    {
        PstMaterialRubberSplitManager.QueryParams queryParams = new PstMaterialRubberSplitManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.barcode = Server.HtmlEncode(txtBarcode.Text);
        queryParams.materCode = hiddenMaterCode.Text;
        queryParams.operPerson = hiddenOperPerson.Text;
        queryParams.beginDate = Convert.ToDateTime(txtBeginTime.Text);
        queryParams.endDate = Convert.ToDateTime(txtEndTime.Value);
        queryParams.storageID = hiddenStorageID.Text;
        queryParams.storagePlaceID = hiddenStoragePlaceID.Text;
        queryParams.isPrinted = cbxIsPrint.Value.ToString();
        queryParams.chejianCode = cbxChejian.SelectedItem.Value;
        queryParams.splitbarcode = Server.HtmlEncode(txtSplitCode.Text);
        return splitManager.GetTablePageDataBySqlPrint(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<PstMaterialRubberSplit> pageParams = new PageResult<PstMaterialRubberSplit>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        //pageParams.Orderfld = "OperTime DESC";

        PageResult<PstMaterialRubberSplit> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }
    #endregion

    [Ext.Net.DirectMethod()]
    public void commandcolumn_direct_print(string barcode, string barcodeSplit)
    {
        DataSet ds = splitManager.GetByPrintInfo(barcodeSplit);
        txtStorageID1.Text = ds.Tables[0].Rows[0]["StorageName"].ToString();
        hiddenStorageID.Text = ds.Tables[0].Rows[0]["StorageID"].ToString();
        txtStoragePlaceID1.Text = ds.Tables[0].Rows[0]["StoragePlaceName"].ToString();
        hiddenStoragePlaceID.Text = ds.Tables[0].Rows[0]["StoragePlaceID"].ToString();
        txtBarcode1.Text = ds.Tables[0].Rows[0]["BarcodeSplit"].ToString();
        txtMaterCode1.Text = ds.Tables[0].Rows[0]["MaterialName"].ToString();
        hiddenMaterCode.Text = ds.Tables[0].Rows[0]["MaterCode"].ToString();
        txtWeight1.Text = ds.Tables[0].Rows[0]["Weight"].ToString();
        hiddenIsPrint.Text = ds.Tables[0].Rows[0]["IsPrint"].ToString();
        txtProductNo1.Text = ds.Tables[0].Rows[0]["LLProductNo"].ToString();

        this.winSet.Show();
    }

    public string IsPass(string barcodes, string storageIDs, string storagePlaceIDs)
    {
        //判断是否是同一批次
        string[] bcArray = barcodes.Split(',');
        List<string> listbcArr = new List<string>();
        for (int i = 0; i < bcArray.Length; i++)
        {
            if (!listbcArr.Contains(bcArray[i]))
            {
                listbcArr.Add(bcArray[i]);
            }
        }
        if (listbcArr.Count > 1)
        {
            return "必须选择同一批次才能批量";
        }
        //判断是否是同一库房和库位
        string[] sArray = storageIDs.Split(',');
        List<string> listsArr = new List<string>();
        for (int i = 0; i < sArray.Length; i++)
        {
            if (!listsArr.Contains(sArray[i]))
            {
                listsArr.Add(sArray[i]);
            }
        }
        if (listsArr.Count > 1)
        {
            return "必须选择同一库房下的批次才能批量";
        }

        string[] spArray = storagePlaceIDs.Split(',');
        List<string> listspArr = new List<string>();
        for (int i = 0; i < spArray.Length; i++)
        {
            if (!listspArr.Contains(spArray[i]))
            {
                listspArr.Add(spArray[i]);
            }
        }
        if (listspArr.Count > 1)
        {
            return "必须选择同一库位下的批次才能批量";
        }

        return "";
    }

    [Ext.Net.DirectMethod()]
    public string BatchScreenWinBatchPrint(string barcodes, string storageIDs, string storagePlaceIDs)
    {
        string isPass = IsPass(barcodes, storageIDs, storagePlaceIDs);
        if (!string.IsNullOrEmpty(isPass))
        {
            return isPass + "打印！";
        }

        //设置hiddenStorageID和hiddenStoragePlaceID值
        string[] sArray = storageIDs.Split(',');
        string[] spArray = storagePlaceIDs.Split(',');
        hiddenStorageID.Text = sArray[0];
        hiddenStoragePlaceID.Text = spArray[0];

        this.winBatch.Show();
        return "OK";
    }

    [Ext.Net.DirectMethod()]
    public void BatchSaveInStorageTime(string barcodes, string llProductNo)
    {
        string[] sBarcodeArray = barcodes.Split(',');
        for (int i = 0; i < sBarcodeArray.Length; i++)
        {
            SaveInStorageTime(sBarcodeArray[i].ToString(), llProductNo);
        }
        txtHouseNo2.Text = string.Empty;
        txtMark2.Text = string.Empty;
        //txtNum2.Text = string.Empty;
        this.winBatch.Close();
        this.pageToolBar.DoRefresh();
    }

    [Ext.Net.DirectMethod()]
    public void SaveInStorageTime(string barcodeSplit, string llProductNo)
    {
        //将人工输入的批次号存到车间库存主表
        EntityArrayList<PstShopStorage> shopList;
        if (barcodeSplit.Length == 22)
            shopList = shopStorageManager.GetListByWhere(PstShopStorage._.Barcode == barcodeSplit.Substring(0, 18) && PstShopStorage._.StorageID == hiddenStorageID.Text && PstShopStorage._.StoragePlaceID == hiddenStoragePlaceID.Text);
        else
            shopList = shopStorageManager.GetListByWhere(PstShopStorage._.Barcode == barcodeSplit.Substring(0, 21) && PstShopStorage._.StorageID == hiddenStorageID.Text && PstShopStorage._.StoragePlaceID == hiddenStoragePlaceID.Text);
        int i = 0;
        //if (string.IsNullOrEmpty(shopList[0].LLProductNo) && int.TryParse(llProductNo, out i))
        //{
        //    shopList[0].LLProductNo = DateTime.Now.Year.ToString() + "-" + llProductNo;

        //    shopStorageManager.Update(shopList[0]);
        //}
        if (int.TryParse(llProductNo, out i))
        {
            shopList[0].LLProductNo = DateTime.Now.Year.ToString() + "-" + llProductNo;

            shopStorageManager.Update(shopList[0]);
        }
        //将打印的数据存到PstMaterialRubberSplit表，首先判断表中是否有该批次的信息，如果有增加补打时间记录；没有插入新数据
        EntityArrayList<PstMaterialRubberSplit> list = splitManager.GetListByWhere(PstMaterialRubberSplit._.BarcodeSplit == barcodeSplit);
        if (list.Count > 0)
        {
            list[0].PrintTime = list[0].PrintTime + DateTime.Now.ToString() + ";";

            splitManager.Update(list[0]);
        }
        else
        {
            list[0].PrintTime = DateTime.Now.ToString() + ";";
            list[0].PrintPerson = this.UserID;

            splitManager.Update(list[0]);
        }
        if (hiddenIsBatchPrint.Text == "0")
        {
            ClearControls();
            this.winSet.Close();
            this.pageToolBar.DoRefresh();
        }
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
        //txtNum1.Text = string.Empty;
        txtWeight1.Text = string.Empty;
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
        if (cbxIsPrint.SelectedItem.Value == "0")
            btnDelete.Hidden = false;
        else
            btnDelete.Hidden = true;
        pageToolBar.DoRefresh();
    }

    [Ext.Net.DirectMethod()]
    public string btnBatchDelete_Click(string barcodes, string barcodeSplits, string storageIDs, string storagePlaceIDs)
    {
        string isPass = IsPass(barcodes, storageIDs, storagePlaceIDs);
        if (!string.IsNullOrEmpty(isPass))
        {
            return isPass + "删除！";
        }

        string[] sArray = storageIDs.Split(',');
        string[] spArray = storagePlaceIDs.Split(',');
        string storageID = sArray[0];
        string storagePlaceID = spArray[0];

        string[] bcsArray = barcodeSplits.Split(',');

        for (int i = 0; i < bcsArray.Length; i++)
        {
            splitManager.CancelBarcodeSplit(storageID, storagePlaceID, bcsArray[i]);
        }

        //foreach (SelectedRow row in this.rowSelectMuti.SelectedRows)
        //{
        //    splitManager.CancelBarcodeSplit(storageID, storagePlaceID, row.RecordID);
        //}

        pageToolBar.DoRefresh();
        return "OK";
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