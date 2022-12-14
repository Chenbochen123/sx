using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Implements;
    public interface IPstMaterShopStoreService : IBaseService<PstMaterShopStore>
    {
        PageResult<PstMaterShopStore> GetTablePageDataBySql(PstMaterShopStoreService.QueryParams queryParams);
        string UpdateAudit(string planDate);
        string UpdateCancelAudit(string planDate);
        string DataAllowAddJZ(string PlanDate, string WorkShopCode, string ShiftID, string MaterCode, string IsAdd);
    }
}
