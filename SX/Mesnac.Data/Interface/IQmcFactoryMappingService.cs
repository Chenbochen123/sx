using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Data.Components;
    using Mesnac.Entity;
    public interface IQmcFactoryMappingService : IBaseService<QmcFactoryMapping>
    {
        PageResult<QmcFactoryMapping> GetTablePageDataBySql(Mesnac.Data.Implements.QmcFactoryMappingService.QueryParams queryParams);
        //获取下一个关系编号
        string GetNextMappingId();
    }
}
