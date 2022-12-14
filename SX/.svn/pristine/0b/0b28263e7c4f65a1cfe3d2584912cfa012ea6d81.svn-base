using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Implements;
    public interface IPpmRubberChkDetailService : IBaseService<PpmRubberChkDetail>
    {
        PageResult<PpmRubberChkDetail> GetTablePageDataBySql(PpmRubberChkDetailService.QueryParams queryParams);
        DataSet GetByBillNo(string BillNo, string IsStoreIn);
        DataSet GetByOtherBillNo(string BillNo, string Barcode, string OrderID);
        DataSet GetNoPassByBillNo(string BillNo);
        string GetBarcode(string BillNo);
        bool UpdateSendChkFlag(string StrBillNo, string SendPerson);
        bool CancelSendChk(string StrBillNo, string SendPerson);
        PpmRubberChkDetail GetEntity(string BillNo, string Barcode, string OrderID);
    }
}
