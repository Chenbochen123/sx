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
using Mesnac.Business.Interface;
using NBear.Common;

public partial class Manager_BasicInfo_CommonPage_QueryEqmSparePart : System.Web.UI.Page
{
    #region 属性注入
    protected IEqmSparePartManager manager = new EqmSparePartManager();
    protected IEqmSparePartMainTypeManager majorManager = new EqmSparePartMainTypeManager();
    protected IEqmSparePartDetailTypeManager minorManager = new EqmSparePartDetailTypeManager();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
           EntityArrayList<EqmSparePartMainType> majorList = majorManager.GetAllList();
           majorStore.Data = majorList;
           majorStore.DataBind();
        }
    }
    private string getQueryString(string key)
    {
        string Result = string.Empty;
        try
        {
            Result = Request.QueryString[key].ToString();
        }
        catch
        {
            Result = string.Empty;
        }
        return Result;
    }
    private PageResult<EqmSparePart> GetPageResultData(PageResult<EqmSparePart> pageParams)
    {
        EqmSparePartManager.QueryParams queryParams = new EqmSparePartManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.sparePartName = txtSpartPartName.Text.TrimEnd().TrimStart();
        queryParams.sparePartMainType = txtSpartPartMajor.SelectedItem.Value;
        queryParams.sparePartDetailType = txtSpartPartMinor.SelectedItem.Value;
        queryParams.deleteFlag = "0";
        return manager.GetSparePartBySearchKey(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<EqmSparePart> pageParams = new PageResult<EqmSparePart>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "SparePartCode ASC";

        PageResult<EqmSparePart> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }

    protected void txtSpartPartMajor_SelectChanged(object sender, EventArgs e)
    {
        string majorType = txtSpartPartMajor.SelectedItem.Value;
        EntityArrayList<EqmSparePartDetailType> minorList = minorManager.GetListByWhere(EqmSparePartDetailType._.MainTypeID == majorType);
        detailStore.Data = minorList;
        detailStore.DataBind();
        txtSpartPartMinor.Select(0);
    }
}