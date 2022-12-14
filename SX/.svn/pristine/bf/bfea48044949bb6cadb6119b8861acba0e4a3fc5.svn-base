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
    public class PpmSemiStorageDetailManager : BaseManager<PpmSemiStorageDetail>, IPpmSemiStorageDetailManager
    {
		#region 属性注入与构造方法
		
        private IPpmSemiStorageDetailService service;

        public PpmSemiStorageDetailManager()
        {
            this.service = new PpmSemiStorageDetailService();
            base.BaseService = this.service;
        }

		public PpmSemiStorageDetailManager(string connectStringKey)
        {
			this.service = new PpmSemiStorageDetailService(connectStringKey);
            base.BaseService = this.service;
        }

        public PpmSemiStorageDetailManager(NBear.Data.Gateway way)
        {
			this.service = new PpmSemiStorageDetailService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
