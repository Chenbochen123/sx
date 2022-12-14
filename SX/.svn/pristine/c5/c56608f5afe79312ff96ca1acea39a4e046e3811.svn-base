using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Implements;
    public interface IPpmRubberStoreinDetailService : IBaseService<PpmRubberStoreinDetail>
    {
        PageResult<PpmRubberStoreinDetail> GetTablePageDataBySql(PpmRubberStoreinDetailService.QueryParams queryParams);
        DataSet GetByBillNo(string BillNo);
        DataSet GetByOtherBillNo(string BillNo, string Barcode, string OrderID);
        DataSet GetFromChkdetail(string BillNo);
        bool UpdateStorage(string BillNo, string StorageID, string StoragePlaceID);
        PpmRubberStoreinDetail GetStoreinDetail(string BillNo, string Barcode);
    }
}
