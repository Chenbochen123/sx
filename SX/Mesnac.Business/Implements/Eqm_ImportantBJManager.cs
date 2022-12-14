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
    public class Eqm_ImportantBJManager : BaseManager<Eqm_ImportantBJ>, IEqm_ImportantBJManager
    {
		#region 属性注入与构造方法
		
        private IEqm_ImportantBJService service;

        public Eqm_ImportantBJManager()
        {
            this.service = new Eqm_ImportantBJService();
            base.BaseService = this.service;
        }

		public Eqm_ImportantBJManager(string connectStringKey)
        {
			this.service = new Eqm_ImportantBJService(connectStringKey);
            base.BaseService = this.service;
        }

        public Eqm_ImportantBJManager(NBear.Data.Gateway way)
        {
			this.service = new Eqm_ImportantBJService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
