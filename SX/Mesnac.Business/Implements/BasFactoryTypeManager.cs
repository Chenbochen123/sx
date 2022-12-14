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
    public class BasFactoryTypeManager : BaseManager<BasFactoryType>, IBasFactoryTypeManager
    {
		#region 属性注入与构造方法
		
        private IBasFactoryTypeService service;

        public BasFactoryTypeManager()
        {
            this.service = new BasFactoryTypeService();
            base.BaseService = this.service;
        }

		public BasFactoryTypeManager(string connectStringKey)
        {
			this.service = new BasFactoryTypeService(connectStringKey);
            base.BaseService = this.service;
        }

        public BasFactoryTypeManager(NBear.Data.Gateway way)
        {
			this.service = new BasFactoryTypeService(way);
            base.BaseService = this.service;
        }

        #endregion
        #region 查询条件类定义
        public class QueryParams : BasFactoryTypeService.QueryParams
        {
        }
        #endregion
        public PageResult<BasFactoryType> GetTablePageDataBySql(Mesnac.Data.Implements.BasFactoryTypeService.QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }
        public string GetNextFactoryTypeCode()
        {
            return this.service.GetNextFactoryTypeCode();
        }
    }
}
