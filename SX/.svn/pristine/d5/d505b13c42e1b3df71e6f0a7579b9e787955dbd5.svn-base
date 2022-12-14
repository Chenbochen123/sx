using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    using System.Data;
    public interface IPpmRubberAdjustManager : IBaseManager<PpmRubberAdjust>
    {
        PageResult<PpmRubberAdjust> GetTablePageDataBySql(PpmRubberAdjustManager.QueryParams queryParams);
        //string GetBillNo();
        string SubmitRubberAdjust(string storageID, string storagePlaceID, string barcode, decimal realWeight, string operPerson, string shiftID, string shiftClassID, string toStorageID, string toStoragePlaceID);
        string CancelRubberAdjust(string storageID, string storagePlaceID, string barcode, decimal realWeight, string toStorageID, string toStoragePlaceID);
        DataSet GetRubberAdjustReportBySql(PpmRubberAdjustManager.QueryParams queryParams);
        DataSet GetRubberAdjustDetailReportBySql(PpmRubberAdjustManager.QueryParams queryParams);
    }
}
