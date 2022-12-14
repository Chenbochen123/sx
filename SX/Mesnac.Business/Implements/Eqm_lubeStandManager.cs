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
    public class Eqm_lubeStandManager : BaseManager<Eqm_lubeStand>, IEqm_lubeStandManager
    {
		#region 属性注入与构造方法
		
        private IEqm_lubeStandService service;

        public Eqm_lubeStandManager()
        {
            this.service = new Eqm_lubeStandService();
            base.BaseService = this.service;
        }

		public Eqm_lubeStandManager(string connectStringKey)
        {
			this.service = new Eqm_lubeStandService(connectStringKey);
            base.BaseService = this.service;
        }

        public Eqm_lubeStandManager(NBear.Data.Gateway way)
        {
			this.service = new Eqm_lubeStandService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
