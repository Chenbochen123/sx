using Ext.Net;
using Mesnac.Business.Implements;
using Mesnac.Business.Interface;
using Mesnac.Entity;
using NBear.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;

namespace Mesnac.Web.UI
{
    /// <summary>
    /// BasePage 实现类
    /// 孙本强 @ 2013-04-03 13:10:36
    /// </summary>
    public class BasePage : System.Web.UI.Page
    {
        #region 属性注入
        /// <summary>
        /// 功能操作管理
        /// 孙本强 @ 2013-04-03 13:10:37
        /// </summary>
        ISysPageActionManager sysPageActionManager = new SysPageActionManager();
        /// <summary>
        /// 用户管理
        /// 孙本强 @ 2013-04-03 13:10:37
        /// </summary>
        IBasUserManager basUserManager = new BasUserManager();
        /// <summary>
        /// 页面管理
        /// 孙本强 @ 2013-04-03 13:10:37
        /// </summary>
        ISysPageMenuManager sysPageMenuManager = new SysPageMenuManager();
        /// <summary>
        /// 页面日志
        /// 孙本强 @ 2013-04-03 13:10:37
        /// </summary>
        ISysWebLogManager sysWebLogManager = new SysWebLogManager();
        #endregion

        #region 页面初始化进行权限设置
        /// <summary>
        /// 当前页面
        /// </summary>
        SysPageMenu thisPage = new SysPageMenu();

        /// <summary>
        /// 是否进行权限判断
        /// 孙本强 @ 2013-04-02 19:27:00
        /// </summary>
        /// <value><c>true</c> if [no check page permission]; otherwise, <c>false</c>.</value>
        public bool NoCheckPagePermission { get; set; }

        /// <summary>
        /// 用户登录ID   对应数据库中的 WorkBarcode
        /// 孙本强 @ 2013-04-02 19:27:00
        /// </summary>
        /// <value>The user ID.</value>
        public string UserID
        {
            get
            {
                return basUserManager.UserID;
            }
        }

        #region 校验页面控件的权限,并进行页面设置
        /// <summary>
        /// 当前页面需要进行权限校验的操作项
        /// 孙本强 @ 2013-04-02 19:27:00
        /// </summary>
        private EntityArrayList<SysPageAction> PageActionList = null;
        /// <summary>
        /// 当前页面用户包含的操作项
        /// 孙本强 @ 2013-04-02 19:27:00
        /// </summary>
        private EntityArrayList<SysPageAction> UserPageActionList = null;

        /// <summary>
        /// 根据权限进行设置
        /// 孙本强 @ 2013-04-02 19:27:00
        /// </summary>
        /// <param name="webcontrol">The webcontrol.</param>
        /// <param name="can">if set to <c>true</c> [can].</param>
        private void SetPageAction(WebControl webcontrol, bool can)
        {
            if (can)
            {
                return;
            }
            //webcontrol.Visible = can;
            if (webcontrol is Ext.Net.ButtonBase)
            {
                (webcontrol as Ext.Net.ButtonBase).Disabled = !can;
                (webcontrol as Ext.Net.ButtonBase).ToolTip = (webcontrol as Ext.Net.ButtonBase).ToolTip + "【无此权限】";
            }
            else if (webcontrol is Ext.Net.Field)
            {
                (webcontrol as Ext.Net.Field).ReadOnly = !can;
            }
            else if (webcontrol is Ext.Net.AbstractComponent)
            {
                (webcontrol as Ext.Net.AbstractComponent).Disabled = !can;
            }
            else
            {
                webcontrol.Enabled = can;
            }
        }
        /// <summary>
        /// 对单个操作项进行权限校验
        /// 孙本强 @ 2013-04-02 19:27:00
        /// </summary>
        /// <param name="actionid">The actionid.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        private bool PageActionPermission(string actionid)
        {
            #region 获取本页权限
            if (PageActionList == null || PageActionList.Count == 0)
            {
                return true;
            }
            if (UserPageActionList == null || UserPageActionList.Count == 0)
            {
                return false;
            }
            #endregion
            bool Result = true;
            //是否需要对此项进行权限校验
            foreach (SysPageAction action in PageActionList)
            {
                if (action.ActionName.ToUpper().Contains("," + actionid.ToUpper() + ","))
                {
                    Result = false;
                    break;
                }
            }
            if (!Result)
            {
                foreach (SysPageAction action in UserPageActionList)
                {
                    if (action.ActionName.ToUpper().Contains("," + actionid.ToUpper() + ","))
                    {
                        Result = true;
                        break;
                    }
                }
            }
            return Result;
        }

        /// <summary>
        /// 系统控件进行权限校验
        /// 孙本强 @ 2013-04-02 19:27:00
        /// </summary>
        /// <param name="webcontrol">The webcontrol.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        private bool CheckPageActionPermission(WebControl webcontrol)
        {
            string id = webcontrol.ID;
            return PageActionPermission(id);
        }
        /// <summary>
        /// 根据权限进行设置
        /// 孙本强 @ 2013-04-02 19:27:00
        /// </summary>
        /// <param name="cmd">The CMD.</param>
        /// <param name="can">if set to <c>true</c> [can].</param>
        private void SetPageAction(Ext.Net.GridCommand cmd, bool can)
        {
            if (can)
            {
                return;
            }
            cmd.Disabled = !can;
            if (!can)
            {
                cmd.ToolTip.Text = cmd.ToolTip.Text + "【无此权限】";
            }
        }
        /// <summary>
        /// 根据权限进行设置
        /// 孙本强 @ 2013-04-02 19:27:00
        /// </summary>
        /// <param name="cmd">The CMD.</param>
        /// <param name="can">if set to <c>true</c> [can].</param>
        private void SetPageAction(Ext.Net.MenuCommand cmd, bool can)
        {
            cmd.Disabled = !can;
        }
        /// <summary>
        /// Ext的Grid中的操作进行权限校验
        /// 孙本强 @ 2013-04-02 19:27:00
        /// </summary>
        /// <param name="cmd">The CMD.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        private bool CheckPageActionPermission(Ext.Net.GridCommand cmd)
        {
            string id = cmd.CommandName;
            return PageActionPermission(id);
        }
        /// <summary>
        /// Ext的菜单中的操作进行权限校验
        /// 孙本强 @ 2013-04-02 19:27:00
        /// </summary>
        /// <param name="cmd">The CMD.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        private bool CheckPageActionPermission(Ext.Net.MenuCommand cmd)
        {
            string id = cmd.CommandName;
            return PageActionPermission(id);
        }
        /// <summary>
        /// 设置Ext中菜单的权限
        /// 孙本强 @ 2013-04-02 19:27:00
        /// </summary>
        /// <param name="menus">The menus.</param>
        private void SetCommandColumn(Ext.Net.CommandMenu menus)
        {
            foreach (Ext.Net.MenuCommand menu in menus.Items)
            {
                bool check = CheckPageActionPermission(menu);
                if (!check)
                {
                    SetPageAction(menu, check);
                }
                else
                {
                    SetCommandColumn(menu.Menu);
                }
            }
        }
        /// <summary>
        /// 设置Ext中GridCommand的权限
        /// 孙本强 @ 2013-04-02 19:27:00
        /// </summary>
        /// <param name="cmds">The CMDS.</param>
        private void SetCommandColumn(Ext.Net.CommandColumn cmds)
        {
            foreach (object cmd in cmds.Commands)
            {
                if (cmd is Ext.Net.GridCommand)
                {
                    bool check = CheckPageActionPermission(cmd as Ext.Net.GridCommand);
                    if (!check)
                    {
                        SetPageAction(cmd as Ext.Net.GridCommand, check);
                    }
                    else
                    {
                        SetCommandColumn((cmd as Ext.Net.GridCommand).Menu);
                    }
                }
            }
        }
        /// <summary>
        /// 递归设置控件
        /// 孙本强 @ 2013-04-02 19:27:00
        /// </summary>
        /// <param name="webcontrol">控件</param>
        private void SetControls(WebControl webcontrol)
        {

            if (webcontrol is Ext.Net.GridPanel)
            {
                var panel = (webcontrol as Ext.Net.GridPanel);
                try
                {
                    if (panel.View != null && panel.View.View != null && (panel.View.View is Ext.Net.GridView))
                    {
                        (panel.View.View as Ext.Net.GridView).EnableTextSelection = true;
                    }
                }
                catch { }
                try
                {
                    panel.View.Add(new Ext.Net.GridView() { ID = Guid.NewGuid().ToString(), EnableTextSelection = true });
                }
                catch { }
            }
            bool check = CheckPageActionPermission(webcontrol);
            if (!check)
            {
                SetPageAction(webcontrol, false);
            }
            else
            {
                foreach (object o in webcontrol.Controls)
                {
                    if (o is WebControl)
                    {
                        WebControl _webcontrol = o as WebControl;
                        SetControls(_webcontrol);
                    }
                }
                if (webcontrol is Ext.Net.CommandColumn)
                {
                    SetCommandColumn(webcontrol as Ext.Net.CommandColumn);
                }
            }
        }

        /// <summary>
        /// 设置页面中的控件
        /// 孙本强 @ 2013-04-02 19:27:00
        /// </summary>
        /// <param name="page">The page.</param>
        private void SetPage(System.Web.UI.Page page)
        {
            foreach (object o in page.Controls)
            {
                if (o is HtmlForm)
                {
                    HtmlForm htmlform = o as HtmlForm;
                    foreach (object _o in htmlform.Controls)
                    {
                        if (_o is WebControl)
                        {
                            SetControls(_o as WebControl);
                        }
                    }
                }
            }
        }
        #endregion

        #region 校验页面访问权限，并进行跳转
        /// <summary>
        /// 获取网页的url，去除和发布相关的内容
        /// 孙本强 @ 2013-04-02 19:27:00
        /// </summary>
        /// <returns>System.String.</returns>
        protected virtual string GetPageName()
        {
            string applicationPath = this.Request.ApplicationPath;
            string rawUrl = this.Request.RawUrl;
            rawUrl = rawUrl.Substring(applicationPath.Length);
            if (rawUrl.ToLower().Contains("_dc="))
            {
                int istart = rawUrl.IndexOf("_dc=");
                int iend = rawUrl.LastIndexOf("&");
                if (iend <= istart)
                {
                    iend = rawUrl.Length;
                }
                rawUrl = rawUrl.Substring(0, istart).Trim() + rawUrl.Substring(iend).Trim();
                while (true)
                {
                    if ((rawUrl.EndsWith("?")) || (rawUrl.EndsWith("&")))
                    {
                        rawUrl = rawUrl.Substring(0, rawUrl.Length - 1);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            if (rawUrl.StartsWith("/"))
            {
                rawUrl = rawUrl.Substring(1);
            }
            return rawUrl;
        }
        /// <summary>
        /// 无权限进行跳转
        /// 孙本强 @ 2013-04-02 19:27:00
        /// </summary>
        private void NoPermission()
        {
            Response.Redirect("~/Manager/Authentication/NoPermission.aspx");
        }
        /// <summary>
        /// 判断页面权限
        /// 孙本强 @ 2013-04-03 13:10:41
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        private bool PagePermission()
        {
            try
            {
                string userid = this.UserID;
                if (string.IsNullOrWhiteSpace(userid))
                {
                    return true;
                }
                return sysPageMenuManager.PagePermission(userid, this.GetPageName());
            }
            catch
            {
                return false;
            }
        }
        #endregion
        #region 权限初始化
        /// <summary>
        /// 获取当前页面，如数据库中不存在就插入一条
        /// 孙本强 @ 2013-04-02 19:27:00
        /// </summary>
        /// <returns>SysPageMenu.</returns>
        private SysPageMenu GetThisPageMenu()
        {
            string url = this.GetPageName();
            EntityArrayList<SysPageMenu> pageList = sysPageMenuManager.GetListByWhere(SysPageMenu._.PageUrl.ToUpper() == url.ToUpper());
            if (pageList.Count == 0)
            {
                SysPageMenu m = new SysPageMenu();
                m.ObjID = (int)sysPageMenuManager.GetMaxValueByProperty(SysPageMenu._.ObjID) + 1;
                m.ShowName = string.IsNullOrWhiteSpace(this.Title) ? "请设置名称" : this.Title;
                m.MenuLevel = m.ObjID.ToString("D8");
                m.PageUrl = url;
                m.IsShow = "0";
                m.DeleteFlag = "1";
                sysPageMenuManager.Insert(m);
                return GetThisPageMenu();
            }
            else
            {
                return pageList[0];
            }

        }

        /// <summary>
        /// 通过反射获取当前页面中的权限设置项
        /// 孙本强 @ 2013-04-02 19:27:00
        /// </summary>
        /// <returns>___.</returns>
        private ___ GetPageActionList()
        {
            ___ Result = null;
            Type type = this.GetType();
            #region 属性
            PropertyInfo[] piList = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (PropertyInfo pi in piList)
            {
                if (pi.PropertyType == typeof(___))
                {
                    return (___)pi.GetValue(this, null);
                }
                if (pi.PropertyType.IsSubclassOf(typeof(___)))
                {
                    return (___)pi.GetValue(this, null);
                }
            }
            #endregion
            #region 字段
            FieldInfo[] miList = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (FieldInfo pi in miList)
            {
                if (pi.FieldType == typeof(___))
                {

                    return (___)pi.GetValue(this);
                }
                if (pi.FieldType.IsSubclassOf(typeof(___)))
                {
                    return (___)pi.GetValue(this);
                }
            }
            #endregion
            return Result;
        }
        /// <summary>
        /// 页面权限项初始化
        /// 孙本强 @ 2013-04-02 19:27:00
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="lst">The LST.</param>
        private void PageActionIni(SysPageMenu page, List<SysPageAction> lst)
        {
            ___ action = GetPageActionList();
            if (action != null)
            {
                action.IniBind(page);
                action.UserBind(lst);
            }
        }
        /// <summary>
        /// 页面权限项初始化
        /// 孙本强 @ 2013-04-02 19:27:00
        /// </summary>
        private void PageActionIni()
        {
            PageActionList = sysPageActionManager.GetListByWhere(SysPageAction._.PageMenuID == thisPage.ObjID);
            if (PageActionList != null)
            {
                foreach (SysPageAction m in PageActionList)
                {
                    if (string.IsNullOrWhiteSpace(m.ActionName))
                    {
                        m.ActionName = string.Empty;
                    }
                    m.ActionName = "," + m.ActionName + ",";
                }
            }
            List<SysPageAction> lst = new List<SysPageAction>();
            UserPageActionList = sysPageActionManager.GetUserPageActionList(this.GetPageName(), this.UserID);
            if (UserPageActionList != null)
            {
                foreach (SysPageAction m in UserPageActionList)
                {
                    if (string.IsNullOrWhiteSpace(m.ActionName))
                    {
                        m.ActionName = string.Empty;
                    }
                    m.ActionName = "," + m.ActionName + ",";
                    lst.Add(m);
                }
            }
            PageActionIni(thisPage, lst);
        }
        #endregion
        /// <summary>
        /// 是否进行权限判断
        /// 孙本强 @ 2013-04-02 19:27:00
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        private bool DoNotPermission()
        {
            if (thisPage.DeleteFlag == "1")
            {
                return true;
            }
            if (this.NoCheckPagePermission)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 重写系统OnInit进行权限判断
        /// 孙本强 @ 2013-04-02 19:27:00
        /// </summary>
        /// <param name="e">包含事件数据的 <see cref="T:System.EventArgs" />。</param>
        protected override void OnInit(EventArgs e)
        {
            try
            {
                base.OnInit(e);
                thisPage = GetThisPageMenu();
                PageActionIni();
                if (this.IsPostBack || X.IsAjaxRequest)
                {
                    return;
                }
                base.Page.ClientScript.RegisterClientScriptInclude("documentOnkeydown", ResolveClientUrl("~/resources/js/onkeydown.js"));
                if (DoNotPermission())
                {
                    return;
                }
                if (PagePermission())
                {
                    SetPage(this);
                }
                else
                {
                    NoPermission();
                }
            }
            catch { }
        }
        #endregion

        #region 日志操作类
        /// <summary>
        /// 添加日志
        /// 孙本强 @ 2013-04-02 19:27:00
        /// </summary>
        /// <param name="Remark">The remark.</param>
        /// <param name="MethodResult">The method result.</param>
        /// <param name="MethodIndex">Index of the method.</param>
        public void AppendWebLog(string Remark, string MethodResult, int MethodIndex)
        {
            SysWebLog sysWebLog = new SysWebLog();
            sysWebLog.UserCode = this.UserID;
            sysWebLog.PageRequest = this.Request.Form.ToString();
            sysWebLog.Remark = Remark;
            sysWebLog.MethodResult = MethodResult;
            sysWebLog.UserIP = this.Request.UserHostAddress;
            if (sysWebLog.UserIP == "::1")
            {
                sysWebLog.UserIP = "172.0.0.1";
            }
            MethodBase stackMethod = new StackTrace().GetFrame(MethodIndex).GetMethod();
            SysPageMethod sysPageMethod = new SysPageMethod();
            sysPageMethod.PageID = new SysPageMenuManager().GetPageID(this.GetPageName());
            sysPageMethod.MethodName = stackMethod.ToString();
            //以下由闫志旭2015.9.24添加 当备注为空时把页面名称记入备注列
            if (String.IsNullOrEmpty(sysWebLog.Remark))
            {
                String sql = "select showname from SysPageMenu where objid ='" + sysPageMethod.PageID + "'";
                DataSet ds = new SysPageMenuManager().GetBySql(sql).ToDataSet();
                if (ds.Tables[0].Rows.Count > 0)
                    sysWebLog.Remark = ds.Tables[0].Rows[0][0].ToString();
            }




            sysWebLogManager.Append(sysWebLog, sysPageMethod);

        }
        /// <summary>
        /// 添加日志
        /// 孙本强 @ 2013-04-02 19:27:00
        /// </summary>
        /// <param name="Remark">The remark.</param>
        /// <param name="MethodResult">The method result.</param>
        public void AppendWebLog(string Remark, string MethodResult)
        {
            AppendWebLog(Remark, MethodResult, 2);
        }
        /// <summary>
        /// 添加日志
        /// 孙本强 @ 2013-04-02 19:27:00
        /// </summary>
        /// <param name="MethodResult">The method result.</param>
        public void AppendWebLog(string MethodResult)
        {
            AppendWebLog(string.Empty, MethodResult, 2);
        }
        /// <summary>
        /// 添加日志
        /// 孙本强 @ 2013-04-02 19:27:00
        /// </summary>
        public void AppendWebLog()
        {
            AppendWebLog(string.Empty, string.Empty, 2);
        }
        #endregion
    }
}