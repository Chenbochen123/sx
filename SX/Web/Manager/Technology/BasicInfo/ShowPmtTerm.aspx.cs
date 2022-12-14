using System;
using System.Collections.Generic;
using System.Data;
using Ext.Net;
using Mesnac.Business.Implements;
using Mesnac.Business.Interface;
using Mesnac.Data.Components;
using Mesnac.Entity;

/// <summary>
/// Manager_Technology_BasicInfo_ShowPmtTerm 实现类
/// 孙本强 @ 2013-04-03 13:05:21
/// </summary>
/// <remarks></remarks>
public partial class Manager_Technology_BasicInfo_ShowPmtTerm : Mesnac.Web.UI.Page
{

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

    #region 属性注入
    /// <summary>
    /// 
    /// 孙本强 @ 2013-04-03 13:05:22
    /// </summary>
    private IPmtTermManager manager = new PmtTermManager();
    #endregion

    /// <summary>
    /// Handles the Load event of the Page control.
    /// 孙本强 @ 2013-04-03 13:05:22
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    /// <remarks></remarks>
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// 获取分页数据集
    /// 孙本强 @ 2013-04-03 13:05:22
    /// </summary>
    /// <param name="pageParams">The page params.</param>
    /// <returns>分页数据集</returns>
    /// <remarks></remarks>
    private PageResult<PmtTerm> GetPageResultData(PageResult<PmtTerm> pageParams)
    {
        PmtTermManager.QueryParams queryParams = new PmtTermManager.QueryParams();
        queryParams.PageParams = pageParams;
        queryParams.Code = txtCode.Text;
        queryParams.ShowName = txtShowName.Text;
        queryParams.Address = txAddress.Text;

        return manager.GetTablePageDataBySql(queryParams);
    }

    /// <summary>
    /// Grids the panel bind data.
    /// 孙本强 @ 2013-04-03 13:05:22
    /// </summary>
    /// <param name="action">The action.</param>
    /// <param name="extraParams">The extra params.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        if (this._.查询.SeqIdx == 0)
        {
            return null;
        }
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<PmtTerm> pageParams = new PageResult<PmtTerm>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "SeqIdx";

        PageResult<PmtTerm> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }
}