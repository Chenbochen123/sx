using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    using System.Data;
    public interface IPptPlanMgrManager : IBaseManager<PptPlanMgr>
    {
        /// <summary>
        /// ∑÷“≥∑Ω∑®
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        PageResult<PptPlanMgr> GetTablePageDataBySql(Mesnac.Data.Implements.PptPlanMgrService.QueryParams queryParams);
        PageResult<PptPlanMgr> GetTablePageAddPlanInfoBySql(Mesnac.Data.Implements.PptPlanMgrService.QueryParams queryParams);
        DataSet GetListAddPlanInfoByWhere(string planDate, string equipcode, string materCode);
    }
}
