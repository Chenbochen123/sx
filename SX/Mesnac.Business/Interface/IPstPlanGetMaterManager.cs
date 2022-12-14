using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    public interface IPstPlanGetMaterManager : IBaseManager<PstPlanGetMater>
    {
        PageResult<PstPlanGetMater> GetTablePageDataBySql(PstPlanGetMaterManager.QueryParams queryParams);
        bool JudgeExistPlan(string PlanDate);
        DataSet GetPlanMaterInfo(string ObjID);
        void ReSetMater(string PlanDate);
    }
}
