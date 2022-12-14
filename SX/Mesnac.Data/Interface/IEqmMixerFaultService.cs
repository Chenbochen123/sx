using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Implements;
using System.Data;
    public interface IEqmMixerFaultService : IBaseService<EqmMixerFault>
    {
        //密炼机故障的分页方法
        PageResult<EqmMixerFault> GetTablePageDataBySql(Mesnac.Data.Implements.EqmMixerFaultService.QueryParams queryParams);
        // 根据属性字段对密炼机故障进行分组的统计方法
        DataSet GetChartGroupAnalysis(string columnName, DateTime faultBeginDate, DateTime faultEndDate, int count);
    }
}
