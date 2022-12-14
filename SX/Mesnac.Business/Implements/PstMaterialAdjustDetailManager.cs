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
    public class PstMaterialAdjustDetailManager : BaseManager<PstMaterialAdjustDetail>, IPstMaterialAdjustDetailManager
    {
		#region 属性注入与构造方法
		
        private IPstMaterialAdjustDetailService service;

        public PstMaterialAdjustDetailManager()
        {
            this.service = new PstMaterialAdjustDetailService();
            base.BaseService = this.service;
        }

		public PstMaterialAdjustDetailManager(string connectStringKey)
        {
			this.service = new PstMaterialAdjustDetailService(connectStringKey);
            base.BaseService = this.service;
        }

        public PstMaterialAdjustDetailManager(NBear.Data.Gateway way)
        {
			this.service = new PstMaterialAdjustDetailService(way);
            base.BaseService = this.service;
        }

        #endregion

        public class QueryParams : PstMaterialAdjustDetailService.QueryParams
        {
        }

        public PageResult<PstMaterialAdjustDetail> GetTablePageDataBySql(QueryParams queryParams)
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

        public DataSet GetDetailInfo(string PlanDate)
        {
            return this.service.GetDetailInfo(PlanDate);
        }
    }
}
