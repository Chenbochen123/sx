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
using NBear.Common;

public partial class Manager_BasicInfo_CommonPage_QueryWorkShop : Mesnac.Web.UI.Page
{
    #region 属性注入
    protected BasWorkShopManager manager = new BasWorkShopManager();
    protected SysCodeManager sysCodeManager = new SysCodeManager();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            EntityArrayList<SysCode> yesNoList = sysCodeManager.GetListByWhere(SysCode._.TypeID == "YesNo");
            foreach (SysCode yseNo in yesNoList)
            {
                Ext.Net.ListItem item = new Ext.Net.ListItem(yseNo.ItemName, yseNo.ItemCode);
                txt_isinner_group.Items.Add(item);
            }
        }
    }

    private PageResult<BasWorkShop> GetPageResultData(PageResult<BasWorkShop> pageParams)
    {
        BasWorkShopManager.QueryParams queryParams = new BasWorkShopManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.workshopName = txt_workshop_name.Text.TrimEnd().TrimStart();
        queryParams.isInnerGroup = txt_isinner_group.SelectedItem != null ?txt_isinner_group.SelectedItem.Value : "";
        queryParams.deleteFlag = "0";

        return manager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<BasWorkShop> pageParams = new PageResult<BasWorkShop>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "ObjID ASC";

        PageResult<BasWorkShop> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }
}