using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using Ext.Net;
using Mesnac.Business.Interface;
using Mesnac.Business.Implements;
using Mesnac.Data.Components;
using Mesnac.Entity;

public partial class Manager_BasicInfo_CommonPage_QueryBasStoragePlace : Mesnac.Web.UI.Page
{
    #region 属性注入
    protected BasStoragePlaceManager manager = new BasStoragePlaceManager();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    private PageResult<BasStoragePlace> GetPageResultData(PageResult<BasStoragePlace> pageParams)
    {
        BasStoragePlaceManager.QueryParams queryParams = new BasStoragePlaceManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.storagePlaceName = txtStoragePlaceName.Text;
        string storageID = string.Empty;
        if (!string.IsNullOrEmpty(Request.QueryString["StorageID"]))
            queryParams.storageID = Request.QueryString["StorageID"].ToString();
        string storageType = string.Empty;
        if (!string.IsNullOrEmpty(Request.QueryString["StorageType"]))
            queryParams.storageType = Request.QueryString["StorageType"].ToString();
        queryParams.lockFlag = "1";
        queryParams.cancelFlag = "0";
        return manager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<BasStoragePlace> pageParams = new PageResult<BasStoragePlace>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "ObjID ASC";

        PageResult<BasStoragePlace> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }
}