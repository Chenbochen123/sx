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
    public class SysDeptRoleManager : BaseManager<SysDeptRole>, ISysDeptRoleManager
    {
		#region 属性注入与构造方法
		
        private ISysDeptRoleService service;

        public SysDeptRoleManager()
        {
            this.service = new SysDeptRoleService();
            base.BaseService = this.service;
        }

		public SysDeptRoleManager(string connectStringKey)
        {
			this.service = new SysDeptRoleService(connectStringKey);
            base.BaseService = this.service;
        }

        public SysDeptRoleManager(NBear.Data.Gateway way)
        {
			this.service = new SysDeptRoleService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
