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

public partial class Manager_ShopStorage_ShopAdjust : Mesnac.Web.UI.Page
{
    protected PstShopAdjustManager shopAdjustManager = new PstShopAdjustManager();
    protected BasUserManager userManager = new BasUserManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest && !IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["BeginTime"]))
            {
                string beginTime = Request.QueryString["BeginTime"].ToString();
                string endTime = Request.QueryString["EndTime"].ToString();
                string barcode = Request.QueryString["Barcode"].ToString();

                txtBeginTime.Text = beginTime;
                txtEndTime.Text = endTime;
                txtBarcode.Text = barcode;
            }
            else
            {
                txtBeginTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtEndTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
                hiddenOperPerson.Text = this.UserID;
                txtOperPerson.Text = userManager.GetListByWhere(BasUser._.WorkBarcode == this.UserID)[0].UserName;
            }
            cbxChejian.Value = "all";
            cbxShift.Value = "all";
        }
    }

    private PageResult<PstShopAdjust> GetPageResultData(PageResult<PstShopAdjust> pageParams)
    {
        PstShopAdjustManager.QueryParams queryParams = new PstShopAdjustManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.storageID = hiddenStorageID.Text;
        queryParams.toStorageID = hiddenToStorageID.Text;
        queryParams.workShopCode = cbxChejian.SelectedItem.Value;
        queryParams.beginDate = Convert.ToDateTime(txtBeginTime.Text);
        queryParams.endDate = Convert.ToDateTime(txtEndTime.Text);
        queryParams.barcode = txtBarcode.Text;
        queryParams.materCode = hiddenMaterCode.Text;
        queryParams.operPerson = hiddenOperPerson.Text;
        queryParams.shiftID = cbxShift.SelectedItem.Value;
        queryParams.deleteFlag = "0";

        return shopAdjustManager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<PstShopAdjust> pageParams = new PageResult<PstShopAdjust>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "RecordDate desc";

        PageResult<PstShopAdjust> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }

    [Ext.Net.DirectMethod()]
    public string btnCancelShopAdjust_Click()
    {
        //string result = hiddenCheckStorageID.Text + ";" + hiddenCheckStoragePlaceID.Text + ";" + hiddenCheckBarcode.Text + ";" + hiddenCheckOrderID.Text;//rubberStorageManager.CancelReturnRubber(hiddenCheckStorageID.Text, hiddenCheckStoragePlaceID.Text, hiddenCheckBarcode.Text);
        string result = shopAdjustManager.CancelShopAdjust(hiddenCheckStorageID.Text, hiddenCheckStoragePlaceID.Text, hiddenCheckBarcode.Text, Convert.ToInt32(hiddenCheckOrderID.Text), this.UserID);
        pageToolBar.DoRefresh();

        return result;
    }

    protected void rowSelectMuti_SelectionChange(object sender, DirectEventArgs e)
    {
        hiddenCheckStorageID.Text = e.ExtraParams["StorageID"];
        hiddenCheckStoragePlaceID.Text = e.ExtraParams["StoragePlaceID"];
        hiddenCheckBarcode.Text = e.ExtraParams["Barcode"];
        hiddenCheckOrderID.Text = e.ExtraParams["OrderID"];
    }
}