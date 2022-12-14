using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mesnac.Business.Implements;
using Mesnac.Business.Interface;
using Mesnac.Entity;
using Mesnac.Data.Components;
using Ext.Net;
using System.Data;
using NBear.Common;

public partial class Manager_BasicInfo_CommonPage_QueryEquipment : Mesnac.Web.UI.Page
{
    #region 属性注入
    protected IBasEquipManager manager = new BasEquipManager();
    protected IBasEquipTypeManager equipTypeManager = new BasEquipTypeManager();
    protected IBasWorkShopManager basWorkShopManager = new BasWorkShopManager();
    private string equipType = "";
    #endregion

    private const string constSelectAllText = "---请选择---";
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((IsPostBack) || (X.IsAjaxRequest))
        {
            return;
        }
        #region 设备类型
        EntityArrayList<BasEquipType> lstBasEquipType = equipTypeManager.GetListByWhere(BasEquipType._.DeleteFlag == "0");
        equipType = Context.Request.QueryString["equipType"];
        txtEquipType.Items.Clear();
        Ext.Net.ListItem allitem = new Ext.Net.ListItem(constSelectAllText, constSelectAllText);
        txtEquipType.Items.Add(allitem);
        foreach (BasEquipType m in lstBasEquipType)
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem();
            item.Value = m.ObjID;
            item.Text = m.EquipTypeName;
            txtEquipType.Items.Add(item);
        }
        if (!string.IsNullOrWhiteSpace(equipType))
        {
            txtEquipType.Text = equipType;
        }
        else
        {
            if (txtEquipType.Items.Count > 0)
            {
                txtEquipType.Text = (txtEquipType.Items[0].Value);
            }
        }
        #endregion

        #region 设备类型
        EntityArrayList<BasWorkShop> lstBasWorkShop = basWorkShopManager.GetListByWhere(BasWorkShop._.DeleteFlag == "0");
        txtWorkShopCode.Items.Clear();
        txtWorkShopCode.Items.Add(allitem);
        foreach (BasWorkShop m in lstBasWorkShop)
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem();
            item.Value = m.ObjID.ToString();
            item.Text = m.WorkShopName;
            txtWorkShopCode.Items.Add(item);
        }
        if (txtWorkShopCode.Items.Count > 0)
        {
            txtWorkShopCode.Text = (txtWorkShopCode.Items[0].Value);
        }
        #endregion
    }

    private PageResult<BasEquip> GetPageResultData(PageResult<BasEquip> pageParams)
    {
        BasEquipManager.QueryParams queryParams = new BasEquipManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.equipType = txtEquipType.Text.Replace(constSelectAllText, "");
        queryParams.WorkShopCode = txtWorkShopCode.Text.Replace(constSelectAllText, "");
        queryParams.equipName = txtEquipName.Text.TrimEnd().TrimStart();

        return manager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<BasEquip> pageParams = new PageResult<BasEquip>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "EquipCode ASC";

        PageResult<BasEquip> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }
}