using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    public interface IIncSapOrderReloadManager : IBaseManager<IncSapOrderReload>
    {
        PageResult<IncSapOrderReload> GetTablePageDataBySql(Mesnac.Data.Implements.IncSapOrderReloadService.QueryParams queryParams);
        bool IsExistData(string MesOrderCode, string MesOrderType);
    }
}
