using System;
using System.Collections.Generic;
using System.Data;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    using NBear.Common;
    /// <summary>
    /// PmtRecipeMixingLogManager 实现类
    /// 孙本强 @ 2013-04-03 12:46:17
    /// </summary>
    /// <remarks></remarks>
    public class PmtRecipeMixingLogManager : BaseManager<PmtRecipeMixingLog>, IPmtRecipeMixingLogManager
    {
		#region 属性注入与构造方法

        /// <summary>
        /// 
        /// 孙本强 @ 2013-04-03 12:46:18
        /// </summary>
        private IPmtRecipeMixingLogService service;

        /// <summary>
        /// 类 PmtRecipeMixingLogManager 构造函数
        /// 孙本强 @ 2013-04-03 12:46:18
        /// </summary>
        /// <remarks></remarks>
        public PmtRecipeMixingLogManager()
        {
            this.service = new PmtRecipeMixingLogService();
            base.BaseService = this.service;
        }

        /// <summary>
        /// 类 PmtRecipeMixingLogManager 构造函数
        /// 孙本强 @ 2013-04-03 12:46:18
        /// </summary>
        /// <param name="connectStringKey">The connect string key.</param>
        /// <remarks></remarks>
		public PmtRecipeMixingLogManager(string connectStringKey)
        {
			this.service = new PmtRecipeMixingLogService(connectStringKey);
            base.BaseService = this.service;
        }

        /// <summary>
        /// 类 PmtRecipeMixingLogManager 构造函数
        /// 孙本强 @ 2013-04-03 12:46:18
        /// </summary>
        /// <param name="way">The way.</param>
        /// <remarks></remarks>
        public PmtRecipeMixingLogManager(NBear.Data.Gateway way)
        {
			this.service = new PmtRecipeMixingLogService(way);
            base.BaseService = this.service;
        }

        #endregion

        /// <summary>
        /// 获取密炼信息日志信息
        /// 孙本强 @ 2013-04-03 12:07:33
        /// 孙本强 @ 2013-04-03 12:46:18
        /// </summary>
        /// <param name="recipe">The recipe.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public DataSet GetPmtRecipeMixingLog(string recipe)
        {
            return this.service.GetPmtRecipeMixingLog(recipe);
        }
    }
}
