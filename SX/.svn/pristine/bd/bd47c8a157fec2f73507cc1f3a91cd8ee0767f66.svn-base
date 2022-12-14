using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Implements;
    public interface IPpmStorageDetailService : IBaseService<PpmStorageDetail>
    {
        DataSet GetByInfo(string Barcode, string StorageID, string StoragePlaceID);
        int GetOrderID(string Barcode);
    }
}
