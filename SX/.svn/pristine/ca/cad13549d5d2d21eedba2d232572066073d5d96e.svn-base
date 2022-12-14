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
    public class PpmRubberStoreinDetailManager : BaseManager<PpmRubberStoreinDetail>, IPpmRubberStoreinDetailManager
    {
		#region 属性注入与构造方法
		
        private IPpmRubberStoreinDetailService service;

        public PpmRubberStoreinDetailManager()
        {
            this.service = new PpmRubberStoreinDetailService();
            base.BaseService = this.service;
        }

		public PpmRubberStoreinDetailManager(string connectStringKey)
        {
			this.service = new PpmRubberStoreinDetailService(connectStringKey);
            base.BaseService = this.service;
        }

        public PpmRubberStoreinDetailManager(NBear.Data.Gateway way)
        {
			this.service = new PpmRubberStoreinDetailService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region 查询条件类定义
        public class QueryParams : PpmRubberStoreinDetailService.QueryParams
        {
        }
        #endregion

        public PageResult<PpmRubberStoreinDetail> GetTablePageDataBySql(QueryParams queryParams)
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

        public DataSet GetFromChkdetail(string BillNo)
        {
            return this.service.GetFromChkdetail(BillNo);
        }

        public bool UpdateStorage(string BillNo, string StorageID, string StoragePlaceID)
        {
            return this.service.UpdateStorage(BillNo, StorageID, StoragePlaceID);
        }

        public PpmRubberStoreinDetail GetStoreinDetail(string BillNo, string Barcode)
        {
            return this.service.GetStoreinDetail(BillNo, Barcode);
        }
    }
}
