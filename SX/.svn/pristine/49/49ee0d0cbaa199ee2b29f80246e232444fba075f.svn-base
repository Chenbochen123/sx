using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    public interface IPpmRubberChkDetailManager : IBaseManager<PpmRubberChkDetail>
    {
        PageResult<PpmRubberChkDetail> GetTablePageDataBySql(PpmRubberChkDetailManager.QueryParams queryParams);
        DataSet GetByBillNo(string BillNo, string IsStoreIn);
        DataSet GetByOtherBillNo(string BillNo, string Barcode, string OrderID);
        DataSet GetNoPassByBillNo(string BillNo);
        string GetBarcode(string BillNo);
        bool UpdateSendChkFlag(string StrBillNo, string SendPerson);
        PpmRubberChkDetail GetEntity(string BillNo, string Barcode, string OrderID);
    }
}
