using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    /// <summary>
    /// ISysLoginLogManager 接口定义
    /// 孙本强 @ 2013-04-03 11:18:05
    /// </summary>
    /// <remarks></remarks>
    public interface ISysLoginLogManager : IBaseManager<SysLoginLog>
    {
        /// <summary>
        /// 获取分页数据集
        /// 孙本强 @ 2013-04-03 11:18:05
        /// </summary>
        /// <param name="queryParams">查询参数</param>
        /// <returns>分页数据集</returns>
        /// <remarks></remarks>
        PageResult<SysLoginLog> GetTablePageDataBySql(SysLoginLogManager.QueryParams queryParams);
    }
}
