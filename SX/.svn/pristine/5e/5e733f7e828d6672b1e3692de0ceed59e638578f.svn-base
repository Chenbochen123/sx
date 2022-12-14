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
    public class SysDeptActionManager : BaseManager<SysDeptAction>, ISysDeptActionManager
    {
		#region 属性注入与构造方法
		
        private ISysDeptActionService service;

        public SysDeptActionManager()
        {
            this.service = new SysDeptActionService();
            base.BaseService = this.service;
        }

		public SysDeptActionManager(string connectStringKey)
        {
			this.service = new SysDeptActionService(connectStringKey);
            base.BaseService = this.service;
        }

        public SysDeptActionManager(NBear.Data.Gateway way)
        {
			this.service = new SysDeptActionService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
