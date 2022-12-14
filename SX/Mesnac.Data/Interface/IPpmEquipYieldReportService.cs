using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Implements;
    public interface IPpmEquipYieldReportService : IBaseService<PpmEquipYieldReport>
    {
        DataSet GetYieldTotalReport(string totalType, string TotalMonth, string workShopCode, string equipCode, string zjsID);
        DataSet GetYieldDetailReport(string TotalMonth, string workShopCode, string equipCode, string zjsID, string shiftID);
    }
}
