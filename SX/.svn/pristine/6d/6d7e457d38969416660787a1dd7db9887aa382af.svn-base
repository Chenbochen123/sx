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
    public class BasMaterialMajorTypeManager : BaseManager<BasMaterialMajorType>, IBasMaterialMajorTypeManager
    {
		#region 属性注入与构造方法
		
        private IBasMaterialMajorTypeService service;

        public BasMaterialMajorTypeManager()
        {
            this.service = new BasMaterialMajorTypeService();
            base.BaseService = this.service;
        }

		public BasMaterialMajorTypeManager(string connectStringKey)
        {
			this.service = new BasMaterialMajorTypeService(connectStringKey);
            base.BaseService = this.service;
        }

        public BasMaterialMajorTypeManager(NBear.Data.Gateway way)
        {
			this.service = new BasMaterialMajorTypeService(way);
            base.BaseService = this.service;
        }

        #endregion  

        #region 查询条件类定义
        public class QueryParams : BasMaterialMajorTypeService.QueryParams
        {
        }
        #endregion

        public PageResult<BasMaterialMajorType> GetTablePageDataBySql(Mesnac.Data.Implements.BasMaterialMajorTypeService.QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }
        public string GetNextMaterialMajorTypeCode()
        {
            return this.service.GetNextMaterialMajorTypeCode();
        }
    }
}
