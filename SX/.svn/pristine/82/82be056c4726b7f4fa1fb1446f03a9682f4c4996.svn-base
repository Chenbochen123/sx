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
    public class PpmRubberAdjustingDetailManager : BaseManager<PpmRubberAdjustingDetail>, IPpmRubberAdjustingDetailManager
    {
		#region 属性注入与构造方法
		
        private IPpmRubberAdjustingDetailService service;

        public PpmRubberAdjustingDetailManager()
        {
            this.service = new PpmRubberAdjustingDetailService();
            base.BaseService = this.service;
        }

		public PpmRubberAdjustingDetailManager(string connectStringKey)
        {
			this.service = new PpmRubberAdjustingDetailService(connectStringKey);
            base.BaseService = this.service;
        }

        public PpmRubberAdjustingDetailManager(NBear.Data.Gateway way)
        {
			this.service = new PpmRubberAdjustingDetailService(way);
            base.BaseService = this.service;
        }

        #endregion

        public class QueryParams : PpmRubberAdjustingDetailService.QueryParams
        {
        }

        public PageResult<PpmRubberAdjustingDetail> GetTablePageDataBySql(QueryParams queryParams)
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
