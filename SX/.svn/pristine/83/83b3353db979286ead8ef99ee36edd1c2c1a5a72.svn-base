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
    public class Ppt_LotManager : BaseManager<Ppt_Lot>, IPpt_LotManager
    {
		#region 属性注入与构造方法
		
        private IPpt_LotService service;

        public Ppt_LotManager()
        {
            this.service = new Ppt_LotService();
            base.BaseService = this.service;
        }

		public Ppt_LotManager(string connectStringKey)
        {
			this.service = new Ppt_LotService(connectStringKey);
            base.BaseService = this.service;
        }

        public Ppt_LotManager(NBear.Data.Gateway way)
        {
			this.service = new Ppt_LotService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
