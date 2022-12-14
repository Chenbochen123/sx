using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Components;
    public interface IEqmStopFaultManager : IBaseManager<EqmStopFault>
    {
        //获取故障点信息
        DataSet GetDataByParas( EqmStopFaultParams queryParams );

        //获取新的故障点代码
        string GetNextFaultCodeByParas(EqmStopFault eqmStopType);
        PageResult<EqmStopFault> GetEqmStopFaultBySearchKey(Mesnac.Data.Implements.EqmStopFaultService.QueryParams queryParams);
    }
}
