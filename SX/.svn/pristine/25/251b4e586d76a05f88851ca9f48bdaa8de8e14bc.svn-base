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
    public class Eqm_MotorPlanManager : BaseManager<Eqm_MotorPlan>, IEqm_MotorPlanManager
    {
		#region 属性注入与构造方法
		
        private IEqm_MotorPlanService service;

        public Eqm_MotorPlanManager()
        {
            this.service = new Eqm_MotorPlanService();
            base.BaseService = this.service;
        }

		public Eqm_MotorPlanManager(string connectStringKey)
        {
			this.service = new Eqm_MotorPlanService(connectStringKey);
            base.BaseService = this.service;
        }

        public Eqm_MotorPlanManager(NBear.Data.Gateway way)
        {
			this.service = new Eqm_MotorPlanService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
