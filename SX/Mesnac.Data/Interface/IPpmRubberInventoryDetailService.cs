using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Implements;
    public interface IPpmRubberInventoryDetailService : IBaseService<PpmRubberInventoryDetail>
    {
        PageResult<PpmRubberInventoryDetail> GetTablePageDataBySql(PpmRubberInventoryDetailService.QueryParams queryParams);
        bool GetByStorage(string billNo, string storageID, string inventoryDate);
        DataSet GetByBillNo(string BillNo);
    }
}
