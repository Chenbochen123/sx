using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    using System.Data;
    public interface IQmcStandardManager : IBaseManager<QmcStandard>
    {
        PageResult<QmcStandard> GetTablePageDataBySql(QmcStandardManager.QueryParams queryParams);
        DataSet GetStandardList();
        string GetNextStandardId();
    }
}
