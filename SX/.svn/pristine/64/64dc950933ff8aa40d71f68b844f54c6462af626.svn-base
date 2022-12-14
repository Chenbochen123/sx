using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Implements;
    /// <summary>
    /// IPmtMixTypeService 接口定义
    /// 孙本强 @ 2013-04-03 12:58:36
    /// </summary>
    /// <remarks></remarks>
    public interface IPmtMixTypeService : IBaseService<PmtMixType>
    {
        /// <summary>
        /// 获取分页数据集
        /// 孙本强 @ 2013-04-03 12:58:37
        /// </summary>
        /// <param name="queryParams">查询参数</param>
        /// <returns>分页数据集</returns>
        /// <remarks></remarks>
        PageResult<PmtMixType> GetTablePageDataBySql(Mesnac.Data.Implements.PmtMixTypeService.QueryParams queryParams);
         
        /// <summary>
        /// 获取单位的下一个主键值
        /// 孙本强 @ 2013-04-03 12:58:37
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        int GetPmtMixTypeNextPrimaryKeyValue();
    }
}
