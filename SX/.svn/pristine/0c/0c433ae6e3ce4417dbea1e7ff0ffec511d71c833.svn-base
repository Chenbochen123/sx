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

public partial class Manager_BasicInfo_CommonPage_QueryEqmStopFault : System.Web.UI.Page
{
    #region 属性注入
    protected IEqmStopFaultManager manager = new EqmStopFaultManager();
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
    private PageResult<EqmStopFault> GetPageResultData(PageResult<EqmStopFault> pageParams)
    {
        EqmStopFaultManager.QueryParams queryParams = new EqmStopFaultManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.faultName = txtFaultName.Text.TrimEnd().TrimStart();
        queryParams.typeID = getQueryString("TypeID");
        queryParams.deleteFlag = "0";
        return manager.GetEqmStopFaultBySearchKey(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<EqmStopFault> pageParams = new PageResult<EqmStopFault>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "FaultCode ASC";

        PageResult<EqmStopFault> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }
}