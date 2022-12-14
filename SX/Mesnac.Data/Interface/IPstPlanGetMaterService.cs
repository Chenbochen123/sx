using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Implements;
    public interface IPstPlanGetMaterService : IBaseService<PstPlanGetMater>
    {
        PageResult<PstPlanGetMater> GetTablePageDataBySql(PstPlanGetMaterService.QueryParams queryParams);
        bool JudgeExistPlan(string PlanDate);
        DataSet GetPlanMaterInfo(string ObjID);
        void ReSetMater(string PlanDate);
    }
}
