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

public partial class Manager_BasicInfo_WorkPositionInfo_WorkPositionSetByDep : Mesnac.Web.UI.Page
{
    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查看 = new SysPageAction() { ActionID = 1 };
            设置人员岗位 = new SysPageAction() { ActionID = 2, ActionName = "btnSetWork" };
        }
        public SysPageAction 查看 { get; private set; } //必须为 public
        public SysPageAction 设置人员岗位 { get; private set; } //必须为 public
    }
    #endregion
    #region 属性注入
    private IBasDeptManager basDeptManager = new BasDeptManager();
    private ISysPageActionManager sysPageActionManager = new SysPageActionManager();
    private IBasUserManager basUserManager = new BasUserManager();
    private IBasWorkManager basWorkManager = new BasWorkManager();
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

    /// <summary>
    /// 初始化部门树的绑定方法
    /// </summary>
    /// <param name="ss"></param>
    /// <returns></returns>
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
    #region 分页相关方法
    private PageResult<BasUser> GetPageResultData(PageResult<BasUser> pageParams)
    {
        BasUserManager.QueryParams queryParams = new BasUserManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.deptCode = hiddenDeptCode.Text;
        queryParams.deleteFlag = "0";

        return basUserManager.GetTablePageDataBySql(queryParams);
    }
    /// <summary>
    /// 根据部门编号绑定用户信息的后台方法
    /// </summary>
    /// <param name="action"></param>
    /// <param name="extraParams"></param>
    /// <returns></returns>
    [DirectMethod]
    public object GridPanelBindDataUserByDep(string action, Dictionary<string, object> extraParams)
    {
        string deptcode = hiddenDeptCode.Text;
        if (string.IsNullOrWhiteSpace(deptcode))
        {
            return new { data = new EntityArrayList<BasUser>(), total = 0 };
        }

        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<BasUser> pageParams = new PageResult<BasUser>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "WorkBarCode ASC";

        PageResult<BasUser> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }

    #endregion

    /// <summary>
    /// 绑定岗位信息的后台相应方法
    /// </summary>
    /// <param name="action"></param>
    /// <param name="extraParams"></param>
    /// <returns></returns>
    [DirectMethod]
    public object GridPanelBindDataWorkPosition(string action, Dictionary<string, object> extraParams)
    {
        EntityArrayList<BasWork> lst = basWorkManager.GetListByWhereAndOrder(BasWork._.DeleteFlag == "0", BasWork._.ObjID.Asc);
        int total = lst.Count;
        return new { data = lst, total };
    }

    /// <summary>
    /// 点击设置岗位信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void btnSetWork_Click(object sender, DirectEventArgs e)
    {
        Ext.Net.MessageBox msg = new Ext.Net.MessageBox();
        string jsonWork = e.ExtraParams["ValuesWork"];
        string jsonUser = e.ExtraParams["ValuesUser"];
        Dictionary<string, string>[] workDic = JSON.Deserialize<Dictionary<string, string>[]>(jsonWork);
        Dictionary<string, string>[] userDic = JSON.Deserialize<Dictionary<string, string>[]>(jsonUser);
        if (workDic.Length == 0)
        {
            msg.Alert("提示","请选择岗位!").Show();
            return;
        }
        if (userDic.Length == 0)
        {
            msg.Alert("提示", "请选择人员!").Show();
            return;
        }
        foreach (Dictionary<string, string> workRow in workDic)
        {
            string workID = workRow["ObjID"];
            foreach (Dictionary<string, string> userRow in userDic)
            {
                BasUser user = basUserManager.GetById(userRow["ObjID"]);
                user.WorkID = Convert.ToInt32(workID);
                basUserManager.Update(user);
            }
        }
        this.pageToolBar.DoRefresh();
        msg.Notify("提示", "用户岗位设置成功!").Show();
    }
}