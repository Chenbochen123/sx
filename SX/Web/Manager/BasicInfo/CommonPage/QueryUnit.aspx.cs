using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mesnac.Data.Components;
using Mesnac.Business.Interface;
using Mesnac.Entity;
using Mesnac.Business.Implements;
using Ext.Net;
using System.Data;

public partial class Manager_BasicInfo_CommonPage_QueryUnit : Mesnac.Web.UI.Page
{
    #region 属性注入
    protected BasUnitManager manager = new BasUnitManager();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    private PageResult<BasUnit> GetPageResultData(PageResult<BasUnit> pageParams)
    {
        BasUnitManager.QueryParams queryParams = new BasUnitManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.unitName = txtUnitName.Text.TrimEnd().TrimStart();

        return manager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<BasUnit> pageParams = new PageResult<BasUnit>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "ObjID ASC";

        PageResult<BasUnit> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }
}