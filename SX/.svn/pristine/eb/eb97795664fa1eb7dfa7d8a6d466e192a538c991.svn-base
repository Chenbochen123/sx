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
using Newtonsoft.Json;


public partial class Manager_System_ShowRoleAction_ShowActionAllUser : Mesnac.Web.UI.Page
{
    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查看 = new SysPageAction() { ActionID = 1};
        }
        public SysPageAction 查看 { get; private set; } //必须为 public
    }
    #endregion

    #region 属性注入
    private IBasDeptManager basDeptManager = new BasDeptManager();
    private ISysPageActionManager sysPageActionManager = new SysPageActionManager();

    private IBasUserManager basUserManager = new BasUserManager();
    private ISysUserAllActionManager sysUserAllActionManager = new SysUserAllActionManager();
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
        string useraction = hiddenDeptCode.Text;
        string hrCode = txt_hr_code.Text;
        if (string.IsNullOrWhiteSpace(useraction))
        {
            return new { data = new EntityArrayList<BasUser>(), total = 0 };
        }
        EntityArrayList<SysUserAllAction> lstAction = sysUserAllActionManager.GetListByWhere(SysUserAllAction._.ActionID == useraction);
        EntityArrayList<BasUser> lst = new EntityArrayList<BasUser>();
        foreach (SysUserAllAction m in lstAction)
        {
            try
            {
                EntityArrayList<BasUser> userList = basUserManager.GetListByWhere(BasUser._.WorkBarcode == m.UserCode & BasUser._.HRCode.Like("%" + hrCode + "%") &BasUser._.DeleteFlag== "0" );
                if (userList.Count > 0)
                {
                    BasUser u = userList[0];
                    string ss = m.Mode.ToString();
                    ss = ss.Replace("0", "");
                    ss = ss.Replace("1", "用户定义;");
                    ss = ss.Replace("2", "用户角色;");
                    ss = ss.Replace("3", "部门权限;");
                    ss = ss.Replace("4", "部门角色;");
                    u.Remark = ss;
                    lst.Add(u);
                }
            }
            catch { }
        }
        return new { data = lst, total = lst.Count };
    }


    [DirectMethod]
    public object GetActionInfo(string user)
    {
        EntityArrayList<SysUserAllAction> lst = sysUserAllActionManager.GetListByWhere(SysUserAllAction._.UserCode == user);
        return lst;
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
        string deptCode = hiddenDeptCode.Value.ToString();
        store.Reload();
    }
    #endregion
}