using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Implements;
    public interface IPstShopStorageService : IBaseService<PstShopStorage>
    {
        PageResult<PstShopStorage> GetTablePageDataBySql(PstShopStorageService.QueryParams queryParams);
        PageResult<PstShopStorage> GetTablePageDataBySql1(PstShopStorageService.QueryParams queryParams);
        DataSet GetStoreOutData();
        PstShopStorage getPstShopStorage(string Barcode, string StorageID, string StoragePlaceID, string MaterCode);
        PstShopStorage getPstShopStorage(string Barcode, string StorageID, string StoragePlaceID, string SourceBillNo, string SourceOrderID);
        DataSet GetShopStorage(string Barcodes);
        DataSet GetShopStorageTotal(PstShopStorageService.QueryParams queryParams);
        string GetNewBarcode(string barcode, string storageID, string storagePlaceID);
    }
}
