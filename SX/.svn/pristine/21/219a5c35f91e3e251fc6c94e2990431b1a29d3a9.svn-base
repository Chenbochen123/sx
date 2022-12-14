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
    public class SYS_USERManager : BaseManager<SYS_USER>, ISYS_USERManager
    {
		#region 属性注入与构造方法
		
        private ISYS_USERService service;

        public SYS_USERManager()
        {
            this.service = new SYS_USERService();
            base.BaseService = this.service;
        }

		public SYS_USERManager(string connectStringKey)
        {
			this.service = new SYS_USERService(connectStringKey);
            base.BaseService = this.service;
        }

        public SYS_USERManager(NBear.Data.Gateway way)
        {
			this.service = new SYS_USERService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
