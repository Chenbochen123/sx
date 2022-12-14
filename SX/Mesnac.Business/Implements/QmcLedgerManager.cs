using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
using System.Data;
    public class QmcLedgerManager : BaseManager<QmcLedger>, IQmcLedgerManager
    {
		#region 属性注入与构造方法
		
        private IQmcLedgerService service;

        public QmcLedgerManager()
        {
            this.service = new QmcLedgerService();
            base.BaseService = this.service;
        }

		public QmcLedgerManager(string connectStringKey)
        {
			this.service = new QmcLedgerService(connectStringKey);
            base.BaseService = this.service;
        }

        public QmcLedgerManager(NBear.Data.Gateway way)
        {
			this.service = new QmcLedgerService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region 查询条件类定义
        public class QueryParams : QmcLedgerService.QueryParams
        {
        }
        #endregion

        public DataTable GetLedgerUnion()
        {
            return this.service.GetLedgerUnion();
        }

        public DataTable GetLedgerUnion(QmcLedgerService.QueryParams param)
        {
            return this.service.GetLedgerUnion(param);
        }

        public DataTable GetReport(QmcLedgerService.QueryParams param)
        {
            return this.service.GetReport(param);
        }

        public string GetNextLedgerId()
        {
            return this.service.GetNextLedgerId();
        }
    }
}
