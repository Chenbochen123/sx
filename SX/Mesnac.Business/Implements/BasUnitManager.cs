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
    public class BasUnitManager : BaseManager<BasUnit>, IBasUnitManager
    {
		#region 属性注入与构造方法
		
        private IBasUnitService service;

        public BasUnitManager()
        {
            this.service = new BasUnitService();
            base.BaseService = this.service;
        }

		public BasUnitManager(string connectStringKey)
        {
			this.service = new BasUnitService(connectStringKey);
            base.BaseService = this.service;
        }

        public BasUnitManager(NBear.Data.Gateway way)
        {
			this.service = new BasUnitService(way);
            base.BaseService = this.service;
        }

        #endregion
        #region 查询条件类定义
        public class QueryParams : BasUnitService.QueryParams
        {
        }
        #endregion
        public PageResult<BasUnit> GetTablePageDataBySql(Mesnac.Data.Implements.BasUnitService.QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }


        public int GetUnitNextPrimaryKeyValue()
        {
            return this.service.GetUnitNextPrimaryKeyValue();
        }
    }
}
