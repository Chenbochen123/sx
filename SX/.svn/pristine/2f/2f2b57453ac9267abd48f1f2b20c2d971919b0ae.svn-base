using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    public interface IBasStorageService : IBaseService<BasStorage>
    {
        PageResult<BasStorage> GetTablePageDataBySql(Mesnac.Data.Implements.BasStorageService.QueryParams queryParams);
        DataSet GetStorageID(string StorageID);
        bool UpdateUsing(string IDS);
        void UpdateLastStorageFlag(string ObjID);
        string GetStorageName(string StorageID);
        string IsStoreIn(string StorageID);
        DataSet GetDuration(string StorageID);
        DataSet GetStorageInfo(string storageType, string lastStorageFlag);
        DataSet GetStorageStr(string storageID);
    }
}
