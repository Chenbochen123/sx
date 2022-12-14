using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    public interface IPpmRubberJZByShiftManager : IBaseManager<PpmRubberJZByShift>
    {
        PageResult<PpmRubberJZByShift> GetTablePageDataBySql(PpmRubberJZByShiftManager.QueryParams queryParams);
        string DataAllowAddJZ(string PlanDate, string WorkShopCode, string ShiftID, string MaterCode, string IsAdd);
    }
}
