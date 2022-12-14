using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Implements
{
    using Mesnac.Business.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Data.Interface;
    using Mesnac.Entity;
    using Mesnac.Util.Cryptography;
    using NBear.Common;
    using Mesnac.Data.Components;
    /// <summary>
    /// PmtTermManager 实现类
    /// 孙本强 @ 2013-04-03 12:45:43
    /// </summary>
    /// <remarks></remarks>
    public class PmtTermManager : BaseManager<PmtTerm>, IPmtTermManager
    {
		#region 属性注入与构造方法

        /// <summary>
        /// 
        /// 孙本强 @ 2013-04-03 12:45:43
        /// </summary>
        private IPmtTermService service;

        /// <summary>
        /// 类 PmtTermManager 构造函数
        /// 孙本强 @ 2013-04-03 12:45:43
        /// </summary>
        /// <remarks></remarks>
        public PmtTermManager()
        {
            this.service = new PmtTermService();
            base.BaseService = this.service;
        }

        /// <summary>
        /// 类 PmtTermManager 构造函数
        /// 孙本强 @ 2013-04-03 12:45:43
        /// </summary>
        /// <param name="connectStringKey">The connect string key.</param>
        /// <remarks></remarks>
		public PmtTermManager(string connectStringKey)
        {
			this.service = new PmtTermService(connectStringKey);
            base.BaseService = this.service;
        }

        /// <summary>
        /// 类 PmtTermManager 构造函数
        /// 孙本强 @ 2013-04-03 12:45:43
        /// </summary>
        /// <param name="way">The way.</param>
        /// <remarks></remarks>
        public PmtTermManager(NBear.Data.Gateway way)
        {
			this.service = new PmtTermService(way);
            base.BaseService = this.service;
        }

        #endregion
        #region 查询条件类定义
        /// <summary>
        /// 查询条件定义类
        /// 孙本强 @ 2013-04-03 12:45:43
        /// </summary>
        /// <remarks></remarks>
        public class QueryParams : PmtTermService.QueryParams
        {
        }
        #endregion

        /// <summary>
        /// 获取分页数据集
        /// 孙本强 @ 2013-04-03 12:45:43
        /// </summary>
        /// <param name="queryParams">查询参数</param>
        /// <returns>分页数据集</returns>
        /// <remarks></remarks>
        public PageResult<PmtTerm> GetTablePageDataBySql(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }
    }
}
