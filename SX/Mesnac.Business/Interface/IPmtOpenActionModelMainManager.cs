using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    public interface IPmtOpenActionModelMainManager : IBaseManager<PmtOpenActionModelMain>
    {
        /// <summary>
        /// 获取分页数据集
        /// 袁洋 @2014年9月29日11:04:06
        /// </summary>
        /// <param name="queryParams">查询参数</param>
        /// <returns>分页数据集</returns>
        /// <remarks></remarks>
        PageResult<PmtOpenActionModelMain> GetTablePageDataBySql(PmtOpenActionModelMainManager.QueryParams queryParams);
    }
}
