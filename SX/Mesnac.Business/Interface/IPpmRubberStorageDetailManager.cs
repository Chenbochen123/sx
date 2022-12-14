using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    public interface IPpmRubberStorageDetailManager : IBaseManager<PpmRubberStorageDetail>
    {
        DataSet GetByInfo(string Barcode, string StorageID, string StoragePlaceID);
    }
}
