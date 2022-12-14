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
    public class PstMaterialAdjustingManager : BaseManager<PstMaterialAdjusting>, IPstMaterialAdjustingManager
    {
		#region 属性注入与构造方法
		
        private IPstMaterialAdjustingService service;

        public PstMaterialAdjustingManager()
        {
            this.service = new PstMaterialAdjustingService();
            base.BaseService = this.service;
        }

		public PstMaterialAdjustingManager(string connectStringKey)
        {
			this.service = new PstMaterialAdjustingService(connectStringKey);
            base.BaseService = this.service;
        }

        public PstMaterialAdjustingManager(NBear.Data.Gateway way)
        {
			this.service = new PstMaterialAdjustingService(way);
            base.BaseService = this.service;
        }

        #endregion

        public class QueryParams : PstMaterialAdjustingService.QueryParams
        {
        }

        public PageResult<PstMaterialAdjusting> GetTablePageDataBySql(QueryParams queryParams)
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
