using System;
using System.Collections.Generic;
using System.Data;
using Ext.Net;
using Mesnac.Business.Implements;
using Mesnac.Business.Interface;
using Mesnac.Data.Components;
using Mesnac.Entity;
using NBear;
using NBear.Common;

public partial class Manager_BasicInfo_CommonPage_QueryQmcUser : Mesnac.Web.UI.Page
{
    #region 属性注入
    protected BasUserManager manager = new BasUserManager();
    protected BasDeptManager deptManager = new BasDeptManager();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            //初始化部门列表
            InitQmcDept();
        }
    }

    private void InitQmcDept()
    {
        this.cbxDepartment.Items.Clear();
        ListItem rootItem = new ListItem();
        rootItem.Text = "检测处";
        rootItem.Value = "01659";
        this.cbxDepartment.Items.Add(rootItem);
        EntityArrayList<BasDept> qmcDeptList = deptManager.GetListByWhere(BasDept._.ParentNum == "01659");
        if (qmcDeptList.Count > 0)
        {
            foreach (BasDept dept in qmcDeptList)
            {
                ListItem item = new ListItem();
                item.Text = dept.DepName;
                item.Value = dept.DepCode;
                this.cbxDepartment.Items.Add(item);
            }
        }
        cbxDepartment.SelectedItem.Index = 0;
    }

    private PageResult<BasUser> GetPageResultData(PageResult<BasUser> pageParams)
    {
        BasUserManager.QueryParams queryParams = new BasUserManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.userName = txtUserName.Text.TrimEnd().TrimStart();
        queryParams.hrcode = txtHRCode.Text.TrimEnd().TrimStart();
        queryParams.deptCode = cbxDepartment.Value.ToString();
        return manager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<BasUser> pageParams = new PageResult<BasUser>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "ObjID ASC";

        PageResult<BasUser> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }
}