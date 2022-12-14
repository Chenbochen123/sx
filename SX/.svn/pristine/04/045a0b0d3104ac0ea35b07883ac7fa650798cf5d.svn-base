using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    using Mesnac.Data.Components;
    public class QmcLedgerKeyManager : BaseManager<QmcLedgerKey>, IQmcLedgerKeyManager
    {
		#region 属性注入与构造方法
		
        private IQmcLedgerKeyService service;

        public QmcLedgerKeyManager()
        {
            this.service = new QmcLedgerKeyService();
            base.BaseService = this.service;
        }

		public QmcLedgerKeyManager(string connectStringKey)
        {
			this.service = new QmcLedgerKeyService(connectStringKey);
            base.BaseService = this.service;
        }

        public QmcLedgerKeyManager(NBear.Data.Gateway way)
        {
			this.service = new QmcLedgerKeyService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region 查询条件类定义
        public class QueryParams : QmcLedgerKeyService.QueryParams
        {
        }
        #endregion

        public PageResult<QmcLedgerKey> GetTablePageDataBySql(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }

        public string GetNextKeyId()
        {
            return this.service.GetNextKeyId();
        }
    }
}
