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
    public class BasStorageManager : BaseManager<BasStorage>, IBasStorageManager
    {
		#region 属性注入与构造方法
		
        private IBasStorageService service;

        public BasStorageManager()
        {
            this.service = new BasStorageService();
            base.BaseService = this.service;
        }

		public BasStorageManager(string connectStringKey)
        {
			this.service = new BasStorageService(connectStringKey);
            base.BaseService = this.service;
        }

        public BasStorageManager(NBear.Data.Gateway way)
        {
			this.service = new BasStorageService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region 查询条件类定义
        public class QueryParams : BasStorageService.QueryParams
        {
        }
        #endregion

        public PageResult<BasStorage> GetTablePageDataBySql(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }

        public DataSet GetStorageID(string StorageID)
        {
            return this.service.GetStorageID(StorageID);
        }

        public bool UpdateUsing(string IDS)
        {
            return this.service.UpdateUsing(IDS);
        }

        public void UpdateLastStorageFlag(string ObjID)
        {
            this.service.UpdateLastStorageFlag(ObjID);
        }

        public string GetStorageName(string StorageID)
        {
            return this.service.GetStorageName(StorageID);
        }

        public string IsStoreIn(string StorageID)
        {
            return this.service.IsStoreIn(StorageID);
        }

        public DataSet GetDuration(string StorageID)
        {
            return this.service.GetDuration(StorageID);
        }

        public DataSet GetStorageInfo(string storageType, string lastStorageFlag)
        {
            return this.service.GetStorageInfo(storageType, lastStorageFlag);
        }

        public DataSet GetStorageStr(string storageID)
        {
            return this.service.GetStorageStr(storageID);
        }
    }
}
