using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mesnac.Business.Implements;
using Mesnac.Data.Components;
using Mesnac.Entity;
using Ext.Net;
using System.Data;

public partial class Manager_BasicInfo_CommonPage_QueryShiftClass : Mesnac.Web.UI.Page
{
    #region 属性注入
    protected PptClassManager manager = new PptClassManager();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    private PageResult<PptClass> GetPageResultData(PageResult<PptClass> pageParams)
    {
        PptClassManager.QueryParams queryParams = new PptClassManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.shiftClassName = txt_shiftclass_name.Text.TrimEnd().TrimStart();
        queryParams.userFlag = "1";

        return manager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<PptClass> pageParams = new PageResult<PptClass>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "ObjID ASC";

        PageResult<PptClass> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }
}