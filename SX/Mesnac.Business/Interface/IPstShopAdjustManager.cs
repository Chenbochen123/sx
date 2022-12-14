using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    public interface IPstShopAdjustManager : IBaseManager<PstShopAdjust>
    {
        PageResult<PstShopAdjust> GetTablePageDataBySql(PstShopAdjustManager.QueryParams queryParams);
        PageResult<PstShopAdjust> GetTablePageTotalBySql(PstShopAdjustManager.QueryParams queryParams);
        string CancelShopAdjust(string storageID, string storagePlaceID, string barcode, int orderID, string operPerson);
        DataSet GetByInfo(string workShopCode, DateTime beginTime, DateTime endTime, string storageID, string storagePlaceID, string materCode, string adjustType);
        DataSet GetReportBySql(PstShopAdjustManager.QueryParams queryParams);
    }
}
