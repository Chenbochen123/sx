using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    public interface IPpmRubberCarryoverManager : IBaseManager<PpmRubberCarryover>
    {
        string GetInaccountDuration(string StorageID);
        List<string> GetDurationFromPpmStorage(string StorageID);
        string GetStorageDuration(string StorageID);
        bool CarryoverStorageDetail(string StorageID, string InaccountDuration);
    }
}
