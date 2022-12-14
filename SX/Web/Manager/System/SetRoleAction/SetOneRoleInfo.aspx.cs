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


public partial class Manager_System_SetRoleAction_SetOneRoleInfo : Mesnac.Web.UI.Page
{
    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            设置角色权限 = new SysPageAction() { ActionID = 1, ActionName = "btnSetRole" };
            复制角色权限 = new SysPageAction() { ActionID = 2, ActionName = "btnRoleCopy" };
        }
        public SysPageAction 设置角色权限 { get; private set; } //必须为 public
        public SysPageAction 复制角色权限 { get; private set; } //必须为 public
    }
    #endregion

    #region 属性注入
    private ISysRoleManager sysRoleManager = new SysRoleManager();
    private ISysPageActionManager sysPageActionManager = new SysPageActionManager();
    private ISysRoleActionManager sysRoleActionManager = new SysRoleActionManager();

    private IBasDeptManager basDeptManager = new BasDeptManager();
    private ISysDeptRoleManager sysDeptRoleManager = new SysDeptRoleManager();
    private ISysUserRoleManager sysUserRoleManager = new SysUserRoleManager();
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
        IniActionTree(TreePanel1);
    }

    #region 初始化权限
    private Node IniNodeByTable(DataRow row, DataTable menuTable, DataTable actionTable)
    {
        Node n = new Node();
        n.Checked = false;
        n.Icon = Icon.Building;
        string[] ss = new string[] { "ObjID", "ShowName" };
        foreach (string s in ss)
        {
            ConfigItem c = new ConfigItem();
            c.Name = s;
            object value = row[c.Name];
            c.Value = value == null ? string.Empty : value.ToString();
            c.Mode = ParameterMode.Value;
            n.CustomAttributes.Add(c);
        }
        return n;
    }
    private List<DataRow> MenuPageSelect(DataTable menu, string menulevel)
    {
        List<DataRow> Result = new List<DataRow>();
        foreach (DataRow row in menu.Rows)
        {
            string thisMid = row["MenuLevel"].ToString();
            if ((thisMid.Length == menulevel.Length + 2) && (thisMid.StartsWith(menulevel)))
            {
                Result.Add(row);
            }
        }
        return Result;
    }
    private List<DataRow> PageActionSelect(DataTable action, string pageid)
    {
        List<DataRow> Result = new List<DataRow>();
        foreach (DataRow row in action.Rows)
        {
            if (row["PageMenuID"].ToString() == pageid)
            {
                Result.Add(row);
            }
        }
        return Result;
    }
    private void IniActionTree(Node node, DataRow parRow, DataTable menuTable, DataTable actionTable)
    {
        List<DataRow> MenuRows = MenuPageSelect(menuTable, parRow["MenuLevel"].ToString());
        List<DataRow> ActionRows = PageActionSelect(actionTable, parRow["ObjID"].ToString());
        foreach (DataRow row in MenuRows)
        {
            Node n = IniNodeByTable(row, menuTable, actionTable);
            n.NodeID = pageidNodeIDStarWith + row["ObjID"];
            n.Leaf = false;
            IniActionTree(n, row, menuTable, actionTable);
            node.Children.Add(n);
        }
        foreach (DataRow row in ActionRows)
        {
            Node n = IniNodeByTable(row, menuTable, actionTable);
            n.NodeID = actionidNodeIDStarWith + row["ObjID"];
            n.Leaf = true;
            node.Children.Add(n);
        }
    }
    private void IniActionTree(TreePanel tree)
    {
        DataSet menuaction = sysPageActionManager.GetAllPageMenuAction();
        if (menuaction.Tables.Count < 2)
        {
            return;
        }
        DataTable menuTable = menuaction.Tables[0];
        DataTable actionTable = menuaction.Tables[1];
        foreach (DataRow row in menuTable.Rows)
        {
            if (row["MenuLevel"].ToString().Length == 2)
            {
                Node n = IniNodeByTable(row, menuTable, actionTable);
                n.NodeID = pageidNodeIDStarWith + row["ObjID"];
                n.Leaf = false;
                IniActionTree(n, row, menuTable, actionTable);
                tree.GetRootNode().AppendChild(n);
            }
        }
    }
    #endregion

    #region 初始化部门(完整部门，加载慢)
    private Node IniNode(string role, object obj)
    {
        Node n = new Node();
        if (obj is BasDept)
        {
            string deptcode = (obj as BasDept).DepCode;
            if (sysDeptRoleManager.GetRowCountByWhere(SysDeptRole._.DeptCode == deptcode && SysDeptRole._.RoleID == role) > 0)
            {
                n.Checked = true;
            }
        }
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
    private void IniDeptTree(string role, Node node, int leavel, string parCode)
    {
        node.NodeID = parCode;
        EntityArrayList<BasDept> lst = basDeptManager.GetListByWhereAndOrder(BasDept._.DepLevel == leavel + 1 && BasDept._.ParentNum == parCode, BasDept._.DisplayId.Asc);
        if (lst.Count == 0)
        {
            node.Icon = Icon.BuildingGo;
            node.Leaf = true;
            return;
        }
        node.Icon = Icon.Building;
        node.Leaf = false;
        foreach (BasDept m in lst)
        {
            Node n = IniNode(role, m);
            IniDeptTree(role, n, (int)m.DepLevel, m.DepCode);
            node.Children.Add(n);
        }
    }



    private void IniDeptTree(string role, TreePanel tree)
    {
        tree.GetRootNode().RemoveAll();
        Node node = new Node();
        EntityArrayList<BasDept> lst = new EntityArrayList<BasDept>();
        lst = basDeptManager.GetListByWhereAndOrder(BasDept._.DepLevel == 1 && BasDept._.ParentNum == "00000", BasDept._.DisplayId.Asc);
        foreach (BasDept m in lst)
        {
            Node n = IniNode(role, m);
            IniDeptTree(role, n, (int)m.DepLevel, m.DepCode);
            tree.GetRootNode().AppendChild(n);
        }
    }
    #endregion

    #region 初始化角色
    [DirectMethod]
    public object GridPanelBindRoleData(string action, Dictionary<string, object> extraParams)
    {
        EntityArrayList<SysRole> lst = sysRoleManager.GetListByWhereAndOrder(SysRole._.DeleteFlag == "0", SysRole._.ObjID.Asc);
        int total = lst.Count;
        return new { data = lst, total };
    }
    #endregion

    #region 初始化部门
    private Dictionary<string, DeptNode> deptInfo = new Dictionary<string, DeptNode>();
    private Dictionary<string, DeptNode> checkedDeptInfo = new Dictionary<string, DeptNode>();
    private class DeptNode
    {
        public DeptNode()
        {
            this.Checked = false;
            this.DeptInfo = new BasDept();
            this.ParDeptNode = null;
            this.Child = new List<DeptNode>();
        }
        public bool Checked { get; set; }
        public BasDept DeptInfo { get; set; }
        public DeptNode ParDeptNode { get; set; }
        public List<DeptNode> Child { get; set; }
    }
    private void IniDeptNodeParent(DeptNode node)
    {
        try
        {
            DeptNode pnode = new DeptNode();
            BasDept dept = basDeptManager.GetListByWhere(BasDept._.DepCode == node.DeptInfo.ParentNum)[0];
            if (deptInfo.TryGetValue(dept.DepCode, out pnode))
            {
                node.ParDeptNode = pnode;
                if (!deptInfo.ContainsKey(node.DeptInfo.DepCode))
                {
                    deptInfo.Add(node.DeptInfo.DepCode, node);
                    pnode.Child.Add(node);
                }
                return;
            }
            else
            {
                pnode = new DeptNode();
                pnode.DeptInfo = dept;
                node.ParDeptNode = pnode;
                if (!deptInfo.ContainsKey(node.DeptInfo.DepCode))
                {
                    deptInfo.Add(node.DeptInfo.DepCode, node);
                    pnode.Child.Add(node);
                }
                if (checkedDeptInfo.ContainsKey(pnode.DeptInfo.DepCode))
                {
                    pnode.Checked = true;
                }
                IniDeptNodeParent(pnode);
                deptInfo.Add(dept.DepCode, pnode);
            }
        }
        catch
        {
        }
    }
    private void IniDeptNode(string role)
    {
        EntityArrayList<SysDeptRole> lst = sysDeptRoleManager.GetListByWhere(SysDeptRole._.RoleID == role);
        foreach (SysDeptRole m in lst)
        {
            DeptNode node = new DeptNode();
            BasDept dept = basDeptManager.GetListByWhere(BasDept._.DepCode == m.DeptCode)[0];
            node.DeptInfo = dept;
            node.Checked = true;
            checkedDeptInfo.Add(dept.DepCode, node);
        }
        foreach (DeptNode node in checkedDeptInfo.Values)
        {
            IniDeptNodeParent(node);
        }
    }
    private Node IniNode(object obj)
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
    int i = 0;
    private void IniDeptNode(DeptNode parDeptNode, Node parTreeNode)
    {
        parTreeNode.NodeID = parDeptNode.DeptInfo.DepCode + (i++).ToString();
        parTreeNode.Text = parDeptNode.DeptInfo.DepName;
        //parTreeNode = IniNode(parDeptNode.DeptInfo);
        if (parDeptNode.Checked)
        {
            parTreeNode.Checked = true;
        }
        if (parDeptNode.Child.Count == 0)
        {
            parTreeNode.Leaf = true;
            return;
        }
        foreach (DeptNode deptNode in parDeptNode.Child)
        {
            Node n = new Node();
            IniDeptNode(deptNode, n);
            parTreeNode.Children.Add(n);
        }
    }
    private void IniDeptNode(TreePanel tree)
    {
        foreach (DeptNode deptNode in deptInfo.Values)
        {
            if (deptNode.ParDeptNode == null)
            {
                Node n = new Node();
                n.Expanded = true;
                IniDeptNode(deptNode, n);
                tree.GetRootNode().AppendChild(n);
            }
        }
    }
    private void IniDeptNode(string role, TreePanel tree)
    {
        tree.GetRootNode().RemoveAll();
        if (string.IsNullOrWhiteSpace(role))
        {
            return;
        }
        IniDeptNode(role);
        IniDeptNode(tree);
    }
    #endregion

    #region 初始化人员
    [DirectMethod]
    public object GridPanelBindUserData(string action, Dictionary<string, object> extraParams)
    {
        string roleid = hiddenRoleID.Text;
        IniDeptNode(roleid, TreePanel2);
        if (string.IsNullOrWhiteSpace(roleid))
        {
            return null;
        }
        SysRole role = new SysRole();
        role.ObjID = int.Parse(roleid);
        BasUser user = new BasUser();
        EntityArrayList<BasUser> lst = sysUserRoleManager.GetRoleUserList(role, user);
        int total = lst.Count;
        return new { data = lst, total };
    }
    #endregion

    #region 权限设置
    [DirectMethod]
    public string ResetRoleAction(string roles, string actions)
    {
        if (this._.设置角色权限.SeqIdx == 0)
        {
            return "您没有进行设置角色权限的权限！";
        }
        string Result = string.Empty;
        List<SysRoleAction> lst = new List<SysRoleAction>();
        string[] roleList = roles.Split('|');
        string[] actionList = actions.Split('|');
        foreach (string role in roleList)
        {
            if (string.IsNullOrWhiteSpace(role))
            {
                continue;
            }
            sysRoleActionManager.DeleteByWhere(SysRoleAction._.RoleID == role);
            foreach (string action in actionList)
            {
                if (string.IsNullOrWhiteSpace(action))
                {
                    continue;
                }
                int irole = 0;
                if (!int.TryParse(role, out irole))
                {
                    continue;
                }
                int iaction = 0;
                if (!int.TryParse(action, out iaction))
                {
                    continue;
                }
                SysRoleAction m = new SysRoleAction();
                m.RoleID = irole;
                m.ActionID = iaction;
                lst.Add(m);
            }
        }
        sysRoleActionManager.BatchInsert(lst);
        return Result;
    }
    #endregion

    #region 权限显示赋值
    [DirectMethod]
    public string GetActionInfo(string role)
    {
        StringBuilder Result = new StringBuilder("|");
        EntityArrayList<SysRoleAction> lst = sysRoleActionManager.GetListByWhere(SysRoleAction._.RoleID == role);
        foreach (SysRoleAction m in lst)
        {
            Result.Append(m.ActionID).Append("|");
        }
        return Result.ToString();
    }
    #endregion

    #region 权限拷贝
    /// <summary>
    /// 拷贝角色权限
    /// 孙本强 @ 2013-04-02 19:27:00
    /// </summary>
    /// <param name="sourceRoleID">The source role ID.</param>
    /// <param name="targetRoleID">The target role ID.</param>
    /// <remarks></remarks>
    [DirectMethod]
    public void SetRoleActionByOther(string sourceRoleID, string targetRoleID)
    {
        if (this._.复制角色权限.SeqIdx == 0)
        {
            X.Msg.Alert("提示", "您没有进行复制角色权限的权限！").Show();
            return;
        }
        if (sourceRoleID == targetRoleID)
        {
            return;
        }
        sysRoleActionManager.ClearRoleAction(targetRoleID);
        sysRoleActionManager.CopyForm(sourceRoleID, targetRoleID);
    }
    #endregion

}