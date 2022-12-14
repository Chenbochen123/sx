using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Implements;
    public interface IPpmRubberReturninService : IBaseService<PpmRubberReturnin>
    {
        PageResult<PpmRubberReturnin> GetTablePageDataBySql(PpmRubberReturninService.QueryParams queryParams);
        string GetBillNo();
        bool UpdateChkResultFlag(string StrBillNo, string UserID);
        bool CancelChkResult(string StrBillNo, string UserID);
    }
}
