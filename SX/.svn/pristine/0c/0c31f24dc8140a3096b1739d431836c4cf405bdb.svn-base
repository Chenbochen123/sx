using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mesnac.Data.Implements;
using System.Data;
using Ext.Net;
public partial class Manager_RubberQuality_Demo_DemoMain : System.Web.UI.Page
{
    private BasUserService m = new BasUserService();
    protected void Page_Load(object sender, EventArgs e)
    {
        txt_obj_id.SetValue(new DateTime(2013,3,1));
        txt_unit_name.SetValue(new DateTime(2013,3,10));
    }

    protected void pnlListFresh(object sender, EventArgs e)
    {
        if (txt_remark.Value == "")
        {
            X.Msg.Alert("提示", "请选择胶料名称").Show();
            return;
        }
        if (TextField1.Value == "")
        {
            X.Msg.Alert("提示", "请选择检验项目").Show();
            return;
        }
        store.DataSource = m.GetDataSetByStoreProcedure("Demo", new Dictionary<string, object>());
        store.DataBind();
        X.Msg.Notify("提示", "查询完毕").Show();
    }
}