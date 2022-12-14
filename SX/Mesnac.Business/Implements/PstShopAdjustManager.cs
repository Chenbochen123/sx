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
    public class PstShopAdjustManager : BaseManager<PstShopAdjust>, IPstShopAdjustManager
    {
		#region 属性注入与构造方法
		
        private IPstShopAdjustService service;

        public PstShopAdjustManager()
        {
            this.service = new PstShopAdjustService();
            base.BaseService = this.service;
        }

		public PstShopAdjustManager(string connectStringKey)
        {
			this.service = new PstShopAdjustService(connectStringKey);
            base.BaseService = this.service;
        }

        public PstShopAdjustManager(NBear.Data.Gateway way)
        {
			this.service = new PstShopAdjustService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region 查询条件类定义
        public class QueryParams : PstShopAdjustService.QueryParams
        {
        }
        #endregion

        public PageResult<PstShopAdjust> GetTablePageDataBySql(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }

        public PageResult<PstShopAdjust> GetTablePageTotalBySql(QueryParams queryParams)
        {
            return this.service.GetTablePageTotalBySql(queryParams);
        }

        public string CancelShopAdjust(string storageID, string storagePlaceID, string barcode, int orderID, string operPerson)
        {
            return this.service.CancelShopAdjust(storageID, storagePlaceID, barcode, orderID, operPerson);
        }

        public DataSet GetByInfo(string workShopCode, DateTime beginTime, DateTime endTime, string storageID, string storagePlaceID, string materCode, string adjustType)
        {
            return this.service.GetByInfo(workShopCode, beginTime, endTime, storageID, storagePlaceID, materCode, adjustType);
        }
        public DataSet GetReportBySql(PstShopAdjustManager.QueryParams queryParams)
        {
            return this.service.GetReportBySql(queryParams);
        }
    }
}
