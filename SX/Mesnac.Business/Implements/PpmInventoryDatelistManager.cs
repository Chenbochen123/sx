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
    public class PpmInventoryDatelistManager : BaseManager<PpmInventoryDatelist>, IPpmInventoryDatelistManager
    {
		#region ����ע���빹�췽��
		
        private IPpmInventoryDatelistService service;

        public PpmInventoryDatelistManager()
        {
            this.service = new PpmInventoryDatelistService();
            base.BaseService = this.service;
        }

		public PpmInventoryDatelistManager(string connectStringKey)
        {
			this.service = new PpmInventoryDatelistService(connectStringKey);
            base.BaseService = this.service;
        }

        public PpmInventoryDatelistManager(NBear.Data.Gateway way)
        {
			this.service = new PpmInventoryDatelistService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
