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
    public class EQM_waiWeiWXManager : BaseManager<EQM_waiWeiWX>, IEQM_waiWeiWXManager
    {
		#region 属性注入与构造方法
		
        private IEQM_waiWeiWXService service;

        public EQM_waiWeiWXManager()
        {
            this.service = new EQM_waiWeiWXService();
            base.BaseService = this.service;
        }

		public EQM_waiWeiWXManager(string connectStringKey)
        {
			this.service = new EQM_waiWeiWXService(connectStringKey);
            base.BaseService = this.service;
        }

        public EQM_waiWeiWXManager(NBear.Data.Gateway way)
        {
			this.service = new EQM_waiWeiWXService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
