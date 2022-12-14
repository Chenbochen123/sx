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
    public class Eqm_lubeManager : BaseManager<Eqm_lube>, IEqm_lubeManager
    {
		#region 属性注入与构造方法
		
        private IEqm_lubeService service;

        public Eqm_lubeManager()
        {
            this.service = new Eqm_lubeService();
            base.BaseService = this.service;
        }

		public Eqm_lubeManager(string connectStringKey)
        {
			this.service = new Eqm_lubeService(connectStringKey);
            base.BaseService = this.service;
        }

        public Eqm_lubeManager(NBear.Data.Gateway way)
        {
			this.service = new Eqm_lubeService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
