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
    public class PpmRubberStorageManager : BaseManager<PpmRubberStorage>, IPpmRubberStorageManager
    {
		#region 属性注入与构造方法
		
        private IPpmRubberStorageService service;

        public PpmRubberStorageManager()
        {
            this.service = new PpmRubberStorageService();
            base.BaseService = this.service;
        }

		public PpmRubberStorageManager(string connectStringKey)
        {
			this.service = new PpmRubberStorageService(connectStringKey);
            base.BaseService = this.service;
        }

        public PpmRubberStorageManager(NBear.Data.Gateway way)
        {
			this.service = new PpmRubberStorageService(way);
            base.BaseService = this.service;
        }

        #endregion

        public class QueryParams : PpmRubberStorageService.QueryParams
        {
        }
        public PageResult<PpmRubberStorage> GetTablePageDataBySql(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }
        public PageResult<PpmRubberStorage> GetTablePageStoreoutBySql(QueryParams queryParams)
        {
            return this.service.GetTablePageStoreoutBySql(queryParams);
        }
        public string SubmitRubberStoreOut(string storageID, string storagePlaceID, string barcode, string shiftID, string shiftClassID, string operPerson, string toStorageID, string toStoragePlaceID)
        {
            return this.service.SubmitRubberStoreOut(storageID, storagePlaceID, barcode, shiftID, shiftClassID, operPerson, toStorageID, toStoragePlaceID);
        }
        public string CancelReturnRubber(string storageID, string storagePlaceID, string barcode)
        {
            return this.service.CancelReturnRubber(storageID, storagePlaceID, barcode);
        }
        public PageResult<PpmRubberStorage> ProcPPMOutDateQuery(QueryParams queryParams, string startDate, string endDate, string workShop, string storageID, string storagePlaceID, int limit, string barCode, string shlefBarCode, int page, int pagenum,string matercode)
        {
            return this.service.ProcPPMOutDateQuery(queryParams,startDate, endDate, workShop, storageID, storagePlaceID, limit, barCode, shlefBarCode, page, pagenum,matercode);
        }
        public DataSet ProcPPMOutDateTotalQuery(string startDate, string endDate, string workShop, string storageID, string storagePlaceID, int limit, string barCode, string shlefBarCode)
        {
            return this.service.ProcPPMOutDateTotalQuery(startDate, endDate, workShop, storageID, storagePlaceID, limit, barCode, shlefBarCode);
        }
        public PageResult<PpmRubberStorage> ProcPPMOutDateQueryDeal(QueryParams queryParams,string matercode, string startDate, string endDate, string workShop, string storageID, string storagePlaceID, string barCode, string shlefBarCode, int type, string orderway, int page, int pagenum)
        {
            return this.service.ProcPPMOutDateQueryDeal(queryParams,matercode,startDate, endDate, workShop, storageID, storagePlaceID, barCode, shlefBarCode, type, orderway, page, pagenum);
        }
        public DataTable GetTableStoreOutReport(PpmRubberStorageManager.QueryParams queryParams)
        {
            return this.service.GetTableStoreOutReport(queryParams);
        }
        public DataTable GetTableStoreOutDetailReport(PpmRubberStorageManager.QueryParams queryParams)
        {
            return this.service.GetTableStoreOutDetailReport(queryParams);
        }
        public DataTable GetTableStoreBackReport(PpmRubberStorageManager.QueryParams queryParams)
        {
            return this.service.GetTableStoreBackReport(queryParams);
        }
        public DataTable GetTableStoreBackDetailReport(PpmRubberStorageManager.QueryParams queryParams)
        {
            return this.service.GetTableStoreBackDetailReport(queryParams);
        }
    }
}
