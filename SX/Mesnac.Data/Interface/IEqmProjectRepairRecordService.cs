using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Implements;
    public interface IEqmProjectRepairRecordService : IBaseService<EqmProjectRepairRecord>
    { 
        //分页方法
        PageResult<EqmProjectRepairRecord> GetTablePageDataBySql(Mesnac.Data.Implements.EqmProjectRepairRecordService.QueryParams queryParams);

        //获取下一个主键值
        int GetNextPrimaryKeyValue();
        string GetNextMainDailyID(string daily);
    }
}
