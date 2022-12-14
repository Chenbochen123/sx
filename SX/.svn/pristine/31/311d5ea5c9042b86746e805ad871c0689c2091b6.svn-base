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
    public class PmtPMILLMainManager : BaseManager<PmtPMILLMain>, IPmtPMILLMainManager
    {
		#region 属性注入与构造方法
		
        private IPmtPMILLMainService service;

        public PmtPMILLMainManager()
        {
            this.service = new PmtPMILLMainService();
            base.BaseService = this.service;
        }

		public PmtPMILLMainManager(string connectStringKey)
        {
			this.service = new PmtPMILLMainService(connectStringKey);
            base.BaseService = this.service;
        }

        public PmtPMILLMainManager(NBear.Data.Gateway way)
        {
			this.service = new PmtPMILLMainService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
