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
    using System.Data;
    public class BasWorkUserInfoManager : BaseManager<BasWorkUserInfo>, IBasWorkUserInfoManager
    {
		#region 属性注入与构造方法
		
        private IBasWorkUserInfoService service;

        public BasWorkUserInfoManager()
        {
            this.service = new BasWorkUserInfoService();
            base.BaseService = this.service;
        }

		public BasWorkUserInfoManager(string connectStringKey)
        {
			this.service = new BasWorkUserInfoService(connectStringKey);
            base.BaseService = this.service;
        }

        public BasWorkUserInfoManager(NBear.Data.Gateway way)
        {
			this.service = new BasWorkUserInfoService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region 查询条件类定义
        public class QueryParams : BasWorkUserInfoService.QueryParams
        {
        }
        #endregion
        public PageResult<BasWorkUserInfo> GetTablePageDataBySql(Mesnac.Data.Implements.BasWorkUserInfoService.QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }
        public string GetNextObjID()
        {
            return this.service.GetNextObjID();
        }

        public DataSet UserQueryByCode(Mesnac.Data.Implements.BasWorkUserInfoService.QueryParams queryParams)
        {
            return this.service.UserQueryByCode(queryParams);
        }
    }
}
