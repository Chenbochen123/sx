using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    public interface IPmtEquipJarStoreLogManager : IBaseManager<PmtEquipJarStoreLog>
    { 
        /// <summary>
        /// 获取分页数据集
        /// yuany @ 2014-04-02 10:28:38
        /// </summary>
        /// <param name="queryParams">查询参数</param>
        /// <returns>分页数据集</returns>
        /// <remarks></remarks>
        PageResult<PmtEquipJarStoreLog> GetTablePageDataBySql(PmtEquipJarStoreLogManager.QueryParams queryParams);
    }
}
