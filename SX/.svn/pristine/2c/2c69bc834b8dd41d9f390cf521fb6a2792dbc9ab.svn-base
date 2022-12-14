using System;
using System.Collections.Generic;
using System.Data;
using Ext.Net;
using Mesnac.Business.Implements;
using Mesnac.Business.Interface;
using Mesnac.Data.Components;
using Mesnac.Entity;

public partial class Manager_BasicInfo_CommonPage_QueryPmtRecipe : Mesnac.Web.UI.Page
{
    #region 属性注入
    private IPmtRecipeManager manager = new PmtRecipeManager();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((IsPostBack) || (X.IsAjaxRequest))
        {
            return;
        }
        string equipName = GetRequest("EquipName");
        txtEuipName.Text = equipName;
    }
    private string GetRequest(string key)
    {
        if (this.Request[key] != null)
        {
            return this.Request[key].ToString();
        }
        return string.Empty;
    }
    private PageResult<PmtRecipe> GetPageResultData(PageResult<PmtRecipe> pageParams)
    {
        PmtRecipeManager.QueryParams queryParams = new PmtRecipeManager.QueryParams();
        queryParams.PageParams = pageParams;

        string equipCode = GetRequest("EquipCode");
        queryParams.RecipeEquipName = txtEuipName.Text;
        queryParams.RecipeMaterialName = txtMaterialName.Text;
        queryParams.RecipeVersionID = txtVersion.Text;
        queryParams.EquipType = GetRequest("EquipType");

        return manager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<PmtRecipe> pageParams = new PageResult<PmtRecipe>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;

        PageResult<PmtRecipe> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }
}