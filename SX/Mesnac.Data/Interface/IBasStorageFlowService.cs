using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    public interface IBasStorageFlowService : IBaseService<BasStorageFlow>
    {
        PageResult<BasStorageFlow> GetTablePageDataBySql(Mesnac.Data.Implements.BasStorageFlowService.QueryParams queryParams);
    }
}
