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
    public class EqmStopFaultManager : BaseManager<EqmStopFault>, IEqmStopFaultManager
    {
		#region 属性注入与构造方法
		
        private IEqmStopFaultService service;

        public EqmStopFaultManager()
        {
            this.service = new EqmStopFaultService();
            base.BaseService = this.service;
        }

		public EqmStopFaultManager(string connectStringKey)
        {
			this.service = new EqmStopFaultService(connectStringKey);
            base.BaseService = this.service;
        }

        public EqmStopFaultManager(NBear.Data.Gateway way)
        {
			this.service = new EqmStopFaultService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region IEqmStopFaultManager 成员

        public System.Data.DataSet GetDataByParas( EqmStopFaultParams queryParams )
        {
            return this.service.GetDataByParas( queryParams );
        }

        public string GetNextFaultCodeByParas( EqmStopFault eqmStopType )
        {
            return this.service.GetNextFaultCodeByParas( eqmStopType );
        }

        #endregion

        #region 查询条件类定义
        public class QueryParams : EqmStopFaultService.QueryParams
        {
        }
        #endregion

        public PageResult<EqmStopFault> GetEqmStopFaultBySearchKey(Mesnac.Data.Implements.EqmStopFaultService.QueryParams queryParams)
        {
            return this.service.GetEqmStopFaultBySearchKey(queryParams);
        }
    }
}
