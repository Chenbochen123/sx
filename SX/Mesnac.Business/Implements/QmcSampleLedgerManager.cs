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
    public class QmcSampleLedgerManager : BaseManager<QmcSampleLedger>, IQmcSampleLedgerManager
    {
		#region 属性注入与构造方法
		
        private IQmcSampleLedgerService service;

        public QmcSampleLedgerManager()
        {
            this.service = new QmcSampleLedgerService();
            base.BaseService = this.service;
        }

		public QmcSampleLedgerManager(string connectStringKey)
        {
			this.service = new QmcSampleLedgerService(connectStringKey);
            base.BaseService = this.service;
        }

        public QmcSampleLedgerManager(NBear.Data.Gateway way)
        {
			this.service = new QmcSampleLedgerService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region 查询条件类定义
        public class QueryParams : QmcSampleLedgerService.QueryParams
        {
        }
        #endregion

        public DataTable GetLedgerUnion()
        {
            return this.service.GetLedgerUnion();
        }

        public DataTable GetLedgerUnion(QmcSampleLedgerService.QueryParams param)
        {
            return this.service.GetLedgerUnion(param);
        }

        public string GetNextLedgerId()
        {
            return this.service.GetNextLedgerId();
        }

        public string GetAutoFlowSampleCode()
        {
            return this.service.GetAutoFlowSampleCode();
        }
    }
}
