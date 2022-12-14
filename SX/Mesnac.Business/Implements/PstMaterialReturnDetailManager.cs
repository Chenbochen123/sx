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
    public class PstMaterialReturnDetailManager : BaseManager<PstMaterialReturnDetail>, IPstMaterialReturnDetailManager
    {
		#region 属性注入与构造方法
		
        private IPstMaterialReturnDetailService service;

        public PstMaterialReturnDetailManager()
        {
            this.service = new PstMaterialReturnDetailService();
            base.BaseService = this.service;
        }

		public PstMaterialReturnDetailManager(string connectStringKey)
        {
			this.service = new PstMaterialReturnDetailService(connectStringKey);
            base.BaseService = this.service;
        }

        public PstMaterialReturnDetailManager(NBear.Data.Gateway way)
        {
			this.service = new PstMaterialReturnDetailService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region 查询条件类定义
        public class QueryParams : PstMaterialReturnDetailService.QueryParams
        {
        }
        #endregion

        public PageResult<PstMaterialReturnDetail> GetTablePageDataBySql(QueryParams queryParams)
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
