using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Implements;
    public interface IPpmRubberChkService : IBaseService<PpmRubberChk>
    {
        PageResult<PpmRubberChk> GetTablePageDataBySql(PpmRubberChkService.QueryParams queryParams);
        string GetBillNo();
        bool UpdateSendChkFlag(string StrBillNo);
        bool CancelSendChk(string StrBillNo);
        bool UpdateStockInFlag(string BillNo);
    }
}
