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
    public class Eqm_bjtpcdManager : BaseManager<Eqm_bjtpcd>, IEqm_bjtpcdManager
    {
		#region 属性注入与构造方法
		
        private IEqm_bjtpcdService service;

        public Eqm_bjtpcdManager()
        {
            this.service = new Eqm_bjtpcdService();
            base.BaseService = this.service;
        }

		public Eqm_bjtpcdManager(string connectStringKey)
        {
			this.service = new Eqm_bjtpcdService(connectStringKey);
            base.BaseService = this.service;
        }

        public Eqm_bjtpcdManager(NBear.Data.Gateway way)
        {
			this.service = new Eqm_bjtpcdService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
