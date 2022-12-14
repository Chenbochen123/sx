using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    public interface IPstMaterialAdjustingDetailManager : IBaseManager<PstMaterialAdjustingDetail>
    {
        PageResult<PstMaterialAdjustingDetail> GetTablePageDataBySql(PstMaterialAdjustingDetailManager.QueryParams queryParams);
        DataSet GetByBillNo(string BillNo);
        DataSet GetByOtherBillNo(string BillNo, string Barcode, string OrderID);
    }
}
