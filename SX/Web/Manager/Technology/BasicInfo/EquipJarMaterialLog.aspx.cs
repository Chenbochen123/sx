using System;
using System.Collections.Generic;
using Ext.Net;
using Mesnac.Business.Implements;
using Mesnac.Business.Interface;
using Mesnac.Entity;
using NBear.Common;
using Mesnac.Data.Components;
using System.Data;
using Newtonsoft.Json;

public partial class Manager_Technology_BasicInfo_EquipJarMaterialLog : Mesnac.Web.UI.Page
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
    /// 孙本强 @ 2013-04-03 13:05:55
    /// </summary>
    private IPmtEquipJarStoreLogManager jarStoreLogManager = new PmtEquipJarStoreLogManager();
    private ISysCodeManager syscodeManager = new SysCodeManager();
    #endregion

    #region 页面初始化
    /// <summary>
    /// Handles the Load event of the Page control.
    /// 孙本强 @ 2013-04-03 13:05:55
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    /// <remarks></remarks>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (X.IsAjaxRequest)
        {
            return;
        }
        EntityArrayList<SysCode> sysCodeList = syscodeManager.GetListByWhere(SysCode._.TypeID == "EquipJar");
        foreach (SysCode code in sysCodeList)
        {
            Ext.Net.ListItem item = new ListItem(code.ItemName, code.ItemCode);
            txtJarType.Items.Add(item);
        }
        txtBeginTime.Text = DateTime.Now.AddYears(-1).ToString("yyyy-MM-dd");
        txtEndTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
    }
    /// <summary>
    /// Reds the HTML.
    /// 孙本强 @ 2013-04-03 13:05:56
    /// </summary>
    /// <param name="ss">The ss.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    private string RedHtml(string ss)
    {
        return "<font color='red'>" + ss + "</font>";
    }
    /// <summary>
    /// Defaults the HTML.
    /// 孙本强 @ 2013-04-03 13:05:56
    /// </summary>
    /// <param name="ss">The ss.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    private string DefaultHtml(string ss)
    {
        return ss;
    }
    #endregion

    #region 查询显示右侧
    /// <summary>
    /// Grids the panel bind data.
    /// yuany @  2014-04-02 11:16:59
    /// </summary>
    /// <param name="action">The action.</param>
    /// <param name="extraParams">The extra params.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        //if (this._.查询.SeqIdx == 0)
        //{
        //    return null;
        //}
        DataTable data = new DataTable();
        int total = 0;
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PmtEquipJarStoreLogManager.QueryParams queryParams = new PmtEquipJarStoreLogManager.QueryParams();
        queryParams.PageParams.PageIndex = prms.Page;
        queryParams.PageParams.PageSize = prms.Limit;
        queryParams.EquipCode = txtEquipCode.Text;
        queryParams.JarType = txtJarType.SelectedItem.Value;
        queryParams.BeginTime = txtBeginTime.Text;
        queryParams.EndTime = txtEndTime.Text;
        PageResult<PmtEquipJarStoreLog> lst = jarStoreLogManager.GetTablePageDataBySql(queryParams);
        data = lst.DataSet.Tables[0];

        total = lst.RecordCount;
        return new { data, total };
    }
    #endregion
}