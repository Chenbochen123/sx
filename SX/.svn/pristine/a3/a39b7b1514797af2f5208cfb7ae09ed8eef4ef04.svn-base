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

public partial class Manager_System_SysTaskRemind_TaskPosition : System.Web.UI.Page
{
    protected ISysTaskRemindManager manager = new SysTaskRemindManager();
    public EntityArrayList<SysTaskRemind> list = new EntityArrayList<SysTaskRemind>();
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = Request["id"];
        int x = int.Parse(Request["x"]);
        int y = int.Parse(Request["y"]);
        string z = Request["z"];


        if ((x < 0 || x > 1100) || (y < 0 || y > 450))
        {
            return;
        }


        string position = x + "x" + y + "x" + z;
        SysTaskRemind task = manager.GetById(id);
        task.XYZ = position;
        manager.Update(task);
    }
}