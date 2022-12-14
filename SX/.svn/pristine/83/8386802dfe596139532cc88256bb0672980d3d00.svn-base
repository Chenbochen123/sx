using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mesnac.Business.Implements;
using Mesnac.Entity;
using Mesnac.Data.Components;
using Ext.Net;
using System.Data;

public partial class Manager_BasicInfo_CommonPage_QueryRubberFactory : Mesnac.Web.UI.Page
{
    #region 属性注入
    protected BasFactoryInfoManager manager = new BasFactoryInfoManager();
    protected BasFactoryTypeManager facTypeManager = new BasFactoryTypeManager();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            DataSet facTypeDs = facTypeManager.GetAllDataSet();
            foreach (DataRow row in facTypeDs.Tables[0].Rows)
            {
                Ext.Net.ListItem item = new Ext.Net.ListItem(row["FactoryTypeName"].ToString(), row["ObjID"].ToString());
                txt_fac_type.Items.Add(item);
            }
        }
    }

    private PageResult<BasFactoryInfo> GetPageResultData(PageResult<BasFactoryInfo> pageParams)
    {
        BasFactoryInfoManager.QueryParams queryParams = new BasFactoryInfoManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.facName = txt_fac_name.Text.TrimEnd().TrimStart();
        queryParams.facType = txt_fac_type.SelectedItem != null ? txt_fac_type.SelectedItem.Value : "";
        queryParams.deleteFlag = "0";

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