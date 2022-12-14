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
    public class Eqm_SparePlanManager : BaseManager<Eqm_SparePlan>, IEqm_SparePlanManager
    {
		#region 属性注入与构造方法
		
        private IEqm_SparePlanService service;

        public Eqm_SparePlanManager()
        {
            this.service = new Eqm_SparePlanService();
            base.BaseService = this.service;
        }

		public Eqm_SparePlanManager(string connectStringKey)
        {
			this.service = new Eqm_SparePlanService(connectStringKey);
            base.BaseService = this.service;
        }

        public Eqm_SparePlanManager(NBear.Data.Gateway way)
        {
			this.service = new Eqm_SparePlanService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
