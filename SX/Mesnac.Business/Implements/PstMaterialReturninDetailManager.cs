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
    public class PstMaterialReturninDetailManager : BaseManager<PstMaterialReturninDetail>, IPstMaterialReturninDetailManager
    {
		#region 属性注入与构造方法
		
        private IPstMaterialReturninDetailService service;

        public PstMaterialReturninDetailManager()
        {
            this.service = new PstMaterialReturninDetailService();
            base.BaseService = this.service;
        }

		public PstMaterialReturninDetailManager(string connectStringKey)
        {
			this.service = new PstMaterialReturninDetailService(connectStringKey);
            base.BaseService = this.service;
        }

        public PstMaterialReturninDetailManager(NBear.Data.Gateway way)
        {
			this.service = new PstMaterialReturninDetailService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region 查询条件类定义
        public class QueryParams : PstMaterialReturninDetailService.QueryParams
        {
        }
        #endregion

        public PageResult<PstMaterialReturninDetail> GetTablePageDataBySql(QueryParams queryParams)
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
