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
    public class PmtCoolMILLMainManager : BaseManager<PmtCoolMILLMain>, IPmtCoolMILLMainManager
    {
		#region 属性注入与构造方法
		
        private IPmtCoolMILLMainService service;

        public PmtCoolMILLMainManager()
        {
            this.service = new PmtCoolMILLMainService();
            base.BaseService = this.service;
        }

		public PmtCoolMILLMainManager(string connectStringKey)
        {
			this.service = new PmtCoolMILLMainService(connectStringKey);
            base.BaseService = this.service;
        }

        public PmtCoolMILLMainManager(NBear.Data.Gateway way)
        {
			this.service = new PmtCoolMILLMainService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
