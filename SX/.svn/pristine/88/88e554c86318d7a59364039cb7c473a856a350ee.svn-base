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
using System.Text;

public partial class Manager_BasicInfo_CommonPage_QueryEqmFaultReason : System.Web.UI.Page
{
    #region 属性注入
    protected IEqmFaultReasonManager manager = new EqmFaultReasonManager();
    #endregion

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
    private PageResult<EqmFaultReason> GetPageResultData(PageResult<EqmFaultReason> pageParams)
    {
        EqmFaultReasonManager.QueryParams queryParams = new EqmFaultReasonManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.reasonName = txtReasonName.Text.TrimEnd().TrimStart();
        queryParams.faultID = getQueryString("FaultID");
        queryParams.deleteFlag = "0";
        return manager.GetEqmFaultReasonBySearchKey(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<EqmFaultReason> pageParams = new PageResult<EqmFaultReason>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "ObjID ASC";

        PageResult<EqmFaultReason> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }

    [DirectMethod]
    public string getFaultReason()
    {
        StringBuilder sb = new StringBuilder();
        RowSelectionModel sm = this.gridPanelCenter.GetSelectionModel() as RowSelectionModel;

        foreach (SelectedRow row in sm.SelectedRows)
        {
            sb.Append(row.RecordID + ",");
        }

        return sb.ToString().TrimEnd(',');
    }
}