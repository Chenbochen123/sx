using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Implements;
    public interface IPpmRubberStoreoutService : IBaseService<PpmRubberStoreout>
    {
        PageResult<PpmRubberStoreout> GetTablePageDataBySql(PpmRubberStoreoutService.QueryParams queryParams);
        string GetBillNo();
        bool UpdateLockedFlag(string StrBillNo, string ChkPerson);
        bool CancelLocked(string StrBillNo, string ChkPerson);
        DataSet GetStorageInfo(string BillNo);
    }
}
