using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Implements;
    public interface IPpmRubberJZByShiftService : IBaseService<PpmRubberJZByShift>
    {
        PageResult<PpmRubberJZByShift> GetTablePageDataBySql(PpmRubberJZByShiftService.QueryParams queryParams);
        string DataAllowAddJZ(string PlanDate, string WorkShopCode, string ShiftID, string MaterCode, string IsAdd);
    }
}
