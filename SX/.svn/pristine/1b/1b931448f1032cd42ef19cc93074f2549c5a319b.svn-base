using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Implements;
    public interface IPstStorageService : IBaseService<PstStorage>
    {
        PageResult<PstStorage> GetTablePageDataBySql(PstStorageService.QueryParams queryParams);
        PageResult<PstStorage> GetTablePageDataBySql1(PstStorageService.QueryParams queryParams);
        PageResult<PstStorage> GetTablePageDataBySql2(PstStorageService.QueryParams queryParams);
        DataSet GetStorageInfo(string StorageID, string StoragePlaceID, string MaterCode);
        DataSet GetStoreOutData();
        PstStorage getPstStorage(string Barcode, string StorageID, string StoragePlaceID, string MaterCode);
        DataSet GetStorage(string Barcodes);
        DataSet GetStorageTotal(PstStorageService.QueryParams queryParams);
    }
}
