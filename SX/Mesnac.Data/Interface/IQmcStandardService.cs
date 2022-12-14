using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using System.Data;
    public interface IQmcStandardService : IBaseService<QmcStandard>
    {
        PageResult<QmcStandard> GetTablePageDataBySql(Mesnac.Data.Implements.QmcStandardService.QueryParams queryParams);
        DataSet GetStandardList();
        string GetNextStandardId();
    }
}
