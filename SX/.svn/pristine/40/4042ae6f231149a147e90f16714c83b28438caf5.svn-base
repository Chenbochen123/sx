using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Implements;
    using System.Data;
    public interface IPpmRubberAdjustService : IBaseService<PpmRubberAdjust>
    {
        PageResult<PpmRubberAdjust> GetTablePageDataBySql(PpmRubberAdjustService.QueryParams queryParams);
       //string GetBillNo();
        string SubmitRubberAdjust(string storageID, string storagePlaceID, string barcode, decimal realWeight, string operPerson, string shiftID, string shiftClassID, string toStorageID, string toStoragePlaceID);
        string CancelRubberAdjust(string storageID, string storagePlaceID, string barcode, decimal realWeight, string toStorageID, string toStoragePlaceID);
        DataSet GetRubberAdjustReportBySql(PpmRubberAdjustService.QueryParams queryParams);
        DataSet GetRubberAdjustDetailReportBySql(PpmRubberAdjustService.QueryParams queryParams);
    }
}
