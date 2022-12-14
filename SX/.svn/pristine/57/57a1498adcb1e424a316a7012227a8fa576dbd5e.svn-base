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
    public class PmtOpenActionManager : BaseManager<PmtOpenAction>, IPmtOpenActionManager
    {
		#region 属性注入与构造方法
		
        private IPmtOpenActionService service;

        public PmtOpenActionManager()
        {
            this.service = new PmtOpenActionService();
            base.BaseService = this.service;
        }

		public PmtOpenActionManager(string connectStringKey)
        {
			this.service = new PmtOpenActionService(connectStringKey);
            base.BaseService = this.service;
        }

        public PmtOpenActionManager(NBear.Data.Gateway way)
        {
			this.service = new PmtOpenActionService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region 查询条件类定义
        /// <summary>
        /// 查询条件定义类
        /// 袁洋 @2014年9月29日11:12:32
        /// </summary>
        /// <remarks></remarks>
        public class QueryParams : PmtOpenActionService.QueryParams
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
        public PageResult<PmtOpenAction> GetTablePageDataBySql(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }
    }
}
