using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Implements;
    public interface IPstMaterialStoreoutService : IBaseService<PstMaterialStoreout>
    {
        PageResult<PstMaterialStoreout> GetTablePageDataBySql(PstMaterialStoreoutService.QueryParams queryParams);
        string GetBillNo();
        bool UpdateLockedFlag(string StrBillNo, string ChkPerson);
        bool CancelLocked(string StrBillNo, string ChkPerson);
        DataSet GetStorageInfo(string BillNo);
    }
}
