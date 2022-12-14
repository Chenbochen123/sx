using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    public interface IPstShopStorageDetailManager : IBaseManager<PstShopStorageDetail>
    {
        PageResult<PstShopStorageDetail> GetTablePageDataBySqlPrint(PstShopStorageDetailManager.QueryParams queryParams);
        DataSet GetByPrintInfo(string Barcode, string StorageID, string StoragePlaceID, string OrderID);
        DataSet GetByInfo(string Barcode, string StorageID, string StoragePlaceID);
        DataSet GetByInfo(string Barcode, string StorageID, string StoragePlaceID, string boxcode);
        int GetOrderID(string Barcode);
        DataSet GetSqlInfo(string sql);
    }
}
