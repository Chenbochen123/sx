using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Data.Implements;
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    /// <summary>
    /// ISysPageMethodService 接口定义
    /// 孙本强 @ 2013-04-03 12:51:20
    /// </summary>
    /// <remarks></remarks>
    public interface ISysPageMethodService : IBaseService<SysPageMethod>
    {
        /// <summary>
        /// 获取分页数据集
        /// 孙本强 @ 2013-04-03 12:51:21
        /// </summary>
        /// <param name="queryParams">查询参数</param>
        /// <returns>分页数据集</returns>
        /// <remarks></remarks>
        PageResult<SysPageMethod> GetTablePageDataBySql(SysPageMethodService.QueryParams queryParams);
    }
}
