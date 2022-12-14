using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    using System.Data;
    using Mesnac.Data.Components;
    public class PpmRubberStorageDealManager : BaseManager<PpmRubberStorageDeal>, IPpmRubberStorageDealManager
    {
		#region 属性注入与构造方法
		
        private IPpmRubberStorageDealService service;
        public class QueryParams : PpmRubberStorageDealService.QueryParams
        {
        }
        public PpmRubberStorageDealManager()
        {
            this.service = new PpmRubberStorageDealService();
            base.BaseService = this.service;
        }

		public PpmRubberStorageDealManager(string connectStringKey)
        {
			this.service = new PpmRubberStorageDealService(connectStringKey);
            base.BaseService = this.service;
        }

        public PpmRubberStorageDealManager(NBear.Data.Gateway way)
        {
			this.service = new PpmRubberStorageDealService(way);
            base.BaseService = this.service;
        }

        #endregion

        public DataTable SubmitOutDateDeal(string BarCode, string StorageID, string StoragePlaceID, string DealWay, string DealDate, string DealRemark, string DealPerson)
        {
            return this.service.SubmitOutDateDeal(BarCode, StorageID, StoragePlaceID,DealWay,DealDate,DealRemark,DealPerson);
        }
        public DataTable GetDateQueryByCode(string BarCode, string StorageID, string StoragePlaceID)
        {
            return this.service.GetDateQueryByCode(BarCode,StorageID,StoragePlaceID);
        }
        public PageResult<PpmRubberStorageDeal> ProcPPMOutDateQueryInvalid(QueryParams queryParams, string workShop, string storageID, string storagePlaceID, string barCode, string shlefBarCode, int type,string dealperson)
        {
            return this.service.ProcPPMOutDateQueryInvalid(queryParams,  workShop, storageID, storagePlaceID,  barCode, shlefBarCode, type,dealperson);
        }
        public string SubmitRubberInValid(int dealid, string OperPerson)
        {
            return this.service.SubmitRubberInValid(dealid,OperPerson);
        }
        public string SubmitOutDateRubberInValid(int dealid, string OperPerson, string dealdate, string dealremark)
        {
            return this.service.SubmitOutDateRubberInValid(dealid, OperPerson,dealdate,dealremark);
        }
        public string SubmitRubberOutDateInValid(int dealid, string OperPerson, string dealway, string dealdate, string dealremark)
        {
            return this.service.SubmitRubberOutDateInValid(dealid, OperPerson, dealway, dealdate,dealremark);
        }
        public PageResult<PpmRubberStorageDeal> ProcPPMValidDateQuery(PpmRubberStorageDealManager.QueryParams queryParams, string workShop, string storageID, string storagePlaceID, string barCode, string shlefBarCode, int type,string dealperson)
        {
            return this.service.ProcPPMValidDateQuery(queryParams, workShop, storageID, storagePlaceID, barCode, shlefBarCode, type,dealperson);
        }
    }
}
