using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Ext.Net;
using Mesnac.Business.Implements;
using Mesnac.Business.Interface;
using Mesnac.Entity;
using NBear.Common;
using System.Reflection;
using Mesnac.Data.Components;


public partial class Manager_System_SetRoleAction_SetUserOneRole : Mesnac.Web.UI.Page
{
    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            设置用户角色 = new SysPageAction() { ActionID = 1, ActionName = "btnSetRole" };
        }
        public SysPageAction 设置用户角色 { get; private set; } //必须为 public
    }
    #endregion

    #region 属性注入
    private IBasDeptManager basDeptManager = new BasDeptManager();
    private ISysPageActionManager sysPageActionManager = new SysPageActionManager();

    private ISysRoleManager sysRoleManager = new SysRoleManager();
    private IBasUserManager basUserManager = new BasUserManager();
    private ISysUserRoleManager sysUserRoleManager = new SysUserRoleManager();
    #endregion

    #region 常量定义
    readonly string pageidNodeIDStarWith = "pageid=";
    readonly string actionidNodeIDStarWith = "actionid=";
    #endregion
    /// <summary>
    /// Gets the request.
    /// 孙本强 @ 2013-04-03 13:05:38
    /// </summary>
    /// <param name="key">The key.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    private string GetRequest(string key)
    {
        if (this.Request[key] != null)
        {
            return this.Request[key].ToString();
        }
        return string.Empty;
    }

    #region 初始化方法
    /// <summary>
    /// 页面初始化方法
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            initUserRoleList();
        }
    }
    #endregion

    #region 绑定方法
    /// <summary>
    /// 绑定所有用户列表的方法
    /// yuany 2014年4月19日16:48:07
    /// </summary>
    /// <param name="action"></param>
    /// <param name="extraParams"></param>
    /// <returns></returns>
    [DirectMethod]
    public object GridPanelBindUserData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<BasUser> pageParams = new PageResult<BasUser>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "ObjID ASC";

        PageResult<BasUser> lst = GetPageResultUserData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }
    /// <summary>
    /// 绑定用户的查询分页方法
    /// </summary>
    /// <param name="pageParams"></param>
    /// <returns></returns>
    private PageResult<BasUser> GetPageResultUserData(PageResult<BasUser> pageParams)
    {
        BasUserManager.QueryParams queryParams = new BasUserManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.userName = txtUserName.Text.TrimEnd().TrimStart();
        queryParams.hrcode = txtHRCode.Text.TrimEnd().TrimStart();

        return basUserManager.GetTablePageDataBySql(queryParams);
    }

    /// <summary>
    /// 校验拖拽人员重复性的方法
    /// </summary>
    /// <param name="usercode"></param>
    /// <param name="jsonStr"></param>
    /// <returns></returns>
    [Ext.Net.DirectMethod]
    public string set_user_power_drop(string usercode, string jsonStr)
    {
        EntityArrayList<BasUser> userList = new EntityArrayList<BasUser>();
        Dictionary<string, string>[] Dic = JSON.Deserialize<Dictionary<string, string>[]>(jsonStr);
        string workBarcodes = "";
        foreach (Dictionary<string, string> row in Dic)
        {
            BasUser user = new BasUser();
            user.UserName = row["UserName"];
            user.WorkBarcode = row["WorkBarcode"];
            user.HRCode = row["HRCode"];
            userList.Add(user);
            workBarcodes += row["WorkBarcode"] + "|";
        }
        if (workBarcodes.IndexOf(usercode) == -1)
        {
            EntityArrayList<BasUser> addUser = basUserManager.GetListByWhere(BasUser._.WorkBarcode == usercode);
            if (addUser.Count > 0)
            {
                userList.Add(addUser[0]);
                SetUserStore.Data = userList;
                SetUserStore.DataBind();
                return "添加用户:" + addUser[0].UserName + "成功";
            }
            return "添加用户异常，请联系管理员";
        }
        else
        {
            SetUserStore.Data = userList;
            SetUserStore.DataBind();

            return "请勿重复添加此用户";
        }

    }

    private void initUserRoleList()
    {
        SysRole role = new SysRole();
        role.ObjID = Convert.ToInt32(GetRequest("RoleID"));
        BasUser user = new BasUser();
        EntityArrayList<BasUser> userList = sysUserRoleManager.GetRoleUserList(role, user);
        SetUserStore.Data = userList;
        SetUserStore.DataBind();
    }
    #endregion

    /// <summary>
    /// 设置用户权限按钮点击方法
    /// 2014年4月19日16:47:42 yuay
    /// </summary>
    /// <param name="users"></param>
    /// <returns></returns>
    [DirectMethod]
    public string ResetRoleAction(string users)
    {
        if (this._.设置用户角色.SeqIdx == 0)
        {
            return "您没有进行设置用户角色的权限！";
        }
        string Result = string.Empty;
        List<SysUserRole> lst = new List<SysUserRole>();

        Dictionary<string, string>[] userDic = JSON.Deserialize<Dictionary<string, string>[]>(users);
        string tempstr = "";
        foreach (Dictionary<string, string> user in userDic)
        {
            tempstr = tempstr + user["WorkBarcode"] + "|";
        };
        string[] userList = tempstr.Split('|');
        string[] roleList = GetRequest("RoleID").Split('|');
        foreach (string role in roleList)
        {
            if (string.IsNullOrWhiteSpace(role))
            {
                continue;
            }
            int irole = 0;
            if (!int.TryParse(role, out irole))
            {
                continue;
            }
            sysUserRoleManager.DeleteByWhere(SysUserRole._.RoleID == irole);
            int i = 0;
            
            foreach (string user in userList)
            {
                if (string.IsNullOrWhiteSpace(user))
                {
                    continue;
                }
                if (i == 0)
                {

                    SysUserRole m = new SysUserRole();
                    m.UserCode = user;
                    m.RoleID = irole;
                    m.ObjID = 0;
                    lst.Add(m);
                    i = i + 1;
                }
                else
                {
                    SysUserRole m = new SysUserRole();
                    m.UserCode = user;
                    m.RoleID = irole;
                    m.ObjID = i;
                    lst.Add(m);
                    i = i + 1;
                }

            }
        }
        sysUserRoleManager.BatchInsert(lst);
        return Result;
    }
}