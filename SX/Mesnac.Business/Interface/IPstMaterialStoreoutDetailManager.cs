using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    public interface IPstMaterialStoreoutDetailManager : IBaseManager<PstMaterialStoreoutDetail>
    {
        PageResult<PstMaterialStoreoutDetail> GetTablePageDataBySql(PstMaterialStoreoutDetailManager.QueryParams queryParams);
        int GetOrderID(string Barcode);
        DataSet GetByBillNo(string BillNo);
        DataSet GetByOtherBillNo(string BillNo, string Barcode, string OrderID);
    }
}
