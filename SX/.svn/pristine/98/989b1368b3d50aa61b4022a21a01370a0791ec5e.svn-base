using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mesnac.Business.Implements;
using Ext.Net;
using Mesnac.Data.Components;
using Mesnac.Entity;
using System.Data;
using Mesnac.Business.Interface;

public partial class Manager_BasicInfo_CommonPage_QueryEqmStopType : Mesnac.Web.UI.Page
{
    #region 属性注入
    protected IEqmStopTypeManager manager = new EqmStopTypeManager();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    private string getQueryString(string key)
    {
        string Result = string.Empty;
        try
        {
            Result = Request.QueryString[key].ToString();
        }
        catch
        {
            Result = string.Empty;
        }
        return Result;
    }
    private PageResult<EqmStopType> GetPageResultData(PageResult<EqmStopType> pageParams)
    {
        EqmStopTypeManager.QueryParams queryParams = new EqmStopTypeManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.typeName = txtTypeName.Text.TrimEnd().TrimStart();
        queryParams.deleteFlag = "0";
        return manager.GetEqmStopTypeBySearchKey(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<EqmStopType> pageParams = new PageResult<EqmStopType>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "TypeCode ASC";

        PageResult<EqmStopType> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }
}