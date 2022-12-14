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
    public class Ppt_SetTimeManager : BaseManager<Ppt_SetTime>, IPpt_SetTimeManager
    {
		#region 属性注入与构造方法
		
        private IPpt_SetTimeService service;

        public Ppt_SetTimeManager()
        {
            this.service = new Ppt_SetTimeService();
            base.BaseService = this.service;
        }

		public Ppt_SetTimeManager(string connectStringKey)
        {
			this.service = new Ppt_SetTimeService(connectStringKey);
            base.BaseService = this.service;
        }

        public Ppt_SetTimeManager(NBear.Data.Gateway way)
        {
			this.service = new Ppt_SetTimeService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
