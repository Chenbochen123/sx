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
    public class Sys_ClearAlarmUserManager : BaseManager<Sys_ClearAlarmUser>, ISys_ClearAlarmUserManager
    {
		#region 属性注入与构造方法
		
        private ISys_ClearAlarmUserService service;

        public Sys_ClearAlarmUserManager()
        {
            this.service = new Sys_ClearAlarmUserService();
            base.BaseService = this.service;
        }

		public Sys_ClearAlarmUserManager(string connectStringKey)
        {
			this.service = new Sys_ClearAlarmUserService(connectStringKey);
            base.BaseService = this.service;
        }

        public Sys_ClearAlarmUserManager(NBear.Data.Gateway way)
        {
			this.service = new Sys_ClearAlarmUserService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
