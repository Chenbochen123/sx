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
    /// PmtWeightActionManager 实现类
    /// 孙本强 @ 2013-04-03 12:45:36
    /// </summary>
    /// <remarks></remarks>
    public class PmtWeightActionManager : BaseManager<PmtWeightAction>, IPmtWeightActionManager
    {
		#region 属性注入与构造方法

        /// <summary>
        /// 
        /// 孙本强 @ 2013-04-03 12:45:36
        /// </summary>
        private IPmtWeightActionService service;

        /// <summary>
        /// 类 PmtWeightActionManager 构造函数
        /// 孙本强 @ 2013-04-03 12:45:36
        /// </summary>
        /// <remarks></remarks>
        public PmtWeightActionManager()
        {
            this.service = new PmtWeightActionService();
            base.BaseService = this.service;
        }

        /// <summary>
        /// 类 PmtWeightActionManager 构造函数
        /// 孙本强 @ 2013-04-03 12:45:36
        /// </summary>
        /// <param name="connectStringKey">The connect string key.</param>
        /// <remarks></remarks>
		public PmtWeightActionManager(string connectStringKey)
        {
			this.service = new PmtWeightActionService(connectStringKey);
            base.BaseService = this.service;
        }

        /// <summary>
        /// 类 PmtWeightActionManager 构造函数
        /// 孙本强 @ 2013-04-03 12:45:36
        /// </summary>
        /// <param name="way">The way.</param>
        /// <remarks></remarks>
        public PmtWeightActionManager(NBear.Data.Gateway way)
        {
			this.service = new PmtWeightActionService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
