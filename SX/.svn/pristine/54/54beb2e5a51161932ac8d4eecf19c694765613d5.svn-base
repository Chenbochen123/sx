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

public partial class Manager_BasicInfo_CommonPage_QueryDepartmentErp : Mesnac.Web.UI.Page
{
    #region 属性注入
    protected BasDeptErpInfoManager manager = new BasDeptErpInfoManager();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    private PageResult<BasDeptErpInfo> GetPageResultData(PageResult<BasDeptErpInfo> pageParams)
    {
        BasDeptErpInfoManager.QueryParams queryParams = new BasDeptErpInfoManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.depName = txt_dep_name.Text.TrimStart().TrimEnd();
        queryParams.deleteFlag = "0";

        return manager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<BasDeptErpInfo> pageParams = new PageResult<BasDeptErpInfo>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "ObjID ASC";

        PageResult<BasDeptErpInfo> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }
}