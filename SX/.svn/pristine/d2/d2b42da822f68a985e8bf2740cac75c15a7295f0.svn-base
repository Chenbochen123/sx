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
    public class PptAlarmManager : BaseManager<PptAlarm>, IPptAlarmManager
    {
		#region 属性注入与构造方法
		
        private IPptAlarmService service;

        public PptAlarmManager()
        {
            this.service = new PptAlarmService();
            base.BaseService = this.service;
        }

		public PptAlarmManager(string connectStringKey)
        {
			this.service = new PptAlarmService(connectStringKey);
            base.BaseService = this.service;
        }

        public PptAlarmManager(NBear.Data.Gateway way)
        {
			this.service = new PptAlarmService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
