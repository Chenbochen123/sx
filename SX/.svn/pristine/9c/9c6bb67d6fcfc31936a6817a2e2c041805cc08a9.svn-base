using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    using NBear.Common;
    public class PpmRubberChkManager : BaseManager<PpmRubberChk>, IPpmRubberChkManager
    {
		#region 属性注入与构造方法
		
        private IPpmRubberChkService service;
        private IPpmRubberChkDetailService detailService = new PpmRubberChkDetailService();

        public PpmRubberChkManager()
        {
            this.service = new PpmRubberChkService();
            base.BaseService = this.service;
        }

		public PpmRubberChkManager(string connectStringKey)
        {
			this.service = new PpmRubberChkService(connectStringKey);
            base.BaseService = this.service;
        }

        public PpmRubberChkManager(NBear.Data.Gateway way)
        {
			this.service = new PpmRubberChkService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region 查询条件类定义
        public class QueryParams : PpmRubberChkService.QueryParams
        {
        }
        #endregion

        public PageResult<PpmRubberChk> GetTablePageDataBySql(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }

        public string GetBillNo()
        {
            return this.service.GetBillNo();
        }

        public bool UpdateSendChkFlag(string StrBillNo, string SendPerson)
        {
            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required))
            {
                try
                {
                    this.service.UpdateSendChkFlag(StrBillNo);
                    this.detailService.UpdateSendChkFlag(StrBillNo, SendPerson);
                    scope.Complete();
                    scope.Dispose();
                    return true;
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    return false;
                }
            }
        }

        public bool CancelSendChk(string StrBillNo, string SendPerson)
        {
            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required))
            {
                try
                {
                    this.service.CancelSendChk(StrBillNo);
                    this.detailService.CancelSendChk(StrBillNo, SendPerson);
                    scope.Complete();
                    scope.Dispose();
                    return true;
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    return false;
                }
            }
        }

        public bool UpdateStockInFlag(string BillNo)
        {
            return this.service.UpdateStockInFlag(BillNo);
        }
    }
}
