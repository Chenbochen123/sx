using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    /// <summary>
    /// IPmtTermManager 接口定义
    /// 孙本强 @ 2013-04-03 12:06:20
    /// </summary>
    /// <remarks></remarks>
    public interface IPmtTermManager : IBaseManager<PmtTerm>
    {
        /// <summary>
        /// 获取分页数据集
        /// 孙本强 @ 2013-04-03 12:06:20
        /// </summary>
        /// <param name="queryParams">查询参数</param>
        /// <returns>分页数据集</returns>
        /// <remarks></remarks>
        PageResult<PmtTerm> GetTablePageDataBySql(PmtTermManager.QueryParams queryParams);
    }
}
