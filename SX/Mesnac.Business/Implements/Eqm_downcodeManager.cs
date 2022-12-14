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
    public class Eqm_downcodeManager : BaseManager<Eqm_downcode>, IEqm_downcodeManager
    {
		#region 属性注入与构造方法
		
        private IEqm_downcodeService service;

        public Eqm_downcodeManager()
        {
            this.service = new Eqm_downcodeService();
            base.BaseService = this.service;
        }

		public Eqm_downcodeManager(string connectStringKey)
        {
			this.service = new Eqm_downcodeService(connectStringKey);
            base.BaseService = this.service;
        }

        public Eqm_downcodeManager(NBear.Data.Gateway way)
        {
			this.service = new Eqm_downcodeService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
