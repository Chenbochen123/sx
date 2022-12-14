using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    public interface IBasRubInfoManager : IBaseManager<BasRubInfo>
    {
        PageResult<BasRubInfo> GetTablePageDataBySql(Mesnac.Data.Implements.BasRubInfoService.QueryParams queryParams);
        string GetNextRubInfoCode();
    }
}
