using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    public class BasStoragePlaceSubManager : BaseManager<BasStoragePlaceSub>, IBasStoragePlaceSubManager
    {
		#region 属性注入与构造方法
		
        private IBasStoragePlaceSubService service;

        public BasStoragePlaceSubManager()
        {
            this.service = new BasStoragePlaceSubService();
            base.BaseService = this.service;
        }

		public BasStoragePlaceSubManager(string connectStringKey)
        {
			this.service = new BasStoragePlaceSubService(connectStringKey);
            base.BaseService = this.service;
        }

        public BasStoragePlaceSubManager(NBear.Data.Gateway way)
        {
			this.service = new BasStoragePlaceSubService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
