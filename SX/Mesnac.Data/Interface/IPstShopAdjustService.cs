using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Implements;
    public interface IPstShopAdjustService : IBaseService<PstShopAdjust>
    {
        PageResult<PstShopAdjust> GetTablePageDataBySql(PstShopAdjustService.QueryParams queryParams);
        PageResult<PstShopAdjust> GetTablePageTotalBySql(PstShopAdjustService.QueryParams queryParams);
        string CancelShopAdjust(string storageID, string storagePlaceID, string barcode, int orderID, string operPerson);
        DataSet GetByInfo(string workShopCode, DateTime beginTime, DateTime endTime, string storageID, string storagePlaceID, string materCode, string adjustType);
        DataSet GetReportBySql(PstShopAdjustService.QueryParams queryParams);
    }
}
