using System;
using System.Collections.Generic;
using Ext.Net;
using Mesnac.Business.Implements;
using Mesnac.Business.Interface;
using Mesnac.Entity;
using NBear.Common;
using Mesnac.Data.Components;
using System.Data;

/// <summary>
/// Manager_System_UserAction_ShowUserAction 实现类
/// 孙本强 @ 2013-04-03 13:09:04
/// </summary>
/// <remarks></remarks>
public partial class Manager_System_UserAction_ShowUserAction : Mesnac.Web.UI.Page
{
    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查看 = new SysPageAction() { ActionID = 1 };
        }
        public SysPageAction 查看 { get; private set; } //必须为 public
    }
    #endregion
    #region 属性注入
    /// <summary>
    /// 
    /// 孙本强 @ 2013-04-03 13:09:04
    /// </summary>
    private ISysUserActionManager sysUserActionManager = new SysUserActionManager();
    /// <summary>
    /// 
    /// 孙本强 @ 2013-04-03 13:09:04
    /// </summary>
    private ISysPageMenuManager sysMenuPageManager = new SysPageMenuManager();
    /// <summary>
    /// 
    /// 孙本强 @ 2013-04-03 13:09:04
    /// </summary>
    private ISysPageActionManager sysPageActionManager = new SysPageActionManager();
    #endregion


    #region 私有常量定义
    /// <summary>
    /// 
    /// 孙本强 @ 2013-04-03 13:09:04
    /// </summary>
    readonly string useridNodeIDStarWith = "userid=";
    /// <summary>
    /// 
    /// 孙本强 @ 2013-04-03 13:09:04
    /// </summary>
    readonly string roleidNodeIDStarWith = "roleid=";
    /// <summary>
    /// 
    /// 孙本强 @ 2013-04-03 13:09:04
    /// </summary>
    readonly string pageidNodeIDStarWith = "pageid=";
    /// <summary>
    /// 
    /// 孙本强 @ 2013-04-03 13:09:05
    /// </summary>
    readonly string actionidNodeIDStarWith = "actionid=";
    #endregion

    #region 页面初始化
    /// <summary>
    /// 
    /// 孙本强 @ 2013-04-03 13:09:05
    /// </summary>
    private const string nohaveinfo = "无功能";
    /// <summary>
    /// Handles the Load event of the Page control.
    /// 孙本强 @ 2013-04-03 13:09:05
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    /// <remarks></remarks>
    protected void Page_Load(object sender, EventArgs e)
    {
        StatusBar1.Text = "";
        StatusBar1.Html = "";
        if (!X.IsAjaxRequest)
        {

            WhereClip where = new WhereClip();
            where.And(SysPageMenu._.DeleteFlag == 0);
            where.And(SysPageMenu._.MenuLevel.Length == 2);
            EntityArrayList<SysPageMenu> lst = sysMenuPageManager.GetListByWhere(where);
            foreach (SysPageMenu menu in lst)
            {
                Node node = new Node();
                node.NodeID = this.pageidNodeIDStarWith + menu.MenuLevel.ToString();
                node.Text = menu.ShowName;
                treePanelUser.GetRootNode().AppendChild(node);
            }
            treePanelUser.GetRootNode().Expand(false);
            StatusBar1.Html = DefaultHtml("数据查询成功！一级菜单数为：" + lst.Count.ToString());
        }
    }
    /// <summary>
    /// Reds the HTML.
    /// 孙本强 @ 2013-04-03 13:09:05
    /// </summary>
    /// <param name="ss">The ss.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    private string RedHtml(string ss)
    {
        return "<font color='red'>" + ss + "</font>";
    }
    /// <summary>
    /// Defaults the HTML.
    /// 孙本强 @ 2013-04-03 13:09:05
    /// </summary>
    /// <param name="ss">The ss.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    private string DefaultHtml(string ss)
    {
        return ss;
    }
    #endregion

    #region 查询显示左侧 角色-用户 树
    /// <summary>
    /// 动态加载树信息
    /// 孙本强 @ 2013-04-02 19:27:00
    /// 孙本强 @ 2013-04-03 13:09:06
    /// </summary>
    /// <param name="pageid">The pageid.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    [DirectMethod]
    public string treePanelActionNodeLoad(string pageid)
    {
        pageid = pageid.Substring(this.pageidNodeIDStarWith.Length);
        WhereClip whereMenu = new WhereClip();
        whereMenu.And(SysPageMenu._.DeleteFlag == 0);
        whereMenu.And(SysPageMenu._.MenuLevel.Like(pageid + "%"));
        whereMenu.And(SysPageMenu._.MenuLevel.Length == pageid.Length + 2);
        EntityArrayList<SysPageMenu> menulst = sysMenuPageManager.GetListByWhere(whereMenu);
        if (menulst.Count > 0)
        {
            NodeCollection nodes = new Ext.Net.NodeCollection();
            foreach (SysPageMenu menu in menulst)
            {
                Node node = new Node();
                node.NodeID = this.pageidNodeIDStarWith + menu.MenuLevel.ToString();
                node.Text = menu.ShowName;
                node.Leaf = false;
                nodes.Add(node);
            }
            return nodes.ToJson();
        }
        else
        {
            NodeCollection nodes = new Ext.Net.NodeCollection();
            WhereClip wherePage = new WhereClip();
            wherePage.And(SysPageMenu._.DeleteFlag == 0);
            wherePage.And(SysPageMenu._.MenuLevel == pageid);
            EntityArrayList<SysPageMenu> pagelst = sysMenuPageManager.GetListByWhere(wherePage);

            WhereClip whereAct = new WhereClip();
            whereAct.And(SysPageAction._.DeleteFlag == 0);
            whereAct.And(SysPageAction._.PageMenuID == pagelst[0].ObjID);
            EntityArrayList<SysPageAction> actlst = sysPageActionManager.GetListByWhere(whereAct);
            if (actlst.Count > 0)
            {
                foreach (SysPageAction menu in actlst)
                {
                    Node node = new Node();
                    node.NodeID = this.actionidNodeIDStarWith + menu.ObjID.ToString();
                    node.Text = menu.ShowName;
                    node.Leaf = true;
                    nodes.Add(node);
                }
            }
            else
            {
                Node node = new Node();
                node.NodeID = this.actionidNodeIDStarWith + nohaveinfo;
                node.Text = nohaveinfo;
                node.Leaf = true;
                nodes.Add(node);
            }
            return nodes.ToJson();
        }
    }
    #endregion

    #region 查询显示右侧
    /// <summary>
    /// 刷新用户信息
    /// 孙本强 @ 2013-04-02 19:27:00
    /// 孙本强 @ 2013-04-03 13:09:06
    /// </summary>
    /// <param name="actionid">The actionid.</param>
    /// <remarks></remarks>
    private void RefreshActinUserGrid(string actionid)
    {
        if (actionid.Length == 0)
        {
            X.MessageBox.Show(new MessageBoxConfig { Title = "提示", Message = "请选择操作类型！", Buttons = MessageBox.Button.OK, Icon = MessageBox.Icon.INFO });
            return;
        }
        if (actionid.StartsWith(this.pageidNodeIDStarWith))
        {
            X.MessageBox.Show(new MessageBoxConfig { Title = "提示", Message = "请选择操作类型！", Buttons = MessageBox.Button.OK, Icon = MessageBox.Icon.INFO });
            return;
        }
        if (actionid.StartsWith(this.actionidNodeIDStarWith))
        {
            actionid = actionid.Substring(this.actionidNodeIDStarWith.Length);
        }
        if (actionid.Contains(nohaveinfo))
        {
            actionid = "";
            txtActionID.Text = actionid;
            return;
        }
        txtActionID.Text = actionid;
        SysPageAction action = sysPageActionManager.GetListByWhere(SysPageAction._.DeleteFlag == 0 && SysPageAction._.ObjID == actionid)[0];
        SysPageMenu page = sysMenuPageManager.GetListByWhere(SysPageMenu._.DeleteFlag == 0 && SysPageMenu._.ObjID == action.PageMenuID)[0];
        SysPageMenu parpage = sysMenuPageManager.GetListByWhere(SysPageMenu._.DeleteFlag == 0 && SysPageMenu._.MenuLevel == page.MenuLevel.Substring(0, page.MenuLevel.Length - 2))[0];
        txtPageParName.Text = parpage.ShowName;
        txtPageName.Text = page.ShowName;
        txtActionName.Text = action.ShowName;
        X.Js.Call("gridPanelRefresh()");
    }
    /// <summary>
    /// 获取选中权限信息
    /// 孙本强 @ 2013-04-02 19:27:00
    /// 孙本强 @ 2013-04-03 13:09:06
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    /// <remarks></remarks>
    protected void GetActinUserGrid(object sender, EventArgs e)
    {
        TreePanel sendertree = sender as TreePanel;
        List<SubmittedNode> nodes = sendertree.SelectedNodes;
        string actionid = "";
        if (nodes.Count > 0)
        {
            actionid = nodes[0].NodeID;
        }
        RefreshActinUserGrid(actionid);
    }
    #endregion


    #region 分页查询获取数据
    /// <summary>
    /// 分页查询获取数据
    /// 孙本强 @ 2013-04-02 19:27:00
    /// 孙本强 @ 2013-04-03 13:09:07
    /// </summary>
    /// <param name="action">The action.</param>
    /// <param name="extraParams">The extra params.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        DataTable data = new DataTable();
        int total = 0;
        string actionid = txtActionID.Text;
        if (actionid.StartsWith(this.actionidNodeIDStarWith))
        {
            actionid = actionid.Substring(this.actionidNodeIDStarWith.Length);
        }
        if (actionid.Length == 0)
        {
            return new { data, total };
        }
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        SysUserActionManager.QueryParams queryParams = new SysUserActionManager.QueryParams();
        queryParams.PageParams.PageIndex = prms.Page;
        queryParams.PageParams.PageSize = prms.Limit;
        queryParams.PageParams.Orderfld = "ObjID";

        queryParams.PageActionID = actionid;

        PageResult<SysUserAction> lst = sysUserActionManager.GetUserTablePageDataByAction(queryParams);
        data = lst.DataSet.Tables[0];

        total = lst.RecordCount;
        return new { data, total };
    }
    #endregion
}