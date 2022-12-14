using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    public interface IPstMaterShopStoreManager : IBaseManager<PstMaterShopStore>
    {
        PageResult<PstMaterShopStore> GetTablePageDataBySql(PstMaterShopStoreManager.QueryParams queryParams);
        string UpdateAudit(string planDate);
        string UpdateCancelAudit(string planDate);
        string DataAllowAddJZ(string PlanDate, string WorkShopCode, string ShiftID, string MaterCode, string IsAdd);
    }
}
