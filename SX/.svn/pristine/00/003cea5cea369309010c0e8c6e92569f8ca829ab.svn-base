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
    public class QmcStandardManager : BaseManager<QmcStandard>, IQmcStandardManager
    {
		#region 属性注入与构造方法
		
        private IQmcStandardService service;

        public QmcStandardManager()
        {
            this.service = new QmcStandardService();
            base.BaseService = this.service;
        }

		public QmcStandardManager(string connectStringKey)
        {
			this.service = new QmcStandardService(connectStringKey);
            base.BaseService = this.service;
        }

        public QmcStandardManager(NBear.Data.Gateway way)
        {
			this.service = new QmcStandardService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region 查询条件类定义
        public class QueryParams : QmcStandardService.QueryParams
        {
        }
        #endregion

        public PageResult<QmcStandard> GetTablePageDataBySql(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }

        public DataSet GetStandardList()
        {
            return this.service.GetStandardList();
        }
        public string GetNextStandardId()
        {
            return this.service.GetNextStandardId();
        }
    }
}
