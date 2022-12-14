using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    public interface IBasFactoryTypeService : IBaseService<BasFactoryType>
    {
        //厂商类型分页方法
        PageResult<BasFactoryType> GetTablePageDataBySql(Mesnac.Data.Implements.BasFactoryTypeService.QueryParams queryParams);
        
        //获取厂商类型下一个编号
        string GetNextFactoryTypeCode();
    }
}
