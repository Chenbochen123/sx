using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Implements;
    public interface IPptScanBarcodeLogService : IBaseService<PptScanBarcodeLog>
    {
        //设备信息分页方法
        PageResult<PptScanBarcodeLog> GetTablePageDataBySql(Mesnac.Data.Implements.PptScanBarcodeLogService.QueryParams queryParams);
    }
}
