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
    public class PpmRubberAdjustManager : BaseManager<PpmRubberAdjust>, IPpmRubberAdjustManager
    {
		#region 属性注入与构造方法
		
        private IPpmRubberAdjustService service;

        public PpmRubberAdjustManager()
        {
            this.service = new PpmRubberAdjustService();
            base.BaseService = this.service;
        }

		public PpmRubberAdjustManager(string connectStringKey)
        {
			this.service = new PpmRubberAdjustService(connectStringKey);
            base.BaseService = this.service;
        }

        public PpmRubberAdjustManager(NBear.Data.Gateway way)
        {
			this.service = new PpmRubberAdjustService(way);
            base.BaseService = this.service;
        }

        #endregion

        public class QueryParams : PpmRubberAdjustService.QueryParams
        {
        }

        public PageResult<PpmRubberAdjust> GetTablePageDataBySql(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }

        //public string GetBillNo()
        //{
        //    return this.service.GetBillNo();
        //}

        public string SubmitRubberAdjust(string storageID, string storagePlaceID, string barcode, decimal realWeight, string operPerson, string shiftID, string shiftClassID, string toStorageID, string toStoragePlaceID)
        {
            return this.service.SubmitRubberAdjust(storageID, storagePlaceID, barcode, realWeight, operPerson, shiftID, shiftClassID, toStorageID, toStoragePlaceID);
        }

        public string CancelRubberAdjust(string storageID, string storagePlaceID, string barcode, decimal realWeight, string toStorageID, string toStoragePlaceID)
        {
            return this.service.CancelRubberAdjust(storageID, storagePlaceID, barcode, realWeight, toStorageID, toStoragePlaceID);
        }
        public  DataSet GetRubberAdjustReportBySql(PpmRubberAdjustManager.QueryParams queryParams)
        {
            return this.service.GetRubberAdjustReportBySql(queryParams);
        }
        public DataSet GetRubberAdjustDetailReportBySql(PpmRubberAdjustManager.QueryParams queryParams)
        { return this.service.GetRubberAdjustDetailReportBySql(queryParams); }

    }
}
