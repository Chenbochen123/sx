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
    public class PmtRecipeWeightMidManager : BaseManager<PmtRecipeWeightMid>, IPmtRecipeWeightMidManager
    {
		#region 属性注入与构造方法
		
        private IPmtRecipeWeightMidService service;

        public PmtRecipeWeightMidManager()
        {
            this.service = new PmtRecipeWeightMidService();
            base.BaseService = this.service;
        }

		public PmtRecipeWeightMidManager(string connectStringKey)
        {
			this.service = new PmtRecipeWeightMidService(connectStringKey);
            base.BaseService = this.service;
        }

        public PmtRecipeWeightMidManager(NBear.Data.Gateway way)
        {
			this.service = new PmtRecipeWeightMidService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
