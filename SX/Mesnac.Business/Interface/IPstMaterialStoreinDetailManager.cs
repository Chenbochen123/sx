using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    public interface IPstMaterialStoreinDetailManager : IBaseManager<PstMaterialStoreinDetail>
    {
        PageResult<PstMaterialStoreinDetail> GetTablePageDataBySql(PstMaterialStoreinDetailManager.QueryParams queryParams);
        DataSet GetByBillNo(string BillNo);
        DataSet GetByOtherBillNo(string BillNo, string Barcode, string OrderID);
        DataSet GetFromChkdetail(string BillNo);
        bool UpdateStorage(string BillNo, string StorageID, string StoragePlaceID);
        PstMaterialStoreinDetail GetStoreinDetail(string BillNo, string Barcode);
    }
}
