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
    public class PpmReturnRubberManager : BaseManager<PpmReturnRubber>, IPpmReturnRubberManager
    {
		#region 属性注入与构造方法
		
        private IPpmReturnRubberService service;

        public PpmReturnRubberManager()
        {
            this.service = new PpmReturnRubberService();
            base.BaseService = this.service;
        }

		public PpmReturnRubberManager(string connectStringKey)
        {
			this.service = new PpmReturnRubberService(connectStringKey);
            base.BaseService = this.service;
        }

        public PpmReturnRubberManager(NBear.Data.Gateway way)
        {
			this.service = new PpmReturnRubberService(way);
            base.BaseService = this.service;
        }

        #endregion

        public class QueryParams : PpmReturnRubberService.QueryParams
        {
        }

        public PageResult<PpmReturnRubber> GetTablePageDataBySql(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }

        public string SubmitReturnRubber(string storageID, string storagePlaceID, string barcode, decimal realWeight, string operPerson, string shiftID, string shiftClassID)
        {
            return this.service.SubmitReturnRubber(storageID, storagePlaceID, barcode, realWeight, operPerson, shiftID, shiftClassID);
        }

        public string CancelReturnRubber(string storageID, string storagePlaceID, string barcode)
        {
            return this.service.CancelReturnRubber(storageID, storagePlaceID, barcode);
        }

        public DataSet GetDayReport(string PlanDate, string workShopCode)
        {
            return this.service.GetDayReport(PlanDate, workShopCode);
        }

        public DataSet GetReturnRubberInfo(string barcode)
        {
            return this.service.GetReturnRubberInfo(barcode);
        }
    }
}
