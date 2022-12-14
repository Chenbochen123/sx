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
    public class EqmProjectRepairRecordManager : BaseManager<EqmProjectRepairRecord>, IEqmProjectRepairRecordManager
    {
		#region 属性注入与构造方法
		
        private IEqmProjectRepairRecordService service;

        public EqmProjectRepairRecordManager()
        {
            this.service = new EqmProjectRepairRecordService();
            base.BaseService = this.service;
        }

		public EqmProjectRepairRecordManager(string connectStringKey)
        {
			this.service = new EqmProjectRepairRecordService(connectStringKey);
            base.BaseService = this.service;
        }

        public EqmProjectRepairRecordManager(NBear.Data.Gateway way)
        {
			this.service = new EqmProjectRepairRecordService(way);
            base.BaseService = this.service;
        }

        #endregion
        #region 查询条件类定义
        public class QueryParams : EqmProjectRepairRecordService.QueryParams
        {
        }
        #endregion
        public PageResult<EqmProjectRepairRecord> GetTablePageDataBySql(Mesnac.Data.Implements.EqmProjectRepairRecordService.QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }


        public int GetNextPrimaryKeyValue()
        {
            return this.service.GetNextPrimaryKeyValue();
        }

        public string GetNextMainDailyID(string daily)
        {
            return this.service.GetNextMainDailyID(daily);
        }
    }
}
