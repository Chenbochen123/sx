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
    public class ppt_workerSetManager : BaseManager<ppt_workerSet>, Ippt_workerSetManager
    {
		#region 属性注入与构造方法
		
        private Ippt_workerSetService service;

        public ppt_workerSetManager()
        {
            this.service = new ppt_workerSetService();
            base.BaseService = this.service;
        }

		public ppt_workerSetManager(string connectStringKey)
        {
			this.service = new ppt_workerSetService(connectStringKey);
            base.BaseService = this.service;
        }

        public ppt_workerSetManager(NBear.Data.Gateway way)
        {
			this.service = new ppt_workerSetService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
