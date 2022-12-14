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
    public class JCZL_workManager : BaseManager<JCZL_work>, IJCZL_workManager
    {
		#region 属性注入与构造方法
		
        private IJCZL_workService service;

        public JCZL_workManager()
        {
            this.service = new JCZL_workService();
            base.BaseService = this.service;
        }

		public JCZL_workManager(string connectStringKey)
        {
			this.service = new JCZL_workService(connectStringKey);
            base.BaseService = this.service;
        }

        public JCZL_workManager(NBear.Data.Gateway way)
        {
			this.service = new JCZL_workService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
