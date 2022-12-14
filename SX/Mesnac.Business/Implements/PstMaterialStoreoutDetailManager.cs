using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    using Mesnac.Data.Components;
    public class PstMaterialStoreoutDetailManager : BaseManager<PstMaterialStoreoutDetail>, IPstMaterialStoreoutDetailManager
    {
		#region 属性注入与构造方法
		
        private IPstMaterialStoreoutDetailService service;

        public PstMaterialStoreoutDetailManager()
        {
            this.service = new PstMaterialStoreoutDetailService();
            base.BaseService = this.service;
        }

		public PstMaterialStoreoutDetailManager(string connectStringKey)
        {
			this.service = new PstMaterialStoreoutDetailService(connectStringKey);
            base.BaseService = this.service;
        }

        public PstMaterialStoreoutDetailManager(NBear.Data.Gateway way)
        {
			this.service = new PstMaterialStoreoutDetailService(way);
            base.BaseService = this.service;
        }

        #endregion

        public class QueryParams : PstMaterialStoreoutDetailService.QueryParams
        {
        }

        public PageResult<PstMaterialStoreoutDetail> GetTablePageDataBySql(QueryParams queryParams)
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
