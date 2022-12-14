using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    public interface IPmtMixTypeManager : IBaseManager<PmtMixType>
    { 
        /// <summary>
        /// 分页方法
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        PageResult<PmtMixType> GetTablePageDataBySql(Mesnac.Data.Implements.PmtMixTypeService.QueryParams queryParams);
        /// <summary>
        /// 获取下一个主键值
        /// </summary>
        /// <returns></returns>
        int GetPmtMixTypeNextPrimaryKeyValue();
    }
}
