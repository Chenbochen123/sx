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
    public class BasWorkCoefficientManager : BaseManager<BasWorkCoefficient>, IBasWorkCoefficientManager
    {
		#region 属性注入与构造方法
		
        private IBasWorkCoefficientService service;

        public BasWorkCoefficientManager()
        {
            this.service = new BasWorkCoefficientService();
            base.BaseService = this.service;
        }

		public BasWorkCoefficientManager(string connectStringKey)
        {
			this.service = new BasWorkCoefficientService(connectStringKey);
            base.BaseService = this.service;
        }

        public BasWorkCoefficientManager(NBear.Data.Gateway way)
        {
			this.service = new BasWorkCoefficientService(way);
            base.BaseService = this.service;
        }

        #endregion
        #region 查询条件类定义
        public class QueryParams : BasWorkCoefficientService.QueryParams
        {
        }
        #endregion
        public PageResult<BasWorkCoefficient> GetTablePageDataBySql(Mesnac.Data.Implements.BasWorkCoefficientService.QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }
        public string GetNextObjID()
        {
            return this.service.GetNextObjID();
        }
    }
}
