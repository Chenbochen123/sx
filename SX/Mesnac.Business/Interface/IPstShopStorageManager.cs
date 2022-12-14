using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    public interface IPstShopStorageManager : IBaseManager<PstShopStorage>
    {
        PageResult<PstShopStorage> GetTablePageDataBySql(PstShopStorageManager.QueryParams queryParams);
        PageResult<PstShopStorage> GetTablePageDataBySql1(PstShopStorageManager.QueryParams queryParams);
        DataSet GetStoreOutData();
        PstShopStorage getPstShopStorage(string Barcode, string StorageID, string StoragePlaceID, string MaterCode);
        PstShopStorage getPstShopStorage(string Barcode, string StorageID, string StoragePlaceID, string SourceBillNo, string SourceOrderID);
        DataSet GetShopStorage(string Barcodes);
        DataSet GetShopStorageTotal(PstShopStorageManager.QueryParams queryParams);
        string GetNewBarcode(string barcode, string storageID, string storagePlaceID);
    }
}
