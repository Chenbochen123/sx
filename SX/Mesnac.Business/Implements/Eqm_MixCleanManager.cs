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
    public class Eqm_MixCleanManager : BaseManager<Eqm_MixClean>, IEqm_MixCleanManager
    {
		#region 属性注入与构造方法
		
        private IEqm_MixCleanService service;

        public Eqm_MixCleanManager()
        {
            this.service = new Eqm_MixCleanService();
            base.BaseService = this.service;
        }

		public Eqm_MixCleanManager(string connectStringKey)
        {
			this.service = new Eqm_MixCleanService(connectStringKey);
            base.BaseService = this.service;
        }

        public Eqm_MixCleanManager(NBear.Data.Gateway way)
        {
			this.service = new Eqm_MixCleanService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
