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
    /// PmtRecipeWeightManager 实现类
    /// 孙本强 @ 2013-04-03 12:45:55
    /// </summary>
    /// <remarks></remarks>
    public class PmtRecipeWeightManager : BaseManager<PmtRecipeWeight>, IPmtRecipeWeightManager
    {
		#region 属性注入与构造方法

        /// <summary>
        /// 
        /// 孙本强 @ 2013-04-03 12:45:55
        /// </summary>
        private IPmtRecipeWeightService service;

        /// <summary>
        /// 类 PmtRecipeWeightManager 构造函数
        /// 孙本强 @ 2013-04-03 12:45:55
        /// </summary>
        /// <remarks></remarks>
        public PmtRecipeWeightManager()
        {
            this.service = new PmtRecipeWeightService();
            base.BaseService = this.service;
        }

        /// <summary>
        /// 类 PmtRecipeWeightManager 构造函数
        /// 孙本强 @ 2013-04-03 12:45:55
        /// </summary>
        /// <param name="connectStringKey">The connect string key.</param>
        /// <remarks></remarks>
		public PmtRecipeWeightManager(string connectStringKey)
        {
			this.service = new PmtRecipeWeightService(connectStringKey);
            base.BaseService = this.service;
        }

        /// <summary>
        /// 类 PmtRecipeWeightManager 构造函数
        /// 孙本强 @ 2013-04-03 12:45:55
        /// </summary>
        /// <param name="way">The way.</param>
        /// <remarks></remarks>
        public PmtRecipeWeightManager(NBear.Data.Gateway way)
        {
			this.service = new PmtRecipeWeightService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
