using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    public class FixSexManager : BaseManager<FixSex>, IFixSexManager
    {
		#region 属性注入与构造方法
		
        private IFixSexService service;

        public FixSexManager()
        {
            this.service = new FixSexService();
            base.BaseService = this.service;
        }

		public FixSexManager(string connectStringKey)
        {
			this.service = new FixSexService(connectStringKey);
            base.BaseService = this.service;
        }

        public FixSexManager(NBear.Data.Gateway way)
        {
			this.service = new FixSexService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
