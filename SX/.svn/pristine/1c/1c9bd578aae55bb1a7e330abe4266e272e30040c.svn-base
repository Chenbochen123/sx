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
    public class Eqm_lubePlanManager : BaseManager<Eqm_lubePlan>, IEqm_lubePlanManager
    {
		#region 属性注入与构造方法
		
        private IEqm_lubePlanService service;

        public Eqm_lubePlanManager()
        {
            this.service = new Eqm_lubePlanService();
            base.BaseService = this.service;
        }

		public Eqm_lubePlanManager(string connectStringKey)
        {
			this.service = new Eqm_lubePlanService(connectStringKey);
            base.BaseService = this.service;
        }

        public Eqm_lubePlanManager(NBear.Data.Gateway way)
        {
			this.service = new Eqm_lubePlanService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
