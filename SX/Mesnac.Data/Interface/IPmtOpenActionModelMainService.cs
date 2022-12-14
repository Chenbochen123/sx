using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Implements;
    public interface IPmtOpenActionModelMainService : IBaseService<PmtOpenActionModelMain>
    {
        /// <summary>
        /// 获取分页数据集
        /// 袁洋 @2014-9-28 11:09:57
        /// </summary>
        /// <param name="queryParams">查询参数</param>
        /// <returns>分页数据集</returns>
        /// <remarks></remarks>
        PageResult<PmtOpenActionModelMain> GetTablePageDataBySql(PmtOpenActionModelMainService.QueryParams queryParams);
    }
}
