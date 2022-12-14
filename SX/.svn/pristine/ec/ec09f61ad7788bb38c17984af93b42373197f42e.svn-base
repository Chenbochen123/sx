using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    public class PstStorgaeManager : BaseManager<PstStorgae>, IPstStorgaeManager
    {
		#region 属性注入与构造方法
		
        private IPstStorgaeService service;

        public PstStorgaeManager()
        {
            this.service = new PstStorgaeService();
            base.BaseService = this.service;
        }

		public PstStorgaeManager(string connectStringKey)
        {
			this.service = new PstStorgaeService(connectStringKey);
            base.BaseService = this.service;
        }

        public PstStorgaeManager(NBear.Data.Gateway way)
        {
			this.service = new PstStorgaeService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
