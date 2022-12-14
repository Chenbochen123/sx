using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Implements;
    public interface IPpmRubberCarryoverService : IBaseService<PpmRubberCarryover>
    {
        string GetInaccountDuration(string StorageID);
        string GetStorageDuration(string StorageID);
        List<string> GetDurationFromPpmStorage(string StorageID);
        bool CarryoverStorageDetail(string StorageID, string InaccountDuration);
    }
}
