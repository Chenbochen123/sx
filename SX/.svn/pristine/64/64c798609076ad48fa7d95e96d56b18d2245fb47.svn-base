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
    /// PmtRecipeWeightLogManager 实现类
    /// 孙本强 @ 2013-04-03 12:46:01
    /// </summary>
    /// <remarks></remarks>
    public class PmtRecipeWeightLogManager : BaseManager<PmtRecipeWeightLog>, IPmtRecipeWeightLogManager
    {
		#region 属性注入与构造方法

        /// <summary>
        /// 
        /// 孙本强 @ 2013-04-03 12:46:01
        /// </summary>
        private IPmtRecipeWeightLogService service;

        /// <summary>
        /// 类 PmtRecipeWeightLogManager 构造函数
        /// 孙本强 @ 2013-04-03 12:46:01
        /// </summary>
        /// <remarks></remarks>
        public PmtRecipeWeightLogManager()
        {
            this.service = new PmtRecipeWeightLogService();
            base.BaseService = this.service;
        }

        /// <summary>
        /// 类 PmtRecipeWeightLogManager 构造函数
        /// 孙本强 @ 2013-04-03 12:46:01
        /// </summary>
        /// <param name="connectStringKey">The connect string key.</param>
        /// <remarks></remarks>
		public PmtRecipeWeightLogManager(string connectStringKey)
        {
			this.service = new PmtRecipeWeightLogService(connectStringKey);
            base.BaseService = this.service;
        }

        /// <summary>
        /// 类 PmtRecipeWeightLogManager 构造函数
        /// 孙本强 @ 2013-04-03 12:46:01
        /// </summary>
        /// <param name="way">The way.</param>
        /// <remarks></remarks>
        public PmtRecipeWeightLogManager(NBear.Data.Gateway way)
        {
			this.service = new PmtRecipeWeightLogService(way);
            base.BaseService = this.service;
        }

        #endregion


        /// <summary>
        /// 获取称量信息的日志信息
        /// 孙本强 @ 2013-04-03 12:06:33
        /// 孙本强 @ 2013-04-03 12:46:01
        /// </summary>
        /// <param name="recipe">The recipe.</param>
        /// <param name="weightType">Type of the weight.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public DataSet GetPmtRecipeWeightLog(string recipe, string weightType)
        {
            return this.service.GetPmtRecipeWeightLog(recipe, weightType);
        }
    }
}
