using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    public interface IPptBarBomDataManager : IBaseManager<PptBarBomData>
    {
        DataTable GetBarBomInfo(string barcode);
        DataTable GetBatchInfo(string barcode);
        DataTable GetUseNodeByCurrentBarcode(string currentBarcode);
    }
}
