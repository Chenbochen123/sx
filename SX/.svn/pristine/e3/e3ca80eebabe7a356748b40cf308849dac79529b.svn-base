using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    public interface IPpmInventoryManager : IBaseManager<PpmInventory>
    {
        PageResult<PpmInventory> GetTablePageDataBySql(PpmInventoryManager.QueryParams queryParams);
        DataSet GetInverntoryEndDate(DateTime BeginDate, DateTime EndDate, string CheJian);
    }
}
