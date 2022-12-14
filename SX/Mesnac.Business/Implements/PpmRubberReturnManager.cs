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
    public class PpmRubberReturnManager : BaseManager<PpmRubberReturn>, IPpmRubberReturnManager
    {
		#region 属性注入与构造方法
		
        private IPpmRubberReturnService service;

        public PpmRubberReturnManager()
        {
            this.service = new PpmRubberReturnService();
            base.BaseService = this.service;
        }

		public PpmRubberReturnManager(string connectStringKey)
        {
			this.service = new PpmRubberReturnService(connectStringKey);
            base.BaseService = this.service;
        }

        public PpmRubberReturnManager(NBear.Data.Gateway way)
        {
			this.service = new PpmRubberReturnService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region 查询条件类定义
        public class QueryParams : PpmRubberReturnService.QueryParams
        {
        }
        #endregion

        public PageResult<PpmRubberReturn> GetTablePageDataBySql(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }

        public string GetBillNo()
        {
            return this.service.GetBillNo();
        }
    }
}
