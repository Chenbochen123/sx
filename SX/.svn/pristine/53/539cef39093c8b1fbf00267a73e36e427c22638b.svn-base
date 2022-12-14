using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mesnac.Business.Implements;
using Mesnac.Data.Components;
using Ext.Net;
using System.Data;
using Mesnac.Entity;

public partial class Manager_BasicInfo_CommonPage_QueryRubberType : Mesnac.Web.UI.Page
{
    #region 属性注入
    protected BasRubTypeManager manager = new BasRubTypeManager();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    private PageResult<BasRubType> GetPageResultData(PageResult<BasRubType> pageParams)
    {
        BasRubTypeManager.QueryParams queryParams = new BasRubTypeManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.rubTypeName = txt_rubber_type_name.Text.TrimEnd().TrimStart();
        queryParams.deleteFlag = "0";

        return manager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<BasRubType> pageParams = new PageResult<BasRubType>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "ObjID ASC";

        PageResult<BasRubType> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }
}