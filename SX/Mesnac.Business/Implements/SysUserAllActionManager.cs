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
    public class SysUserAllActionManager : BaseManager<SysUserAllAction>, ISysUserAllActionManager
    {
		#region ����ע���빹�췽��
		
        private ISysUserAllActionService service;

        public SysUserAllActionManager()
        {
            this.service = new SysUserAllActionService();
            base.BaseService = this.service;
        }

		public SysUserAllActionManager(string connectStringKey)
        {
			this.service = new SysUserAllActionService(connectStringKey);
            base.BaseService = this.service;
        }

        public SysUserAllActionManager(NBear.Data.Gateway way)
        {
			this.service = new SysUserAllActionService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
