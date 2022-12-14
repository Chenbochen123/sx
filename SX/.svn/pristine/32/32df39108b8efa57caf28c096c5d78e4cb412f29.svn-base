using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using System.Data;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    public interface IPpmRubberStorageDealManager : IBaseManager<PpmRubberStorageDeal>
    {
        DataTable SubmitOutDateDeal(string BarCode, string StorageID, string StoragePlaceID, string DealWay, string DealDate, string DealRemark, string DealPerson);
        DataTable GetDateQueryByCode(string BarCode, string StorageID, string StoragePlaceID);
        PageResult<PpmRubberStorageDeal> ProcPPMOutDateQueryInvalid(PpmRubberStorageDealManager.QueryParams queryParams, string workShop, string storageID, string storagePlaceID, string barCode, string shlefBarCode, int type,string dealperson);
        string SubmitRubberInValid(int dealid, string OperPerson);
        string SubmitOutDateRubberInValid(int dealid, string OperPerson, string dealdate, string dealremark);
        string SubmitRubberOutDateInValid(int dealid, string OperPerson, string dealway, string dealdate, string dealremark);
        PageResult<PpmRubberStorageDeal> ProcPPMValidDateQuery(PpmRubberStorageDealManager.QueryParams queryParams, string workShop, string storageID, string storagePlaceID, string barCode, string shlefBarCode, int type, string dealperson);

    }
}
