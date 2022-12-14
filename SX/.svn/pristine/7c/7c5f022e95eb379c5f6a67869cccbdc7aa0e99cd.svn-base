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
    public class QmcSpecManager : BaseManager<QmcSpec>, IQmcSpecManager
    {
		#region 属性注入与构造方法
		
        private IQmcSpecService service;

        public QmcSpecManager()
        {
            this.service = new QmcSpecService();
            base.BaseService = this.service;
        }

		public QmcSpecManager(string connectStringKey)
        {
			this.service = new QmcSpecService(connectStringKey);
            base.BaseService = this.service;
        }

        public QmcSpecManager(NBear.Data.Gateway way)
        {
			this.service = new QmcSpecService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region 查询条件类定义
        public class QueryParams : QmcSpecService.QueryParams
        {
        }
        #endregion

        public PageResult<QmcSpec> GetTablePageDataBySql(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }

        public string GetNextSpecId()
        {
            return this.service.GetNextSpecId();
        }
    }
}
