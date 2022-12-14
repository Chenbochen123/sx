using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    public interface IEqmSparePartRepairOutManager : IBaseManager<EqmSparePartRepairOut>
    { 
        /// <summary>
        /// ∑÷“≥∑Ω∑®
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        PageResult<EqmSparePartRepairOut> GetTablePageDataBySql(Mesnac.Data.Implements.EqmSparePartRepairOutService.QueryParams queryParams);
        string GetNextSparePartStoreOutCode(DateTime storeOutDate);
    }
}
