using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Data.Implements;
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    /// <summary>
    /// IPmtActionService 接口定义
    /// 孙本强 @ 2013-04-03 13:01:00
    /// </summary>
    /// <remarks></remarks>
    public interface IPmtActionService : IBaseService<PmtAction>
    {
        /// <summary>
        /// 获取分页数据集
        /// 孙本强 @ 2013-04-03 13:01:00
        /// </summary>
        /// <param name="queryParams">查询参数</param>
        /// <returns>分页数据集</returns>
        /// <remarks></remarks>
        PageResult<PmtAction> GetTablePageDataBySql(PmtActionService.QueryParams queryParams);
    }
}
