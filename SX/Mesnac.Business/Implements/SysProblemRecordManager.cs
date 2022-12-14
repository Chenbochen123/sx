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
    public class SysProblemRecordManager : BaseManager<SysProblemRecord>, ISysProblemRecordManager
    {
		#region 属性注入与构造方法
		
        private ISysProblemRecordService service;

        public SysProblemRecordManager()
        {
            this.service = new SysProblemRecordService();
            base.BaseService = this.service;
        }

		public SysProblemRecordManager(string connectStringKey)
        {
			this.service = new SysProblemRecordService(connectStringKey);
            base.BaseService = this.service;
        }

        public SysProblemRecordManager(NBear.Data.Gateway way)
        {
			this.service = new SysProblemRecordService(way);
            base.BaseService = this.service;
        }

        #endregion
        #region 查询条件类定义
        public class QueryParams : SysProblemRecordService.QueryParams
        {
        }
        #endregion
        public Data.Components.PageResult<SysProblemRecord> GetTablePageDataBySql(SysProblemRecordService.QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }
    }
}
