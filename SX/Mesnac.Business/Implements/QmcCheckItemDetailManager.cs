using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    public class QmcCheckItemDetailManager : BaseManager<QmcCheckItemDetail>, IQmcCheckItemDetailManager
    {
		#region 属性注入与构造方法
		
        private IQmcCheckItemDetailService service;

        public QmcCheckItemDetailManager()
        {
            this.service = new QmcCheckItemDetailService();
            base.BaseService = this.service;
        }

		public QmcCheckItemDetailManager(string connectStringKey)
        {
			this.service = new QmcCheckItemDetailService(connectStringKey);
            base.BaseService = this.service;
        }

        public QmcCheckItemDetailManager(NBear.Data.Gateway way)
        {
			this.service = new QmcCheckItemDetailService(way);
            base.BaseService = this.service;
        }

        #endregion

        public string GetNextDetailId()
        {
            return this.service.GetNextDetailId();
        }
    }
}
