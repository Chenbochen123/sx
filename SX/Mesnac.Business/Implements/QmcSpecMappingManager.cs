using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    public class QmcSpecMappingManager : BaseManager<QmcSpecMapping>, IQmcSpecMappingManager
    {
		#region 属性注入与构造方法
		
        private IQmcSpecMappingService service;

        public QmcSpecMappingManager()
        {
            this.service = new QmcSpecMappingService();
            base.BaseService = this.service;
        }

		public QmcSpecMappingManager(string connectStringKey)
        {
			this.service = new QmcSpecMappingService(connectStringKey);
            base.BaseService = this.service;
        }

        public QmcSpecMappingManager(NBear.Data.Gateway way)
        {
			this.service = new QmcSpecMappingService(way);
            base.BaseService = this.service;
        }

        #endregion

        public string GetNextMappingId()
        {
            return this.service.GetNextMappingId();
        }
    }
}
