﻿using System;
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

public partial class Manager_BasicInfo_CommonPage_QueryRubberInfo : Mesnac.Web.UI.Page
{
    #region 属性注入
    protected BasRubTypeManager basRubTypeManager = new BasRubTypeManager();
    protected BasRubInfoManager basRubInfoManager = new BasRubInfoManager();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            EntityArrayList<BasRubType> lst = basRubTypeManager.GetAllList();
            txtRubType.Items.Clear();
            Ext.Net.ListItem allitem = new Ext.Net.ListItem("全部", "全部");
            txtRubType.Items.Add(allitem);
            foreach (BasRubType m in lst)
            {
                Ext.Net.ListItem item = new Ext.Net.ListItem(m.RubTypeName, m.ObjID.ToString());
                txtRubType.Items.Add(item);
            }
        }
    }

    private PageResult<BasRubInfo> GetPageResultData(PageResult<BasRubInfo> pageParams)
    {
        BasRubInfoManager.QueryParams queryParams = new BasRubInfoManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.rubName = txtRubName.Text.TrimEnd().TrimStart().Replace("'","");
        queryParams.rubTypeCode = txtRubType.SelectedItem.Value != null ? txtRubType.SelectedItem.Value.Replace("全部", "") : "";
        queryParams.deleteFlag = "0";

        return basRubInfoManager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<BasRubInfo> pageParams = new PageResult<BasRubInfo>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;

        PageResult<BasRubInfo> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }
}