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
    public class SysMesActionManager : BaseManager<SysMesAction>, ISysMesActionManager
    {
		#region 属性注入与构造方法
		
        private ISysMesActionService service;

        public SysMesActionManager()
        {
            this.service = new SysMesActionService();
            base.BaseService = this.service;
        }

		public SysMesActionManager(string connectStringKey)
        {
			this.service = new SysMesActionService(connectStringKey);
            base.BaseService = this.service;
        }

        public SysMesActionManager(NBear.Data.Gateway way)
        {
			this.service = new SysMesActionService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
