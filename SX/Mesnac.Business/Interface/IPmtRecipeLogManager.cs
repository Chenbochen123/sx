using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    /// <summary>
    /// IPmtRecipeLogManager 接口定义
    /// 孙本强 @ 2013-04-03 12:44:22
    /// </summary>
    /// <remarks></remarks>
    public interface IPmtRecipeLogManager : IBaseManager<PmtRecipeLog>
    {
        /// <summary>
        /// 获取分页数据集
        /// 孙本强 @ 2013-04-03 12:44:22
        /// </summary>
        /// <param name="queryParams">查询参数</param>
        /// <returns>分页数据集</returns>
        /// <remarks></remarks>
        PageResult<PmtRecipeLog> GetTablePageDataBySql(PmtRecipeLogManager.QueryParams queryParams);
    }
}
