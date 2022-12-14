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
    public class PpmRubberJZByShiftManager : BaseManager<PpmRubberJZByShift>, IPpmRubberJZByShiftManager
    {
		#region 属性注入与构造方法
		
        private IPpmRubberJZByShiftService service;

        public PpmRubberJZByShiftManager()
        {
            this.service = new PpmRubberJZByShiftService();
            base.BaseService = this.service;
        }

		public PpmRubberJZByShiftManager(string connectStringKey)
        {
			this.service = new PpmRubberJZByShiftService(connectStringKey);
            base.BaseService = this.service;
        }

        public PpmRubberJZByShiftManager(NBear.Data.Gateway way)
        {
			this.service = new PpmRubberJZByShiftService(way);
            base.BaseService = this.service;
        }

        #endregion

        public class QueryParams : PpmRubberJZByShiftService.QueryParams
        {
        }

        public PageResult<PpmRubberJZByShift> GetTablePageDataBySql(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }

        public string DataAllowAddJZ(string PlanDate, string WorkShopCode, string ShiftID, string MaterCode, string IsAdd)
        {
            return this.service.DataAllowAddJZ(PlanDate, WorkShopCode, ShiftID, MaterCode, IsAdd);
        }
    }
}
