using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    public interface IPptScanBarcodeLogManager : IBaseManager<PptScanBarcodeLog>
    {
        PageResult<PptScanBarcodeLog> GetTablePageDataBySql(Mesnac.Data.Implements.PptScanBarcodeLogService.QueryParams queryParams);
    }
}
