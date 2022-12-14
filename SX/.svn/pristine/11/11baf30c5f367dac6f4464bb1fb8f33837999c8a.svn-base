using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    public interface IPpmEquipScanRateManager : IBaseManager<PpmEquipScanRate>
    {
        DataSet GetPpmScanCalcWorkShop(string startDate, string endDate, string workShopCode, string zjsID);
        DataSet GetPpmScanCalcEquipCode(string startDate, string endDate, string workShopCode);
        DataSet GetPpmScanCalcHrCode(string startDate, string endDate, string workShopCode);
        DataSet GetPpmScanCalcDetail(string startDate, string endDate, string zjsID);
    }
}
