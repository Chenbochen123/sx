using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    public class QmcLedgerKeyValueManager : BaseManager<QmcLedgerKeyValue>, IQmcLedgerKeyValueManager
    {
		#region 属性注入与构造方法
		
        private IQmcLedgerKeyValueService service;

        public QmcLedgerKeyValueManager()
        {
            this.service = new QmcLedgerKeyValueService();
            base.BaseService = this.service;
        }

		public QmcLedgerKeyValueManager(string connectStringKey)
        {
			this.service = new QmcLedgerKeyValueService(connectStringKey);
            base.BaseService = this.service;
        }

        public QmcLedgerKeyValueManager(NBear.Data.Gateway way)
        {
			this.service = new QmcLedgerKeyValueService(way);
            base.BaseService = this.service;
        }

        #endregion

        public string GetNextValueId()
        {
            return this.service.GetNextValueId();
        }
    }
}
