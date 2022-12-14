using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    using System.Data;
    public class PptWeighManager : BaseManager<PptWeigh>, IPptWeighManager
    {
		#region 属性注入与构造方法
		
        private IPptWeighService service;

        public PptWeighManager()
        {
            this.service = new PptWeighService();
            base.BaseService = this.service;
        }

		public PptWeighManager(string connectStringKey)
        {
			this.service = new PptWeighService(connectStringKey);
            base.BaseService = this.service;
        }

        public PptWeighManager(NBear.Data.Gateway way)
        {
			this.service = new PptWeighService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region IPptWeighManager 成员

        /// <summary>
        /// 根据计划ID查询出小料称量信息
        /// 孙宜建
        /// 2013-4-2
        /// </summary>
        /// <param name="planID"></param>
        /// <returns></returns>
        public DataTable GetSmallMaterWeighListByPlanID(string planID)
        {
            return this.service.GetSmallMaterWeighListByPlanID(planID);
        }

        #endregion
    }
}
