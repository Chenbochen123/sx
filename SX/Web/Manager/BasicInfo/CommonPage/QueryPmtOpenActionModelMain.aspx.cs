using System;
using System.Collections.Generic;
using System.Data;
using Ext.Net;
using Mesnac.Business.Implements;
using Mesnac.Business.Interface;
using Mesnac.Data.Components;
using Mesnac.Entity;

public partial class Manager_BasicInfo_CommonPage_QueryPmtOpenActionModelMain : Mesnac.Web.UI.Page
{
    #region 属性注入
    private IPmtOpenActionModelMainManager manager = new PmtOpenActionModelMainManager();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    private PageResult<PmtOpenActionModelMain> GetPageResultData(PageResult<PmtOpenActionModelMain> pageParams)
    {
        PmtOpenActionModelMainManager.QueryParams queryParams = new PmtOpenActionModelMainManager.QueryParams();
        queryParams.PageParams = pageParams;
        queryParams.ModelName = txtModelName.Text;
        queryParams.DeleteFlag = "0";

        return manager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<PmtOpenActionModelMain> pageParams = new PageResult<PmtOpenActionModelMain>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;

        PageResult<PmtOpenActionModelMain> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }
}