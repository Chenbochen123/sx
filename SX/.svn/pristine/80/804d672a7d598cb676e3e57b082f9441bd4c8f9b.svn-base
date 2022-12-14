using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    using Mesnac.Data.Components;
    public class PstMaterialStoreinManager : BaseManager<PstMaterialStorein>, IPstMaterialStoreinManager
    {
		#region 属性注入与构造方法
		
        private IPstMaterialStoreinService service;

        public PstMaterialStoreinManager()
        {
            this.service = new PstMaterialStoreinService();
            base.BaseService = this.service;
        }

		public PstMaterialStoreinManager(string connectStringKey)
        {
			this.service = new PstMaterialStoreinService(connectStringKey);
            base.BaseService = this.service;
        }

        public PstMaterialStoreinManager(NBear.Data.Gateway way)
        {
			this.service = new PstMaterialStoreinService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region 查询条件类定义
        public class QueryParams : PstMaterialStoreinService.QueryParams
        {
        }
        #endregion

        public PageResult<PstMaterialStorein> GetTablePageDataBySql(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }

        public PageResult<PstMaterialStorein> GetTablePageDataBySqlPrint(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySqlPrint(queryParams);
        }

        public string GetBillNo()
        {
            return this.service.GetBillNo();
        }

        public bool UpdateChkResultFlag(string StrBillNo, string UserID)
        {
            return this.service.UpdateChkResultFlag(StrBillNo, UserID);
        }

        public bool CancelChkResult(string StrBillNo, string UserID)
        {
            return this.service.CancelChkResult(StrBillNo, UserID);
        }

        public DataSet GetDetailInfo(string billNo, string storageID, string storagePlaceID, string barcode, string orderID)
        {
            return this.service.GetDetailInfo(billNo, storageID, storagePlaceID, barcode, orderID);
        }

        public DataSet GetSqlInfo(string sql)
        {
            return this.service.GetSqlInfo(sql);
        }
    }
}
