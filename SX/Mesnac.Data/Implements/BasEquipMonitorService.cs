using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    public class BasEquipMonitorService : BaseService<BasEquipMonitor>, IBasEquipMonitorService
    {
		#region 构造方法

        public BasEquipMonitorService() : base(){ }

        public BasEquipMonitorService(string connectStringKey) : base(connectStringKey){ }

        public BasEquipMonitorService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法
    }
}
