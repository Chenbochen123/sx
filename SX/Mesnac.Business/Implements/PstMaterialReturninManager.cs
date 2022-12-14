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
    public class PstMaterialReturninManager : BaseManager<PstMaterialReturnin>, IPstMaterialReturninManager
    {
		#region 属性注入与构造方法
		
        private IPstMaterialReturninService service;

        public PstMaterialReturninManager()
        {
            this.service = new PstMaterialReturninService();
            base.BaseService = this.service;
        }

		public PstMaterialReturninManager(string connectStringKey)
        {
			this.service = new PstMaterialReturninService(connectStringKey);
            base.BaseService = this.service;
        }

        public PstMaterialReturninManager(NBear.Data.Gateway way)
        {
			this.service = new PstMaterialReturninService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region 查询条件类定义
        public class QueryParams : PstMaterialReturninService.QueryParams
        {
        }
        #endregion

        public PageResult<PstMaterialReturnin> GetTablePageDataBySql(QueryParams queryParams)
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
