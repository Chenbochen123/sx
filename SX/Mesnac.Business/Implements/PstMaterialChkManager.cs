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
    public class PstMaterialChkManager : BaseManager<PstMaterialChk>, IPstMaterialChkManager
    {
		#region 属性注入与构造方法
		
        private IPstMaterialChkService service;
        private IPstMaterialChkDetailService detailService=new PstMaterialChkDetailService();

        public PstMaterialChkManager()
        {
            this.service = new PstMaterialChkService();
            base.BaseService = this.service;
        }

		public PstMaterialChkManager(string connectStringKey)
        {
			this.service = new PstMaterialChkService(connectStringKey);
            base.BaseService = this.service;
        }

        public PstMaterialChkManager(NBear.Data.Gateway way)
        {
			this.service = new PstMaterialChkService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region 查询条件类定义
        public class QueryParams : PstMaterialChkService.QueryParams
        {
        }
        #endregion

        public PageResult<PstMaterialChk> GetTablePageDataBySql(QueryParams queryParams)
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
                catch(Exception ex)
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

        public string GetFactoryID(string BillNo)
        {
            return this.service.GetFactoryID(BillNo);
        }
    }
}
