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

public partial class Manager_BasicInfo_CommonPage_QueryBasStorage : Mesnac.Web.UI.Page
{
    #region 属性注入
    protected BasStorageManager manager = new BasStorageManager();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    private PageResult<BasStorage> GetPageResultData(PageResult<BasStorage> pageParams)
    {
        BasStorageManager.QueryParams queryParams = new BasStorageManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.storageName = txtStorageName.Text;
        if (!string.IsNullOrEmpty(Request.QueryString["LastStorageFlag"]))
            queryParams.lastStorageFlag = Request.QueryString["LastStorageFlag"].ToString();
        if (!string.IsNullOrEmpty(Request.QueryString["StorageHigherID"]))
            queryParams.storageHigherLevel = Request.QueryString["StorageHigherID"].ToString();
        if (!string.IsNullOrEmpty(Request.QueryString["NoStorageID"]))
            queryParams.noStorageID = Request.QueryString["NoStorageID"].ToString();
        if (string.IsNullOrEmpty(Request.QueryString["UsedFlag"]))
           queryParams.usedFlag = "1";
        if (!string.IsNullOrEmpty(Request.QueryString["StorageType"]))
        {
            if (Request.QueryString["StorageType"].ToString() != "all")
                queryParams.storageType = Request.QueryString["StorageType"].ToString();
        }
        queryParams.cancelFlag = "0";
        queryParams.deleteFlag = "0";

        return manager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<BasStorage> pageParams = new PageResult<BasStorage>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "ObjID ASC";

        PageResult<BasStorage> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }
}