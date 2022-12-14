using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Implements;
    public interface IPpmRubberAdjustingService : IBaseService<PpmRubberAdjusting>
    {
        PageResult<PpmRubberAdjusting> GetTablePageDataBySql(PpmRubberAdjustingService.QueryParams queryParams);
        string GetBillNo();
        bool UpdateChkResultFlag(string StrBillNo, string ChkPerson);
    }
}
