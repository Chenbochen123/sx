using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    /// <summary>
    /// ISysPageMethodManager 接口定义
    /// 孙本强 @ 2013-04-03 11:46:24
    /// </summary>
    /// <remarks></remarks>
    public interface ISysPageMethodManager : IBaseManager<SysPageMethod>
    {
        /// <summary>
        /// Appends the specified sys page method.
        /// 孙本强 @ 2013-04-03 11:46:24
        /// </summary>
        /// <param name="sysPageMethod">The sys page method.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        int Append(SysPageMethod sysPageMethod);

        /// <summary>
        /// 获取分页数据集
        /// 孙本强 @ 2013-04-03 11:46:24
        /// </summary>
        /// <param name="queryParams">查询参数</param>
        /// <returns>分页数据集</returns>
        /// <remarks></remarks>
        PageResult<SysPageMethod> GetTablePageDataBySql(SysPageMethodManager.QueryParams queryParams);
    }
}
