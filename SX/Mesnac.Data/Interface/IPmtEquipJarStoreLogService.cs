using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Implements;
    public interface IPmtEquipJarStoreLogService : IBaseService<PmtEquipJarStoreLog>
    {
        /// <summary>
        /// 获取分页数据集
        /// 孙本强 @ 2013-04-03 12:58:57
        /// </summary>
        /// <param name="queryParams">查询参数</param>
        /// <returns>分页数据集</returns>
        /// <remarks></remarks>
        PageResult<PmtEquipJarStoreLog> GetTablePageDataBySql(PmtEquipJarStoreLogService.QueryParams queryParams);

    }
}
