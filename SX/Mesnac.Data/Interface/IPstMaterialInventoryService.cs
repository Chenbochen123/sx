using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Implements;
    public interface IPstMaterialInventoryService : IBaseService<PstMaterialInventory>
    {
        PageResult<PstMaterialInventory> GetTablePageDataBySql(PstMaterialInventoryService.QueryParams queryParams);
        string GetBillNo();
        bool UpdateChkResultFlag(string StrBillNo, string UserID);
    }
}
