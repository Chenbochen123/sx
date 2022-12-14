using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mesnac.Data.Components;
using Mesnac.Business.Interface;
using Mesnac.Business.Implements;
using Mesnac.Entity;
using Ext.Net;
using System.Data;

public partial class Manager_BasicInfo_CommonPage_QueryMaterialStaticClass : Mesnac.Web.UI.Page
{
    #region 属性注入
    protected BasMaterialStaticClassManager manager = new BasMaterialStaticClassManager();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    private PageResult<BasMaterialStaticClass> GetPageResultData(PageResult<BasMaterialStaticClass> pageParams)
    {
        BasMaterialStaticClassManager.QueryParams queryParams = new BasMaterialStaticClassManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.staticClassName = txtStaticClassName.Text.TrimEnd().TrimStart();
        queryParams.remark = txtRemark.Text.TrimEnd().TrimStart();

        return manager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<BasMaterialStaticClass> pageParams = new PageResult<BasMaterialStaticClass>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "ObjID ASC";

        PageResult<BasMaterialStaticClass> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }
}