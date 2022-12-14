using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    using Mesnac.Data.Components;
    public class QmcFactoryMappingManager : BaseManager<QmcFactoryMapping>, IQmcFactoryMappingManager
    {
		#region 属性注入与构造方法
		
        private IQmcFactoryMappingService service;

        public QmcFactoryMappingManager()
        {
            this.service = new QmcFactoryMappingService();
            base.BaseService = this.service;
        }

		public QmcFactoryMappingManager(string connectStringKey)
        {
			this.service = new QmcFactoryMappingService(connectStringKey);
            base.BaseService = this.service;
        }

        public QmcFactoryMappingManager(NBear.Data.Gateway way)
        {
			this.service = new QmcFactoryMappingService(way);
            base.BaseService = this.service;
        }

        #endregion


        #region 查询条件类定义
        public class QueryParams : QmcFactoryMappingService.QueryParams
        {
        }
        #endregion

        public PageResult<QmcFactoryMapping> GetTablePageDataBySql(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }

        public string GetNextMappingId()
        {
            return this.service.GetNextMappingId();
        }
    }
}
