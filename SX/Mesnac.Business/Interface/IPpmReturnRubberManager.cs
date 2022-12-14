using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    public interface IPpmReturnRubberManager : IBaseManager<PpmReturnRubber>
    {
        PageResult<PpmReturnRubber> GetTablePageDataBySql(PpmReturnRubberManager.QueryParams queryParams);
        string SubmitReturnRubber(string storageID, string storagePlaceID, string barcode, decimal realWeight, string operPerson, string shiftID, string shiftClassID);
        string CancelReturnRubber(string storageID, string storagePlaceID, string barcode);
        DataSet GetDayReport(string PlanDate, string workShopCode);
        DataSet GetReturnRubberInfo(string barcode);
    }
}
