using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    using NBear.Common;
    public class PpmInventoryManager : BaseManager<PpmInventory>, IPpmInventoryManager
    {
		#region 属性注入与构造方法
		
        private IPpmInventoryService service;

        public PpmInventoryManager()
        {
            this.service = new PpmInventoryService();
            base.BaseService = this.service;
        }

		public PpmInventoryManager(string connectStringKey)
        {
			this.service = new PpmInventoryService(connectStringKey);
            base.BaseService = this.service;
        }

        public PpmInventoryManager(NBear.Data.Gateway way)
        {
			this.service = new PpmInventoryService(way);
            base.BaseService = this.service;
        }

        #endregion

        public class QueryParams : PpmInventoryService.QueryParams
        {
        }

        public PageResult<PpmInventory> GetTablePageDataBySql(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }

        public DataSet GetInverntoryEndDate(DateTime BeginDate, DateTime EndDate, string CheJian)
        {
            return this.service.GetInverntoryEndDate(BeginDate, EndDate, CheJian);
        }
    }
}
