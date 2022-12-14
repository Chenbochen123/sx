using System;
using System.Collections.Generic;
using System.Data;
using Ext.Net;
using Mesnac.Business.Implements;
using Mesnac.Business.Interface;
using Mesnac.Data.Components;
using Mesnac.Entity;

public partial class Manager_BasicInfo_CommonPage_QueryBasUser : Mesnac.Web.UI.Page
{
    #region 属性注入
    protected BasUserManager manager = new BasUserManager();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    private PageResult<BasUser> GetPageResultData(PageResult<BasUser> pageParams)
    {
        BasUserManager.QueryParams queryParams = new BasUserManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.userName = txtUserName.Text.TrimEnd().TrimStart();
        queryParams.hrcode = txtHRCode.Text.TrimEnd().TrimStart();
        
        return manager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<BasUser> pageParams = new PageResult<BasUser>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "ObjID ASC";

        PageResult<BasUser> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }
}