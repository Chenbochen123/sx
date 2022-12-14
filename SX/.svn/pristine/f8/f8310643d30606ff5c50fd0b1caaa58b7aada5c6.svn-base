using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mesnac.Business.Interface;
using Mesnac.Data.Components;
using Mesnac.Entity;
using Mesnac.Business.Implements;
using Ext.Net;
using System.Data;

public partial class Manager_BasicInfo_CommonPage_QueryFactoryType : Mesnac.Web.UI.Page
{
    #region 属性注入
    protected BasFactoryTypeManager manager = new BasFactoryTypeManager();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    private PageResult<BasFactoryType> GetPageResultData(PageResult<BasFactoryType> pageParams)
    {
        BasFactoryTypeManager.QueryParams queryParams = new BasFactoryTypeManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.factoryTypeName = txtFactoryTypeName.Text.TrimEnd().TrimStart();
        queryParams.deleteFlag = "0";

        return manager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<BasFactoryType> pageParams = new PageResult<BasFactoryType>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "ObjID ASC";

        PageResult<BasFactoryType> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }
}