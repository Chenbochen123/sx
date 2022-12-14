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
    public class Eqm_bjsparecdManager : BaseManager<Eqm_bjsparecd>, IEqm_bjsparecdManager
    {
		#region 属性注入与构造方法
		
        private IEqm_bjsparecdService service;

        public Eqm_bjsparecdManager()
        {
            this.service = new Eqm_bjsparecdService();
            base.BaseService = this.service;
        }

		public Eqm_bjsparecdManager(string connectStringKey)
        {
			this.service = new Eqm_bjsparecdService(connectStringKey);
            base.BaseService = this.service;
        }

        public Eqm_bjsparecdManager(NBear.Data.Gateway way)
        {
			this.service = new Eqm_bjsparecdService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
