using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Implements;
    using Mesnac.Data.Components;
    /// <summary>
    /// ISysLoginLogService 接口定义
    /// 孙本强 @ 2013-04-03 12:52:28
    /// </summary>
    /// <remarks></remarks>
    public interface ISysLoginLogService : IBaseService<SysLoginLog>
    {
        /// <summary>
        /// 获取分页数据集
        /// 孙本强 @ 2013-04-03 12:52:28
        /// </summary>
        /// <param name="queryParams">查询参数</param>
        /// <returns>分页数据集</returns>
        /// <remarks></remarks>
        PageResult<SysLoginLog> GetTablePageDataBySql(SysLoginLogService.QueryParams queryParams);
    }
}
