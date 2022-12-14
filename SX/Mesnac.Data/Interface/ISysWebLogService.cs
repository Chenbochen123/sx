using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{    
    using Mesnac.Data.Implements;
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    /// <summary>
    /// ISysWebLogService 接口定义
    /// 孙本强 @ 2013-04-03 12:48:24
    /// </summary>
    /// <remarks></remarks>
    public interface ISysWebLogService : IBaseService<SysWebLog>
    {
        /// <summary>
        /// 获取分页数据集
        /// 孙本强 @ 2013-04-03 12:48:24
        /// </summary>
        /// <param name="queryParams">查询参数</param>
        /// <returns>分页数据集</returns>
        /// <remarks></remarks>
        PageResult<SysWebLog> GetTablePageDataBySql(SysWebLogService.QueryParams queryParams);
    }
}

