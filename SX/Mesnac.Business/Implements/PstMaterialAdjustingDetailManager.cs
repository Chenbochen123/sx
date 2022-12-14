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
    public class PstMaterialAdjustingDetailManager : BaseManager<PstMaterialAdjustingDetail>, IPstMaterialAdjustingDetailManager
    {
		#region 属性注入与构造方法
		
        private IPstMaterialAdjustingDetailService service;

        public PstMaterialAdjustingDetailManager()
        {
            this.service = new PstMaterialAdjustingDetailService();
            base.BaseService = this.service;
        }

		public PstMaterialAdjustingDetailManager(string connectStringKey)
        {
			this.service = new PstMaterialAdjustingDetailService(connectStringKey);
            base.BaseService = this.service;
        }

        public PstMaterialAdjustingDetailManager(NBear.Data.Gateway way)
        {
			this.service = new PstMaterialAdjustingDetailService(way);
            base.BaseService = this.service;
        }

        #endregion

        public class QueryParams : PstMaterialAdjustingDetailService.QueryParams
        {
        }

        public PageResult<PstMaterialAdjustingDetail> GetTablePageDataBySql(QueryParams queryParams)
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
