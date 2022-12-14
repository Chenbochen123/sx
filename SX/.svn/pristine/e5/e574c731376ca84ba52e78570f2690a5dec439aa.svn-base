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
    public class SysUserActionEqualManager : BaseManager<SysUserActionEqual>, ISysUserActionEqualManager
    {
		#region 属性注入与构造方法
		
        private ISysUserActionEqualService service;

        public SysUserActionEqualManager()
        {
            this.service = new SysUserActionEqualService();
            base.BaseService = this.service;
        }

		public SysUserActionEqualManager(string connectStringKey)
        {
			this.service = new SysUserActionEqualService(connectStringKey);
            base.BaseService = this.service;
        }

        public SysUserActionEqualManager(NBear.Data.Gateway way)
        {
			this.service = new SysUserActionEqualService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
