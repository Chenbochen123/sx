using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    using NBear.Common;
    public class BasMaterialStaticGroupManager : BaseManager<BasMaterialStaticGroup>, IBasMaterialStaticGroupManager
    {
		#region 属性注入与构造方法
		
        private IBasMaterialStaticGroupService service;

        public BasMaterialStaticGroupManager()
        {
            this.service = new BasMaterialStaticGroupService();
            base.BaseService = this.service;
        }

		public BasMaterialStaticGroupManager(string connectStringKey)
        {
			this.service = new BasMaterialStaticGroupService(connectStringKey);
            base.BaseService = this.service;
        }

        public BasMaterialStaticGroupManager(NBear.Data.Gateway way)
        {
			this.service = new BasMaterialStaticGroupService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region 查询条件类定义
        public class QueryParams : BasMaterialStaticGroupService.QueryParams
        {
        }
        #endregion

        public PageResult<BasMaterialStaticGroup> GetTablePageDataBySql(Mesnac.Data.Implements.BasMaterialStaticGroupService.QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }
    }
}
