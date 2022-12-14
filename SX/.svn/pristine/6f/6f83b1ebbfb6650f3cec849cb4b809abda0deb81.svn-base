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
    public class SysRubPowerUserManager : BaseManager<SysRubPowerUser>, ISysRubPowerUserManager
    {
		#region 属性注入与构造方法
		
        private ISysRubPowerUserService service;

        public SysRubPowerUserManager()
        {
            this.service = new SysRubPowerUserService();
            base.BaseService = this.service;
        }

		public SysRubPowerUserManager(string connectStringKey)
        {
			this.service = new SysRubPowerUserService(connectStringKey);
            base.BaseService = this.service;
        }

        public SysRubPowerUserManager(NBear.Data.Gateway way)
        {
			this.service = new SysRubPowerUserService(way);
            base.BaseService = this.service;
        }

        #endregion


        #region 查询条件类定义
        public class QueryParams : SysRubPowerUserService.QueryParams
        {
        }
        #endregion

        public PageResult<SysRubPowerUser> GetTablePageDataBySql(Mesnac.Data.Implements.SysRubPowerUserService.QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }
    }
}
