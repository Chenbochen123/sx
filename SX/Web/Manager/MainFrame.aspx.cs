using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using NBear.Common;
using Mesnac.Entity;
using Mesnac.Business.Implements;
using Mesnac.Business.Interface;
using Mesnac.Util;
using System.Text;
using System.Data;

/// <summary>
/// Manager_MainFrame 实现类
/// 孙本强 @ 2013-04-03 13:07:26
/// </summary>
/// <remarks></remarks>
public partial class Manager_MainFrame : Mesnac.Web.UI.Page
{
    #region 属性注入

    /// <summary>
    /// 
    /// 孙本强 @ 2013-04-03 13:07:26
    /// </summary>
    private ISysPageMenuManager sysPageMenuManager = new SysPageMenuManager();
    /// <summary>
    /// 
    /// 孙本强 @ 2013-04-03 13:07:27
    /// </summary>
    private ISysPageActionManager sysPageActionManager = new SysPageActionManager();
    /// <summary>
    /// 
    /// 孙本强 @ 2013-04-03 13:07:27
    /// </summary>
    private IBasUserManager basUserManager = new BasUserManager();
    #endregion

    /// <summary>
    /// Handles the Load event of the Page control.
    /// 孙本强 @ 2013-04-03 13:07:27
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    /// <remarks></remarks>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !X.IsAjaxRequest)
        {
            BasUser user = basUserManager.GetListByWhere(BasUser._.WorkBarcode == this.UserID)[0];
            BorderPanelWest.Title = "[" + user.UserName + "]";

          
        }
    }

    /// <summary>
    /// Adds the tab.
    /// 孙本强 @ 2013-04-03 13:07:27
    /// </summary>
    /// <param name="id">The id.</param>
    /// <param name="title">The title.</param>
    /// <param name="url">The URL.</param>
    /// <param name="closable">if set to <c>true</c> [closable].</param>
    /// <returns></returns>
    /// <remarks></remarks>
    private string addTab(string id, string title, string url, bool closable)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("addTab('").Append(id).Append("','");
        sb.Append(title).Append("','");
        sb.Append(url).Append("',");
        sb.Append(closable.ToString().ToLower()).Append(");");
        return sb.ToString();
    }
    /// <summary>
    /// 添加默认页
    /// 孙本强 @ 2013-04-03 13:07:27
    /// </summary>
    /// <returns></returns>
    /// <remarks></remarks>
    protected string AddTabDefault()
    {
        StringBuilder sb = new StringBuilder();
        string url = "System/SysTaskRemind/SysTaskRemind.aspx";
        string tabid = "Manager_System_SysTaskRemind_SysTaskRemind";
        sb.AppendLine(addTab(tabid, "首页", url, false));
        return sb.ToString();
    }

    /// <summary>
    /// Visions this instance.
    /// 孙本强 @ 2013-04-03 13:07:28
    /// </summary>
    /// <returns></returns>
    /// <remarks></remarks>
    protected string Vision()
    {
        string fileName = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
        fileName = System.Reflection.Assembly.GetExecutingAssembly().Location;
        System.IO.FileInfo fi = new System.IO.FileInfo(fileName);
        return fi.LastWriteTime.ToString("yyyy.MM.dd.HHmmss");
    }

    /// <summary>
    /// 初始化一级菜单数据
    /// 孙本强 @ 2013-04-03 13:07:28
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="Ext.Net.NodeLoadEventArgs"/> instance containing the event data.</param>
    /// <remarks></remarks>
    protected void GetUserPageGroup(object sender, NodeLoadEventArgs e)
    {
        TreePanel trp = (sender as TreeStore).Parent as TreePanel;
        EntityArrayList<SysPageMenu> lst = this.sysPageMenuManager.GetUserMenuPageList(this.UserID, string.Empty);
        foreach (SysPageMenu menu in lst)
        {
            Node treeNode = new Node();
            treeNode.NodeID = menu.ObjID.ToString();
            treeNode.Text = menu.ShowName;
            treeNode.Leaf = false;
            if ((menu.PageUrl != null) && (menu.PageUrl.Length > 0))
            {
                if (menu.PageUrl.ToLower().StartsWith("http://") || menu.PageUrl.ToLower().StartsWith("https://")
                    || menu.PageUrl.ToLower().Replace(" ", string.Empty).StartsWith("javascript:"))
                {
                    treeNode.Href = menu.PageUrl;
                }
                else
                {
                    treeNode.Href = Page.ResolveUrl("~/") + menu.PageUrl;
                }
                treeNode.Leaf = true;
            }
            trp.GetRootNode().AppendChild(treeNode);
        }
        trp.GetRootNode().Expand(false);
    }
    /// <summary>
    /// Ajax方式加载二级菜单
    /// 孙本强 @ 2013-04-03 13:07:28
    /// </summary>
    /// <param name="storeID">The store ID.</param>
    /// <param name="nodeID">The node ID.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    [DirectMethod]
    public string NodeLoad(string storeID, string nodeID)
    {
        NodeCollection nodes = new Ext.Net.NodeCollection();
        string parid = sysPageMenuManager.GetListByWhere(SysPageMenu._.ObjID == nodeID)[0].MenuLevel;
        EntityArrayList<SysPageMenu> lst = this.sysPageMenuManager.GetUserMenuPageList(this.UserID, parid);
        foreach (SysPageMenu menu in lst)
        {
            Node treeNode = new Node();
            treeNode.NodeID = menu.ObjID.ToString();
            treeNode.Text = menu.ShowName;
            treeNode.Leaf = false;
            if ((menu.PageUrl != null) && (menu.PageUrl.Length > 0))
            {
                if (menu.PageUrl.ToLower().StartsWith("http://") || menu.PageUrl.ToLower().StartsWith("https://") 
                    || menu.PageUrl.ToLower().Replace(" ", string.Empty).StartsWith("javascript:"))
                {
                    treeNode.Href = menu.PageUrl;
                }
                else
                {
                    treeNode.Href = Page.ResolveUrl("~/") + menu.PageUrl;
                }
                treeNode.Leaf = true;
            }
            nodes.Add(treeNode);
        }
        if (lst.Count == 0)
        {
            Node treeNode = new Node();
            treeNode.NodeID = "无相关菜单=" + DateTime.Now.ToString("yyyyMMddHHmmss");
            treeNode.Text = "无相关菜单";
            treeNode.Leaf = true;
            nodes.Add(treeNode);
        }
        return nodes.ToJson();
    }

    /// <summary>
    /// Called when [timer].
    /// 孙本强 @ 2013-04-03 13:07:28
    /// </summary>
    /// <returns></returns>
    /// <remarks></remarks>
    [DirectMethod]
    public string OnTimer()
    {
        string UserID = Session["UserID"].ToString();
        Session["UserID"] = UserID;
        return DateTime.Now.ToString("HH:mm");
    }
    /// <summary>
    /// 设置皮肤
    /// </summary>
    /// <param name="theme"></param>
    /// <returns></returns>
    [DirectMethod]
    public string GetThemeUrl(string theme)
    {
        Theme temp = (Theme)Enum.Parse(typeof(Theme), theme);

        this.Session["Ext.Net.Theme"] = temp;

        return this.ResourceManager1.GetThemeUrl(temp);
    }
}