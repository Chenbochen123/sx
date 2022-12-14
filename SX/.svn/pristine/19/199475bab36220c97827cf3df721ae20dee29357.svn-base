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

public partial class Manager_BasicInfo_RawMaterialQuality_QueryRawMaterial : Mesnac.Web.UI.Page
{
    #region 属性注入
    protected BasMaterialManager manager = new BasMaterialManager();
    #endregion

    #region 页面初始化
    protected void Page_Load(object sender, EventArgs e)
    {

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
    #endregion

    #region 分页相关方法
    private PageResult<BasMaterial> GetPageResultData(PageResult<BasMaterial> pageParams)
    {
        BasMaterialManager.QueryParams queryParams = new BasMaterialManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.materialName = txtMaterialName.Text.TrimEnd().TrimStart();
        queryParams.deleteFlag = "0";
        queryParams.minorTypeID = getQueryString("MinorTypeID");
        queryParams.majorTypeID = "1";
        queryParams.minMajorTypeID = getQueryString("MinMajorTypeID");


        //return manager.GetMaterialByChineseSearchKey(queryParams);
        return manager.GetMaterialBySearchKey(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<BasMaterial> pageParams = new PageResult<BasMaterial>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "MaterialCode ASC";

        PageResult<BasMaterial> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }
    #endregion
}