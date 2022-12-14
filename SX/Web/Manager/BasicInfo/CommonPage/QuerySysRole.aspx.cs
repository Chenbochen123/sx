using System;
using System.Collections.Generic;
using System.Data;
using Ext.Net;
using Mesnac.Business.Implements;
using Mesnac.Business.Interface;
using Mesnac.Data.Components;
using Mesnac.Entity;

public partial class Manager_BasicInfo_CommonPage_QuerySysRole : Mesnac.Web.UI.Page
{
    #region 属性注入
    private ISysRoleManager manager = new SysRoleManager();//业务对象
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    private PageResult<SysRole> GetPageResultData(PageResult<SysRole> pageParams)
    {
        SysRoleManager.QueryParams queryParams = new SysRoleManager.QueryParams();
        queryParams.PageParams = pageParams;
        queryParams.RoleName = txtShowName.Text;
        queryParams.DeleteFlag = "0";

        return manager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<SysRole> pageParams = new PageResult<SysRole>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "ObjID ASC";

        PageResult<SysRole> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }
}