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
    public class Ppt_ShiftConfigManager : BaseManager<Ppt_ShiftConfig>, IPpt_ShiftConfigManager
    {
		#region 属性注入与构造方法
		
        private IPpt_ShiftConfigService service;

        public Ppt_ShiftConfigManager()
        {
            this.service = new Ppt_ShiftConfigService();
            base.BaseService = this.service;
        }

		public Ppt_ShiftConfigManager(string connectStringKey)
        {
			this.service = new Ppt_ShiftConfigService(connectStringKey);
            base.BaseService = this.service;
        }

        public Ppt_ShiftConfigManager(NBear.Data.Gateway way)
        {
			this.service = new Ppt_ShiftConfigService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
