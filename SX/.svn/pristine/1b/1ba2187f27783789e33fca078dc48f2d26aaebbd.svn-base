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
    public class BasMaterialStaticClassManager : BaseManager<BasMaterialStaticClass>, IBasMaterialStaticClassManager
    {
		#region 属性注入与构造方法
		
        private IBasMaterialStaticClassService service;

        public BasMaterialStaticClassManager()
        {
            this.service = new BasMaterialStaticClassService();
            base.BaseService = this.service;
        }

		public BasMaterialStaticClassManager(string connectStringKey)
        {
			this.service = new BasMaterialStaticClassService(connectStringKey);
            base.BaseService = this.service;
        }

        public BasMaterialStaticClassManager(NBear.Data.Gateway way)
        {
			this.service = new BasMaterialStaticClassService(way);
            base.BaseService = this.service;
        }

        #endregion
        #region 查询条件类定义
        public class QueryParams : BasMaterialStaticClassService.QueryParams
        {
        }
        #endregion

        public PageResult<BasMaterialStaticClass> GetTablePageDataBySql(Mesnac.Data.Implements.BasMaterialStaticClassService.QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }
        public string GetNextMaterialStaticClassCode()
        {
            return this.service.GetNextMaterialStaticClassCode();
        }
    }
}
