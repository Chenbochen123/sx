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
    public class Eqm_CleanStandManager : BaseManager<Eqm_CleanStand>, IEqm_CleanStandManager
    {
		#region 属性注入与构造方法
		
        private IEqm_CleanStandService service;

        public Eqm_CleanStandManager()
        {
            this.service = new Eqm_CleanStandService();
            base.BaseService = this.service;
        }

		public Eqm_CleanStandManager(string connectStringKey)
        {
			this.service = new Eqm_CleanStandService(connectStringKey);
            base.BaseService = this.service;
        }

        public Eqm_CleanStandManager(NBear.Data.Gateway way)
        {
			this.service = new Eqm_CleanStandService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
