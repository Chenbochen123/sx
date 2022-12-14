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
    public class PmtRecipeOpenMixingManager : BaseManager<PmtRecipeOpenMixing>, IPmtRecipeOpenMixingManager
    {
		#region 属性注入与构造方法
		
        private IPmtRecipeOpenMixingService service;

        public PmtRecipeOpenMixingManager()
        {
            this.service = new PmtRecipeOpenMixingService();
            base.BaseService = this.service;
        }

		public PmtRecipeOpenMixingManager(string connectStringKey)
        {
			this.service = new PmtRecipeOpenMixingService(connectStringKey);
            base.BaseService = this.service;
        }

        public PmtRecipeOpenMixingManager(NBear.Data.Gateway way)
        {
			this.service = new PmtRecipeOpenMixingService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
