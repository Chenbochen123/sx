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
    public class Ppt_WorkerManager : BaseManager<Ppt_Worker>, IPpt_WorkerManager
    {
		#region 属性注入与构造方法
		
        private IPpt_WorkerService service;

        public Ppt_WorkerManager()
        {
            this.service = new Ppt_WorkerService();
            base.BaseService = this.service;
        }

		public Ppt_WorkerManager(string connectStringKey)
        {
			this.service = new Ppt_WorkerService(connectStringKey);
            base.BaseService = this.service;
        }

        public Ppt_WorkerManager(NBear.Data.Gateway way)
        {
			this.service = new Ppt_WorkerService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
