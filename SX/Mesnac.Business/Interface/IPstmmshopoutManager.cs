using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    public interface IPstmmshopoutManager : IBaseManager<Pstmmshopout>
    {
        PageResult<Pstmmshopout> GetTablePageDataBySql(PstmmshopoutManager.QueryParams queryParams);
        DataSet GetShopConsumeTotal(string beginDate, string endDate, string materType, string classid, string equipcode);
    }
}
