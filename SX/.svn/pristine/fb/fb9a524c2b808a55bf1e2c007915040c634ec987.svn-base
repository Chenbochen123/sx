using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    public interface IPpmRubberInventoryDetailManager : IBaseManager<PpmRubberInventoryDetail>
    {
        PageResult<PpmRubberInventoryDetail> GetTablePageDataBySql(PpmRubberInventoryDetailManager.QueryParams queryParams);
        bool GetByStorage(string billNo, string storageID, string inventoryDate);
        DataSet GetByBillNo(string BillNo);
    }
}
