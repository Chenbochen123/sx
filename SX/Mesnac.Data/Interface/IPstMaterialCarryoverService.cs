using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Implements;
    public interface IPstMaterialCarryoverService : IBaseService<PstMaterialCarryover>
    {
        string GetInaccountDuration(string StorageID);
        string GetStorageDuration(string StorageID);
        List<string> GetDurationFromPststorage(string StorageID);
        bool CarryoverStorageDetail(string StorageID, string InaccountDuration);
        bool UpdateStorageDuring(string StorageID);
    }
}
