using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    public interface IPstMaterialChkManager : IBaseManager<PstMaterialChk>
    {
        PageResult<PstMaterialChk> GetTablePageDataBySql(PstMaterialChkManager.QueryParams queryParams);
        string GetBillNo();
        bool UpdateSendChkFlag(string StrBillNo, string SendPerson);
        bool CancelSendChk(string StrBillNo, string SendPerson);
        bool UpdateStockInFlag(string BillNo);
        string GetFactoryID(string BillNo);
    }
}
