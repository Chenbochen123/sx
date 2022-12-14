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
    public class BasStorageFlowManager : BaseManager<BasStorageFlow>, IBasStorageFlowManager
    {
		#region 属性注入与构造方法
		
        private IBasStorageFlowService service;

        public BasStorageFlowManager()
        {
            this.service = new BasStorageFlowService();
            base.BaseService = this.service;
        }

		public BasStorageFlowManager(string connectStringKey)
        {
			this.service = new BasStorageFlowService(connectStringKey);
            base.BaseService = this.service;
        }

        public BasStorageFlowManager(NBear.Data.Gateway way)
        {
			this.service = new BasStorageFlowService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region 查询条件类定义
        public class QueryParams : BasStorageFlowService.QueryParams
        {
        }
        #endregion

        public PageResult<BasStorageFlow> GetTablePageDataBySql(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }
    }
}
