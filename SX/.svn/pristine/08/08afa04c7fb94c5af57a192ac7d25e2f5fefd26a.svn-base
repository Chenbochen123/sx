using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    public interface IPstMaterialInventoryDetailManager : IBaseManager<PstMaterialInventoryDetail>
    {
        PageResult<PstMaterialInventoryDetail> GetTablePageDataBySql(PstMaterialInventoryDetailManager.QueryParams queryParams);
        int GetByStorage(string billNo, string storageID, string inventoryDate);
        DataSet GetByBillNo(string BillNo);
    }
}
