using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Implements;
    public interface IPstMaterialInventoryDetailService : IBaseService<PstMaterialInventoryDetail>
    {
        PageResult<PstMaterialInventoryDetail> GetTablePageDataBySql(PstMaterialInventoryDetailService.QueryParams queryParams);
        int GetByStorage(string billNo, string storageID, string inventoryDate);
        DataSet GetByBillNo(string BillNo);
    }
}
