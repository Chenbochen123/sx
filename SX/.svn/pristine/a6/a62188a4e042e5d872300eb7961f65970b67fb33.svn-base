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


public partial class Manager_System_SetRoleAction_SetOneUserAction : Mesnac.Web.UI.Page
{
    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            设置用户权限 = new SysPageAction() { ActionID = 1, ActionName = "btnSetRole" };
            复制用户权限 = new SysPageAction() { ActionID = 2, ActionName = "btnRoleCopy" };
        }
        public SysPageAction 设置用户权限 { get; private set; } //必须为 public
        public SysPageAction 复制用户权限 { get; private set; } //必须为 public
    }
    #endregion

    #region 属性注入
    private IBasDeptManager basDeptManager = new BasDeptManager();
    private ISysPageActionManager sysPageActionManager = new SysPageActionManager();

    private IBasUserManager basUserManager = new BasUserManager();
    private ISysUserActionManager sysUserActionManager = new SysUserActionManager();
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
        IniActionTree(TreePanel2);
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
        string deptcode = hiddenDeptCode.Text;
        if (string.IsNullOrWhiteSpace(deptcode) && string.Empty.Equals(txt_user_name.Text) && string.Empty.Equals(txt_work_barcode.Text))
        {
            return new { data = new EntityArrayList<BasUser>(), total = 0 };
        }
        if (string.Empty.Equals(deptcode))
        {
            EntityArrayList<BasUser> lst = basUserManager.GetListByWhere(BasUser._.UserName.Like("%" + txt_user_name.Text + "%")
                && BasUser._.WorkBarcode.Like(txt_work_barcode.Text + "%") && BasUser._.DeleteFlag == "0");
            return new { data = lst, total = lst.Count };
        }
        else
        {
            EntityArrayList<BasUser> lst = basUserManager.GetListByWhere(BasUser._.DeptCode == deptcode && BasUser._.DeleteFlag == "0");
            return new { data = lst, total = lst.Count };
        }
    }

    [DirectMethod]
    public string ResetRoleAction(string users, string actions)
    {
        if (this._.设置用户权限.SeqIdx == 0)
        {
            return "您没有进行设置用户权限的权限！";
        }
        string Result = string.Empty;
        List<SysUserAction> lst = new List<SysUserAction>();
        string[] userList = users.Split('|');
        string[] actionList = actions.Split('|');
        foreach (string user in userList)
        {
            if (string.IsNullOrWhiteSpace(user))
            {
                continue;
            }
            sysUserActionManager.DeleteByWhere(SysUserAction._.UserCode == user);
            foreach (string action in actionList)
            {
                if (string.IsNullOrWhiteSpace(action))
                {
                    continue;
                }
                int iaction = 0;
                if (!int.TryParse(action, out iaction))
                {
                    continue;
                }
                SysUserAction m = new SysUserAction();
                m.UserCode = user;
                m.ActionID = iaction;
                lst.Add(m);
            }
        }
        sysUserActionManager.BatchInsert(lst);
        return Result;
    }

    [DirectMethod]
    public string GetActionInfo(string user)
    {
        StringBuilder Result = new StringBuilder("|");
        EntityArrayList<SysUserAction> lst = sysUserActionManager.GetListByWhere(SysUserAction._.UserCode == user);
        foreach (SysUserAction m in lst)
        {
            Result.Append(m.ActionID).Append("|");
        }
        return Result.ToString();
    }
    /// <summary>
    /// 拷贝用户权限
    /// 孙本强 @ 2013-04-02 19:27:00
    /// 孙本强 @ 2013-04-03 13:08:57
    /// </summary>
    /// <param name="sourceUserID">The source user ID.</param>
    /// <param name="targetUserID">The target user ID.</param>
    /// <remarks></remarks>
    [DirectMethod]
    public void SetUserActionByOther(string sourceUserID, string targetUserID)
    {
        if (this._.复制用户权限.SeqIdx == 0)
        {
            X.Msg.Alert("提示", "您没有进行复制用户权限的权限！").Show();
            return;
        }
        if (sourceUserID == targetUserID)
        {
            return;
        }
        sysUserActionManager.ClearUserAction(targetUserID);
        sysUserActionManager.CopyForm(sourceUserID, targetUserID);
    }
    #region 点击查询按钮操作
    /// <summary>
    /// 点击查询按钮筛选出用户信息
    /// yuany 2013年7月15日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    [DirectMethod]
    protected void btn_search_Click(object sender, EventArgs e)
    {
        if (string.Empty.Equals(txt_user_name.Text) && string.Empty.Equals(txt_work_barcode.Text))
        {
            X.Msg.Alert("提示", "请填写查询条件!").Show();
            return;
        }
        hiddenDeptCode.Text = "";
        TreePanel1.CollapseAll();
        ;
        GridPanel1.Title = "用户信息";
        store.Reload();
    }
    #endregion
    
}