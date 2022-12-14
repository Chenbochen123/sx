using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    public class QmcLedgerDetailManager : BaseManager<QmcLedgerDetail>, IQmcLedgerDetailManager
    {
		#region 属性注入与构造方法
		
        private IQmcLedgerDetailService service;

        public QmcLedgerDetailManager()
        {
            this.service = new QmcLedgerDetailService();
            base.BaseService = this.service;
        }

		public QmcLedgerDetailManager(string connectStringKey)
        {
			this.service = new QmcLedgerDetailService(connectStringKey);
            base.BaseService = this.service;
        }

        public QmcLedgerDetailManager(NBear.Data.Gateway way)
        {
			this.service = new QmcLedgerDetailService(way);
            base.BaseService = this.service;
        }

        #endregion

        public string GetNextDetailId()
        {
            return this.service.GetNextDetailId();
        }
    }
}
