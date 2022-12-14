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
    using System.Data;
    public class PptWeighDataManager : BaseManager<PptWeighData>, IPptWeighDataManager
    {
		#region 属性注入与构造方法
		
        private IPptWeighDataService service;

        public PptWeighDataManager()
        {
            this.service = new PptWeighDataService();
            base.BaseService = this.service;
        }

		public PptWeighDataManager(string connectStringKey)
        {
			this.service = new PptWeighDataService(connectStringKey);
            base.BaseService = this.service;
        }

        public PptWeighDataManager(NBear.Data.Gateway way)
        {
			this.service = new PptWeighDataService(way);
            base.BaseService = this.service;
        }

        #endregion
        public class QueryParams : PptWeighDataService.QueryParams
        {
        }
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
        /// <summary>
        /// 根据计划ID查询出小料称量标准信息
        /// 孙宜建
        /// 2013-4-2
        /// </summary>
        /// <param name="planID"></param>
        /// <returns></returns>
        public DataTable GetSmallMaterWeighStandardByPlanID(string planID)
        {
            return this.service.GetSmallMaterWeighStandardByPlanID(planID);
        }

        public PageResult<PptWeighData> GetOverErrorAllowPageDataBySql(QueryParams queryParams)
        {
            return this.service.GetOverErrorAllowPageDataBySql(queryParams);
        }
        public PageResult<PptWeighData> GetWeighRatePageDataBySql(QueryParams queryParams)
        {
            return this.service.GetWeighRatePageDataBySql(queryParams);
        }

        public DataTable GetWeighMaterialByBarcode(string barcode)
        {
            return this.service.GetWeighMaterialByBarcode(barcode);
        }
        #endregion
    }
}
