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
    /// SysRoleManager 实现类
    /// 孙本强 @ 2013-04-03 11:40:26
    /// </summary>
    /// <remarks></remarks>
    public class SysRoleManager : BaseManager<SysRole>, ISysRoleManager
    {
        #region 属性注入与构造方法

        /// <summary>
        /// 数据库操作类
        /// 孙本强 @ 2013-04-03 11:40:26
        /// </summary>
        private ISysRoleService service;

        /// <summary>
        /// 类 SysRoleManager 构造函数
        /// 孙本强 @ 2013-04-03 11:40:26
        /// </summary>
        /// <remarks></remarks>
        public SysRoleManager()
        {
            this.service = new SysRoleService();
            base.BaseService = this.service;
        }

        /// <summary>
        /// 类 SysRoleManager 构造函数
        /// 孙本强 @ 2013-04-03 11:40:26
        /// </summary>
        /// <param name="connectStringKey">The connect string key.</param>
        /// <remarks></remarks>
        public SysRoleManager(string connectStringKey)
        {
            this.service = new SysRoleService(connectStringKey);
            base.BaseService = this.service;
        }

        /// <summary>
        /// 类 SysRoleManager 构造函数
        /// 孙本强 @ 2013-04-03 11:40:27
        /// </summary>
        /// <param name="way">The way.</param>
        /// <remarks></remarks>
        public SysRoleManager(NBear.Data.Gateway way)
        {
            this.service = new SysRoleService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region 查询条件类定义
        /// <summary>
        /// 查询条件定义类
        /// 孙本强 @ 2013-04-03 11:40:27
        /// </summary>
        /// <remarks></remarks>
        public class QueryParams : SysRoleService.QueryParams
        {
        }
        #endregion

        /// <summary>
        /// 获取分页数据集
        /// 孙本强 @ 2013-04-03 11:40:27
        /// </summary>
        /// <param name="queryParams">查询参数</param>
        /// <returns>分页数据集</returns>
        /// <remarks></remarks>
        public PageResult<SysRole> GetTablePageDataBySql(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }

    }
}
