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
    public class Ppt_WeighManager : BaseManager<Ppt_Weigh>, IPpt_WeighManager
    {
		#region 属性注入与构造方法
		
        private IPpt_WeighService service;

        public Ppt_WeighManager()
        {
            this.service = new Ppt_WeighService();
            base.BaseService = this.service;
        }

		public Ppt_WeighManager(string connectStringKey)
        {
			this.service = new Ppt_WeighService(connectStringKey);
            base.BaseService = this.service;
        }

        public Ppt_WeighManager(NBear.Data.Gateway way)
        {
			this.service = new Ppt_WeighService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
