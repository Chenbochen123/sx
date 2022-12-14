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
    public class PmtRecipeMixingManager : BaseManager<PmtRecipeMixing>, IPmtRecipeMixingManager
    {
		#region 属性注入与构造方法
		
        private IPmtRecipeMixingService service;

        public PmtRecipeMixingManager()
        {
            this.service = new PmtRecipeMixingService();
            base.BaseService = this.service;
        }

		public PmtRecipeMixingManager(string connectStringKey)
        {
			this.service = new PmtRecipeMixingService(connectStringKey);
            base.BaseService = this.service;
        }

        public PmtRecipeMixingManager(NBear.Data.Gateway way)
        {
			this.service = new PmtRecipeMixingService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
