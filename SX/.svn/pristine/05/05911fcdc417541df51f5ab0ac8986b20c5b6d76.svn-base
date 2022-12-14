using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    public class CltQmtCheckItemManager : BaseManager<CltQmtCheckItem>, ICltQmtCheckItemManager
    {
		#region 属性注入与构造方法
		
        private ICltQmtCheckItemService service;

        public CltQmtCheckItemManager()
        {
            this.service = new CltQmtCheckItemService();
            base.BaseService = this.service;
        }

		public CltQmtCheckItemManager(string connectStringKey)
        {
			this.service = new CltQmtCheckItemService(connectStringKey);
            base.BaseService = this.service;
        }

        public CltQmtCheckItemManager(NBear.Data.Gateway way)
        {
			this.service = new CltQmtCheckItemService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
