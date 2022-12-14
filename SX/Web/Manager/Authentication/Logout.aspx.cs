using System;
using Mesnac.Business.Implements;
using Mesnac.Business.Interface;

/// <summary>
/// Manager_Authentication_Logout 实现类
/// 孙本强 @ 2013-04-03 13:10:02
/// </summary>
/// <remarks></remarks>
public partial class Manager_Authentication_Logout : System.Web.UI.Page  //不能使用 Mesnac.Web.UI.Page
{
    #region 属性注入
    /// <summary>
    /// 
    /// 孙本强 @ 2013-04-03 13:10:02
    /// </summary>
    private IBasUserManager basUserManager = new BasUserManager();
    #endregion

    /// <summary>
    /// Handles the Load event of the Page control.
    /// 孙本强 @ 2013-04-03 13:10:02
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    /// <remarks></remarks>
    protected void Page_Load(object sender, EventArgs e)
    {
        string uri = this.Session["MyReturnUrl"] as string;
        this.basUserManager.Logout();
        if (String.IsNullOrEmpty(uri))
        {
            uri = this.ResolveUrl("~/");
        }
        this.Response.Redirect(uri, true);
    }
}