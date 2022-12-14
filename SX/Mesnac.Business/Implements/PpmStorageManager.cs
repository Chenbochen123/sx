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
    public class PpmStorageManager : BaseManager<PpmStorage>, IPpmStorageManager
    {
		#region 属性注入与构造方法
		
        private IPpmStorageService service;

        public PpmStorageManager()
        {
            this.service = new PpmStorageService();
            base.BaseService = this.service;
        }

		public PpmStorageManager(string connectStringKey)
        {
			this.service = new PpmStorageService(connectStringKey);
            base.BaseService = this.service;
        }

        public PpmStorageManager(NBear.Data.Gateway way)
        {
			this.service = new PpmStorageService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region 查询条件类定义
        public class QueryParams : PpmStorageService.QueryParams
        {
        }
        #endregion

        public PageResult<PpmStorage> GetTablePageDataBySql(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }

        public PageResult<PpmStorage> GetTablePageDataBySql1(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql1(queryParams);
        }

        public DataSet GetStoreOutData()
        {
            return this.service.GetStoreOutData();
        }

        public PpmStorage getPpmStorage(string Barcode, string StorageID, string StoragePlaceID, string MaterCode)
        {
            return this.service.getPpmStorage(Barcode, StorageID, StoragePlaceID, MaterCode);
        }

        public DataSet GetStorage(string Barcodes)
        {
            return this.service.GetStorage(Barcodes);
        }
    }
}
