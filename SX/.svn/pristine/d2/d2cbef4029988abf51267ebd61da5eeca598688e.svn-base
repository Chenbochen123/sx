using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    public interface IBasUnitService : IBaseService<BasUnit>
    {
        //单位的分页方法
        PageResult<BasUnit> GetTablePageDataBySql(Mesnac.Data.Implements.BasUnitService.QueryParams queryParams);

        //获取单位的下一个主键值
        int GetUnitNextPrimaryKeyValue();
    }
}
