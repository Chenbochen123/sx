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

public partial class Manager_Storage_MaterialStoreinPrint : Mesnac.Web.UI.Page
{
    protected PstMaterialStoreinManager manager = new PstMaterialStoreinManager();
    protected PstMaterialStoreinDetailManager detailManager = new PstMaterialStoreinDetailManager();

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
    private PageResult<PstMaterialStorein> GetPageResultData(PageResult<PstMaterialStorein> pageParams)
    {
        PstMaterialStoreinManager.QueryParams queryParams = new PstMaterialStoreinManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.billNo = txtBillNo.Text;
        queryParams.barcode = txtBarcode.Text;
        queryParams.beginDate = Convert.ToDateTime(txtBeginTime.Text);
        queryParams.endDate = Convert.ToDateTime(txtEndTime.Value);
        queryParams.hrCode = hiddenMakerPerson.Text;
        queryParams.userCode = this.UserID;

        return manager.GetTablePageDataBySqlPrint(queryParams);
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
    #endregion

    [Ext.Net.DirectMethod()]
    public void commandcolumn_direct_print(string billNo, string storageID, string storagePlaceID, string barcode, string orderID)
    {
        DataSet ds = manager.GetDetailInfo(billNo, storageID, storagePlaceID, barcode, orderID);
        txtBillNo1.Text = ds.Tables[0].Rows[0]["BillNo"].ToString();
        hiddenBillNo.Text = ds.Tables[0].Rows[0]["BillNo"].ToString();
        txtStorageID1.Text = ds.Tables[0].Rows[0]["StorageName"].ToString();
        hiddenStorageID.Text = ds.Tables[0].Rows[0]["StorageID"].ToString();
        txtStoragePlaceID1.Text = ds.Tables[0].Rows[0]["StoragePlaceName"].ToString();
        hiddenStoragePlaceID.Text = ds.Tables[0].Rows[0]["StoragePlaceID"].ToString();
        txtBarcode1.Text = ds.Tables[0].Rows[0]["Barcode"].ToString();
        txtMaterCode1.Text = ds.Tables[0].Rows[0]["MaterialName"].ToString();
        txtShelfPieceWeight1.Text = ds.Tables[0].Rows[0]["PieceWeight"].ToString();
        txtNum1.Text = ds.Tables[0].Rows[0]["InputNum"].ToString();
        txtWeight1.Text = ds.Tables[0].Rows[0]["InputWeight"].ToString();
        hiddenOrderID.Text = ds.Tables[0].Rows[0]["OrderID"].ToString();

        this.winSet.Show();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtBillNo1.Text = string.Empty;
        hiddenBillNo.Text = string.Empty;
        txtStorageID1.Text = string.Empty;
        hiddenStorageID.Text = string.Empty;
        txtStoragePlaceID1.Text = string.Empty;
        hiddenStoragePlaceID.Text = string.Empty;
        txtBarcode1.Text = string.Empty;
        txtMaterCode1.Text = string.Empty;
        txtShelfPieceWeight1.Text = string.Empty;
        txtNum1.Text = string.Empty;
        txtWeight1.Text = string.Empty;
        hiddenOrderID.Text = string.Empty;

        this.winSet.Close();
    }

    public void txtShelfPieceWeight1_Change(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(txtWeight1.Text) && !string.IsNullOrEmpty(txtWeight1.Text))
                txtNum1.Text = Math.Round(Convert.ToDecimal(txtWeight1.Text) / Convert.ToDecimal(txtShelfPieceWeight1.Text), 0).ToString();
        }
        catch { }
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