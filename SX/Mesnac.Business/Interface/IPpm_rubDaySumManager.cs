using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    using System.Data;
    public interface IPpm_rubDaySumManager : IBaseManager<Ppm_rubDaySum>
    {
        DataTable GetTableStoreDaySum(Ppm_rubDaySumManager.QueryParams queryParams);
    }
}
