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
    public class PpmRubberAdjustDetailManager : BaseManager<PpmRubberAdjustDetail>, IPpmRubberAdjustDetailManager
    {
		#region 属性注入与构造方法
		
        private IPpmRubberAdjustDetailService service;

        public PpmRubberAdjustDetailManager()
        {
            this.service = new PpmRubberAdjustDetailService();
            base.BaseService = this.service;
        }

		public PpmRubberAdjustDetailManager(string connectStringKey)
        {
			this.service = new PpmRubberAdjustDetailService(connectStringKey);
            base.BaseService = this.service;
        }

        public PpmRubberAdjustDetailManager(NBear.Data.Gateway way)
        {
			this.service = new PpmRubberAdjustDetailService(way);
            base.BaseService = this.service;
        }

        #endregion

        public class QueryParams : PpmRubberAdjustDetailService.QueryParams
        {
        }

        public PageResult<PpmRubberAdjustDetail> GetTablePageDataBySql(QueryParams queryParams)
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
