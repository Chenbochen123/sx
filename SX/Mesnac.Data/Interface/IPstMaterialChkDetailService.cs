using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Implements;
    public interface IPstMaterialChkDetailService : IBaseService<PstMaterialChkDetail>
    {
        PageResult<PstMaterialChkDetail> GetTablePageDataBySql(PstMaterialChkDetailService.QueryParams queryParams);
        PageResult<PstMaterialChkDetail> GetCheckSequence(PstMaterialChkDetailService.QueryParams queryParams);
        DataSet GetByBillNo(string BillNo, string IsStoreIn);
        DataSet GetByOtherBillNo(string BillNo, string Barcode, string OrderID);
        DataSet GetNoPassByBillNo(string BillNo);
        string GetBarcode(string BillNo);
        bool UpdateSendChkFlag(string StrBillNo, string SendPerson);
        bool CancelSendChk(string StrBillNo, string SendPerson);
        PstMaterialChkDetail GetEntity(string BillNo, string Barcode, string OrderID);
        string GetLLBarcode(string Barcode);
        string GetChkResult(string Barcode);
        DataSet GetPstMaterialCheckDetailQueryInfoByParams(IPstMaterialCheckDetailQueryParams paras);
        DataSet GetAddLedgerCheckDetail(string BillNo, string Barcode, string OrderID);
    }

    public interface IPstMaterialCheckDetailQueryParams
    {
        string BillNo { get; set; }
        string NoticeNo { get; set; }
        string FactoryId { get; set; }
        string BeginSendChkDate { get; set; }
        string EndSendChkDate { get; set; }
        string SendChkFlag { get; set; }
        string StockInFlag { get; set; }
        string DetailSendChkFlag { get; set; }
        string ChkResultFlag { get; set; }
    }
}
