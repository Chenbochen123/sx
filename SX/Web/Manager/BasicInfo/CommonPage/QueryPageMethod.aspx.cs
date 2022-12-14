using System;
using System.Collections.Generic;
using System.Data;
using Ext.Net;
using Mesnac.Business.Implements;
using Mesnac.Business.Interface;
using Mesnac.Data.Components;
using Mesnac.Entity;

public partial class Manager_BasicInfo_CommonPage_QueryPageMethod : Mesnac.Web.UI.Page
{
    #region 属性注入
    private ISysPageMethodManager manager = new SysPageMethodManager();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    private PageResult<SysPageMethod> GetPageResultData(PageResult<SysPageMethod> pageParams)
    {
        SysPageMethodManager.QueryParams queryParams = new SysPageMethodManager.QueryParams();
        queryParams.PageParams = pageParams;
        queryParams.MethodName = txtShowName.Text;

        return manager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<SysPageMethod> pageParams = new PageResult<SysPageMethod>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "ObjID ASC";

        PageResult<SysPageMethod> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }
}