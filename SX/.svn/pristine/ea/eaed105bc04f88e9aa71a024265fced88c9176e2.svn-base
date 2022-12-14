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
    public class PmtOpenActionModelMainManager : BaseManager<PmtOpenActionModelMain>, IPmtOpenActionModelMainManager
    {
		#region 属性注入与构造方法
		
        private IPmtOpenActionModelMainService service;

        public PmtOpenActionModelMainManager()
        {
            this.service = new PmtOpenActionModelMainService();
            base.BaseService = this.service;
        }

		public PmtOpenActionModelMainManager(string connectStringKey)
        {
			this.service = new PmtOpenActionModelMainService(connectStringKey);
            base.BaseService = this.service;
        }

        public PmtOpenActionModelMainManager(NBear.Data.Gateway way)
        {
			this.service = new PmtOpenActionModelMainService(way);
            base.BaseService = this.service;
        }

        #endregion
        #region 查询条件类定义
        /// <summary>
        /// 查询条件定义类
        /// 袁洋 @2014年9月29日11:12:32
        /// </summary>
        /// <remarks></remarks>
        public class QueryParams : PmtOpenActionModelMainService.QueryParams
        {
        }
        #endregion

        /// <summary>
        /// 获取分页数据集
        /// 袁洋 @2014年9月29日11:12:32
        /// </summary>
        /// <param name="queryParams">查询参数</param>
        /// <returns>分页数据集</returns>
        /// <remarks></remarks>
        public PageResult<PmtOpenActionModelMain> GetTablePageDataBySql(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }
    }
}
