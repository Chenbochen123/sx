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
    /// PmtActionManager 实现类
    /// 孙本强 @ 2013-04-03 11:52:35
    /// </summary>
    /// <remarks></remarks>
    public class PmtActionManager : BaseManager<PmtAction>, IPmtActionManager
    {
		#region 属性注入与构造方法

        /// <summary>
        /// 数据库操作类
        /// 孙本强 @ 2013-04-03 11:52:36
        /// </summary>
        private IPmtActionService service;

        /// <summary>
        /// 类 PmtActionManager 构造函数
        /// 孙本强 @ 2013-04-03 11:52:37
        /// </summary>
        /// <remarks></remarks>
        public PmtActionManager()
        {
            this.service = new PmtActionService();
            base.BaseService = this.service;
        }

        /// <summary>
        /// 类 PmtActionManager 构造函数
        /// 孙本强 @ 2013-04-03 11:52:37
        /// </summary>
        /// <param name="connectStringKey">The connect string key.</param>
        /// <remarks></remarks>
		public PmtActionManager(string connectStringKey)
        {
			this.service = new PmtActionService(connectStringKey);
            base.BaseService = this.service;
        }

        /// <summary>
        /// 类 PmtActionManager 构造函数
        /// 孙本强 @ 2013-04-03 11:52:37
        /// </summary>
        /// <param name="way">The way.</param>
        /// <remarks></remarks>
        public PmtActionManager(NBear.Data.Gateway way)
        {
			this.service = new PmtActionService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region 查询条件类定义
        /// <summary>
        /// 查询条件定义类
        /// 孙本强 @ 2013-04-03 11:52:37
        /// </summary>
        /// <remarks></remarks>
        public class QueryParams : PmtActionService.QueryParams
        {
        }
        #endregion

        /// <summary>
        /// 获取分页数据集
        /// 孙本强 @ 2013-04-03 11:52:37
        /// </summary>
        /// <param name="queryParams">查询参数</param>
        /// <returns>分页数据集</returns>
        /// <remarks></remarks>
        public PageResult<PmtAction> GetTablePageDataBySql(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }

    }
}
