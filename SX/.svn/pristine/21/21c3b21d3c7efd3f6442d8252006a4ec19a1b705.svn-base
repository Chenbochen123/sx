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
    using System.Data;
    public class EqmMixerFaultManager : BaseManager<EqmMixerFault>, IEqmMixerFaultManager
    {
		#region 属性注入与构造方法
		
        private IEqmMixerFaultService service;

        public EqmMixerFaultManager()
        {
            this.service = new EqmMixerFaultService();
            base.BaseService = this.service;
        }

		public EqmMixerFaultManager(string connectStringKey)
        {
			this.service = new EqmMixerFaultService(connectStringKey);
            base.BaseService = this.service;
        }

        public EqmMixerFaultManager(NBear.Data.Gateway way)
        {
			this.service = new EqmMixerFaultService(way);
            base.BaseService = this.service;
        }

        #endregion
        #region 查询条件类定义
        public class QueryParams : EqmMixerFaultService.QueryParams
        {
        }
        #endregion

        public PageResult<EqmMixerFault> GetTablePageDataBySql(Mesnac.Data.Implements.EqmMixerFaultService.QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }

        public DataSet GetChartGroupAnalysis(string columnName, DateTime faultBeginDate, DateTime faultEndDate, int count)
        {
            return this.service.GetChartGroupAnalysis(columnName, faultBeginDate, faultEndDate, count);
        }
    }
}
