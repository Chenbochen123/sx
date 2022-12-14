using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    using NBear.Common;
    public class PstMaterShopStoreManager : BaseManager<PstMaterShopStore>, IPstMaterShopStoreManager
    {
		#region 属性注入与构造方法
		
        private IPstMaterShopStoreService service;

        public PstMaterShopStoreManager()
        {
            this.service = new PstMaterShopStoreService();
            base.BaseService = this.service;
        }

		public PstMaterShopStoreManager(string connectStringKey)
        {
			this.service = new PstMaterShopStoreService(connectStringKey);
            base.BaseService = this.service;
        }

        public PstMaterShopStoreManager(NBear.Data.Gateway way)
        {
			this.service = new PstMaterShopStoreService(way);
            base.BaseService = this.service;
        }

        #endregion

        public class QueryParams : PstMaterShopStoreService.QueryParams
        {
        }

        public PageResult<PstMaterShopStore> GetTablePageDataBySql(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }

        public string UpdateAudit(string planDate)
        {
            return this.service.UpdateAudit(planDate);
        }

        public string UpdateCancelAudit(string planDate)
        {
            return this.service.UpdateCancelAudit(planDate);
        }

        public string DataAllowAddJZ(string PlanDate, string WorkShopCode, string ShiftID, string MaterCode, string IsAdd)
        {
            return this.service.DataAllowAddJZ(PlanDate, WorkShopCode, ShiftID, MaterCode, IsAdd);
        }
    }
}
