using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;

using Mesnac.Web.UI;
using Mesnac.Business.Interface;
using Mesnac.Business.Implements;
using Mesnac.Data.Interface;
using Mesnac.Data.Implements;
using Mesnac.Entity;
using Ext.Net;
public partial class Manager_Rubber_Report_RubberInvalidQuery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        #region 加载CSS样式
        HtmlGenericControl cssLink = new HtmlGenericControl("link");
        cssLink.Attributes.Add("type", "text/css");
        cssLink.Attributes.Add("href", this.ResolveUrl("~/resources/css/main.css"));
        this.Page.Header.Controls.Add(cssLink);
        #endregion 加载CSS样式
        if (!X.IsAjaxRequest)
        {
            txtBeginTime.SetValue(DateTime.Today.ToString("yyyy-MM-dd"));
            txtEndTime.SetValue(DateTime.Today.ToString("yyyy-MM-dd"));
        }
    }
    protected void btnSearch_Click(object sender, DirectEventArgs e)
    {

    }
    protected void btnExport_Click(object sender, EventArgs e)
    {

    }
}