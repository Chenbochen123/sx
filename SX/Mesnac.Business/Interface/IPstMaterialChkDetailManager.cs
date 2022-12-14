using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    using Mesnac.Data.Interface;

    public interface IPstMaterialChkDetailManager : IBaseManager<PstMaterialChkDetail>
    {
        PageResult<PstMaterialChkDetail> GetTablePageDataBySql(PstMaterialChkDetailManager.QueryParams queryParams);
        PageResult<PstMaterialChkDetail> GetCheckSequence(PstMaterialChkDetailManager.QueryParams queryParams);
        DataSet GetByBillNo(string BillNo, string IsStoreIn);
        DataSet GetByOtherBillNo(string BillNo, string Barcode, string OrderID);
        DataSet GetNoPassByBillNo(string BillNo);
        string GetBarcode(string BillNo);
        bool UpdateSendChkFlag(string StrBillNo, string SendPerson);
        PstMaterialChkDetail GetEntity(string BillNo, string Barcode, string OrderID);
        string GetLLBarcode(string Barcode);
        string GetChkResult(string Barcode);
        DataSet GetPstMaterialCheckDetailQueryInfoByParams(IPstMaterialCheckDetailQueryParams paras);
        DataSet GetAddLedgerCheckDetail(string BillNo, string Barcode, string OrderID);
    }
}
