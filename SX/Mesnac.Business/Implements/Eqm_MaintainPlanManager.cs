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
    public class Eqm_MaintainPlanManager : BaseManager<Eqm_MaintainPlan>, IEqm_MaintainPlanManager
    {
		#region 属性注入与构造方法
		
        private IEqm_MaintainPlanService service;

        public Eqm_MaintainPlanManager()
        {
            this.service = new Eqm_MaintainPlanService();
            base.BaseService = this.service;
        }

		public Eqm_MaintainPlanManager(string connectStringKey)
        {
			this.service = new Eqm_MaintainPlanService(connectStringKey);
            base.BaseService = this.service;
        }

        public Eqm_MaintainPlanManager(NBear.Data.Gateway way)
        {
			this.service = new Eqm_MaintainPlanService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
