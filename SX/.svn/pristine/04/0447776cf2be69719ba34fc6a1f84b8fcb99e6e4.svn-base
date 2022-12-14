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
    public class PstMaterialInOastManager : BaseManager<PstMaterialInOast>, IPstMaterialInOastManager
    {
		#region 属性注入与构造方法
		
        private IPstMaterialInOastService service;

        public PstMaterialInOastManager()
        {
            this.service = new PstMaterialInOastService();
            base.BaseService = this.service;
        }

		public PstMaterialInOastManager(string connectStringKey)
        {
			this.service = new PstMaterialInOastService(connectStringKey);
            base.BaseService = this.service;
        }

        public PstMaterialInOastManager(NBear.Data.Gateway way)
        {
			this.service = new PstMaterialInOastService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
