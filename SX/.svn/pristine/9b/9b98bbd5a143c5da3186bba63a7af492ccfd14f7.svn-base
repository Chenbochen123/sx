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
    public class PpmRubberAdjustingManager : BaseManager<PpmRubberAdjusting>, IPpmRubberAdjustingManager
    {
		#region 属性注入与构造方法
		
        private IPpmRubberAdjustingService service;

        public PpmRubberAdjustingManager()
        {
            this.service = new PpmRubberAdjustingService();
            base.BaseService = this.service;
        }

		public PpmRubberAdjustingManager(string connectStringKey)
        {
			this.service = new PpmRubberAdjustingService(connectStringKey);
            base.BaseService = this.service;
        }

        public PpmRubberAdjustingManager(NBear.Data.Gateway way)
        {
			this.service = new PpmRubberAdjustingService(way);
            base.BaseService = this.service;
        }

        #endregion

        public class QueryParams : PpmRubberAdjustingService.QueryParams
        {
        }

        public PageResult<PpmRubberAdjusting> GetTablePageDataBySql(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }

        public string GetBillNo()
        {
            return this.service.GetBillNo();
        }

        public bool UpdateChkResultFlag(string StrBillNo, string ChkPerson)
        {
            return this.service.UpdateChkResultFlag(StrBillNo, ChkPerson);
        }
    }
}
