using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    public interface IPPTEquipChanliangReportManager : IBaseManager<PPTEquipChanliangReport>
    {
        DataSet GetCLHZReport(string totalType, string TotalMonth, string workShopCode, string equipCode, string zjsID);
        DataSet GetCLHZDetailReport(string TotalMonth, string workShopCode, string equipCode, string zjsID, string shiftID);
    }
}
