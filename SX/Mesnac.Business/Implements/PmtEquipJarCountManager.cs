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
    /// PmtEquipJarCountManager 实现类
    /// 孙本强 @ 2013-04-03 11:55:24
    /// </summary>
    /// <remarks></remarks>
    public class PmtEquipJarCountManager : BaseManager<PmtEquipJarCount>, IPmtEquipJarCountManager
    {
		#region 属性注入与构造方法

        /// <summary>
        /// 数据库操作类
        /// 孙本强 @ 2013-04-03 11:55:24
        /// </summary>
        private IPmtEquipJarCountService service;

        /// <summary>
        /// 类 PmtEquipJarCountManager 构造函数
        /// 孙本强 @ 2013-04-03 11:55:25
        /// </summary>
        /// <remarks></remarks>
        public PmtEquipJarCountManager()
        {
            this.service = new PmtEquipJarCountService();
            base.BaseService = this.service;
        }

        /// <summary>
        /// 类 PmtEquipJarCountManager 构造函数
        /// 孙本强 @ 2013-04-03 11:55:25
        /// </summary>
        /// <param name="connectStringKey">The connect string key.</param>
        /// <remarks></remarks>
		public PmtEquipJarCountManager(string connectStringKey)
        {
			this.service = new PmtEquipJarCountService(connectStringKey);
            base.BaseService = this.service;
        }

        /// <summary>
        /// 类 PmtEquipJarCountManager 构造函数
        /// 孙本强 @ 2013-04-03 11:55:25
        /// </summary>
        /// <param name="way">The way.</param>
        /// <remarks></remarks>
        public PmtEquipJarCountManager(NBear.Data.Gateway way)
        {
			this.service = new PmtEquipJarCountService(way);
            base.BaseService = this.service;
        }

        #endregion

    }
}
