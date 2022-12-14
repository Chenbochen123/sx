using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using NBear.Common;
    using System.Data;
    public interface IBasWorkShopService : IBaseService<BasWorkShop>
    {
        PageResult<BasWorkShop> GetTablePageDataBySql(Mesnac.Data.Implements.BasWorkShopService.QueryParams queryParams);
        string GetNextWorkShopCode();
        EntityArrayList<BasWorkShop> getAllMiLanWorkShop();
        DataSet getAllMiLanWorkShopNode();
    }
}
