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

public partial class Manager_BasicInfo_CommonPage_QueryDepartment : Mesnac.Web.UI.Page
{
    #region 属性注入
    protected BasDeptManager manager = new BasDeptManager();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    private PageResult<BasDept> GetPageResultData(PageResult<BasDept> pageParams)
    {
        BasDeptManager.QueryParams queryParams = new BasDeptManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.depName = txt_dep_name.Text.TrimEnd().TrimStart();
        if (Request.QueryString["DeptLevel"] != null)
            queryParams.depLevel = Request.QueryString["DeptLevel"].ToString();
        queryParams.deleteFlag = "0";

        return manager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<BasDept> pageParams = new PageResult<BasDept>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "ObjID ASC";

        PageResult<BasDept> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }
}