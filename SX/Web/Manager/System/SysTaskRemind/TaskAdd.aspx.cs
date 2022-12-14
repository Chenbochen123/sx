using Mesnac.Business.Implements;
using Mesnac.Business.Interface;
using Mesnac.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Manager_System_SysTaskRemind_TaskAdd : Mesnac.Web.UI.Page
{
    public string result = "";
    protected ISysTaskRemindManager manager = new SysTaskRemindManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        string title = Request["title"];
        string text = Request["body"];
        string color = Request["color"];
        string x=Request["left"];
        string y=Request["top"];
        if (string.IsNullOrEmpty(x))
        {
            x = new Random().Next(200).ToString();
        }
        if (string.IsNullOrEmpty(y))
        {
            y = new Random().Next(200).ToString();
        }
        string position=x+"x"+y+"x"+Request["zindex"];
        SysTaskRemind task = new SysTaskRemind();
        task.EventName = title;
        task.Details = text;
        task.ReceiveUser = this.UserID;
        task.CreateUser = this.UserID;
        task.StartDate = DateTime.Now;
        task.EndDate = DateTime.Now;
        task.XYZ = position;
        task.Color = color;
        task.DeleteFlag = "0";
        manager.Insert(task);
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