using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    using Mesnac.Data.Components;
    public class PptShiftConfigManager : BaseManager<PptShiftConfig>, IPptShiftConfigManager
    {
		#region 属性注入与构造方法
		
        private IPptShiftConfigService service;

        public PptShiftConfigManager()
        {
            this.service = new PptShiftConfigService();
            base.BaseService = this.service;
        }

		public PptShiftConfigManager(string connectStringKey)
        {
			this.service = new PptShiftConfigService(connectStringKey);
            base.BaseService = this.service;
        }

        public PptShiftConfigManager(NBear.Data.Gateway way)
        {
			this.service = new PptShiftConfigService(way);
            base.BaseService = this.service;
        }

        #endregion
        #region 查询条件类定义
        public class QueryParams : PptShiftConfigService.QueryParams
        {
        }
        #endregion

        public PageResult<PptShiftConfig> GetTablePageDataBySql(Mesnac.Data.Implements.PptShiftConfigService.QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }
        public PageResult<PptShiftConfig> GetTablePageDataBySql2(Mesnac.Data.Implements.PptShiftConfigService.QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql2(queryParams);
        }
        public DataSet GetMaterInfoList(string PlanDate, string ShiftID, string EquipCode)
        {
            return this.service.GetMaterInfoList(PlanDate, ShiftID, EquipCode);
        }


        public DataSet GetInfoByBarcode(string barcode, string userCode)
        {
            return this.service.GetInfoByBarcode(barcode, userCode);
        }
    }
}
