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
    public class BasStoragePlaceManager : BaseManager<BasStoragePlace>, IBasStoragePlaceManager
    {
		#region 属性注入与构造方法
		
        private IBasStoragePlaceService service;

        public BasStoragePlaceManager()
        {
            this.service = new BasStoragePlaceService();
            base.BaseService = this.service;
        }

		public BasStoragePlaceManager(string connectStringKey)
        {
			this.service = new BasStoragePlaceService(connectStringKey);
            base.BaseService = this.service;
        }

        public BasStoragePlaceManager(NBear.Data.Gateway way)
        {
			this.service = new BasStoragePlaceService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region 查询条件类定义
        public class QueryParams : BasStoragePlaceService.QueryParams
        {
        }
        #endregion

        public PageResult<BasStoragePlace> GetTablePageDataBySql(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }

        public void SetDefaultStoragePlace(string ObjID, string StorageID)
        {
            this.service.SetDefaultStoragePlace(ObjID, StorageID);
        }

        public void SetAutoGenDefault(string ObjID, string StorageID)
        {
            this.service.SetAutoGenDefault(ObjID, StorageID);
        }

        public DataSet GetStoragePlaceID(string StorageID)
        {
            return this.service.GetStoragePlaceID(StorageID);
        }

        public string GetStoragePlaceName(string StorageID, string StoragePlaceID)
        {
            return this.service.GetStoragePlaceName(StorageID, StoragePlaceID);
        }

        public bool UpdateLocked(string IDS)
        {
            return this.service.UpdateLocked(IDS);
        }

        public bool UpdateUsingByStorageID(string IDS)
        {
            return this.service.UpdateUsingByStorageID(IDS);
        }

        public string GetStorageType(string StoragePlaceID, string StorageID)
        {
            return this.service.GetStorageType(StoragePlaceID, StorageID);
        }

        public DataSet GetStoragePlaceInfo(string storageID)
        {
            return this.service.GetStoragePlaceInfo(storageID);
        }
    }
}
