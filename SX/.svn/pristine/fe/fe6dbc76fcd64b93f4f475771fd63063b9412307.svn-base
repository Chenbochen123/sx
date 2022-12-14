using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Implements;
    public interface IPstMaterialStoreinService : IBaseService<PstMaterialStorein>
    {
        PageResult<PstMaterialStorein> GetTablePageDataBySql(PstMaterialStoreinService.QueryParams queryParams);
        PageResult<PstMaterialStorein> GetTablePageDataBySqlPrint(PstMaterialStoreinService.QueryParams queryParams);
        string GetBillNo();
        bool UpdateChkResultFlag(string StrBillNo, string UserID);
        bool CancelChkResult(string StrBillNo, string UserID);
        DataSet GetDetailInfo(string billNo, string storageID, string storagePlaceID, string barcode, string orderID);
        DataSet GetSqlInfo(string sql);
    }
}
