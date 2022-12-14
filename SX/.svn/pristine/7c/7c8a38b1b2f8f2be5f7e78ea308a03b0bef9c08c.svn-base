using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    public interface IPstMaterialStoreoutManager : IBaseManager<PstMaterialStoreout>
    {
        PageResult<PstMaterialStoreout> GetTablePageDataBySql(PstMaterialStoreoutManager.QueryParams queryParams);
        string GetBillNo();
        bool UpdateLockedFlag(string StrBillNo, string ChkPerson);
        bool CancelLocked(string StrBillNo, string ChkPerson);
        DataSet GetStorageInfo(string BillNo);
    }
}
