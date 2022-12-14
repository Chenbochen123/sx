using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    /// <summary>
    /// PmtWeightActionService 实现类
    /// 孙本强 @ 2013-04-03 13:03:34
    /// </summary>
    /// <remarks></remarks>
    public class PmtWeightActionService : BaseService<PmtWeightAction>, IPmtWeightActionService
    {
		#region 构造方法

        /// <summary>
        /// 类 PmtWeightActionService 构造函数
        /// 孙本强 @ 2013-04-03 13:03:34
        /// </summary>
        /// <remarks></remarks>
        public PmtWeightActionService() : base(){ }

        /// <summary>
        /// 类 PmtWeightActionService 构造函数
        /// 孙本强 @ 2013-04-03 13:03:34
        /// </summary>
        /// <param name="connectStringKey">The connect string key.</param>
        /// <remarks></remarks>
        public PmtWeightActionService(string connectStringKey) : base(connectStringKey){ }

        /// <summary>
        /// 类 PmtWeightActionService 构造函数
        /// 孙本强 @ 2013-04-03 13:03:34
        /// </summary>
        /// <param name="way">The way.</param>
        /// <remarks></remarks>
        public PmtWeightActionService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法
    }
}
