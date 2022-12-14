using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    public interface IPstMaterialInventoryManager : IBaseManager<PstMaterialInventory>
    {
        PageResult<PstMaterialInventory> GetTablePageDataBySql(PstMaterialInventoryManager.QueryParams queryParams);
        string GetBillNo();
        bool UpdateChkResultFlag(string StrBillNo, string UserID);
    }
}
