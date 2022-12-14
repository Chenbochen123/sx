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
    /// PmtMixTypeManager 实现类
    /// 孙本强 @ 2013-04-03 12:47:13
    /// </summary>
    /// <remarks></remarks>
    public class PmtMixTypeManager : BaseManager<PmtMixType>, IPmtMixTypeManager
    { 
		#region 属性注入与构造方法

        /// <summary>
        /// 
        /// 孙本强 @ 2013-04-03 12:47:13
        /// </summary>
        private IPmtMixTypeService service;

        /// <summary>
        /// 类 PmtMixTypeManager 构造函数
        /// 孙本强 @ 2013-04-03 12:47:13
        /// </summary>
        /// <remarks></remarks>
        public PmtMixTypeManager()
        {
            this.service = new PmtMixTypeService();
            base.BaseService = this.service;
        }

        /// <summary>
        /// 类 PmtMixTypeManager 构造函数
        /// 孙本强 @ 2013-04-03 12:47:13
        /// </summary>
        /// <param name="connectStringKey">The connect string key.</param>
        /// <remarks></remarks>
		public PmtMixTypeManager(string connectStringKey)
        {
			this.service = new PmtMixTypeService(connectStringKey);
            base.BaseService = this.service;
        }

        /// <summary>
        /// 类 PmtMixTypeManager 构造函数
        /// 孙本强 @ 2013-04-03 12:47:13
        /// </summary>
        /// <param name="way">The way.</param>
        /// <remarks></remarks>
        public PmtMixTypeManager(NBear.Data.Gateway way)
        {
			this.service = new PmtMixTypeService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region 查询条件类定义
        /// <summary>
        /// 查询条件定义类
        /// 孙本强 @ 2013-04-03 12:47:13
        /// </summary>
        /// <remarks></remarks>
        public class QueryParams : PmtMixTypeService.QueryParams
        {
        }
        #endregion
        /// <summary>
        /// 获取分页数据集
        /// 孙本强 @ 2013-04-03 12:47:14
        /// </summary>
        /// <param name="queryParams">查询参数</param>
        /// <returns>分页数据集</returns>
        /// <remarks></remarks>
        public PageResult<PmtMixType> GetTablePageDataBySql(Mesnac.Data.Implements.PmtMixTypeService.QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }


        /// <summary>
        /// 获取下一个主键值
        /// 孙本强 @ 2013-04-03 12:47:14
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public int GetPmtMixTypeNextPrimaryKeyValue()
        {
            return this.service.GetPmtMixTypeNextPrimaryKeyValue();
        }
    }
}
