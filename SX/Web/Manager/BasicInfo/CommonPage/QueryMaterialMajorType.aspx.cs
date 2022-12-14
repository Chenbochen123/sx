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

public partial class Manager_BasicInfo_CommonPage_QueryMaterialMajorType : Mesnac.Web.UI.Page
{
    #region 属性注入
    protected BasMaterialMajorTypeManager manager = new BasMaterialMajorTypeManager();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    private PageResult<BasMaterialMajorType> GetPageResultData(PageResult<BasMaterialMajorType> pageParams)
    {
        BasMaterialMajorTypeManager.QueryParams queryParams = new BasMaterialMajorTypeManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.majorTypeName = txtMajorTypeName.Text.TrimEnd().TrimStart();
        queryParams.remark = txtRemark.Text.TrimEnd().TrimStart();

        return manager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<BasMaterialMajorType> pageParams = new PageResult<BasMaterialMajorType>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "ObjID ASC";

        PageResult<BasMaterialMajorType> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }
}