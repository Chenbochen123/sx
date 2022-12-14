using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    public class BasEquipMonitorManager : BaseManager<BasEquipMonitor>, IBasEquipMonitorManager
    {
		#region 属性注入与构造方法
		
        private IBasEquipMonitorService service;

        public BasEquipMonitorManager()
        {
            this.service = new BasEquipMonitorService();
            base.BaseService = this.service;
        }

		public BasEquipMonitorManager(string connectStringKey)
        {
			this.service = new BasEquipMonitorService(connectStringKey);
            base.BaseService = this.service;
        }

        public BasEquipMonitorManager(NBear.Data.Gateway way)
        {
			this.service = new BasEquipMonitorService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
