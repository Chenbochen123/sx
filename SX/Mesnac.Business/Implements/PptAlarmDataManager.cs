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
    public class PptAlarmDataManager : BaseManager<PptAlarmData>, IPptAlarmDataManager
    {
		#region 属性注入与构造方法
		
        private IPptAlarmDataService service;

        public PptAlarmDataManager()
        {
            this.service = new PptAlarmDataService();
            base.BaseService = this.service;
        }

		public PptAlarmDataManager(string connectStringKey)
        {
			this.service = new PptAlarmDataService(connectStringKey);
            base.BaseService = this.service;
        }

        public PptAlarmDataManager(NBear.Data.Gateway way)
        {
			this.service = new PptAlarmDataService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
