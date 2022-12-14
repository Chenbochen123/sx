using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Implements;
    public interface IPstmmshopoutService : IBaseService<Pstmmshopout>
    {
        PageResult<Pstmmshopout> GetTablePageDataBySql(PstmmshopoutService.QueryParams queryParams);
        DataSet GetShopConsumeTotal(string beginDate, string endDate, string materType, string classid, string equipcode);
    }
}
