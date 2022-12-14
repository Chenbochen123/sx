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
    public class PstShopStorageDetailManager : BaseManager<PstShopStorageDetail>, IPstShopStorageDetailManager
    {
		#region 属性注入与构造方法
		
        private IPstShopStorageDetailService service;

        public PstShopStorageDetailManager()
        {
            this.service = new PstShopStorageDetailService();
            base.BaseService = this.service;
        }

		public PstShopStorageDetailManager(string connectStringKey)
        {
			this.service = new PstShopStorageDetailService(connectStringKey);
            base.BaseService = this.service;
        }

        public PstShopStorageDetailManager(NBear.Data.Gateway way)
        {
			this.service = new PstShopStorageDetailService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region 查询条件类定义
        public class QueryParams : PstShopStorageDetailService.QueryParams
        {
        }
        #endregion

        public PageResult<PstShopStorageDetail> GetTablePageDataBySqlPrint(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySqlPrint(queryParams);
        }

        public DataSet GetByPrintInfo(string Barcode, string StorageID, string StoragePlaceID, string OrderID)
        {
            return this.service.GetByPrintInfo(Barcode, StorageID, StoragePlaceID, OrderID);
        }

        public DataSet GetByInfo(string Barcode, string StorageID, string StoragePlaceID)
        {
            return this.service.GetByInfo(Barcode, StorageID, StoragePlaceID);
        }
        public DataSet GetByInfo(string Barcode, string StorageID, string StoragePlaceID,string boxcode)
        {
            return this.service.GetByInfo(Barcode, StorageID, StoragePlaceID,boxcode);
        }
        public int GetOrderID(string Barcode)
        {
            return this.service.GetOrderID(Barcode);
        }

        public DataSet GetSqlInfo(string sql)
        {
            return this.service.GetSqlInfo(sql);
        }
    }
}
