using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    /// <summary>
    /// 混炼模版
    /// 孙本强 @ 2013-04-03 13:02:59
    /// </summary>
    /// <remarks></remarks>
    public class PmtMixingModelService : BaseService<PmtMixingModel>, IPmtMixingModelService
    {
		#region 构造方法

        /// <summary>
        /// 类 PmtMixingModelService 构造函数
        /// 孙本强 @ 2013-04-03 13:02:59
        /// </summary>
        /// <remarks></remarks>
        public PmtMixingModelService() : base(){ }

        /// <summary>
        /// 类 PmtMixingModelService 构造函数
        /// 孙本强 @ 2013-04-03 13:02:59
        /// </summary>
        /// <param name="connectStringKey">The connect string key.</param>
        /// <remarks></remarks>
        public PmtMixingModelService(string connectStringKey) : base(connectStringKey){ }

        /// <summary>
        /// 类 PmtMixingModelService 构造函数
        /// 孙本强 @ 2013-04-03 13:02:59
        /// </summary>
        /// <param name="way">The way.</param>
        /// <remarks></remarks>
        public PmtMixingModelService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法
    }
}
