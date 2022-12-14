<%@ WebHandler Language="C#" Class="LoginHandler" %>

using System;
using System.Web;
using Mesnac;
using Mesnac.Entity;
using Mesnac.Business.Interface;
using Mesnac.Business.Implements;

public class LoginHandler : IHttpHandler ,System.Web.SessionState.IRequiresSessionState {

    public void ProcessRequest(HttpContext context)
    {
        
        LoginUser(sLoginName, sLoginPass, sCode_op, UserKey);       //登录
        
    }

    /// <summary>
    /// 进行登陆操作
    /// </summary>
    /// <param name="sLoginName">用户名</param>
    /// <param name="sLoginPass">密码</param>
    /// <param name="sCode_op">验证码</param>
    /// <param name="UserKey">用户key</param>
    private void LoginUser(string sLoginName, string sLoginPass, string sCode_op, string UserKey)
    {
        BasUserManager basUserManager = null;
        Mesnac.Entity.Custom.CdbVersion dbVersion = HttpContext.Current.Session["dbVersion"] as Mesnac.Entity.Custom.CdbVersion;
        if (dbVersion != null)
        {
            Mesnac.Entity.Custom.CdbDatabase db = dbVersion.Databases["Default"];
            NBear.Data.Gateway gateway = new NBear.Data.Gateway(db.AssemblyName, db.ClassName, db.ConnStr);
            basUserManager = new BasUserManager(gateway);
        }
        else
        {
            basUserManager = new BasUserManager();
        }


        BasUser currUser = new BasUser();
        currUser.HRCode = sLoginName.Trim();
        currUser.UserPWD = sLoginPass.Trim();

        string urlReferrer = HttpContext.Current.Request.UrlReferrer.AbsolutePath;
        if (DispCode && (HttpContext.Current.Session["CheckCode"] == null || sCode_op != HttpContext.Current.Session["CheckCode"].ToString()))
        {
            HttpContext.Current.Response.Redirect(urlReferrer + "?code=4&isCaptcha=1", true);
        }
        else if (this.LoginErrors > 20)
        {
            HttpContext.Current.Response.Redirect(urlReferrer + "?code=6&isCaptcha=1", true);
        }
        else if (basUserManager.Login(currUser, out currUser))
        {
            //写登入日志
            HttpContext.Current.Session["user"] = currUser; //保存当前用户信息
            //Mesnac.Util.LoginUtil.Signin(currUser);         //发票证
            if (HttpContext.Current.Request.UrlReferrer != null)
            {
                HttpContext.Current.Session["MyReturnUrl"] = HttpContext.Current.Request.UrlReferrer.AbsolutePath;  //保存起始登录页的访问路径
            }
            else
            {
                HttpContext.Current.Session["MyReturnUrl"] = "~/";
            }
            HttpContext.Current.Response.Redirect("~/Manager/MainFrame.aspx", true);
        }
        else
        {
            this.LoginErrors++;
            if (DispCode)
            {
                //登录失败，显示验证码
                HttpContext.Current.Response.Redirect(urlReferrer + "?code=1&isCaptcha=1", true);
            }
            else
            {
                //登录失败，不显示验证码
                HttpContext.Current.Response.Redirect(urlReferrer + "?code=1&isCaptcha=2", true);
            }
        }
    }

    /// <summary>
    /// 是否显示验证码(出错6次,出现验证码)
    /// </summary>
    private bool DispCode
    {
        get
        {
            if (this.LoginErrors > 5)
                return true;
            else
                return false;
        }
    }

    /// <summary>
    /// 获得用户登陆Key(根据IP)
    /// </summary>
    string UserKey
    {
        get
        {
            return Mesnac.Util.CommonUtil.GetIPAddress().Replace(".", "_");
        }
    }
    /// <summary>
    /// 登录名
    /// </summary>
    string sLoginName
    {
        get
        {
            return HttpContext.Current.Request.Form["user"] as string;
        }
    }
    /// <summary>
    /// 密码
    /// </summary>
    string sLoginPass
    {
        get
        {
            return HttpContext.Current.Request.Form["password"] as string;
        }
    }
    /// <summary>
    /// 验证码
    /// </summary>
    string sCode_op
    {
        get
        {
            return HttpContext.Current.Request.Form["auth_code"] as string;
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

    /// <summary>
    /// 登录出错次数
    /// </summary>
    public int LoginErrors
    {
        get
        {
            return Convert.ToInt32(HttpContext.Current.Session["LoginErrors"]);
        }
        set
        {
            HttpContext.Current.Session["LoginErrors"] = value;
        }
    }

}