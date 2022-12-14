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
    public class Eqm_lvdaiPlanManager : BaseManager<Eqm_lvdaiPlan>, IEqm_lvdaiPlanManager
    {
		#region 属性注入与构造方法
		
        private IEqm_lvdaiPlanService service;

        public Eqm_lvdaiPlanManager()
        {
            this.service = new Eqm_lvdaiPlanService();
            base.BaseService = this.service;
        }

		public Eqm_lvdaiPlanManager(string connectStringKey)
        {
			this.service = new Eqm_lvdaiPlanService(connectStringKey);
            base.BaseService = this.service;
        }

        public Eqm_lvdaiPlanManager(NBear.Data.Gateway way)
        {
			this.service = new Eqm_lvdaiPlanService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
