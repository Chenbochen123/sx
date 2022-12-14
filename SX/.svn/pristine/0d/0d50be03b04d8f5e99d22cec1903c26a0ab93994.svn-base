using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    public interface IBasUnitManager : IBaseManager<BasUnit>
    {
        /// <summary>
        /// 分页方法
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        PageResult<BasUnit> GetTablePageDataBySql(Mesnac.Data.Implements.BasUnitService.QueryParams queryParams);
        /// <summary>
        /// 获取下一个主键值
        /// </summary>
        /// <returns></returns>
        int GetUnitNextPrimaryKeyValue();
    }
}
