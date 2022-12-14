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
    public class Eqm_KongTiaoManager : BaseManager<Eqm_KongTiao>, IEqm_KongTiaoManager
    {
		#region 属性注入与构造方法
		
        private IEqm_KongTiaoService service;

        public Eqm_KongTiaoManager()
        {
            this.service = new Eqm_KongTiaoService();
            base.BaseService = this.service;
        }

		public Eqm_KongTiaoManager(string connectStringKey)
        {
			this.service = new Eqm_KongTiaoService(connectStringKey);
            base.BaseService = this.service;
        }

        public Eqm_KongTiaoManager(NBear.Data.Gateway way)
        {
			this.service = new Eqm_KongTiaoService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
