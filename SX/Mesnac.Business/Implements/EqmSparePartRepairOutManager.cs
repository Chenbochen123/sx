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
    public class EqmSparePartRepairOutManager : BaseManager<EqmSparePartRepairOut>, IEqmSparePartRepairOutManager
    {
		#region 属性注入与构造方法
		
        private IEqmSparePartRepairOutService service;

        public EqmSparePartRepairOutManager()
        {
            this.service = new EqmSparePartRepairOutService();
            base.BaseService = this.service;
        }

		public EqmSparePartRepairOutManager(string connectStringKey)
        {
			this.service = new EqmSparePartRepairOutService(connectStringKey);
            base.BaseService = this.service;
        }

        public EqmSparePartRepairOutManager(NBear.Data.Gateway way)
        {
			this.service = new EqmSparePartRepairOutService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region 查询条件类定义
        public class QueryParams : EqmSparePartRepairOutService.QueryParams
        {
        }
        #endregion
        public PageResult<EqmSparePartRepairOut> GetTablePageDataBySql(Mesnac.Data.Implements.EqmSparePartRepairOutService.QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }

        public string GetNextSparePartStoreOutCode(DateTime storeOutDate)
        {
            return this.service.GetNextSparePartStoreOutCode(storeOutDate);
        }
    }
}
