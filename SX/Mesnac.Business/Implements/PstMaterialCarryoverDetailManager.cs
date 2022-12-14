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
    public class PstMaterialCarryoverDetailManager : BaseManager<PstMaterialCarryoverDetail>, IPstMaterialCarryoverDetailManager
    {
		#region ����ע���빹�췽��
		
        private IPstMaterialCarryoverDetailService service;

        public PstMaterialCarryoverDetailManager()
        {
            this.service = new PstMaterialCarryoverDetailService();
            base.BaseService = this.service;
        }

		public PstMaterialCarryoverDetailManager(string connectStringKey)
        {
			this.service = new PstMaterialCarryoverDetailService(connectStringKey);
            base.BaseService = this.service;
        }

        public PstMaterialCarryoverDetailManager(NBear.Data.Gateway way)
        {
			this.service = new PstMaterialCarryoverDetailService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
