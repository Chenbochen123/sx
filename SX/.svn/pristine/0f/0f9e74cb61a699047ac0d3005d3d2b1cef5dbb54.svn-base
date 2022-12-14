using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    public class QmcPropertyDetailManager : BaseManager<QmcPropertyDetail>, IQmcPropertyDetailManager
    {
		#region 属性注入与构造方法
		
        private IQmcPropertyDetailService service;

        public QmcPropertyDetailManager()
        {
            this.service = new QmcPropertyDetailService();
            base.BaseService = this.service;
        }

		public QmcPropertyDetailManager(string connectStringKey)
        {
			this.service = new QmcPropertyDetailService(connectStringKey);
            base.BaseService = this.service;
        }

        public QmcPropertyDetailManager(NBear.Data.Gateway way)
        {
			this.service = new QmcPropertyDetailService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
