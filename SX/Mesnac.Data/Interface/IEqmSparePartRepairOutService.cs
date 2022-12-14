using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Implements;
    public interface IEqmSparePartRepairOutService : IBaseService<EqmSparePartRepairOut>
    {
        //Sap备件的分页方法
        PageResult<EqmSparePartRepairOut> GetTablePageDataBySql(Mesnac.Data.Implements.EqmSparePartRepairOutService.QueryParams queryParams);
        string GetNextSparePartStoreOutCode(DateTime storeOutDate);
    }
}
