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
    public class PpmRubberStoreoutDetailManager : BaseManager<PpmRubberStoreoutDetail>, IPpmRubberStoreoutDetailManager
    {
		#region 属性注入与构造方法
		
        private IPpmRubberStoreoutDetailService service;

        public PpmRubberStoreoutDetailManager()
        {
            this.service = new PpmRubberStoreoutDetailService();
            base.BaseService = this.service;
        }

		public PpmRubberStoreoutDetailManager(string connectStringKey)
        {
			this.service = new PpmRubberStoreoutDetailService(connectStringKey);
            base.BaseService = this.service;
        }

        public PpmRubberStoreoutDetailManager(NBear.Data.Gateway way)
        {
			this.service = new PpmRubberStoreoutDetailService(way);
            base.BaseService = this.service;
        }

        #endregion

        public class QueryParams : PpmRubberStoreoutDetailService.QueryParams
        {
        }

        public PageResult<PpmRubberStoreoutDetail> GetTablePageDataBySql(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }

        public int GetOrderID(string Barcode)
        {
            return this.service.GetOrderID(Barcode);
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
