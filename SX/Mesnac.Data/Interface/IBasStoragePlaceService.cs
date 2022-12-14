using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    public interface IBasStoragePlaceService : IBaseService<BasStoragePlace>
    {
        PageResult<BasStoragePlace> GetTablePageDataBySql(Mesnac.Data.Implements.BasStoragePlaceService.QueryParams queryParams);
        void SetDefaultStoragePlace(string ObjID, string StorageID);
        void SetAutoGenDefault(string ObjID, string StorageID);
        DataSet GetStoragePlaceID(string StorageID);
        string GetStoragePlaceName(string StorageID, string StoragePlaceID);
        bool UpdateLocked(string IDS);
        bool UpdateUsingByStorageID(string IDS);
        string GetStorageType(string StoragePlaceID, string StorageID);
        DataSet GetStoragePlaceInfo(string storageID);
    }
}
