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
    public class BasDeptErpInfoManager : BaseManager<BasDeptErpInfo>, IBasDeptErpInfoManager
    {
        #region 查询条件类定义
        public class QueryParams : BasDeptErpInfoService.QueryParams
        {
        }
        #endregion
		#region 属性注入与构造方法
		
        private IBasDeptErpInfoService service;

        public BasDeptErpInfoManager()
        {
            this.service = new BasDeptErpInfoService();
            base.BaseService = this.service;
        }

		public BasDeptErpInfoManager(string connectStringKey)
        {
			this.service = new BasDeptErpInfoService(connectStringKey);
            base.BaseService = this.service;
        }

        public BasDeptErpInfoManager(NBear.Data.Gateway way)
        {
			this.service = new BasDeptErpInfoService(way);
            base.BaseService = this.service;
        }

        #endregion
        public PageResult<BasDeptErpInfo> GetTablePageDataBySql(Mesnac.Data.Implements.BasDeptErpInfoService.QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }
    }

}
