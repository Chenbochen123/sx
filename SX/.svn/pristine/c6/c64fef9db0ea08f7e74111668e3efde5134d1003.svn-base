using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    public interface IBasFactoryInfoManager : IBaseManager<BasFactoryInfo>
    {
        PageResult<BasFactoryInfo> GetTablePageDataBySql(Mesnac.Data.Implements.BasFactoryInfoService.QueryParams queryParams);
        string GetNextFactoryCode();
    }
}
