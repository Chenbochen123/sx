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
    public class PpmRubberInventoryDetailManager : BaseManager<PpmRubberInventoryDetail>, IPpmRubberInventoryDetailManager
    {
		#region 属性注入与构造方法
		
        private IPpmRubberInventoryDetailService service;

        public PpmRubberInventoryDetailManager()
        {
            this.service = new PpmRubberInventoryDetailService();
            base.BaseService = this.service;
        }

		public PpmRubberInventoryDetailManager(string connectStringKey)
        {
			this.service = new PpmRubberInventoryDetailService(connectStringKey);
            base.BaseService = this.service;
        }

        public PpmRubberInventoryDetailManager(NBear.Data.Gateway way)
        {
			this.service = new PpmRubberInventoryDetailService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region 查询条件类定义
        public class QueryParams : PpmRubberInventoryDetailService.QueryParams
        {
        }
        #endregion

        public PageResult<PpmRubberInventoryDetail> GetTablePageDataBySql(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }

        public bool GetByStorage(string billNo, string storageID, string inventoryDate)
        {
            return this.service.GetByStorage(billNo, storageID, inventoryDate);
        }

        public DataSet GetByBillNo(string BillNo)
        {
            return this.service.GetByBillNo(BillNo);
        }
    }
}
