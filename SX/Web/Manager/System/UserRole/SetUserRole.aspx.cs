using System;
using Ext.Net;
using Mesnac.Business.Implements;
using Mesnac.Business.Interface;
using Mesnac.Data.Components;
using Mesnac.Entity;
using System.Collections.Generic;
using System.Data;
using NBear.Common;

/// <summary>
/// Manager_System_UserRole_SetUserRole 实现类
/// 孙本强 @ 2013-04-03 13:09:35
/// </summary>
/// <remarks></remarks>
public partial class Manager_System_UserRole_SetUserRole : Mesnac.Web.UI.Page
{

    #region 属性注入
    /// <summary>
    /// 
    /// 孙本强 @ 2013-04-03 13:09:35
    /// </summary>
    private ISysUserRoleManager sysUserRoleManager = new SysUserRoleManager();
    /// <summary>
    /// 
    /// 孙本强 @ 2013-04-03 13:09:35
    /// </summary>
    private ISysRoleManager sysRoleManager = new SysRoleManager();
    #endregion

    #region 页面初始化
    /// <summary>
    /// Handles the Load event of the Page control.
    /// 孙本强 @ 2013-04-03 13:09:35
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    /// <remarks></remarks>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            InitUserRole();
        }
    }
    /// <summary>
    /// 初始化用户的角色列表
    /// </summary>
    private void InitUserRole()
    {
        
        OrderByClip oc = new OrderByClip();
        oc = SysRole._.SeqIdx.Asc;
        EntityArrayList<SysRole> alllst = sysRoleManager.GetAllListOrder(oc);

        string userid = this.Request["userid"].ToString();
        //X.Msg.Alert(userid,"").Show();
        WhereClip wc = new WhereClip();
        wc.And(SysUserRole._.UserCode == userid);
        EntityArrayList<SysUserRole> haslst = sysUserRoleManager.GetListByWhere(wc);

        List<SysRole> alldata = new List<SysRole>();
        List<SysRole> hasdata = new List<SysRole>();
        foreach (SysRole role in alllst)
        {
            bool ishas = false;
            foreach (SysUserRole has in haslst)
            {
                if (role.ObjID == has.RoleID)
                {
                    ishas = true;
                    break;
                }
            }
            if (ishas)
            {
                hasdata.Add(role);
            }
            else
            {
                alldata.Add(role);
            }
        }

        this.Store1.DataSource = alldata;
        this.Store1.DataBind();
        this.Store2.DataSource = hasdata;
        this.Store2.DataBind();
    }
    #endregion

    /// <summary>
    /// 清除用户角色
    /// 孙本强 @ 2013-04-02 19:27:00
    /// 孙本强 @ 2013-04-03 13:09:36
    /// </summary>
    /// <param name="userid">The userid.</param>
    /// <remarks></remarks>
    [DirectMethod]
    public void ClearUserRole(string userid)
    {
        sysUserRoleManager.DeleteByWhere(SysUserRole._.UserCode == userid);
    }
    /// <summary>
    /// 设置用户角色
    /// 孙本强 @ 2013-04-02 19:27:00
    /// 孙本强 @ 2013-04-03 13:09:36
    /// </summary>
    /// <param name="roleid">The roleid.</param>
    /// <param name="userid">The userid.</param>
    /// <remarks></remarks>
    [DirectMethod]
    public void setUserRole(string roleid, string userid)
    {
        //SysUserRole u = new SysUserRole();
        //u.UserCode = userid;
        //u.RoleID = Convert.ToInt32(roleid);
        //sysUserRoleManager.Insert(u);

        string sql = "insert into SysUserRole values('" + userid + "','" + roleid + "',GETDATE() ) ";
        sysUserRoleManager.GetBySql(sql).ToDataSet();
    }
    /// <summary>
    /// 设置用户角色
    /// 孙本强 @ 2013-04-02 19:27:00
    /// 孙本强 @ 2013-04-03 13:09:36
    /// </summary>
    /// <param name="roleid">The roleid.</param>
    /// <param name="userid">The userid.</param>
    /// <remarks></remarks>
    [DirectMethod]
    public void deleteUserRole(string roleid, string userid)
    {
        //sysUserRoleManager.DeleteByWhere(SysUserRole._.UserCode == userid && SysUserRole._.RoleID == roleid);
        string sql = "delete SysUserRole where UserCode='"+ userid + "' and RoleID='"+ roleid + "'";
        sysUserRoleManager.GetBySql(sql).ToDataSet();
    }

    /// <summary>
    /// 设置用户角色成功 yuany
    /// </summary>
    /// <param name="roleid">The roleid.</param>
    /// <param name="userid">The userid.</param>
    /// <remarks></remarks>
    [DirectMethod]
    public void setUserRoleSuccess(string flag)
    {
        InitUserRole();
    }
}