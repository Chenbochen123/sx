using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    public interface IPpmRubberStoreoutManager : IBaseManager<PpmRubberStoreout>
    {
        PageResult<PpmRubberStoreout> GetTablePageDataBySql(PpmRubberStoreoutManager.QueryParams queryParams);
        string GetBillNo();
        bool UpdateLockedFlag(string StrBillNo, string ChkPerson);
        bool CancelLocked(string StrBillNo, string ChkPerson);
        DataSet GetStorageInfo(string BillNo);
    }
}
