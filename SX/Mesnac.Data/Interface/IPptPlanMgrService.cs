using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Implements;
    using System.Data;
    public interface IPptPlanMgrService : IBaseService<PptPlanMgr>
    {
        //��λ�ķ�ҳ����
        PageResult<PptPlanMgr> GetTablePageDataBySql(Mesnac.Data.Implements.PptPlanMgrService.QueryParams queryParams);
        PageResult<PptPlanMgr> GetTablePageAddPlanInfoBySql(Mesnac.Data.Implements.PptPlanMgrService.QueryParams queryParams);
        DataSet GetListAddPlanInfoByWhere(string planDate, string equipcode, string materCode);
    }
}
