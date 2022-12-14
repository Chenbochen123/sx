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
    public class Eqm_MeasPlanManager : BaseManager<Eqm_MeasPlan>, IEqm_MeasPlanManager
    {
		#region 属性注入与构造方法
		
        private IEqm_MeasPlanService service;

        public Eqm_MeasPlanManager()
        {
            this.service = new Eqm_MeasPlanService();
            base.BaseService = this.service;
        }

		public Eqm_MeasPlanManager(string connectStringKey)
        {
			this.service = new Eqm_MeasPlanService(connectStringKey);
            base.BaseService = this.service;
        }

        public Eqm_MeasPlanManager(NBear.Data.Gateway way)
        {
			this.service = new Eqm_MeasPlanService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
