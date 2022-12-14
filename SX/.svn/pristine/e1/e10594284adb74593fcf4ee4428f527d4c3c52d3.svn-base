using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NBear.Common;
using Mesnac.Business.Implements;
using Mesnac.Entity;
using Mesnac.Data.Components;
using Mesnac.Business.Interface;
using Mesnac.Data.Implements;
using Ext.Net;

public partial class Manager_Storage_StoragePlaceMonitor : System.Web.UI.Page
{
    protected BasStoragePlacePropManager storagePlaceManager = new BasStoragePlacePropManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            String sql = @"update BasStoragePlaceProp
set storagenumber =

(select count(*) from PpmrubberStorage
where PpmrubberStorage.storageplaceid =BasStoragePlaceProp.storageplaceid
and realweight <>'0')";
            storagePlaceManager.GetBySql(sql).ToDataSet();


            sql = @"update BasStoragePlaceProp set isfull =0 where storagecapacity<>0 and storagenumber =0";
            storagePlaceManager.GetBySql(sql).ToDataSet();
            MainTabPanel.SetActiveTab(1);
            MainTabPanel.CloseTab(1, CloseAction.Hide);
            MainTabPanel.SetActiveTab(0);
            
            InitStoragePlace();
        }
    }


    [Ext.Net.DirectMethod()]
    public string Dashboard_ItemClick(string equipId)
    {
        //cboEquipId.SetValue(equipId);
        //chartMainBindData(equipId);
        return "";
    }
    [Ext.Net.DirectMethod()]
    public string GridPanelBindData(string storagePlaceId)
    {
        DataSet dsState = GetStoragePlaceState(storagePlaceId);
   
        store.DataSource = dsState;
        store.DataBind();
        return "";
    }

    public void InitStoragePlace()
    {
        //string defaultIcon = "../../resources/images/fanhui.png";
        //string hege1Icon = "../../resources/images/hege1.png";
        //string hege2Icon = "../../resources/images/hege2.png";
        //string buhegeIcon = "../../resources/images/buhege.png";
        string defaultIcon = "../../resources/images/hege1.png";
        string hege1Icon = "../../resources/images/hege1.png";
        string hege2Icon = "../../resources/images/hege1.png";
        string buhegeIcon = "../../resources/images/hege1.png";
        string storagePlaceId = "";
        BasStoragePlace storagePlace = new BasStoragePlace();
        DataSet dsGroup = storagePlaceManager.GetStoragePlaceGroup();
        DataSet dsState = GetStoragePlaceState(storagePlaceId);
        IEnumerable<object> query = from gp in dsGroup.Tables[0].AsEnumerable()
                                    select new
                                    {
                                        Title = gp.Field<string>("PositionType") != null ? gp.Field<string>("PositionType").ToString().Remove(0) : "",
                                        Item = (from m in dsState.Tables[0].AsEnumerable().Where(dr => dr.Field<string>("PositionType").Contains(gp.Field<string>("PositionType")))
                                                 select new
                                                 {
                                                     StoragePlaceName = m.Field<string>("StoragePlaceName") != null ? m.Field<string>("StoragePlaceName").ToString() : "",
                                                     Icon = m.Field<string>("PositionType").Contains("0") ? buhegeIcon:(m.Field<string>("PositionType").Contains("E") || m.Field<string>("PositionType").Contains("F") || m.Field<string>("PositionType").Contains("G")) ? hege1Icon : ((m.Field<string>("PositionType").Contains("H") || m.Field<string>("PositionType").Contains("I")) ? hege2Icon : defaultIcon) ,
                                                     StoragePlaceId = m.Field<string>("StoragePlaceID") != null ? m.Field<string>("StoragePlaceID") : "",
                                                      StorageNum=m.Field<int>("StorageNumber").ToString() != null ? m.Field<int>("StorageNumber").ToString() : ""
                                                 }
                                            )
                                    };
        this.Store1.DataSource = query;
    }
    public DataSet GetStoragePlaceState(string storagePlace)
    {
        var s = "";
        if (string.IsNullOrWhiteSpace(storagePlace))
        {
            s = "<>''";
        }
        else
            s = "='" + storagePlace + "'";
        var sql = @"select b.ObjID, c.StoragePlaceID,c.StoragePlaceName, a.MaterialCode,a.MaterialName,b.StorageCapacity,b.StorageNumber,b.SpecialPlace,b.PositionType
                           from  BasMaterial a
                          left join BasStoragePlaceProp b on charindex(a.MaterialCode,b.MaterialCode)>0
                          left join BasStoragePlace c on b.StoragePlaceID=c.StoragePlaceID
						    where b.StoragePlaceID <> ''
                          and c.StoragePlaceID" + s + "order by c.StoragePlaceName";
         sql = @"select b.ObjID, c.StoragePlaceID,c.StoragePlaceName, b.MaterialCode,b.MaterialName,b.StorageCapacity,b.StorageNumber,b.SpecialPlace,b.PositionType
                           from   BasStoragePlaceProp b 
                          left join BasStoragePlace c on b.StoragePlaceID=c.StoragePlaceID
						    where b.StoragePlaceID <> ''
                          and c.StoragePlaceID" + s + "order by c.StoragePlaceName";
        NBear.Data.CustomSqlSection a = storagePlaceManager.GetBySql(sql);
        return a.ToDataSet();
    }
}