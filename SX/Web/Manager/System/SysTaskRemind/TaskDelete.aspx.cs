using Mesnac.Business.Implements;
using Mesnac.Business.Interface;
using Mesnac.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Manager_System_SysTaskRemind_TaskDelete : System.Web.UI.Page
{
    protected ISysTaskRemindManager manager = new SysTaskRemindManager();
    public string result = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string eventId = GetRequest("id");
            SysTaskRemind task = manager.GetById(eventId);
            if (task != null)
            {
                task.DeleteFlag = "1";
                task.EndDate = DateTime.Now;
                manager.Update(task);
            }
            result = "SUCCESS";
        }
        catch
        {
        }
    }

    /// <summary>
    /// Gets the request.
    /// yuany 2014年5月9日17:02:02
    /// </summary>
    /// <param name="key">The key.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    private string GetRequest(string key)
    {
        if (this.Request[key] != null)
        {
            return this.Request[key].ToString().Trim();
        }
        return string.Empty;
    }
}