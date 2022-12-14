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
    public class Eqm_pmspareManager : BaseManager<Eqm_pmspare>, IEqm_pmspareManager
    {
		#region 属性注入与构造方法
		
        private IEqm_pmspareService service;

        public Eqm_pmspareManager()
        {
            this.service = new Eqm_pmspareService();
            base.BaseService = this.service;
        }

		public Eqm_pmspareManager(string connectStringKey)
        {
			this.service = new Eqm_pmspareService(connectStringKey);
            base.BaseService = this.service;
        }

        public Eqm_pmspareManager(NBear.Data.Gateway way)
        {
			this.service = new Eqm_pmspareService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
