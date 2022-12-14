using System;
using System.Collections.Generic;
using System.Data;
using Ext.Net;
using Mesnac.Business.Implements;
using Mesnac.Business.Interface;
using Mesnac.Data.Components;
using Mesnac.Entity;

public partial class Manager_BasicInfo_CommonPage_QueryPageMenu : Mesnac.Web.UI.Page
{
    #region 属性注入
    private ISysPageMenuManager manager = new SysPageMenuManager();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    private PageResult<SysPageMenu> GetPageResultData(PageResult<SysPageMenu> pageParams)
    {
        SysPageMenuManager.QueryParams queryParams = new SysPageMenuManager.QueryParams();
        queryParams.PageParams = pageParams;
        queryParams.PageName = txtShowName.Text;

        return manager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<SysPageMenu> pageParams = new PageResult<SysPageMenu>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "ObjID ASC";

        PageResult<SysPageMenu> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }
}