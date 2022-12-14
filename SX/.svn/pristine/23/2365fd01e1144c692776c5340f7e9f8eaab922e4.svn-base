using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    public interface IPpmRubberInventoryManager : IBaseManager<PpmRubberInventory>
    {
        PageResult<PpmRubberInventory> GetTablePageDataBySql(PpmRubberInventoryManager.QueryParams queryParams);
        string GetBillNo();
        bool UpdateChkResultFlag(string StrBillNo, string UserID);
    }
}
