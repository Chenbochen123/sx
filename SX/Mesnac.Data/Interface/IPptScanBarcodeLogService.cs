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
        //�豸��Ϣ��ҳ����
        PageResult<PptScanBarcodeLog> GetTablePageDataBySql(Mesnac.Data.Implements.PptScanBarcodeLogService.QueryParams queryParams);
    }
}
