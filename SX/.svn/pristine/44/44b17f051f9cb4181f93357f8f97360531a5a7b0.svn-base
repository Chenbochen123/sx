using System;
using System.Collections.Generic;
using System.Data;
using Ext.Net;
using Mesnac.Business.Implements;
using Mesnac.Business.Interface;
using Mesnac.Data.Components;
using Mesnac.Entity;
using NBear.Common;

public partial class Manager_RawMaterialQuality_QueryFactoryMapping : Mesnac.Web.UI.Page
{
    #region 属性注入
    protected IQmcFactoryMappingManager mappingManager = new QmcFactoryMappingManager();
    protected IBasFactoryInfoManager factoryManager = new BasFactoryInfoManager();
    protected IBasMaterialMinorTypeManager seriesManager = new BasMaterialMinorTypeManager();
    #endregion

    #region 页面初始化
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            InitSeries();
            txtSupplierName.Text = Request.Params["SupplierName"];
        }
    }

    /// <summary>
    /// 初始化系列树
    /// </summary>
    protected void InitSeries()
    {
        EntityArrayList<BasMaterialMinorType> lst = new EntityArrayList<BasMaterialMinorType>();
        lst = seriesManager.GetListByWhere(BasMaterialMinorType._.MajorID == 1 && BasMaterialMinorType._.DeleteFlag == "0");
        foreach (BasMaterialMinorType type in lst)
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem();
            item.Text = type.MinorTypeName;
            item.Value = type.MinorTypeID;
            cbxSeriesName.Items.Add(item);
        }
    }
    #endregion

    #region 分页相关方法
    private PageResult<QmcFactoryMapping> GetPageResultData(PageResult<QmcFactoryMapping> pageParams)
    {
        QmcFactoryMappingManager.QueryParams queryParams = new QmcFactoryMappingManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.supplierName = txtSupplierName.Text.TrimEnd().TrimStart();
        queryParams.manufacturerName = txtManufacturerName.Text.TrimEnd().TrimStart();
        queryParams.seriesId = cbxSeriesName.SelectedItem.Value;
        queryParams.deleteFlag = "0";
        return mappingManager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<QmcFactoryMapping> pageParams = new PageResult<QmcFactoryMapping>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "MappingId ASC";

        PageResult<QmcFactoryMapping> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }
    #endregion
}