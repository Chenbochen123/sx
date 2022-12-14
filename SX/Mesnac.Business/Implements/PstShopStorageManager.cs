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
    public class PstShopStorageManager : BaseManager<PstShopStorage>, IPstShopStorageManager
    {
		#region 属性注入与构造方法
		
        private IPstShopStorageService service;

        public PstShopStorageManager()
        {
            this.service = new PstShopStorageService();
            base.BaseService = this.service;
        }

		public PstShopStorageManager(string connectStringKey)
        {
			this.service = new PstShopStorageService(connectStringKey);
            base.BaseService = this.service;
        }

        public PstShopStorageManager(NBear.Data.Gateway way)
        {
			this.service = new PstShopStorageService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region 查询条件类定义
        public class QueryParams : PstShopStorageService.QueryParams
        {
        }
        #endregion

        public PageResult<PstShopStorage> GetTablePageDataBySql(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }

        public PageResult<PstShopStorage> GetTablePageDataBySql1(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql1(queryParams);
        }

        public DataSet GetStoreOutData()
        {
            return this.service.GetStoreOutData();
        }

        public PstShopStorage getPstShopStorage(string Barcode, string StorageID, string StoragePlaceID, string MaterCode)
        {
            return this.service.getPstShopStorage(Barcode, StorageID, StoragePlaceID, MaterCode);
        }

        public PstShopStorage getPstShopStorage(string Barcode, string StorageID, string StoragePlaceID, string SourceBillNo, string SourceOrderID)
        {
            return this.service.getPstShopStorage(Barcode, StorageID, StoragePlaceID, SourceBillNo, SourceOrderID);
        }

        public DataSet GetShopStorage(string Barcodes)
        {
            return this.service.GetShopStorage(Barcodes);
        }

        public DataSet GetShopStorageTotal(QueryParams queryParams)
        {
            return this.service.GetShopStorageTotal(queryParams);
        }

        public string GetNewBarcode(string barcode, string storageID, string storagePlaceID)
        {
            return this.service.GetNewBarcode(barcode, storageID, storagePlaceID);
        }
    }
}
