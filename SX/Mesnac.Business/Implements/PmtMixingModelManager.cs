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
    /// PmtMixingModelManager 实现类
    /// 孙本强 @ 2013-04-03 12:47:20
    /// </summary>
    /// <remarks></remarks>
    public class PmtMixingModelManager : BaseManager<PmtMixingModel>, IPmtMixingModelManager
    {
		#region 属性注入与构造方法

        /// <summary>
        /// 
        /// 孙本强 @ 2013-04-03 12:47:20
        /// </summary>
        private IPmtMixingModelService service;

        /// <summary>
        /// 类 PmtMixingModelManager 构造函数
        /// 孙本强 @ 2013-04-03 12:47:20
        /// </summary>
        /// <remarks></remarks>
        public PmtMixingModelManager()
        {
            this.service = new PmtMixingModelService();
            base.BaseService = this.service;
        }

        /// <summary>
        /// 类 PmtMixingModelManager 构造函数
        /// 孙本强 @ 2013-04-03 12:47:20
        /// </summary>
        /// <param name="connectStringKey">The connect string key.</param>
        /// <remarks></remarks>
		public PmtMixingModelManager(string connectStringKey)
        {
			this.service = new PmtMixingModelService(connectStringKey);
            base.BaseService = this.service;
        }

        /// <summary>
        /// 类 PmtMixingModelManager 构造函数
        /// 孙本强 @ 2013-04-03 12:47:20
        /// </summary>
        /// <param name="way">The way.</param>
        /// <remarks></remarks>
        public PmtMixingModelManager(NBear.Data.Gateway way)
        {
			this.service = new PmtMixingModelService(way);
            base.BaseService = this.service;
        }

        #endregion

        /// <summary>
        /// 校验模板
        /// 孙本强 @ 2013-04-03 12:47:20
        /// </summary>
        /// <param name="lst">The LST.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private string ValidityModel(List<PmtMixingModel> lst)
        {
            string Result = string.Empty;
            return Result;
        }
        /// <summary>
        /// 保存密炼模板
        /// 孙本强 @ 2013-04-03 12:44:33
        /// 孙本强 @ 2013-04-03 12:47:21
        /// </summary>
        /// <param name="lst">The LST.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public string SaveModel(List<PmtMixingModel> lst)
        {
            string Result = ValidityModel(lst);
            if (Result.Length > 0)
            {
                return Result;
            }
            foreach (PmtMixingModel m in lst)
            {
                this.Insert(m);
            }
            return Result;
        }
    }
}
