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
    public class EqmRepairProtectPlanManager : BaseManager<EqmRepairProtectPlan>, IEqmRepairProtectPlanManager
    {
		#region 属性注入与构造方法
		
        private IEqmRepairProtectPlanService service;

        public EqmRepairProtectPlanManager()
        {
            this.service = new EqmRepairProtectPlanService();
            base.BaseService = this.service;
        }

		public EqmRepairProtectPlanManager(string connectStringKey)
        {
			this.service = new EqmRepairProtectPlanService(connectStringKey);
            base.BaseService = this.service;
        }

        public EqmRepairProtectPlanManager(NBear.Data.Gateway way)
        {
			this.service = new EqmRepairProtectPlanService(way);
            base.BaseService = this.service;
        }

        #endregion
        #region 查询条件类定义
        public class QueryParams : EqmRepairProtectPlanService.QueryParams
        {
        }
        #endregion
        public Data.Components.PageResult<EqmRepairProtectPlan> GetTablePageDataBySql(EqmRepairProtectPlanService.QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }
        public string GetNeedStopTimeCount(string equipCode, string planName, string planMonth)
        {
            return this.service.GetNeedStopTimeCount(equipCode, planName, planMonth);
        }
    }
}
