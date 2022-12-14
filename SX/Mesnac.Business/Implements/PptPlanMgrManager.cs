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
    public class PptPlanMgrManager : BaseManager<PptPlanMgr>, IPptPlanMgrManager
    {
		#region 属性注入与构造方法
		
        private IPptPlanMgrService service;

        public PptPlanMgrManager()
        {
            this.service = new PptPlanMgrService();
            base.BaseService = this.service;
        }

		public PptPlanMgrManager(string connectStringKey)
        {
			this.service = new PptPlanMgrService(connectStringKey);
            base.BaseService = this.service;
        }

        public PptPlanMgrManager(NBear.Data.Gateway way)
        {
			this.service = new PptPlanMgrService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region 查询条件类定义
        public class QueryParams : PptPlanMgrService.QueryParams
        {
        }
        #endregion
        public PageResult<PptPlanMgr> GetTablePageDataBySql(Mesnac.Data.Implements.PptPlanMgrService.QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }

        public PageResult<PptPlanMgr> GetTablePageAddPlanInfoBySql(Mesnac.Data.Implements.PptPlanMgrService.QueryParams queryParams)
        {
            return this.service.GetTablePageAddPlanInfoBySql(queryParams);
        }


        public DataSet GetListAddPlanInfoByWhere(string planDate, string equipcode, string materCode)
        {
            return this.service.GetListAddPlanInfoByWhere(planDate, equipcode, materCode);
        }
    }
}
