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
    public class PstMaterialStoreoutManager : BaseManager<PstMaterialStoreout>, IPstMaterialStoreoutManager
    {
		#region 属性注入与构造方法
		
        private IPstMaterialStoreoutService service;

        public PstMaterialStoreoutManager()
        {
            this.service = new PstMaterialStoreoutService();
            base.BaseService = this.service;
        }

		public PstMaterialStoreoutManager(string connectStringKey)
        {
			this.service = new PstMaterialStoreoutService(connectStringKey);
            base.BaseService = this.service;
        }

        public PstMaterialStoreoutManager(NBear.Data.Gateway way)
        {
			this.service = new PstMaterialStoreoutService(way);
            base.BaseService = this.service;
        }

        #endregion

        public class QueryParams : PstMaterialStoreoutService.QueryParams
        {
        }

        public PageResult<PstMaterialStoreout> GetTablePageDataBySql(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }

        public string GetBillNo()
        {
            return this.service.GetBillNo();
        }

        public bool UpdateLockedFlag(string StrBillNo, string ChkPerson)
        {
            return this.service.UpdateLockedFlag(StrBillNo, ChkPerson);
        }

        public DataSet GetStorageInfo(string BillNo)
        {
            return this.service.GetStorageInfo(BillNo);
        }

        public bool CancelLocked(string StrBillNo, string ChkPerson)
        {
            return this.service.CancelLocked(StrBillNo, ChkPerson);
        }
    }
}
