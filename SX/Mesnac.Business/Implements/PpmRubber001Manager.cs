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
    public class PpmRubber001Manager : BaseManager<PpmRubber001>, IPpmRubber001Manager
    {
		#region 属性注入与构造方法
		
        private IPpmRubber001Service service;

        public PpmRubber001Manager()
        {
            this.service = new PpmRubber001Service();
            base.BaseService = this.service;
        }

		public PpmRubber001Manager(string connectStringKey)
        {
			this.service = new PpmRubber001Service(connectStringKey);
            base.BaseService = this.service;
        }

        public PpmRubber001Manager(NBear.Data.Gateway way)
        {
			this.service = new PpmRubber001Service(way);
            base.BaseService = this.service;
        }

        #endregion

        public class QueryParams : PpmRubber001Service.QueryParams
        {
        }

        public PageResult<PpmRubber001> GetTablePageDataBySql(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }

        public DataSet GetCondition(string ObjID)
        {
            return this.service.GetCondition(ObjID);
        }
    }
}
