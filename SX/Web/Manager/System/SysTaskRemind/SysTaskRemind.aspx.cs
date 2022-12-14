using Mesnac.Business.Implements;
using Mesnac.Business.Interface;
using Mesnac.Entity;
using NBear.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Manager_System_SysTaskRemind_SysTaskRemind : Mesnac.Web.UI.Page
{
    public string note = "";
    public string left = "";
    public string top = "";
    public string zindex = "";
    protected ISysTaskRemindManager manager = new SysTaskRemindManager();
    protected IBasUserManager userManager = new BasUserManager();
    public EntityArrayList<SysTaskRemind> list = new EntityArrayList<SysTaskRemind>();
    protected void Page_Load(object sender, EventArgs e)
    {
        list = manager.GetListByWhere(SysTaskRemind._.ReceiveUser == this.UserID && SysTaskRemind._.DeleteFlag == "0");
        StringBuilder sb = new StringBuilder();
        foreach (SysTaskRemind task in list)
        {
            EntityArrayList<BasUser> userList = userManager.GetListByWhere(BasUser._.WorkBarcode == task.CreateUser);
            string[] position = task.XYZ.Split(new char[] { 'x' });
            left = position[0];
            top = position[1];
            zindex = position[2];
            sb.Append("<div   class=\"note " + task.Color + "\" style=\"left:" + left + "px; top:" + top + "px; z-index:" + zindex + "\">");
            sb.Append("<div class=\"delbtn\" noteid=" + task.EventID + ">x</div>");
            sb.Append(task.EventName);
            if (userList.Count > 0)
            {
                sb.Append("<div class=\"author\"><span style=\"padding-right:10px\">" + userList[0].UserName + "</span><span style=\"padding-left:10px\">" + Convert.ToDateTime(task.StartDate).ToString("yy-MM-dd") + "</span></div>");
            }
            sb.Append("<span class=\"data\">" + task.Details + "</span>");
            sb.Append("<span class=\"eventId\">" + task.EventID + "</span>");
            sb.Append("</div>");
        }
        lbl_notes.Text = sb.ToString();
    }
}