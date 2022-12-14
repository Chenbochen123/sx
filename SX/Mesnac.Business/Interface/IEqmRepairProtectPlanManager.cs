using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    public interface IEqmRepairProtectPlanManager : IBaseManager<EqmRepairProtectPlan>
    {
        PageResult<EqmRepairProtectPlan> GetTablePageDataBySql(Mesnac.Data.Implements.EqmRepairProtectPlanService.QueryParams queryParams);
        string GetNeedStopTimeCount(string equipCode, string planName, string planMonth);
    }
}
