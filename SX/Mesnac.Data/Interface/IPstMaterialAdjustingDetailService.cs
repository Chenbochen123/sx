using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Implements;
    public interface IPstMaterialAdjustingDetailService : IBaseService<PstMaterialAdjustingDetail>
    {
        PageResult<PstMaterialAdjustingDetail> GetTablePageDataBySql(PstMaterialAdjustingDetailService.QueryParams queryParams);
        DataSet GetByBillNo(string BillNo);
        DataSet GetByOtherBillNo(string BillNo, string Barcode, string OrderID);
    }
}
