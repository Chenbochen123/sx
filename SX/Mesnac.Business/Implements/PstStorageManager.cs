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
    public class PstStorageManager : BaseManager<PstStorage>, IPstStorageManager
    {
		#region 属性注入与构造方法
		
        private IPstStorageService service;

        public PstStorageManager()
        {
            this.service = new PstStorageService();
            base.BaseService = this.service;
        }

		public PstStorageManager(string connectStringKey)
        {
			this.service = new PstStorageService(connectStringKey);
            base.BaseService = this.service;
        }

        public PstStorageManager(NBear.Data.Gateway way)
        {
			this.service = new PstStorageService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region 查询条件类定义
        public class QueryParams : PstStorageService.QueryParams
        {
        }
        #endregion

        public PageResult<PstStorage> GetTablePageDataBySql(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }

        public PageResult<PstStorage> GetTablePageDataBySql1(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql1(queryParams);
        }

        public PageResult<PstStorage> GetTablePageDataBySql2(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql2(queryParams);
        }

        public DataSet GetStorageInfo(string StorageID, string StoragePlaceID, string MaterCode)
        {
            return this.service.GetStorageInfo(StorageID, StoragePlaceID, MaterCode);
        }

        public DataSet GetStoreOutData()
        {
            return this.service.GetStoreOutData();
        }

        public PstStorage getPstStorage(string Barcode, string StorageID, string StoragePlaceID, string MaterCode)
        {
            return this.service.getPstStorage(Barcode, StorageID, StoragePlaceID, MaterCode);
        }

        public DataSet GetStorage(string Barcodes)
        {
            return this.service.GetStorage(Barcodes);
        }

        public DataSet GetStorageTotal(QueryParams queryParams)
        {
            return this.service.GetStorageTotal(queryParams);
        }
    }
}
