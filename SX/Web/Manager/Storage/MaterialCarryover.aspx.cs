using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

using Ext.Net;
using NBear.Common;
using Mesnac.Business.Implements;
using Mesnac.Entity;
using Mesnac.Data.Components;
using Mesnac.Business.Interface;
using Mesnac.Data.Implements;

public partial class Manager_BasicInfo_StorageInfo_MaterialCarryover : Mesnac.Web.UI.Page
{
    private BasStorageManager manager = new BasStorageManager();
    private BasStoragePlaceManager placeManager = new BasStoragePlaceManager();
    private PstStorageDetailManager storageDetailManager = new PstStorageDetailManager();
    private PstMaterialCarryoverManager carryoverManager = new PstMaterialCarryoverManager();
    private PstMaterialCarryoverDetailManager carryoverDetailManager = new PstMaterialCarryoverDetailManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
        }
    }

    #region 分页相关方法
    private PageResult<BasStorage> GetPageResultData(PageResult<BasStorage> pageParams)
    {
        BasStorageManager.QueryParams queryParams = new BasStorageManager.QueryParams();
        queryParams.pageParams = pageParams;
        //if (ckxStorageType.SelectedItem.Value != "all")
        //    queryParams.storageType = ckxStorageType.SelectedItem.Value;
        queryParams.storageType = "0";
        queryParams.storageName = txtStorageName.Text;
        queryParams.usedFlag = "1";
        queryParams.cancelFlag = "0";
        queryParams.lastStorageFlag = "1";
        queryParams.erpCode = txtERPCode.Text;
        queryParams.deleteFlag = "0";
        
        return manager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<BasStorage> pageParams = new PageResult<BasStorage>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "CreateDate DESC";

        PageResult<BasStorage> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }
    #endregion

    #region 增删改查按钮激发的事件
    [Ext.Net.DirectMethod()]
    public string btnBatchCarryover_Click()
    {
        string storageName = string.Empty;
        foreach (SelectedRow row in this.rowSelectMuti.SelectedRows)
        {
            EntityArrayList<BasStorage> storageList = manager.GetListByWhere(BasStorage._.ObjID == row.RecordID);
            string storageCarryoverDuration = carryoverManager.GetStorageDuration(storageList[0].StorageID);//应该结转到的期间
            //判断是否有需要结转的期间
            EntityArrayList<PstStorageDetail> storageDetailList = storageDetailManager.GetListByWhere(PstStorageDetail._.InaccountDuration == storageCarryoverDuration);
            //EntityArrayList<PstMaterialCarryover> carryoverList = carryoverManager.GetListByWhere(PstMaterialCarryover._.InaccountDuration == string.Format("{0:yyyyMM}", DateTime.Now.AddMonths(-1)));
            string inaccountDuration = carryoverManager.GetInaccountDuration(storageList[0].StorageID);//已经结转的期间

            if (storageCarryoverDuration == inaccountDuration || storageDetailList.Count == 0)
            {//已经结转的库房，不用再结转，提示
                storageName += storageList[0].StorageName + "；";
            }
            else
            {
                //storageList[0].LockFlag = "1";
                //manager.Update(storageList[0]);

                List<string> arrList = carryoverManager.GetDurationFromPststorage(storageList[0].StorageID);
                foreach (string arr in arrList)
                {
                    //结转操作
                    carryoverManager.CarryoverStorageDetail(storageList[0].StorageID, arr);
                }
                //修改库房的当前期间为本月
                carryoverManager.UpdateStorageDuring(storageList[0].StorageID);
            }
        }

        if (!string.IsNullOrEmpty(storageName))
            return storageName + "没有可结转的数据！";
        else
            return "结转成功！";
        //bool result = manager.UpdateUsing(IDS.Remove(IDS.Length - 2, 1));
        //bool result1 = placeManager.UpdateUsingByStorageID(IDS.Remove(IDS.Length - 2, 1));

        //if (result == true && result1 == true)
        //{
        //    pageToolBar.DoRefresh();
        //    return "启用成功！";
        //}
        //else
        //{
        //    return "启用失败！";
        //}
    }
    #endregion
}