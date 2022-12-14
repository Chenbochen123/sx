using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    /// <summary>
    /// PmtProcedureManager 实现类
    /// 孙本强 @ 2013-04-03 12:47:08
    /// </summary>
    /// <remarks></remarks>
    public class PmtProcedureManager : BaseManager<PmtProcedure>, IPmtProcedureManager
    {
		#region 属性注入与构造方法

        /// <summary>
        /// 
        /// 孙本强 @ 2013-04-03 12:47:08
        /// </summary>
        private IPmtProcedureService service;

        /// <summary>
        /// 类 PmtProcedureManager 构造函数
        /// 孙本强 @ 2013-04-03 12:47:08
        /// </summary>
        /// <remarks></remarks>
        public PmtProcedureManager()
        {
            this.service = new PmtProcedureService();
            base.BaseService = this.service;
        }

        /// <summary>
        /// 类 PmtProcedureManager 构造函数
        /// 孙本强 @ 2013-04-03 12:47:09
        /// </summary>
        /// <param name="connectStringKey">The connect string key.</param>
        /// <remarks></remarks>
		public PmtProcedureManager(string connectStringKey)
        {
			this.service = new PmtProcedureService(connectStringKey);
            base.BaseService = this.service;
        }

        /// <summary>
        /// 类 PmtProcedureManager 构造函数
        /// 孙本强 @ 2013-04-03 12:47:09
        /// </summary>
        /// <param name="way">The way.</param>
        /// <remarks></remarks>
        public PmtProcedureManager(NBear.Data.Gateway way)
        {
			this.service = new PmtProcedureService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
