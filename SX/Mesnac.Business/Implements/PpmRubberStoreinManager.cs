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
    public class PpmRubberStoreinManager : BaseManager<PpmRubberStorein>, IPpmRubberStoreinManager
    {
		#region 属性注入与构造方法
		
        private IPpmRubberStoreinService service;

        public PpmRubberStoreinManager()
        {
            this.service = new PpmRubberStoreinService();
            base.BaseService = this.service;
        }

		public PpmRubberStoreinManager(string connectStringKey)
        {
			this.service = new PpmRubberStoreinService(connectStringKey);
            base.BaseService = this.service;
        }

        public PpmRubberStoreinManager(NBear.Data.Gateway way)
        {
			this.service = new PpmRubberStoreinService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region 查询条件类定义
        public class QueryParams : PpmRubberStoreinService.QueryParams
        {
        }
        #endregion

        public PageResult<PpmRubberStorein> GetTablePageDataBySql(QueryParams queryParams)
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
