using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    public class QmcPropertyValueManager : BaseManager<QmcPropertyValue>, IQmcPropertyValueManager
    {
		#region 属性注入与构造方法
		
        private IQmcPropertyValueService service;

        public QmcPropertyValueManager()
        {
            this.service = new QmcPropertyValueService();
            base.BaseService = this.service;
        }

		public QmcPropertyValueManager(string connectStringKey)
        {
			this.service = new QmcPropertyValueService(connectStringKey);
            base.BaseService = this.service;
        }

        public QmcPropertyValueManager(NBear.Data.Gateway way)
        {
			this.service = new QmcPropertyValueService(way);
            base.BaseService = this.service;
        }

        #endregion

        public string GetNextValueId()
        {
            return this.service.GetNextValueId();
        }
    }
}
