using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Implements;
    public interface IPstShopStorageDetailService : IBaseService<PstShopStorageDetail>
    {
        PageResult<PstShopStorageDetail> GetTablePageDataBySqlPrint(PstShopStorageDetailService.QueryParams queryParams);
        DataSet GetByPrintInfo(string Barcode, string StorageID, string StoragePlaceID, string OrderID);
        DataSet GetByInfo(string Barcode, string StorageID, string StoragePlaceID);
        DataSet GetByInfo(string Barcode, string StorageID, string StoragePlaceID,string boxcode);
        int GetOrderID(string Barcode);
        DataSet GetSqlInfo(string sql);
    }
}
