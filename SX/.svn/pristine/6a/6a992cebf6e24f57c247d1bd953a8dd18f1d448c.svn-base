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
    public class Ppm_rubDaySumManager : BaseManager<Ppm_rubDaySum>, IPpm_rubDaySumManager
    {
		#region 属性注入与构造方法
		
        private IPpm_rubDaySumService service;

        public Ppm_rubDaySumManager()
        {
            this.service = new Ppm_rubDaySumService();
            base.BaseService = this.service;
        }

		public Ppm_rubDaySumManager(string connectStringKey)
        {
			this.service = new Ppm_rubDaySumService(connectStringKey);
            base.BaseService = this.service;
        }

        public Ppm_rubDaySumManager(NBear.Data.Gateway way)
        {
			this.service = new Ppm_rubDaySumService(way);
            base.BaseService = this.service;
        }

        #endregion
        public class QueryParams : Ppm_rubDaySumService.QueryParams
        {
        }
        public DataTable GetTableStoreDaySum(Ppm_rubDaySumManager.QueryParams queryParams)
        {
            return this.service.GetTableStoreDaySum(queryParams);
        }
    }
}
