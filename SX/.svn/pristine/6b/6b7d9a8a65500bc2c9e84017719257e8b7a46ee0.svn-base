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
    public class PPTEquipChanliangReportManager : BaseManager<PPTEquipChanliangReport>, IPPTEquipChanliangReportManager
    {
		#region 属性注入与构造方法
		
        private IPPTEquipChanliangReportService service;

        public PPTEquipChanliangReportManager()
        {
            this.service = new PPTEquipChanliangReportService();
            base.BaseService = this.service;
        }

		public PPTEquipChanliangReportManager(string connectStringKey)
        {
			this.service = new PPTEquipChanliangReportService(connectStringKey);
            base.BaseService = this.service;
        }

        public PPTEquipChanliangReportManager(NBear.Data.Gateway way)
        {
			this.service = new PPTEquipChanliangReportService(way);
            base.BaseService = this.service;
        }

        #endregion

        public DataSet GetCLHZReport(string totalType, string TotalMonth, string workShopCode, string equipCode, string zjsID)
        {
            return this.service.GetCLHZReport(totalType, TotalMonth, workShopCode, equipCode, zjsID);
        }

        public DataSet GetCLHZDetailReport(string TotalMonth, string workShopCode, string equipCode, string zjsID, string shiftID)
        {
            return this.service.GetCLHZDetailReport(TotalMonth, workShopCode, equipCode, zjsID, shiftID);
        }
    }
}
