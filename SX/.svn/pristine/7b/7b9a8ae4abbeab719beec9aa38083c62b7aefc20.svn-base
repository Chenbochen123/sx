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
    public class Eqm_DianManager : BaseManager<Eqm_Dian>, IEqm_DianManager
    {
		#region 属性注入与构造方法
		
        private IEqm_DianService service;

        public Eqm_DianManager()
        {
            this.service = new Eqm_DianService();
            base.BaseService = this.service;
        }

		public Eqm_DianManager(string connectStringKey)
        {
			this.service = new Eqm_DianService(connectStringKey);
            base.BaseService = this.service;
        }

        public Eqm_DianManager(NBear.Data.Gateway way)
        {
			this.service = new Eqm_DianService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
