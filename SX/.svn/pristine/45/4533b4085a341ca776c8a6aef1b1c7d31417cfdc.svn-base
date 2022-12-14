using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mesnac.Business.Interface;
using Mesnac.Entity;
using Mesnac.Data.Components;
using Mesnac.Business.Implements;
using Ext.Net;
using System.Data;

public partial class Manager_BasicInfo_CommonPage_QueryMaterialMinorType : Mesnac.Web.UI.Page
{
    #region 属性注入
    protected BasMaterialMinorTypeManager manager = new BasMaterialMinorTypeManager();
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
    private PageResult<BasMaterialMinorType> GetPageResultData(PageResult<BasMaterialMinorType> pageParams)
    {
        BasMaterialMinorTypeManager.QueryParams queryParams = new BasMaterialMinorTypeManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.minorTypeName = txtMinorTypeName.Text.TrimEnd().TrimStart();
        queryParams.remark = txtRemark.Text.TrimEnd().TrimStart();
        queryParams.majorID = getQueryString("MajorTypeID");
        return manager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<BasMaterialMinorType> pageParams = new PageResult<BasMaterialMinorType>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "ObjID ASC";

        PageResult<BasMaterialMinorType> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }
}