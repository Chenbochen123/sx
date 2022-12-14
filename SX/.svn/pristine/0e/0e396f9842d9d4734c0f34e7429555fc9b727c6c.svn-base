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

public partial class Manager_BasicInfo_CommonPage_QueryEquipmentPart : Mesnac.Web.UI.Page
{
    #region 属性注入
    protected BasEquipPartInfoManager manager = new BasEquipPartInfoManager();
    private string equipType = "";
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        equipType = Context.Request.QueryString["equipType"];
    }

    private PageResult<BasEquipPartInfo> GetPageResultData(PageResult<BasEquipPartInfo> pageParams)
    {
        BasEquipPartInfoManager.QueryParams queryParams = new BasEquipPartInfoManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.equipType = equipType;
        queryParams.partName = txt_part_name.Text.TrimEnd().TrimStart();

        return manager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<BasEquipPartInfo> pageParams = new PageResult<BasEquipPartInfo>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "ObjID ASC";

        PageResult<BasEquipPartInfo> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }
}