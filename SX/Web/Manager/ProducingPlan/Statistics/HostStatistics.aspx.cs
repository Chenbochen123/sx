using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using Mesnac.Data.Components;
using Mesnac.Entity;
using System.Data;
using Mesnac.Business.Implements;
using Mesnac.Business.Interface;

public partial class Manager_ProducingPlan_Statistics_HostStatistics : Mesnac.Web.UI.Page
{
    IPptLotDataManager pptLotDataManager = new PptLotDataManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            this.txtStratDate.Text = DateTime.Now.AddDays(-DateTime.Now.Day + 1).ToString("yyyy-MM-dd");
            this.txtStopDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
    }
    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查询 = new SysPageAction() { ActionID = 1, ActionName = "btnSearch" };
        }
        public SysPageAction 查询 { get; private set; } //必须为 public
    }
    #endregion
    #region 分页相关方法
    private PageResult<PptLotData> GetPageResultData(PageResult<PptLotData> pageParams)
    {
        PptLotDataManager.QueryParams queryParams = new PptLotDataManager.QueryParams();
        queryParams.PageParams = pageParams;
        if (!String.IsNullOrEmpty(this.txtStratDate.Text))
        {
            queryParams.BeginTime = this.txtStratDate.Text;
        }
        if (!String.IsNullOrEmpty(this.txtStopDate.Text))
        {
            queryParams.StopTime = this.txtStopDate.Text;
        }
        return pptLotDataManager.GetTablePageHostStatisticsBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        if (this._.查询.SeqIdx == 0)
        {
            return null;
        }
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<PptLotData> pageParams = new PageResult<PptLotData>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        PageResult<PptLotData> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];
        int total = lst.RecordCount;
        return new { data, total };
    }
    #endregion
}