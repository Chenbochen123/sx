using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Implements;
    using System.Data;
    public interface IPpmRubberStorageService : IBaseService<PpmRubberStorage>
    {
        PageResult<PpmRubberStorage> GetTablePageDataBySql(PpmRubberStorageService.QueryParams queryParams);
        PageResult<PpmRubberStorage> GetTablePageStoreoutBySql(PpmRubberStorageService.QueryParams queryParams);
        string SubmitRubberStoreOut(string storageID, string storagePlaceID, string barcode, string shiftID, string shiftClassID, string operPerson, string toStorageID, string toStoragePlaceID);
        string CancelReturnRubber(string storageID, string storagePlaceID, string barcode);
        PageResult<PpmRubberStorage> ProcPPMOutDateQuery(PpmRubberStorageService.QueryParams queryParams, string startDate, string endDate, string workShop, string storageID, string storagePlaceID, int limit, string barCode, string shlefBarCode, int page, int pagenum,string matercode);
        DataSet ProcPPMOutDateTotalQuery(string startDate, string endDate, string workShop, string storageID, string storagePlaceID, int limit, string barCode, string shlefBarCode);
        PageResult<PpmRubberStorage> ProcPPMOutDateQueryDeal(PpmRubberStorageService.QueryParams queryParams,string matercode, string startDate, string endDate, string workShop, string storageID, string storagePlaceID, string barCode, string shlefBarCode, int type, string orderway, int page, int pagenum);
        DataTable GetTableStoreOutReport(PpmRubberStorageService.QueryParams queryParams);
        DataTable GetTableStoreOutDetailReport(PpmRubberStorageService.QueryParams queryParams);
        DataTable GetTableStoreBackDetailReport(PpmRubberStorageService.QueryParams queryParams);
        DataTable GetTableStoreBackReport(PpmRubberStorageService.QueryParams queryParams);
    }
}
