using Ext.Net;
using Mesnac.Business.Implements;
using Mesnac.Business.Interface;
using Mesnac.Entity;
using Newtonsoft.Json;
using System;
using NBear.Common;
using System.Collections.Generic;


/// <summary>
/// Manager_Technology_Comparison_Default 实现类
/// 孙本强 @ 2013-04-03 13:05:31
/// </summary>
/// <remarks></remarks>
public partial class Manager_Technology_Comparison_Default : Mesnac.Web.UI.Page
{
    /// <summary>
    /// Gets the request.
    /// 孙本强 @ 2013-04-03 13:05:32
    /// </summary>
    /// <param name="key">The key.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    private string GetRequest(string key)
    {
        if (this.Request[key] != null)
        {
            return this.Request[key].ToString();
        }
        return string.Empty;
    }
    /// <summary>
    /// Handles the Load event of the Page control.
    /// 孙本强 @ 2013-04-03 13:05:32
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    /// <remarks></remarks>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (X.IsAjaxRequest)
        {
            return;
        }
        a.Text = GetRequest("a");
        b.Text = GetRequest("b");
    }
}