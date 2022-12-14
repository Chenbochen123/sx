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
    public class Eqm_MainStandManager : BaseManager<Eqm_MainStand>, IEqm_MainStandManager
    {
		#region 属性注入与构造方法
		
        private IEqm_MainStandService service;

        public Eqm_MainStandManager()
        {
            this.service = new Eqm_MainStandService();
            base.BaseService = this.service;
        }

		public Eqm_MainStandManager(string connectStringKey)
        {
			this.service = new Eqm_MainStandService(connectStringKey);
            base.BaseService = this.service;
        }

        public Eqm_MainStandManager(NBear.Data.Gateway way)
        {
			this.service = new Eqm_MainStandService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
