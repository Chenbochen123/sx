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
    public class PstmmshopoutManager : BaseManager<Pstmmshopout>, IPstmmshopoutManager
    {
		#region 属性注入与构造方法
		
        private IPstmmshopoutService service;

        public PstmmshopoutManager()
        {
            this.service = new PstmmshopoutService();
            base.BaseService = this.service;
        }

		public PstmmshopoutManager(string connectStringKey)
        {
			this.service = new PstmmshopoutService(connectStringKey);
            base.BaseService = this.service;
        }

        public PstmmshopoutManager(NBear.Data.Gateway way)
        {
			this.service = new PstmmshopoutService(way);
            base.BaseService = this.service;
        }

        #endregion

        public class QueryParams : PstmmshopoutService.QueryParams
        {
        }

        public PageResult<Pstmmshopout> GetTablePageDataBySql(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }

        public DataSet GetShopConsumeTotal(string beginDate, string endDate, string materType, string classid, string equipcode)
        {
            return this.service.GetShopConsumeTotal(beginDate, endDate, materType,classid,equipcode);
        }
    }
}
