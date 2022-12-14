using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Implements;
    public interface IPpmInventoryService : IBaseService<PpmInventory>
    {
        PageResult<PpmInventory> GetTablePageDataBySql(PpmInventoryService.QueryParams queryParams);
        DataSet GetInverntoryEndDate(DateTime BeginDate, DateTime EndDate, string CheJian);
    }
}
