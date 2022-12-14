using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    using NBear.Common;
    public class PstPlanGetMaterManager : BaseManager<PstPlanGetMater>, IPstPlanGetMaterManager
    {
		#region 属性注入与构造方法
		
        private IPstPlanGetMaterService service;

        public PstPlanGetMaterManager()
        {
            this.service = new PstPlanGetMaterService();
            base.BaseService = this.service;
        }

		public PstPlanGetMaterManager(string connectStringKey)
        {
			this.service = new PstPlanGetMaterService(connectStringKey);
            base.BaseService = this.service;
        }

        public PstPlanGetMaterManager(NBear.Data.Gateway way)
        {
			this.service = new PstPlanGetMaterService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region 查询条件类定义
        public class QueryParams : PstPlanGetMaterService.QueryParams
        {
        }
        #endregion

        public PageResult<PstPlanGetMater> GetTablePageDataBySql(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }

        public bool JudgeExistPlan(string PlanDate)
        {
            return this.service.JudgeExistPlan(PlanDate);
        }

        public DataSet GetPlanMaterInfo(string ObjID)
        {
            return this.service.GetPlanMaterInfo(ObjID);
        }
        public void ReSetMater(string PlanDate)
        {
            this.service.ReSetMater(PlanDate);
        }
    }
}
