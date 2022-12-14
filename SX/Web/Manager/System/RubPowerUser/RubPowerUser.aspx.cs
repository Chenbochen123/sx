using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Manager_System_RubPowerUser_RubPowerUser : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //lblTitle.Text = Request["RubCode"].ToString();
        List<object> data = new List<object>();

        for (int i = 0; i < 10; i++)
        {
            data.Add(new
            {
                Name = "Rec " + i,
                Column1 = i.ToString(),
                Column2 = i.ToString()
            });
        }

        this.Store1.DataSource = data;
        this.Store1.DataBind();
    }
}