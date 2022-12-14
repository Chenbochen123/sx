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
    public class PpmSemiStorageManager : BaseManager<PpmSemiStorage>, IPpmSemiStorageManager
    {
		#region 属性注入与构造方法
		
        private IPpmSemiStorageService service;

        public PpmSemiStorageManager()
        {
            this.service = new PpmSemiStorageService();
            base.BaseService = this.service;
        }

		public PpmSemiStorageManager(string connectStringKey)
        {
			this.service = new PpmSemiStorageService(connectStringKey);
            base.BaseService = this.service;
        }

        public PpmSemiStorageManager(NBear.Data.Gateway way)
        {
			this.service = new PpmSemiStorageService(way);
            base.BaseService = this.service;
        }

        #endregion

        public class QueryParams : PpmSemiStorageService.QueryParams
        {
        }

        public PageResult<PpmSemiStorage> GetTablePageDataBySql(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }

        public string SubmitRubberBack(string storageID, string storagePlaceID, string barcode, decimal backWeight, string normalFlag, string backReason, string shiftID, string operPerson)
        {
            return this.service.SubmitRubberBack(storageID, storagePlaceID, barcode, backWeight, normalFlag, backReason, shiftID, operPerson);
        }

        public string CancelRubberBack(string barcode, string shiftID, string operPerson)
        {
            return this.service.CancelRubberBack(barcode, shiftID, operPerson);
        }
    }
}
