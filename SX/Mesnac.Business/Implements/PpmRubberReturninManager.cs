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
    public class PpmRubberReturninManager : BaseManager<PpmRubberReturnin>, IPpmRubberReturninManager
    {
		#region ����ע���빹�췽��
		
        private IPpmRubberReturninService service;

        public PpmRubberReturninManager()
        {
            this.service = new PpmRubberReturninService();
            base.BaseService = this.service;
        }

		public PpmRubberReturninManager(string connectStringKey)
        {
			this.service = new PpmRubberReturninService(connectStringKey);
            base.BaseService = this.service;
        }

        public PpmRubberReturninManager(NBear.Data.Gateway way)
        {
			this.service = new PpmRubberReturninService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region ��ѯ�����ඨ��
        public class QueryParams : PpmRubberReturninService.QueryParams
        {
        }
        #endregion

        public PageResult<PpmRubberReturnin> GetTablePageDataBySql(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }

        public string GetBillNo()
        {
            return this.service.GetBillNo();
        }

        public bool UpdateChkResultFlag(string StrBillNo, string UserID)
        {
            return this.service.UpdateChkResultFlag(StrBillNo, UserID);
        }

        public bool CancelChkResult(string StrBillNo, string UserID)
        {
            return this.service.CancelChkResult(StrBillNo, UserID);
        }
    }
}
