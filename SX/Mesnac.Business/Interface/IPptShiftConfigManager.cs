using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    public interface IPptShiftConfigManager : IBaseManager<PptShiftConfig>
    {
        //架子信息分页方法
        PageResult<PptShiftConfig> GetTablePageDataBySql(Mesnac.Data.Implements.PptShiftConfigService.QueryParams queryParams);
        PageResult<PptShiftConfig> GetTablePageDataBySql2(Mesnac.Data.Implements.PptShiftConfigService.QueryParams queryParams);
        DataSet GetMaterInfoList(string PlanDate, string ShiftID, string EquipCode);
        DataSet GetInfoByBarcode(string barcode, string userCode);
    }
}
