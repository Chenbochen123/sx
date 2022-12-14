using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    using System.Data;
    public interface IPpmRubberStorageManager : IBaseManager<PpmRubberStorage>
    {
        PageResult<PpmRubberStorage> GetTablePageDataBySql(PpmRubberStorageManager.QueryParams queryParams);
        PageResult<PpmRubberStorage> GetTablePageStoreoutBySql(PpmRubberStorageManager.QueryParams queryParams);
        string SubmitRubberStoreOut(string storageID, string storagePlaceID, string barcode, string shiftID, string shiftClassID, string operPerson, string toStorageID, string toStoragePlaceID);
        string CancelReturnRubber(string storageID, string storagePlaceID, string barcode);
        PageResult<PpmRubberStorage> ProcPPMOutDateQuery(PpmRubberStorageManager.QueryParams queryParams, string startDate, string endDate, string workShop, string storageID, string storagePlaceID, int limit, string barCode, string shlefBarCode, int page, int pagenum,string matercode);
        DataSet ProcPPMOutDateTotalQuery(string startDate, string endDate, string workShop, string storageID, string storagePlaceID, int limit, string barCode, string shlefBarCode);
        PageResult<PpmRubberStorage> ProcPPMOutDateQueryDeal(PpmRubberStorageManager.QueryParams queryParams,string matercode, string startDate, string endDate, string workShop, string storageID, string storagePlaceID, string barCode, string shlefBarCode, int type, string orderway, int page, int pagenum);
        DataTable GetTableStoreOutReport(PpmRubberStorageManager.QueryParams queryParams);
        DataTable GetTableStoreOutDetailReport(PpmRubberStorageManager.QueryParams queryParams);
        DataTable GetTableStoreBackDetailReport(PpmRubberStorageManager.QueryParams queryParams);
        DataTable GetTableStoreBackReport(PpmRubberStorageManager.QueryParams queryParams);
    }
}
