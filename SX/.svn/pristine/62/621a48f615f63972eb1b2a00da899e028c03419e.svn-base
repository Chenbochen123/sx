using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Implements;
    public interface IEqmRepairProtectPlanService : IBaseService<EqmRepairProtectPlan>
    {
        PageResult<EqmRepairProtectPlan> GetTablePageDataBySql(Mesnac.Data.Implements.EqmRepairProtectPlanService.QueryParams queryParams);
        string GetNeedStopTimeCount(string equipCode, string planName, string planMonth);
        
    }
}
