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
    /// <summary>
    /// PmtRecipeLogManager 实现类
    /// 孙本强 @ 2013-04-03 12:47:02
    /// </summary>
    /// <remarks></remarks>
    public class PmtRecipeLogManager : BaseManager<PmtRecipeLog>, IPmtRecipeLogManager
    {
		#region 属性注入与构造方法

        /// <summary>
        /// 
        /// 孙本强 @ 2013-04-03 12:47:02
        /// </summary>
        private IPmtRecipeLogService service;

        /// <summary>
        /// 类 PmtRecipeLogManager 构造函数
        /// 孙本强 @ 2013-04-03 12:47:02
        /// </summary>
        /// <remarks></remarks>
        public PmtRecipeLogManager()
        {
            this.service = new PmtRecipeLogService();
            base.BaseService = this.service;
        }

        /// <summary>
        /// 类 PmtRecipeLogManager 构造函数
        /// 孙本强 @ 2013-04-03 12:47:02
        /// </summary>
        /// <param name="connectStringKey">The connect string key.</param>
        /// <remarks></remarks>
		public PmtRecipeLogManager(string connectStringKey)
        {
			this.service = new PmtRecipeLogService(connectStringKey);
            base.BaseService = this.service;
        }

        /// <summary>
        /// 类 PmtRecipeLogManager 构造函数
        /// 孙本强 @ 2013-04-03 12:47:02
        /// </summary>
        /// <param name="way">The way.</param>
        /// <remarks></remarks>
        public PmtRecipeLogManager(NBear.Data.Gateway way)
        {
			this.service = new PmtRecipeLogService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region 查询条件类定义
        /// <summary>
        /// 查询条件定义类
        /// 孙本强 @ 2013-04-03 12:47:02
        /// </summary>
        /// <remarks></remarks>
        public class QueryParams : PmtRecipeLogService.QueryParams
        {
        }
        #endregion

        /// <summary>
        /// 获取分页数据集
        /// 孙本强 @ 2013-04-03 12:47:02
        /// </summary>
        /// <param name="queryParams">查询参数</param>
        /// <returns>分页数据集</returns>
        /// <remarks></remarks>
        public PageResult<PmtRecipeLog> GetTablePageDataBySql(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }
    }
}
