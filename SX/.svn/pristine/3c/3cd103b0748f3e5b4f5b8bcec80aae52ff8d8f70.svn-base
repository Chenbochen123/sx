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
    public class PpmRubberStoreoutManager : BaseManager<PpmRubberStoreout>, IPpmRubberStoreoutManager
    {
		#region 属性注入与构造方法
		
        private IPpmRubberStoreoutService service;

        public PpmRubberStoreoutManager()
        {
            this.service = new PpmRubberStoreoutService();
            base.BaseService = this.service;
        }

		public PpmRubberStoreoutManager(string connectStringKey)
        {
			this.service = new PpmRubberStoreoutService(connectStringKey);
            base.BaseService = this.service;
        }

        public PpmRubberStoreoutManager(NBear.Data.Gateway way)
        {
			this.service = new PpmRubberStoreoutService(way);
            base.BaseService = this.service;
        }

        #endregion

        public class QueryParams : PpmRubberStoreoutService.QueryParams
        {
        }

        public PageResult<PpmRubberStoreout> GetTablePageDataBySql(QueryParams queryParams)
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
