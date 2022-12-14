using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    public interface IPptMonthlyEnteringService : IBaseService<PptMonthlyEntering>
    {
        PageResult<PptMonthlyEntering> GetPptMonthlyEnteringPageDataBySql(Mesnac.Data.Implements.PptMonthlyEnteringService.QueryParams queryParams);
    }
}
