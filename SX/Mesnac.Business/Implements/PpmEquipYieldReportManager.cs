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
    public class PpmEquipYieldReportManager : BaseManager<PpmEquipYieldReport>, IPpmEquipYieldReportManager
    {
		#region 属性注入与构造方法
		
        private IPpmEquipYieldReportService service;

        public PpmEquipYieldReportManager()
        {
            this.service = new PpmEquipYieldReportService();
            base.BaseService = this.service;
        }

		public PpmEquipYieldReportManager(string connectStringKey)
        {
			this.service = new PpmEquipYieldReportService(connectStringKey);
            base.BaseService = this.service;
        }

        public PpmEquipYieldReportManager(NBear.Data.Gateway way)
        {
			this.service = new PpmEquipYieldReportService(way);
            base.BaseService = this.service;
        }

        #endregion

        public DataSet GetYieldTotalReport(string totalType, string TotalMonth, string workShopCode, string equipCode, string zjsID)
        {
            return this.service.GetYieldTotalReport(totalType, TotalMonth, workShopCode, equipCode, zjsID);
        }

        public DataSet GetYieldDetailReport(string TotalMonth, string workShopCode, string equipCode, string zjsID, string shiftID)
        {
            return this.service.GetYieldDetailReport(TotalMonth, workShopCode, equipCode, zjsID, shiftID);
        }
    }
}
