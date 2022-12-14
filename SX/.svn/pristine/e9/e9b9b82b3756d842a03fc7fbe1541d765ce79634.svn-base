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
    public class PstMaterialAdjustManager : BaseManager<PstMaterialAdjust>, IPstMaterialAdjustManager
    {
		#region 属性注入与构造方法
		
        private IPstMaterialAdjustService service;

        public PstMaterialAdjustManager()
        {
            this.service = new PstMaterialAdjustService();
            base.BaseService = this.service;
        }

		public PstMaterialAdjustManager(string connectStringKey)
        {
			this.service = new PstMaterialAdjustService(connectStringKey);
            base.BaseService = this.service;
        }

        public PstMaterialAdjustManager(NBear.Data.Gateway way)
        {
			this.service = new PstMaterialAdjustService(way);
            base.BaseService = this.service;
        }

        #endregion

        public class QueryParams : PstMaterialAdjustService.QueryParams
        {
        }

        public PageResult<PstMaterialAdjust> GetTablePageDataBySql(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }

        public PageResult<PstMaterialAdjust> GetTablePageDataBySqlPrint(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySqlPrint(queryParams);
        }

        public string GetBillNo()
        {
            return this.service.GetBillNo();
        }

        public DataSet GetDetailInfo(string billNo, string storageID, string storagePlaceID, string barcode, string orderID)
        {
            return this.service.GetDetailInfo(billNo, storageID, storagePlaceID, barcode, orderID);
        }

        public bool IsSameStorageType(string sourceStorageID, string targetStorageID)
        {
            return this.service.IsSameStorageType(sourceStorageID, targetStorageID);
        }

        public DataSet GetSqlInfo(string sql)
        {
            return this.service.GetSqlInfo(sql);
        }
    }
}
