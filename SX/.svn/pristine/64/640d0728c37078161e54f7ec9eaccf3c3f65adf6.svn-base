using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    public interface IBasWorkManager : IBaseManager<BasWork>
    {
        PageResult<BasWork> GetTablePageDataBySql(Mesnac.Data.Implements.BasWorkService.QueryParams queryParams);
        string GetNextWorkPositionCode();
    }
}
