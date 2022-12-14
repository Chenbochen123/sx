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
    public class Eqm_bjStockManager : BaseManager<Eqm_bjStock>, IEqm_bjStockManager
    {
		#region 属性注入与构造方法
		
        private IEqm_bjStockService service;

        public Eqm_bjStockManager()
        {
            this.service = new Eqm_bjStockService();
            base.BaseService = this.service;
        }

		public Eqm_bjStockManager(string connectStringKey)
        {
			this.service = new Eqm_bjStockService(connectStringKey);
            base.BaseService = this.service;
        }

        public Eqm_bjStockManager(NBear.Data.Gateway way)
        {
			this.service = new Eqm_bjStockService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
