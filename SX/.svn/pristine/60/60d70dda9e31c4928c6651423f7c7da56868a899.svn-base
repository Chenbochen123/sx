using System;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Xml;

namespace Mesnac.Business.Implements
{
    using System.Collections.Generic;
    using Mesnac.Business.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Data.Interface;
    using Mesnac.Entity;
    using Mesnac.Util.Cryptography;
    using NBear.Common;
    using Mesnac.Data.Components;
    public class BasUserManager : BaseManager<BasUser>, IBasUserManager
    {
        #region 属性注入与构造方法

        private IBasUserService service;

        public BasUserManager()
        {
            this.service = new BasUserService();
            base.BaseService = this.service;
        }

        public BasUserManager(string connectStringKey)
        {
            this.service = new BasUserService(connectStringKey);
            base.BaseService = this.service;
        }

        public BasUserManager(NBear.Data.Gateway way)
        {
            this.service = new BasUserService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region 查询条件类定义
        public class QueryParams : BasUserService.QueryParams
        {
        }
        #endregion

        #region UserID
        private string _userid = string.Empty;

        /// <summary>
        /// 签发票证（临时票证）
        /// </summary>
        /// <param name="userid"></param>
        private void SetUserID(string userid)
        {
            this._userid = userid;
            FormsAuthenticationTicket tk = new FormsAuthenticationTicket(1,
                    userid,
                    DateTime.Now,
                    DateTime.Now.AddMinutes(20),
                    false,
                    userid,
                    FormsAuthentication.FormsCookiePath
                    );
            string key = FormsAuthentication.Encrypt(tk); //得到加密后的身份验证票字串 
            HttpCookie ck = new HttpCookie(FormsAuthentication.FormsCookieName, key);
            HttpContext.Current.Response.Cookies.Add(ck);
            HttpContext.Current.Session["UserID"] = userid;
        }
        private string GetUserID()
        {
            if (!string.IsNullOrWhiteSpace(_userid))
            {
                return _userid;
            }
            string Result = string.Empty;
            if (HttpContext.Current.Session["UserID"] != null)
            {
                Result = HttpContext.Current.Session["UserID"].ToString();
            }
            return Result;
            string cookieName = FormsAuthentication.FormsCookieName;
            HttpCookie authCookie = HttpContext.Current.Request.Cookies[cookieName];
            FormsAuthenticationTicket authTicket = null;
            try
            {
                authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                if (authTicket != null)
                {
                    Result = authTicket.Name;
                }
            }
            catch
            {
                Result = string.Empty;
            }
            if (string.IsNullOrWhiteSpace(Result))
            {
                if (HttpContext.Current.Session["UserID"] != null)
                {
                    Result = HttpContext.Current.Session["UserID"].ToString();
                }
            }
            _userid = Result;
            return Result;
        }

        public string UserID
        {
            get { return GetUserID(); }
            private set { SetUserID(value); }
        }
        #endregion
        #region Login Logout
        private BasUser GetBaseUer(BasUser loginUser)
        {
            BasUser user = null;
            WhereClip where = new WhereClip();
            where.And(BasUser._.DeleteFlag == 0);
            if (!string.IsNullOrWhiteSpace(loginUser.UserName))
            {
                where.And(BasUser._.UserName == loginUser.UserName);
            }
            if (!string.IsNullOrWhiteSpace(loginUser.WorkBarcode))
            {
                where.And(BasUser._.WorkBarcode == loginUser.WorkBarcode);
            }
            if (!string.IsNullOrWhiteSpace(loginUser.HRCode))
            {
                where.And(BasUser._.HRCode == loginUser.HRCode);
            }
            EntityArrayList<BasUser> users = this.service.GetListByWhere(where);
            if (users != null && users.Count > 0)
            {
                user = users[0];
            }
            return user;
        }
        private void LoginSuccess(BasUser user)
        {
            Logout(false);
            this.UserID = user.WorkBarcode.ToString();
            SysLoginLog loginlog = new SysLoginLog();
            loginlog.UserCode = user.WorkBarcode;
            loginlog.LoginTime = DateTime.Now;
            loginlog.LogoutTime = null;
            loginlog.LoginIP = HttpContext.Current.Request.UserHostAddress;
            if (loginlog.LoginIP == "::1")
            {
                loginlog.LoginIP = "172.0.0.1";
            }
            new SysLoginLogManager().Insert(loginlog);
        }
        
        /// <summary>
        /// 根据用户名称和密码进行登录（去掉了异常处理）
        /// </summary>
        /// <param name="loginUser">保存用户名称和密码的用户实体</param>
        /// <param name="user">验证成功后，返回用户实体</param>
        /// <returns>登录成功返回真，否则返回假</returns>
        public bool Login(BasUser loginUser, out BasUser user)
        {
            user = GetBaseUer(loginUser);
            if (user == null)
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(user.UserPWD))
            {
                return false;
            }
            string loginPass = loginUser.UserPWD;
            string spassword = new MesnacEngine().DecryptString(user.UserPWD.Trim(), string.Empty, Encoding.ASCII);
            if (loginPass.Trim() == spassword.Trim())
            {
                LoginSuccess(user);
                return true;
            }
            user = null;
            return false;
        }

        /// <summary>
        /// 用户注销方法
        /// </summary>
        private void Logout(bool clearAuthentication)
        {
            string userid = this.GetUserID();
            if (!string.IsNullOrWhiteSpace(userid))
            {
                string IP = HttpContext.Current.Request.UserHostAddress;
                if (IP == "::1")
                {
                    IP = "172.0.0.1";
                }
                int pp_count = 2;
                PropertyItem[] properties = new PropertyItem[pp_count];
                object[] values = new object[pp_count];
                pp_count = 0;
                properties[pp_count] = SysLoginLog._.LogoutIP;
                values[pp_count] = IP;
                pp_count++;
                properties[pp_count] = SysLoginLog._.LogoutTime;
                values[pp_count] = DateTime.Now;

                WhereClip where = new WhereClip();
                where.And(SysLoginLog._.UserCode == userid);
                where.And(SysLoginLog._.LogoutIP.Length == null || SysLoginLog._.LogoutIP.Length == 0);
                new SysLoginLogManager().Update(properties, values, where);
            }
            if (clearAuthentication)
            {
                FormsAuthentication.SignOut();
                HttpContext.Current.Session.Clear();
            }
        }
        public void Logout()
        {
            Logout(true);
        }
        #endregion
        public PageResult<BasUser> GetTablePageDataBySql(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }
        public string GetNextWorkBarcode()
        {
            return this.service.GetNextWorkBarcode();
        }
    }
}
