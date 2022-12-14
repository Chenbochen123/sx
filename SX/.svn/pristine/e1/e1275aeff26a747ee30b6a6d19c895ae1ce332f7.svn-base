using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    public interface IPpmSemiStorageManager : IBaseManager<PpmSemiStorage>
    {
        PageResult<PpmSemiStorage> GetTablePageDataBySql(PpmSemiStorageManager.QueryParams queryParams);
        string SubmitRubberBack(string storageID, string storagePlaceID, string barcode, decimal backWeight, string normalFlag, string backReason, string shiftID, string operPerson);
        string CancelRubberBack(string barcode, string shiftID, string operPerson);
    }
}
