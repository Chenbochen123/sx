using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    using System.Data;
    using Mesnac.Data.Components;
    public class BasStoragePlacePropManager : BaseManager<BasStoragePlaceProp>, IBasStoragePlacePropManager
    {
		#region 属性注入与构造方法
		
        private IBasStoragePlacePropService service;

        public BasStoragePlacePropManager()
        {
            this.service = new BasStoragePlacePropService();
            base.BaseService = this.service;
        }

		public BasStoragePlacePropManager(string connectStringKey)
        {
			this.service = new BasStoragePlacePropService(connectStringKey);
            base.BaseService = this.service;
        }

        public BasStoragePlacePropManager(NBear.Data.Gateway way)
        {
			this.service = new BasStoragePlacePropService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region 查询条件类定义
        public class QueryParams : BasStoragePlacePropService.QueryParams
        {
        }
        #endregion

        public PageResult<BasStoragePlaceProp> GetTablePageDataBySql(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }

        public DataSet GetStoragePlaceGroup()
        {
            return this.service.GetStoragePlaceGroup();

        }
        public DataSet GetStoragePlaceState(string storagePlace)
        {
            return this.service.GetStoragePlaceState(storagePlace);
        }
    }
}
