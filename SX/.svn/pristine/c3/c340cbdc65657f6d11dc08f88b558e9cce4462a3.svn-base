using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    public interface IEqmProjectRepairRecordManager : IBaseManager<EqmProjectRepairRecord>
    { 
        /// <summary>
        /// 分页方法
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        PageResult<EqmProjectRepairRecord> GetTablePageDataBySql(Mesnac.Data.Implements.EqmProjectRepairRecordService.QueryParams queryParams);
        /// <summary>
        /// 获取下一个主键值
        /// </summary>
        /// <returns></returns>
        int GetNextPrimaryKeyValue();

        string GetNextMainDailyID(string daily);
    }
}
