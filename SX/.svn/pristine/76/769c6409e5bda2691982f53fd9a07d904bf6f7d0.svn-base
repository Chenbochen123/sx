using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    public interface IPstMaterialStoreinManager : IBaseManager<PstMaterialStorein>
    {
        PageResult<PstMaterialStorein> GetTablePageDataBySql(PstMaterialStoreinManager.QueryParams queryParams);
        PageResult<PstMaterialStorein> GetTablePageDataBySqlPrint(PstMaterialStoreinManager.QueryParams queryParams);
        string GetBillNo();
        bool UpdateChkResultFlag(string StrBillNo, string UserID);
        bool CancelChkResult(string StrBillNo, string UserID);
        DataSet GetDetailInfo(string billNo, string storageID, string storagePlaceID, string barcode, string orderID);
        DataSet GetSqlInfo(string sql);
    }
}
