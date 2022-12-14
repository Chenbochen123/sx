using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Components;
    public interface IEqmFaultReasonManager : IBaseManager<EqmFaultReason>
    {
        //获取停机故障原因信息
        DataSet GetDataByParas(EqmFaultReasonParams queryParams);
        PageResult<EqmFaultReason> GetEqmFaultReasonBySearchKey(Mesnac.Data.Implements.EqmFaultReasonService.QueryParams queryParams);
    }
}
