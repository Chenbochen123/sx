using System;
using System.Collections.Generic;
using System.Data;
using Ext.Net;
using Mesnac.Business.Implements;
using Mesnac.Business.Interface;
using Mesnac.Data.Components;
using Mesnac.Entity;

/// <summary>
/// Manager_System_SysUser_UserManager 实现类
/// 孙本强 @ 2013-04-03 13:08:44
/// </summary>
/// <remarks></remarks>
public partial class Manager_System_SysUser_UserManager : Mesnac.Web.UI.Page
{
    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查询 = new SysPageAction() { ActionID = 1, ActionName = "btnSearch" };
            设置用户角色 = new SysPageAction() { ActionID = 2, ActionName = "SetUserRole" };
            初始化密码 = new SysPageAction() { ActionID = 3, ActionName = "ResetPassword" };
            清除密码 = new SysPageAction() { ActionID = 4, ActionName = "ClearPassword" };
        }
        public SysPageAction 查询 { get; private set; } //必须为 public
        public SysPageAction 设置用户角色 { get; private set; } //必须为 public
        public SysPageAction 初始化密码 { get; private set; } //必须为 public
        public SysPageAction 清除密码 { get; private set; } //必须为 public
    }
    #endregion

    #region 属性注入
    /// <summary>
    /// 
    /// 孙本强 @ 2013-04-03 13:08:44
    /// </summary>
    private IBasUserManager basUserManager = new BasUserManager();
    /// <summary>
    /// 
    /// 孙本强 @ 2013-04-03 13:08:44
    /// </summary>
    private ISysUserActionManager sysUserActionManager = new SysUserActionManager();
    /// <summary>
    /// 
    /// 孙本强 @ 2013-04-03 13:08:44
    /// </summary>
    private ISysUserRoleManager sysUserRoleManager = new SysUserRoleManager();
    #endregion
    #region 页面初始化
    /// <summary>
    /// Handles the Load event of the Page control.
    /// 孙本强 @ 2013-04-03 13:08:44
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    /// <remarks></remarks>
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    #endregion
    #region 分页查询获取数据
    /// <summary>
    /// 分页查询获取数据
    /// 孙本强 @ 2013-04-02 19:27:00
    /// 孙本强 @ 2013-04-03 13:08:45
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
        DataTable data = new DataTable();
        int total = 0;
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        BasUserManager.QueryParams queryParams = new BasUserManager.QueryParams();
        queryParams.pageParams.PageIndex = prms.Page;
        queryParams.pageParams.PageSize = prms.Limit;
        queryParams.pageParams.Orderfld = "WorkBarcode";

        queryParams.deleteFlag = "0";
        queryParams.workbarcode = txtUserName.Text;
        queryParams.realname = txtUserRealName.Text;
        PageResult<BasUser> lst = basUserManager.GetTablePageDataBySql(queryParams);

        data = lst.DataSet.Tables[0];
        total = lst.RecordCount;

        //X.Msg.Notify(data.Rows.Count.ToString(), data.Rows.Count.ToString()).Show();
        return new { data, total };
    }
    #endregion
    /// <summary>
    /// 清空密码
    /// 孙本强 @ 2013-04-02 19:27:00
    /// 孙本强 @ 2013-04-03 13:08:45
    /// </summary>
    /// <param name="id">The id.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    [DirectMethod]
    public string commandcolumn_direct_ClearPassword(string id)
    {
        if (this._.清除密码.SeqIdx == 0)
        {
            return "您没有进行清除密码的权限！";
        }
        //BasUser sys_user = new BasUser();
        //sys_user.ObjID = Convert.ToInt32(id);
        //sys_user.Attach();
        //sys_user.UserPWD = "";
        //basUserManager.Update(sys_user);
        //sysUserActionManager.ClearUserAction(id);
        //sysUserRoleManager.ClearUserRole(id);
        //X.Msg.Notify(id,"").Show();

        string sql = "delete SysUserAction where UserCode='" + id + "'";
        sysUserActionManager.GetBySql(sql).ToDataSet();

        sql = "delete SysUserRole where UserCode='"+ id + "'";
        sysUserRoleManager.GetBySql(sql).ToDataSet();

        sql = "update BasUser set UserPWD='' where WorkBarcode='"+ id + "'";
        basUserManager.GetBySql(sql).ToDataSet();
        return "已将用户密码清空，用户不能再进行登录！";
    }
    /// <summary>
    /// 初始化密码
    /// 孙本强 @ 2013-04-02 19:27:00
    /// 孙本强 @ 2013-04-03 13:08:45
    /// </summary>
    /// <param name="id">The id.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    [DirectMethod]
    public string commandcolumn_direct_ResetPassword(string id)
    {
        if (this._.初始化密码.SeqIdx == 0)
        {
            return "您没有进行初始化密码的权限！";
        }
        //BasUser sys_user = new BasUser();
        //sys_user.WorkBarcode = id;
        //sys_user.Attach();
        //sys_user.UserPWD = "C5BBA09E"; //8CA048F82FD0
        //X.Msg.Notify("用户", id).Show();
        //basUserManager.Update(sys_user);
        string sql = "update BasUser set UserPWD ='C5BBA09E' where WorkBarcode='" + id + "'";
        basUserManager.GetBySql(sql).ToDataSet();
        return "已将用户密码初始化为[123]！";
    }
}