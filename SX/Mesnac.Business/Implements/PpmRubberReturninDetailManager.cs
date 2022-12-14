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
    public class PpmRubberReturninDetailManager : BaseManager<PpmRubberReturninDetail>, IPpmRubberReturninDetailManager
    {
		#region 属性注入与构造方法
		
        private IPpmRubberReturninDetailService service;

        public PpmRubberReturninDetailManager()
        {
            this.service = new PpmRubberReturninDetailService();
            base.BaseService = this.service;
        }

		public PpmRubberReturninDetailManager(string connectStringKey)
        {
			this.service = new PpmRubberReturninDetailService(connectStringKey);
            base.BaseService = this.service;
        }

        public PpmRubberReturninDetailManager(NBear.Data.Gateway way)
        {
			this.service = new PpmRubberReturninDetailService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region 查询条件类定义
        public class QueryParams : PpmRubberReturninDetailService.QueryParams
        {
        }
        #endregion

        public PageResult<PpmRubberReturninDetail> GetTablePageDataBySql(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }

        public DataSet GetByBillNo(string BillNo)
        {
            return this.service.GetByBillNo(BillNo);
        }

        public DataSet GetByOtherBillNo(string BillNo, string Barcode, string OrderID)
        {
            return this.service.GetByOtherBillNo(BillNo, Barcode, OrderID);
        }
    }
}
