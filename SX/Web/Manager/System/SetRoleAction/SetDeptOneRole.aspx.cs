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


public partial class Manager_System_SetRoleAction_SetDeptOneRole : Mesnac.Web.UI.Page
{
    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            设置部门角色 = new SysPageAction() { ActionID = 1, ActionName = "btnSetRole" };
        }
        public SysPageAction 设置部门角色 { get; private set; } //必须为 public
    }
    #endregion

    #region 属性注入
    private IBasDeptManager basDeptManager = new BasDeptManager();
    private ISysRoleManager sysRoleManager = new SysRoleManager();
    private ISysDeptRoleManager sysDeptRoleManager = new SysDeptRoleManager();
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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack || X.IsAjaxRequest)
        {
            return;
        }
        SysRole role = sysRoleManager.GetListByWhere(SysRole._.ObjID == GetRequest("RoleID"))[0];
        TreePanel1.Title = "部门信息[" + role.RoleName + "]";
        EntityArrayList<SysDeptRole> lst = sysDeptRoleManager.GetListByWhere(SysDeptRole._.RoleID == GetRequest("RoleID"));
        StringBuilder sb = new StringBuilder(",");
        foreach (SysDeptRole m in lst)
        {
            sb.Append(m.DeptCode).Append(",");
        }
        hiddenHaveRole.Text = sb.ToString();
    }
    private void CheckNode(Node node)
    {
        if (hiddenHaveRole.Text.Contains("," + node.NodeID + ","))
        {
            node.Checked = true;
        }
    }
    private Node IniNode(object obj)
    {
        Node n = new Node();
        n.Checked = false;
        n.Icon = Icon.Building;
        PropertyInfo[] fields = obj.GetType().GetProperties();
        foreach (PropertyInfo f in fields)
        {
            ConfigItem c = new ConfigItem();
            object value = f.GetValue(obj, null);
            c.Name = f.Name.ToString();
            if (f.Name.ToString().ToLower() == "DeleteFlag".ToLower())
            {
                if (value == null)
                {
                    c.Value = "无";
                }
                else
                {
                    c.Value = value.ToString() == "1" ? "停用" : "正常";
                }
            }
            else
            {
                c.Value = value == null ? string.Empty : value.ToString();
            }
            c.Mode = ParameterMode.Value;
            n.CustomAttributes.Add(c);
        }
        return n;
    }
    private void IniDeptTree(Node node, int leavel, string parCode)
    {
        EntityArrayList<BasDept> lst = basDeptManager.GetListByWhereAndOrder(BasDept._.DepLevel == leavel + 1 && BasDept._.ParentNum == parCode, BasDept._.DisplayId.Asc);
        if (lst.Count == 0)
        {
            node.Icon = Icon.BuildingGo;
            node.Leaf = true;
            return;
        }
        foreach (BasDept m in lst)
        {
            Node n = IniNode(m);
            n.NodeID = m.DepCode;
            CheckNode(n);
            if (basDeptManager.GetRowCountByWhere(BasDept._.DepLevel == leavel + 2 && BasDept._.ParentNum == m.DepCode) == 0)
            {
                n.Icon = Icon.BuildingGo;
                n.Leaf = true;
            }
            else
            {
                n.Icon = Icon.Building;
                n.Leaf = false;
            }
            node.Children.Add(n);
        }
    }
    [DirectMethod]
    public Node IniDeptTree(string ss)
    {
        Node node = new Node();
        EntityArrayList<BasDept> lst = new EntityArrayList<BasDept>();
        if (ss.ToLower() == "root".ToLower())
        {
            lst = basDeptManager.GetListByWhereAndOrder(BasDept._.DepLevel == 1 && BasDept._.ParentNum == "00000", BasDept._.DisplayId.Asc);
            foreach (BasDept m in lst)
            {
                Node n = IniNode(m);
                n.NodeID = m.DepCode;
                CheckNode(n);
                n.Icon = Icon.Building;
                n.Leaf = basDeptManager.GetRowCountByWhere(BasDept._.ParentNum == m.DepCode) == 0;
                node.Children.Add(n);
            }
        }
        else
        {
            lst = basDeptManager.GetListByWhere(BasDept._.DepCode == ss);
            foreach (BasDept m in lst)
            {
                IniDeptTree(node, (int)m.DepLevel, m.DepCode);
            }
        }
        return node;//.Children.ToJson();
    }


    [DirectMethod]
    public string ResetRoleAction(string depts, string roles)
    {
        if (this._.设置部门角色.SeqIdx == 0)
        {
            return "您没有进行设置部门角色的权限！";
        }
        string Result = string.Empty;
        List<SysDeptRole> lst = new List<SysDeptRole>();
        string[] deptList = depts.Split('|');

        int irole = 0;
        if (!int.TryParse(GetRequest("RoleID"), out irole))
        {
            return "请选择角色";
        }
        string[] roleList = roles.Split('|');
        foreach (string dept in deptList)
        {
            if (string.IsNullOrWhiteSpace(dept))
            {
                continue;
            }
            sysDeptRoleManager.DeleteByWhere(SysDeptRole._.RoleID == irole && SysDeptRole._.DeptCode == dept);
        }
        foreach (string dept in roleList)
        {
            if (string.IsNullOrWhiteSpace(dept))
            {
                continue;
            }
            SysDeptRole m = new SysDeptRole();
            m.DeptCode = dept;
            m.RoleID = irole;
            lst.Add(m);
        }
        sysDeptRoleManager.BatchInsert(lst);
        return Result;
    }
}