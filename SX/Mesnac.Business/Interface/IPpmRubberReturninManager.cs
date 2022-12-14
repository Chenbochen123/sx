using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    public interface IPpmRubberReturninManager : IBaseManager<PpmRubberReturnin>
    {
        PageResult<PpmRubberReturnin> GetTablePageDataBySql(PpmRubberReturninManager.QueryParams queryParams);
        string GetBillNo();
        bool UpdateChkResultFlag(string StrBillNo, string UserID);
        bool CancelChkResult(string StrBillNo, string UserID);
    }
}
