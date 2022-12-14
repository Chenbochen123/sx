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

public partial class Manager_BasicInfo_CommonPage_QueryFactory : Mesnac.Web.UI.Page
{
    #region 属性注入
    protected BasFactoryInfoManager manager = new BasFactoryInfoManager();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest && !IsPostBack)
        {
            if (Request.QueryString["QueryFlag"] != null)
            {
                cbxDeleteFlag.Hidden = false;
            }
            cbxDeleteFlag.SelectedItem.Value = "0";
        }
    }

    private PageResult<BasFactoryInfo> GetPageResultData(PageResult<BasFactoryInfo> pageParams)
    {
        BasFactoryInfoManager.QueryParams queryParams = new BasFactoryInfoManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.facName = txtFactoryName.Text.TrimEnd().TrimStart();
        queryParams.facType = hiddenSelectFacType.Text;
        if (cbxDeleteFlag.SelectedItem.Value != "all")
            queryParams.deleteFlag = cbxDeleteFlag.SelectedItem.Value;

        return manager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<BasFactoryInfo> pageParams = new PageResult<BasFactoryInfo>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "ObjID ASC";

        PageResult<BasFactoryInfo> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }
}