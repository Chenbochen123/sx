using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Implements;
    public interface IPpmSemiStorageService : IBaseService<PpmSemiStorage>
    {
        PageResult<PpmSemiStorage> GetTablePageDataBySql(PpmSemiStorageService.QueryParams queryParams);
        string SubmitRubberBack(string storageID, string storagePlaceID, string barcode, decimal backWeight, string normalFlag, string backReason, string shiftID, string operPerson);
        string CancelRubberBack(string barcode, string shiftID, string operPerson);
    }
}
