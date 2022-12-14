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
    public class BasRubTypeManager : BaseManager<BasRubType>, IBasRubTypeManager
    {
		#region 属性注入与构造方法
		
        private IBasRubTypeService service;

        public BasRubTypeManager()
        {
            this.service = new BasRubTypeService();
            base.BaseService = this.service;
        }

		public BasRubTypeManager(string connectStringKey)
        {
			this.service = new BasRubTypeService(connectStringKey);
            base.BaseService = this.service;
        }

        public BasRubTypeManager(NBear.Data.Gateway way)
        {
			this.service = new BasRubTypeService(way);
            base.BaseService = this.service;
        }

        #endregion
        #region 查询条件类定义
        public class QueryParams : BasRubTypeService.QueryParams
        {
        }
        #endregion

        public PageResult<BasRubType> GetTablePageDataBySql(Mesnac.Data.Implements.BasRubTypeService.QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }
        public string GetNextRubTypeCode()
        {
            return this.service.GetNextRubTypeCode();
        }
    }
}
