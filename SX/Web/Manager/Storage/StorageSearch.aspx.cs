using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using NBear.Common;
using Mesnac.Business.Implements;
using Mesnac.Entity;
using Mesnac.Data.Components;
using Mesnac.Business.Interface;
using Mesnac.Data.Implements;

public partial class Manager_Storage_StorageSearch : System.Web.UI.Page
{
    protected BasStorageManager basStorageManager = new BasStorageManager();
    protected BasStoragePlaceManager basStoragePlaceManager = new BasStoragePlaceManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataSet ds = basStorageManager.GetStorageInfo("0", "1");
            ddlStorageName.DataSource = ds;
            ddlStorageName.DataTextField = "StorageName";
            ddlStorageName.DataValueField = "StorageID";
            ddlStorageName.DataBind();
        }
    }

    protected void ddlStorageName_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlStoragePlaceName.Items.Clear();
        DataSet ds = basStoragePlaceManager.GetStoragePlaceInfo(ddlStorageName.SelectedItem.Value);
        ddlStoragePlaceName.DataSource = ds;
        ddlStoragePlaceName.DataTextField = "StoragePlaceName";
        ddlStoragePlaceName.DataValueField = "StoragePlaceID";
        ddlStoragePlaceName.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string path = "StorageAutoScroll.aspx";
        if (ddlStorageName.SelectedValue != "0")
            path += "?StorageID=" + ddlStorageName.SelectedValue;
        else
            path += "?StorageID=";
        if (ddlStorageName.SelectedValue != "0" && ddlStoragePlaceName.SelectedValue != "0")
            path += "&&StoragePlaceID=" + ddlStoragePlaceName.SelectedValue;
        else
            path += "&&StoragePlaceID=";
        Response.Write("<script>window.open(" + path + ", '_blank')</script>");
        //Response.Redirect(path);
    }
}