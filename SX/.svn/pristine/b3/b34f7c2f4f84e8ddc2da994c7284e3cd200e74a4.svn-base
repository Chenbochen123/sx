using System;
using System.Collections.Generic;
using System.Data;
using Ext.Net;
using Mesnac.Business.Implements;
using Mesnac.Business.Interface;
using Mesnac.Data.Components;
using Mesnac.Entity;
using Mesnac.Data.Implements;

/// <summary>
/// Manager_System_SysLog_LoginLogQuery 实现类
/// 孙本强 @ 2013-04-03 13:08:13
/// </summary>
/// <remarks></remarks>
public partial class Manager_System_SysLog_LoginLogQuery : Mesnac.Web.UI.Page
{
    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查询 = new SysPageAction() { ActionID = 1, ActionName = "btnSearch" };
            导出 = new SysPageAction() { ActionID = 2, ActionName = "btnExport" };
        }
        public SysPageAction 查询 { get; private set; } //必须为 public
        public SysPageAction 导出 { get; private set; } //必须为 public
    }
    #endregion
    #region 属性注入
    /// <summary>
    /// 
    /// 孙本强 @ 2013-04-03 13:08:13
    /// </summary>
    private ISysLoginLogManager sysLoginLogManager = new SysLoginLogManager();
    #endregion

    #region 页面初始化
    /// <summary>
    /// Handles the Load event of the Page control.
    /// 孙本强 @ 2013-04-03 13:08:13
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    /// <remarks></remarks>
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((!X.IsAjaxRequest) && (!IsPostBack))
        {
            txtBeginTime.Text = DateTime.Now.AddDays(0).ToString("yyyy-MM-dd");
            txtEndTime.Text = DateTime.Now.AddDays(0).ToString("yyyy-MM-dd");
            txtLoginState.Items.Add(new ListItem() { Text = "所有日志", Value = "0" });
            txtLoginState.Items.Add(new ListItem() { Text = "在线", Value = "1" });
            txtLoginState.Items.Add(new ListItem() { Text = "已退出", Value = "2" });
            txtLoginState.Text = "0";
        }
    }
    #endregion

    #region 分页查询获取数据
    /// <summary>
    /// 分页查询获取数据
    /// 孙本强 @ 2013-04-02 19:27:00
    /// 孙本强 @ 2013-04-03 13:08:14
    /// </summary>
    /// <param name="pageParams">The page params.</param>
    /// <returns>分页数据集</returns>
    /// <remarks></remarks>
    private PageResult<SysLoginLog> GetPageResultData(PageResult<SysLoginLog> pageParams)
    {
        SysLoginLogManager.QueryParams queryParams = new SysLoginLogManager.QueryParams();
        queryParams.PageParams = pageParams;
        queryParams.BeginTime = Convert.ToDateTime(txtBeginTime.Text);
        queryParams.EndTime = Convert.ToDateTime(txtEndTime.Text).AddDays(1);
        queryParams.LoginState = txtLoginState.Text;
        queryParams.UserName = txtUserName.Text;
        queryParams.UserRealName = txtUserRealName.Text;

        return sysLoginLogManager.GetTablePageDataBySql(queryParams);
    }
    /// <summary>
    /// 分页查询获取数据 绑定 Grid
    /// 孙本强 @ 2013-04-02 19:27:00
    /// 孙本强 @ 2013-04-03 13:08:14
    /// </summary>
    /// <param name="action">The action.</param>
    /// <param name="extraParams">The extra params.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        if (this._.查询.SeqIdx==0)
        {
            return null;
        }
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<SysLoginLog> pageParams = new PageResult<SysLoginLog>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "ObjID DESC";

        PageResult<SysLoginLog> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }
    #endregion

    #region 数据导出
    /// <summary>
    /// 数据导出
    /// 孙本强 @ 2013-04-02 19:27:00
    /// 孙本强 @ 2013-04-03 13:08:14
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    /// <remarks></remarks>
    protected void btnExportSubmit_Click(object sender, EventArgs e)
    {
        PageResult<SysLoginLog> pageParams = new PageResult<SysLoginLog>();
        pageParams.PageSize = -100;
        PageResult<SysLoginLog> lst = GetPageResultData(pageParams);
        for (int i = 0; i < lst.DataSet.Tables[0].Columns.Count; i++)
        {
            bool isshow = false;
            DataColumn dc = lst.DataSet.Tables[0].Columns[i];
            foreach (ColumnBase cb in this.gridPanelCenter.ColumnModel.Columns)
            {
                if ((cb.DataIndex != null) && (cb.DataIndex.ToUpper() == dc.ColumnName.ToUpper()))
                {
                    dc.ColumnName = cb.Text;
                    isshow = true;
                    break;
                }
            }
            if (!isshow)
            {
                lst.DataSet.Tables[0].Columns.Remove(dc.ColumnName);
                i--;
            }
        }

        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(lst.DataSet, "登录日志");
    }
    #endregion
}
