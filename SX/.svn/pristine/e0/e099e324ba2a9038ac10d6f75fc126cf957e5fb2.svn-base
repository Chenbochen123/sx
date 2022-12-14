using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    public interface IPstStorageDetailService : IBaseService<PstStorageDetail>
    {
        DataSet GetByInfo(string Barcode, string StorageID, string StoragePlaceID);
        int GetOrderID(string Barcode);
    }
}
