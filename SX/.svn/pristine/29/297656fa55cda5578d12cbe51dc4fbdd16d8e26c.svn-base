using System;
using System.Collections.Generic;
using System.Data;
using Ext.Net;
using Mesnac.Business.Implements;
using Mesnac.Business.Interface;
using Mesnac.Data.Components;
using Mesnac.Entity;

public partial class Manager_BasicInfo_CommonPage_QueryPmtAction : Mesnac.Web.UI.Page
{
    #region 属性注入
    private IPmtActionManager manager = new PmtActionManager();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    private PageResult<PmtAction> GetPageResultData(PageResult<PmtAction> pageParams)
    {
        PmtActionManager.QueryParams queryParams = new PmtActionManager.QueryParams();
        queryParams.PageParams = pageParams;
        queryParams.Code = txtCode.Text;
        queryParams.ShowName = txtShowName.Text;
        queryParams.Address = txAddress.Text;

        return manager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<PmtAction> pageParams = new PageResult<PmtAction>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "SeqIdx";

        PageResult<PmtAction> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }
}