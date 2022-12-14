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
    /// SysWebLogManager 实现类
    /// 孙本强 @ 2013-04-03 11:45:23
    /// </summary>
    /// <remarks></remarks>
    public class SysWebLogManager : BaseManager<SysWebLog>, ISysWebLogManager
    {
        #region 属性注入与构造方法

        /// <summary>
        /// 数据库操作类
        /// 孙本强 @ 2013-04-03 11:45:24
        /// </summary>
        private ISysWebLogService service;

        /// <summary>
        /// 类 SysWebLogManager 构造函数
        /// 孙本强 @ 2013-04-03 11:45:24
        /// </summary>
        /// <remarks></remarks>
        public SysWebLogManager()
        {
            this.service = new SysWebLogService();
            base.BaseService = this.service;
        }

        /// <summary>
        /// 类 SysWebLogManager 构造函数
        /// 孙本强 @ 2013-04-03 11:45:24
        /// </summary>
        /// <param name="connectStringKey">The connect string key.</param>
        /// <remarks></remarks>
        public SysWebLogManager(string connectStringKey)
        {
            this.service = new SysWebLogService(connectStringKey);
            base.BaseService = this.service;
        }

        /// <summary>
        /// 类 SysWebLogManager 构造函数
        /// 孙本强 @ 2013-04-03 11:45:24
        /// </summary>
        /// <param name="way">The way.</param>
        /// <remarks></remarks>
        public SysWebLogManager(NBear.Data.Gateway way)
        {
            this.service = new SysWebLogService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region 查询条件类定义
        /// <summary>
        /// 查询条件定义类
        /// 孙本强 @ 2013-04-03 11:45:24
        /// </summary>
        /// <remarks></remarks>
        public class QueryParams : SysWebLogService.QueryParams
        {
        }
        #endregion

        /// <summary>
        /// 添加操作日志
        /// 孙本强 @ 2013-04-03 11:45:24
        /// </summary>
        /// <param name="sysWebLog">The sys web log.</param>
        /// <param name="sysPageMethod">The sys page method.</param>
        /// <remarks></remarks>
        public void Append(SysWebLog sysWebLog, SysPageMethod sysPageMethod)
        {
            sysWebLog.PageID = sysPageMethod.PageID;
            sysWebLog.MethodID = new SysPageMethodManager().Append(sysPageMethod);
            sysWebLog.RecordTime = DateTime.Now;
            this.Insert(sysWebLog);
        }


        /// <summary>
        /// 获取分页数据集
        /// 孙本强 @ 2013-04-03 11:45:24
        /// </summary>
        /// <param name="queryParams">查询参数</param>
        /// <returns>分页数据集</returns>
        /// <remarks></remarks>
        public PageResult<SysWebLog> GetTablePageDataBySql(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }

    }
}
