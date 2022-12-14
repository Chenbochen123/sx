using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    public interface IPstStorageDetailManager : IBaseManager<PstStorageDetail>
    {
        DataSet GetByInfo(string Barcode, string StorageID, string StoragePlaceID);
        int GetOrderID(string Barcode);
    }
}
