using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Xml;

namespace Mesnac.Util.Module.Authentication
{
    /// <summary>
    /// AuthenticationModule 实现类
    /// 孙本强 @ 2013-04-03 13:12:27
    /// </summary>
    /// <remarks></remarks>
    public class AuthenticationModule : IHttpModule , System.Web.SessionState.IRequiresSessionState
    {
        /// <summary>
        /// 
        /// 孙本强 @ 2013-04-03 13:12:27
        /// </summary>
        private static List<string> NoCheckList = new List<string>();
        /// <summary>
        /// 
        /// 孙本强 @ 2013-04-03 13:12:27
        /// </summary>
        private static string RedirectTo = "Index.htm";
        /// <summary>
        /// 
        /// 孙本强 @ 2013-04-03 13:12:27
        /// </summary>
        private HttpApplication app;
        /// <summary>
        /// Inits the specified HTTP application.
        /// 孙本强 @ 2013-04-03 13:12:27
        /// </summary>
        /// <param name="httpApplication">The HTTP application.</param>
        /// <remarks></remarks>
        public void Init(HttpApplication httpApplication)
        {
            app = httpApplication;
            app.PreRequestHandlerExecute += app_PreRequestHandlerExecute;
            //app.BeginRequest += new EventHandler(context_BeginRequest);
            if (NoCheckList.Count == 0)
            {
                AuthenticationXML authentication = new AuthenticationXML();
                NoCheckList = authentication.NoCheckList;
                RedirectTo = authentication.RedirectTo;
            }
        }

        /// <summary>
        /// Handles the PreRequestHandlerExecute event of the app control.
        /// 孙本强 @ 2013-04-03 13:12:27
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        void app_PreRequestHandlerExecute(object sender, EventArgs e)
        {
            HttpApplication Application = (HttpApplication)sender;
            HttpContext context = Application.Context;

            if (GetCurrentPageUrl(context).ToLower().IndexOf(".aspx") > 0)
            {
                if (NoCheck(context))
                {
                    return;
                }
                if (!IsAuthenticationBySession())
                {
                    if (GetCurrentPageUrl(context).ToLower().IndexOf(RedirectTo.ToLower()) < 0)
                    {
                        Application.Response.Redirect("~/" + RedirectTo);
                    }
                }
            }
        }

        /// <summary>
        /// 处置由实现 <see cref="T:System.Web.IHttpModule"/> 的模块使用的资源（内存除外）。
        /// 孙本强 @ 2013-04-03 13:12:27
        /// </summary>
        /// <remarks></remarks>
        public void Dispose()
        {
            return;
            throw new NotImplementedException();
        }
        /// <summary>
        /// Gets the current page URL.
        /// 孙本强 @ 2013-04-03 13:12:28
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private string GetCurrentPageUrl(HttpContext context)
        {
            string applicationPath = context.Request.ApplicationPath;
            string rawUrl = context.Request.RawUrl;
            rawUrl = rawUrl.Substring(applicationPath.Length);
            int indexOfstring = 0;
            indexOfstring = rawUrl.IndexOf("?");
            if (indexOfstring >= 0)
            {
                rawUrl = rawUrl.Substring(0, indexOfstring);
            }
            indexOfstring = rawUrl.IndexOf("#");
            if (indexOfstring >= 0)
            {
                rawUrl = rawUrl.Substring(0, indexOfstring);
            }
            if (rawUrl.StartsWith("/"))
            {
                rawUrl = rawUrl.Substring(1);
            }
            return rawUrl;
        }
        /// <summary>
        /// Noes the check.
        /// 孙本强 @ 2013-04-03 13:12:28
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private bool NoCheck(HttpContext context)
        {
            if (GetCurrentPageUrl(context).ToLower().IndexOf("fastreport") > -1)
            {
                return true;
            }
            foreach (string url in NoCheckList)
            {
                if (GetCurrentPageUrl(context).ToLower().StartsWith(url.ToLower()))
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Handles the BeginRequest event of the context control.
        /// 孙本强 @ 2013-04-03 13:12:28
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        void context_BeginRequest(object sender, EventArgs e)
        {
            HttpApplication Application = (HttpApplication)sender;
            HttpContext context = Application.Context;

            if (GetCurrentPageUrl(context).ToLower().IndexOf(".aspx") > 0)
            {
                if (NoCheck(context))
                {
                    return;
                }
                if (!IsAuthenticationByCookie())
                {
                    if (GetCurrentPageUrl(context).ToLower().IndexOf(RedirectTo.ToLower()) < 0)
                    {
                        Application.Response.Redirect("~/" + RedirectTo);
                    }
                }
            }
        }

        /// <summary>
        /// Determines whether [is authentication by cookie].
        /// 孙本强 @ 2013-04-03 13:12:28
        /// </summary>
        /// <returns><c>true</c> if [is authentication by cookie]; otherwise, <c>false</c>.</returns>
        /// <remarks></remarks>
        protected bool IsAuthenticationByCookie()
        {
            string cookieName = FormsAuthentication.FormsCookieName;
            HttpCookie authCookie = app.Context.Request.Cookies[cookieName];

            if (null == authCookie)
            {
                // 没有验证 Cookie。
                return false;
            }

            FormsAuthenticationTicket authTicket = null;
            try
            {
                authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            }
            catch (Exception ex)
            {
                return false;
            }

            if (null == authTicket)
            {
                // Cookie 无法解密。
                return false;
            }
            return true;
        }

        /// <summary>
        /// Determines whether [is authentication by session].
        /// 孙本强 @ 2013-04-03 13:12:28
        /// </summary>
        /// <returns><c>true</c> if [is authentication by session]; otherwise, <c>false</c>.</returns>
        /// <remarks></remarks>
        protected bool IsAuthenticationBySession()
        {
            if (null == app.Context.Session)
            {
                // 没有验证 Cookie。
                return false;
            }
            if (null == app.Context.Session["UserID"])
            {
                // 没有验证 Cookie。
                return false;
            }
            return true;
        }

    }

    /// <summary>
    /// AuthenticationXML 实现类
    /// 孙本强 @ 2013-04-03 13:12:28
    /// </summary>
    /// <remarks></remarks>
    public class AuthenticationXML
    {
        /// <summary>
        /// 
        /// 孙本强 @ 2013-04-03 13:12:28
        /// </summary>
        private readonly string configFilePath = "~/App_Data/Authentication.xml";
        /// <summary>
        /// Finds the node.
        /// 孙本强 @ 2013-04-03 13:12:28
        /// </summary>
        /// <param name="Nodes">The nodes.</param>
        /// <param name="NoCheckList">The no check list.</param>
        /// <remarks></remarks>
        private void FindNode(XmlNodeList Nodes, ref List<string> NoCheckList)
        {
            foreach (XmlNode node in Nodes)
            {
                bool isRead = false;

                if (node.Name.ToLower() == "location".ToLower()
                    && node.ParentNode != null && node.ParentNode.Name.ToLower() == "NoCheckAuthentication".ToLower())
                {
                    XmlAttributeCollection Attributes = node.Attributes;
                    foreach (XmlAttribute Attribute in Attributes)
                    {
                        if (Attribute.Name.ToLower() == "path".ToLower())
                        {
                            NoCheckList.Add(Attribute.Value.Trim());
                        }
                    }
                }
                if (node.Name.ToLower() == "RedirectTo".ToLower()
                    && node.ParentNode != null && node.ParentNode.Name.ToLower() == "NoCheckAuthentication".ToLower())
                {
                    XmlAttributeCollection Attributes = node.Attributes;
                    foreach (XmlAttribute Attribute in Attributes)
                    {
                        if (Attribute.Name.ToLower() == "path".ToLower())
                        {
                            this.RedirectTo = (Attribute.Value.Trim());
                        }
                    }
                }
                if (!isRead)
                {
                    FindNode(node.ChildNodes, ref NoCheckList);
                }
            }
        }
        /// <summary>
        /// 类 AuthenticationXML 构造函数
        /// 孙本强 @ 2013-04-03 13:12:28
        /// </summary>
        /// <remarks></remarks>
        public AuthenticationXML()
        {
            List<string> Result = new List<string>();
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(HttpContext.Current.Server.MapPath(this.configFilePath));
            FindNode(xmlDocument.ChildNodes, ref Result);
            NoCheckList = Result;
        }
        /// <summary>
        /// Gets the no check list.
        /// 孙本强 @ 2013-04-03 13:12:29
        /// </summary>
        /// <remarks></remarks>
        public List<string> NoCheckList { get; private set; }
        /// <summary>
        /// Gets the redirect to.
        /// 孙本强 @ 2013-04-03 13:12:29
        /// </summary>
        /// <remarks></remarks>
        public string RedirectTo { get; private set; }
    }
}
