using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    public interface IPstMaterialAdjustManager : IBaseManager<PstMaterialAdjust>
    {
        PageResult<PstMaterialAdjust> GetTablePageDataBySql(PstMaterialAdjustManager.QueryParams queryParams);
        PageResult<PstMaterialAdjust> GetTablePageDataBySqlPrint(PstMaterialAdjustManager.QueryParams queryParams);
        string GetBillNo();
        DataSet GetDetailInfo(string billNo, string storageID, string storagePlaceID, string barcode, string orderID);
        bool IsSameStorageType(string sourceStorageID, string targetStorageID);
        DataSet GetSqlInfo(string sql);
    }
}
