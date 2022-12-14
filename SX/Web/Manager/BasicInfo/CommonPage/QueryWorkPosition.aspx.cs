using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using Mesnac.Business.Implements;
using Mesnac.Data.Components;
using Mesnac.Entity;
using System.Data;

public partial class Manager_BasicInfo_CommonPage_QueryWorkPosition : Mesnac.Web.UI.Page
{
    #region 属性注入
    protected BasWorkManager manager = new BasWorkManager();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    private PageResult<BasWork> GetPageResultData(PageResult<BasWork> pageParams)
    {
        BasWorkManager.QueryParams queryParams = new BasWorkManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.workName = txt_work_name.Text.TrimEnd().TrimStart();
        queryParams.deleteFlag = "0";

        return manager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<BasWork> pageParams = new PageResult<BasWork>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "ObjID ASC";

        PageResult<BasWork> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }
}