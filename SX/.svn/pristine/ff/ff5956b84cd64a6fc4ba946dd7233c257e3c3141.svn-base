using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Implements;
    public interface IIncSapOrderReloadService : IBaseService<IncSapOrderReload>
    {
        //单位的分页方法
        PageResult<IncSapOrderReload> GetTablePageDataBySql(Mesnac.Data.Implements.IncSapOrderReloadService.QueryParams queryParams);
        bool IsExistData(string MesOrderCode, string MesOrderType);
    }
}
