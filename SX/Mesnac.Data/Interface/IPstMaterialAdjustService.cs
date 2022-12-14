using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Implements;
    public interface IPstMaterialAdjustService : IBaseService<PstMaterialAdjust>
    {
        PageResult<PstMaterialAdjust> GetTablePageDataBySql(PstMaterialAdjustService.QueryParams queryParams);
        PageResult<PstMaterialAdjust> GetTablePageDataBySqlPrint(PstMaterialAdjustService.QueryParams queryParams);
        string GetBillNo();
        DataSet GetDetailInfo(string billNo, string storageID, string storagePlaceID, string barcode, string orderID);
        bool IsSameStorageType(string sourceStorageID, string targetStorageID);
        DataSet GetSqlInfo(string sql);
    }
}
