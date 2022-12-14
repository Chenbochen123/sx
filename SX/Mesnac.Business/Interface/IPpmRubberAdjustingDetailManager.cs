using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    public interface IPpmRubberAdjustingDetailManager : IBaseManager<PpmRubberAdjustingDetail>
    {
        PageResult<PpmRubberAdjustingDetail> GetTablePageDataBySql(PpmRubberAdjustingDetailManager.QueryParams queryParams);
        DataSet GetByBillNo(string BillNo);
        DataSet GetByOtherBillNo(string BillNo, string Barcode, string OrderID);
    }
}
