using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    public class PptMixDataManager : BaseManager<PptMixData>, IPptMixDataManager
    {
		#region 属性注入与构造方法
		
        private IPptMixDataService service;

        public PptMixDataManager()
        {
            this.service = new PptMixDataService();
            base.BaseService = this.service;
        }

		public PptMixDataManager(string connectStringKey)
        {
			this.service = new PptMixDataService(connectStringKey);
            base.BaseService = this.service;
        }

        public PptMixDataManager(NBear.Data.Gateway way)
        {
			this.service = new PptMixDataService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
