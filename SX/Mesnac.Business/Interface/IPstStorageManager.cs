using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    public interface IPstStorageManager : IBaseManager<PstStorage>
    {
        PageResult<PstStorage> GetTablePageDataBySql(PstStorageManager.QueryParams queryParams);
        PageResult<PstStorage> GetTablePageDataBySql1(PstStorageManager.QueryParams queryParams);
        PageResult<PstStorage> GetTablePageDataBySql2(PstStorageManager.QueryParams queryParams);
        DataSet GetStorageInfo(string StorageID, string StoragePlaceID, string MaterCode);
        DataSet GetStoreOutData();
        PstStorage getPstStorage(string Barcode, string StorageID, string StoragePlaceID, string MaterCode);
        DataSet GetStorage(string Barcodes);
        DataSet GetStorageTotal(PstStorageManager.QueryParams queryParams);
    }
}
