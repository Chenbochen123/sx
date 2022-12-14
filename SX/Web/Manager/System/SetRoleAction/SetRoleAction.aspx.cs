using System;
using System.Collections.Generic;
using System.Data;
using Ext.Net;
using Mesnac.Business.Implements;
using Mesnac.Business.Interface;
using Mesnac.Entity;
using NBear.Common;
using System.Reflection;


public partial class Manager_System_SetRoleAction_SetRoleAction : Mesnac.Web.UI.Page
{
    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            设置角色权限 = new SysPageAction() { ActionID = 1, ActionName = "btnSetRole" };
        }
        public SysPageAction 设置角色权限 { get; private set; } //必须为 public
    }
    #endregion

    #region 属性注入
    private ISysRoleManager sysRoleManager = new SysRoleManager();
    private ISysPageActionManager sysPageActionManager = new SysPageActionManager();

    private ISysRoleActionManager sysRoleActionManager = new SysRoleActionManager();
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
    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        EntityArrayList<SysRole> lst = sysRoleManager.GetListByWhereAndOrder(SysRole._.DeleteFlag == "0", SysRole._.ObjID.Asc);
        int total = lst.Count;
        return new { data = lst, total };
    }


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
}