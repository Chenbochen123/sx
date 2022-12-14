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
    public class Ppt_balanceCheckManager : BaseManager<Ppt_balanceCheck>, IPpt_balanceCheckManager
    {
		#region 属性注入与构造方法
		
        private IPpt_balanceCheckService service;

        public Ppt_balanceCheckManager()
        {
            this.service = new Ppt_balanceCheckService();
            base.BaseService = this.service;
        }

		public Ppt_balanceCheckManager(string connectStringKey)
        {
			this.service = new Ppt_balanceCheckService(connectStringKey);
            base.BaseService = this.service;
        }

        public Ppt_balanceCheckManager(NBear.Data.Gateway way)
        {
			this.service = new Ppt_balanceCheckService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
