using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    /// <summary>
    /// ISysWebLogManager 接口定义
    /// 孙本强 @ 2013-04-03 11:46:45
    /// </summary>
    /// <remarks></remarks>
    public interface ISysWebLogManager : IBaseManager<SysWebLog>
    {
        /// <summary>
        /// 添加操作日志
        /// 孙本强 @ 2013-04-03 11:46:45
        /// </summary>
        /// <param name="sysWebLog">The sys web log.</param>
        /// <param name="sysPageMethod">The sys page method.</param>
        /// <remarks></remarks>
        void Append(SysWebLog sysWebLog, SysPageMethod sysPageMethod);

        /// <summary>
        /// 获取分页数据集
        /// 孙本强 @ 2013-04-03 11:46:45
        /// </summary>
        /// <param name="queryParams">查询参数</param>
        /// <returns>分页数据集</returns>
        /// <remarks></remarks>
        PageResult<SysWebLog> GetTablePageDataBySql(SysWebLogManager.QueryParams queryParams);
    }
}
