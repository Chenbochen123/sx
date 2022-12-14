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
    public class BasDeptManager : BaseManager<BasDept>, IBasDeptManager
    {
		#region 属性注入与构造方法
		
        private IBasDeptService service;

        public BasDeptManager()
        {
            this.service = new BasDeptService();
            base.BaseService = this.service;
        }

		public BasDeptManager(string connectStringKey)
        {
			this.service = new BasDeptService(connectStringKey);
            base.BaseService = this.service;
        }

        public BasDeptManager(NBear.Data.Gateway way)
        {
			this.service = new BasDeptService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region 查询条件类定义
        public class QueryParams : BasDeptService.QueryParams
        {
        }
        #endregion

        public PageResult<BasDept> GetTablePageDataBySql(Mesnac.Data.Implements.BasDeptService.QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }
        public string GetNextDepCode() {
            return this.service.GetNextDepCode();
        }
    }
}
