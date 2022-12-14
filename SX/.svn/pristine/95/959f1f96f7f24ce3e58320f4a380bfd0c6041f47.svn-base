using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    public interface IPstMaterialAdjustingManager : IBaseManager<PstMaterialAdjusting>
    {
        PageResult<PstMaterialAdjusting> GetTablePageDataBySql(PstMaterialAdjustingManager.QueryParams queryParams);
        string GetBillNo();
        bool UpdateChkResultFlag(string StrBillNo, string ChkPerson);
    }
}
