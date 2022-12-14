using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    using NBear.Common;
    public class PpmEquipScanRateManager : BaseManager<PpmEquipScanRate>, IPpmEquipScanRateManager
    {
		#region 属性注入与构造方法
		
        private IPpmEquipScanRateService service;

        public PpmEquipScanRateManager()
        {
            this.service = new PpmEquipScanRateService();
            base.BaseService = this.service;
        }

		public PpmEquipScanRateManager(string connectStringKey)
        {
			this.service = new PpmEquipScanRateService(connectStringKey);
            base.BaseService = this.service;
        }

        public PpmEquipScanRateManager(NBear.Data.Gateway way)
        {
			this.service = new PpmEquipScanRateService(way);
            base.BaseService = this.service;
        }

        #endregion

        public DataSet GetPpmScanCalcWorkShop(string startDate, string endDate, string workShopCode, string zjsID)
        {
            return this.service.GetPpmScanCalcWorkShop(startDate, endDate, workShopCode, zjsID);
        }

        public DataSet GetPpmScanCalcEquipCode(string startDate, string endDate, string workShopCode)
        {
            return this.service.GetPpmScanCalcEquipCode(startDate, endDate, workShopCode);
        }

        public DataSet GetPpmScanCalcHrCode(string startDate, string endDate, string workShopCode)
        {
            return this.service.GetPpmScanCalcHrCode(startDate, endDate, workShopCode);
        }

        public DataSet GetPpmScanCalcDetail(string startDate, string endDate, string zjsID)
        {
            return this.service.GetPpmScanCalcDetail(startDate, endDate, zjsID);
        }
    }
}
