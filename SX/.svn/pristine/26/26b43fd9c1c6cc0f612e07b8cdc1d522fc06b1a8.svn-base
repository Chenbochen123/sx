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
    public class Eqm_LuDaiInfoManager : BaseManager<Eqm_LuDaiInfo>, IEqm_LuDaiInfoManager
    {
		#region 属性注入与构造方法
		
        private IEqm_LuDaiInfoService service;

        public Eqm_LuDaiInfoManager()
        {
            this.service = new Eqm_LuDaiInfoService();
            base.BaseService = this.service;
        }

		public Eqm_LuDaiInfoManager(string connectStringKey)
        {
			this.service = new Eqm_LuDaiInfoService(connectStringKey);
            base.BaseService = this.service;
        }

        public Eqm_LuDaiInfoManager(NBear.Data.Gateway way)
        {
			this.service = new Eqm_LuDaiInfoService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
