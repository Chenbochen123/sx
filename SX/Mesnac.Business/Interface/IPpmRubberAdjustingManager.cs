using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    public interface IPpmRubberAdjustingManager : IBaseManager<PpmRubberAdjusting>
    {
        PageResult<PpmRubberAdjusting> GetTablePageDataBySql(PpmRubberAdjustingManager.QueryParams queryParams);
        string GetBillNo();
        bool UpdateChkResultFlag(string StrBillNo, string ChkPerson);
    }
}
