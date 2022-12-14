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
    public class BasMaterialMinorTypeManager : BaseManager<BasMaterialMinorType>, IBasMaterialMinorTypeManager
    {
		#region 属性注入与构造方法
		
        private IBasMaterialMinorTypeService service;

        public BasMaterialMinorTypeManager()
        {
            this.service = new BasMaterialMinorTypeService();
            base.BaseService = this.service;
        }

		public BasMaterialMinorTypeManager(string connectStringKey)
        {
			this.service = new BasMaterialMinorTypeService(connectStringKey);
            base.BaseService = this.service;
        }

        public BasMaterialMinorTypeManager(NBear.Data.Gateway way)
        {
			this.service = new BasMaterialMinorTypeService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region 查询条件类定义
        public class QueryParams : BasMaterialMinorTypeService.QueryParams
        {
        }
        #endregion

        public PageResult<BasMaterialMinorType> GetTablePageDataBySql(Mesnac.Data.Implements.BasMaterialMinorTypeService.QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }
        public PageResult<BasMaterialMinorType> GetQueryRubSectDataPageBySql(Mesnac.Data.Implements.BasMaterialMinorTypeService.QueryParams queryParams)
        {
            return this.service.GetQueryRubSectDataPageBySql(queryParams);
        }
        public string GetNextMaterialMinorTypeCode(string majorid)
        {
            return this.service.GetNextMaterialMinorTypeCode(majorid);
        }
        public string GetNextMaterialMinorObjIDCode() {
            return this.service.GetNextMaterialMinorObjIDCode();
        }
    }
}
