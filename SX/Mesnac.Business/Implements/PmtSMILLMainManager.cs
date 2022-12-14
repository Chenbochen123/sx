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
    public class PmtSMILLMainManager : BaseManager<PmtSMILLMain>, IPmtSMILLMainManager
    {
		#region 属性注入与构造方法
		
        private IPmtSMILLMainService service;

        public PmtSMILLMainManager()
        {
            this.service = new PmtSMILLMainService();
            base.BaseService = this.service;
        }

		public PmtSMILLMainManager(string connectStringKey)
        {
			this.service = new PmtSMILLMainService(connectStringKey);
            base.BaseService = this.service;
        }

        public PmtSMILLMainManager(NBear.Data.Gateway way)
        {
			this.service = new PmtSMILLMainService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
