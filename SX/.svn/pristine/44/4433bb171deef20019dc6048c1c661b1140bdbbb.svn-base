using System;
using System.Collections.Generic;
using System.Data;
using Ext.Net;
using Mesnac.Business.Implements;
using Mesnac.Business.Interface;
using Mesnac.Entity;
using NBear.Common;
using System.Reflection;


public partial class Manager_System_SetRoleAction_SetUserActionEqual : Mesnac.Web.UI.Page
{
    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            设置等同权限用户 = new SysPageAction() { ActionID = 1, ActionName = "btnSetRole" };
        }
        public SysPageAction 设置等同权限用户 { get; private set; } //必须为 public
    }
    #endregion

    #region 属性注入
    private IBasDeptManager basDeptManager = new BasDeptManager();
    private ISysPageActionManager sysPageActionManager = new SysPageActionManager();

    private ISysRoleManager sysRoleManager = new SysRoleManager();
    private IBasUserManager basUserManager = new BasUserManager();
    private ISysUserActionEqualManager sysUserActionEqualManager = new SysUserActionEqualManager();
    #endregion

    #region 常量定义
    readonly string pageidNodeIDStarWith = "pageid=";
    readonly string actionidNodeIDStarWith = "actionid=";
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack || X.IsAjaxRequest)
        {
            return;
        }
    }
    private Node IniNodeByProperty(object obj)
    {
        Node n = new Node();
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
            Node n = IniNodeByProperty(m);
            n.NodeID = m.DepCode;
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
                Node n = IniNodeByProperty(m);
                n.NodeID = m.DepCode;
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
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        string deptcode = hiddenDeptCode.Text;
        if (string.IsNullOrWhiteSpace(deptcode))
        {
            return new { data = new EntityArrayList<BasUser>(), total = 0 };
        }
        EntityArrayList<BasUser> lst = basUserManager.GetListByWhere(BasUser._.DeptCode == deptcode && BasUser._.DeleteFlag == "0");
        return new { data = lst, total = lst.Count };
    }

    [DirectMethod]
    public string ResetRoleAction(string user1s, string user2s)
    {
        if (this._.设置等同权限用户.SeqIdx == 0)
        {
            return "您没有进行设置等同权限用户的权限！";
        }
        string Result = string.Empty;
        List<SysUserActionEqual> lst = new List<SysUserActionEqual>();
        string[] user1List = user1s.Split('|');
        string[] user2List = user2s.Split('|');
        foreach (string user1 in user1List)
        {
            if (string.IsNullOrWhiteSpace(user1))
            {
                continue;
            }
            sysUserActionEqualManager.DeleteByWhere(SysUserActionEqual._.User1Code == user1);
            foreach (string user2 in user2List)
            {
                if (string.IsNullOrWhiteSpace(user2))
                {
                    continue;
                }
                if (user1 == user2)
                {
                    continue;
                }
                SysUserActionEqual m = new SysUserActionEqual();
                m.User1Code = user1;
                m.User2Code = user2;
                lst.Add(m);
            }
        }
        sysUserActionEqualManager.BatchInsert(lst);
        return Result;
    }
}