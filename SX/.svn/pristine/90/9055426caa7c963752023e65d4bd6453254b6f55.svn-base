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
    /// <summary>
    /// SysLoginLogManager 实现类
    /// 孙本强 @ 2013-04-03 11:33:00
    /// </summary>
    /// <remarks></remarks>
    public class SysLoginLogManager : BaseManager<SysLoginLog>, ISysLoginLogManager
    {
		#region 属性注入与构造方法

        /// <summary>
        /// 数据操作类
        /// 孙本强 @ 2013-04-03 11:33:00
        /// </summary>
        private ISysLoginLogService service;

        /// <summary>
        /// 类 SysLoginLogManager 构造函数
        /// 孙本强 @ 2013-04-03 11:33:01
        /// </summary>
        /// <remarks></remarks>
        public SysLoginLogManager()
        {
            this.service = new SysLoginLogService();
            base.BaseService = this.service;
        }

        /// <summary>
        /// 类 SysLoginLogManager 构造函数
        /// 孙本强 @ 2013-04-03 11:33:01
        /// </summary>
        /// <param name="connectStringKey">The connect string key.</param>
        /// <remarks></remarks>
		public SysLoginLogManager(string connectStringKey)
        {
			this.service = new SysLoginLogService(connectStringKey);
            base.BaseService = this.service;
        }

        /// <summary>
        /// 类 SysLoginLogManager 构造函数
        /// 孙本强 @ 2013-04-03 11:33:01
        /// </summary>
        /// <param name="way">The way.</param>
        /// <remarks></remarks>
        public SysLoginLogManager(NBear.Data.Gateway way)
        {
			this.service = new SysLoginLogService(way);
            base.BaseService = this.service;
        }

        #endregion
        #region 查询条件类定义
        /// <summary>
        /// 查询条件定义类
        /// 孙本强 @ 2013-04-03 11:33:01
        /// </summary>
        /// <remarks></remarks>
        public class QueryParams : SysLoginLogService.QueryParams
        {
        }
        #endregion

        /// <summary>
        /// 获取分页数据集
        /// 孙本强 @ 2013-04-03 11:33:01
        /// </summary>
        /// <param name="queryParams">查询参数</param>
        /// <returns>分页数据集</returns>
        /// <remarks></remarks>
        public PageResult<SysLoginLog> GetTablePageDataBySql(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }
    }
}
