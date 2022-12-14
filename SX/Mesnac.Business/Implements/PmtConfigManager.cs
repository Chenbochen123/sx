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
    /// PmtConfigManager 实现类
    /// 孙本强 @ 2013-04-03 11:52:54
    /// </summary>
    /// <remarks></remarks>
    public class PmtConfigManager : BaseManager<PmtConfig>, IPmtConfigManager
    {
        #region 属性注入与构造方法

        /// <summary>
        /// 数据库操作类
        /// 孙本强 @ 2013-04-03 11:52:54
        /// </summary>
        private IPmtConfigService service;

        /// <summary>
        /// 类 PmtConfigManager 构造函数
        /// 孙本强 @ 2013-04-03 11:52:54
        /// </summary>
        /// <remarks></remarks>
        public PmtConfigManager()
        {
            this.service = new PmtConfigService();
            base.BaseService = this.service;
        }

        /// <summary>
        /// 类 PmtConfigManager 构造函数
        /// 孙本强 @ 2013-04-03 11:52:54
        /// </summary>
        /// <param name="connectStringKey">The connect string key.</param>
        /// <remarks></remarks>
        public PmtConfigManager(string connectStringKey)
        {
            this.service = new PmtConfigService(connectStringKey);
            base.BaseService = this.service;
        }

        /// <summary>
        /// 类 PmtConfigManager 构造函数
        /// 孙本强 @ 2013-04-03 11:52:54
        /// </summary>
        /// <param name="way">The way.</param>
        /// <remarks></remarks>
        public PmtConfigManager(NBear.Data.Gateway way)
        {
            this.service = new PmtConfigService(way);
            base.BaseService = this.service;
        }

        #endregion

        /// <summary>
        /// 类型
        /// 孙本强 @ 2013-04-03 11:52:55
        /// </summary>
        /// <remarks></remarks>
        public enum TypeCode
        {
            /// <summary>
            /// 称量物料
            /// </summary>
            WeightMaterial,
            /// <summary>
            /// 称量动作
            /// </summary>
            WeightAction,
            /// <summary>
            /// 审核人
            /// </summary>
            AuditUser,
            /// <summary>
            /// 设备
            /// </summary>
            Equip,
            /// <summary>
            /// 配方类型
            /// </summary>
            RecipeType,
            /// <summary>
            /// 配方状态
            /// </summary>
            RecipeState,
            /// <summary>
            /// 炭黑回收类型
            /// </summary>
            CarbonRecycleType,
            /// <summary>
            /// 油（2）称量判断
            /// </summary>
            WeightTypey,
        }
    }
}
