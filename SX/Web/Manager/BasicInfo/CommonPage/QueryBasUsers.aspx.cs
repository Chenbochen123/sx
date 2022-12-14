using System;
using System.Data;
using System.Text;
using Ext.Net;
using Mesnac.Business.Implements;

public partial class Manager_BasicInfo_CommonPage_QueryBasUsers : Mesnac.Web.UI.Page
{
    protected BasUserManager manager = new BasUserManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            bindlist();
        }
    }
    private void bindlist()
    {
        DataSet ds = manager.GetDataSetByFieldsAndWhere("ObjID,UserName,WorkBarcode,RealName,HRCode", "WHERE EXISTS(SELECT * FROM SysParam TB WHERE TB.ParamName='dailyRepair' AND CHARINDEX(','+CAST(WorkID AS VARCHAR(4))+',',','+TB.ParamValue+',')>0)");
        this.store.DataSource = ds;
        this.store.DataBind();
    }
    [DirectMethod]
    public string getWorkBarcode()
    {
        StringBuilder sb = new StringBuilder();
        RowSelectionModel sm = this.gridPanel.GetSelectionModel() as RowSelectionModel;

        foreach ( SelectedRow row in sm.SelectedRows )
        {
            sb.Append(manager.GetById(row.RecordID).UserName + ",");
        }

        return sb.ToString().TrimEnd( ',' );
    }

}