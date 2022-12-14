using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    public interface IBasFactoryInfoService : IBaseService<BasFactoryInfo>
    {
        PageResult<BasFactoryInfo> GetTablePageDataBySql(Mesnac.Data.Implements.BasFactoryInfoService.QueryParams queryParams);

        //获取厂商下一个编号
        string GetNextFactoryCode();
    }
}
