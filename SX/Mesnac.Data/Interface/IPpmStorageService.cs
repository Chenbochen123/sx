using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Implements;
    public interface IPpmStorageService : IBaseService<PpmStorage>
    {
        PageResult<PpmStorage> GetTablePageDataBySql(PpmStorageService.QueryParams queryParams);
        PageResult<PpmStorage> GetTablePageDataBySql1(PpmStorageService.QueryParams queryParams);
        DataSet GetStoreOutData();
        PpmStorage getPpmStorage(string Barcode, string StorageID, string StoragePlaceID, string MaterCode);
        DataSet GetStorage(string Barcodes);
    }
}
