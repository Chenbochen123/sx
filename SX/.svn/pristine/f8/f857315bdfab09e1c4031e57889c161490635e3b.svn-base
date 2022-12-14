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
    public class PpmRubberChkDetailManager : BaseManager<PpmRubberChkDetail>, IPpmRubberChkDetailManager
    {
		#region 属性注入与构造方法
		
        private IPpmRubberChkDetailService service;

        public PpmRubberChkDetailManager()
        {
            this.service = new PpmRubberChkDetailService();
            base.BaseService = this.service;
        }

		public PpmRubberChkDetailManager(string connectStringKey)
        {
			this.service = new PpmRubberChkDetailService(connectStringKey);
            base.BaseService = this.service;
        }

        public PpmRubberChkDetailManager(NBear.Data.Gateway way)
        {
			this.service = new PpmRubberChkDetailService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region 查询条件类定义
        public class QueryParams : PpmRubberChkDetailService.QueryParams
        {
        }
        #endregion

        public PageResult<PpmRubberChkDetail> GetTablePageDataBySql(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }

        public DataSet GetByBillNo(string BillNo, string IsStoreIn)
        {
            return this.service.GetByBillNo(BillNo, IsStoreIn);
        }

        public DataSet GetByOtherBillNo(string BillNo, string Barcode, string OrderID)
        {
            return this.service.GetByOtherBillNo(BillNo, Barcode, OrderID);
        }

        public DataSet GetNoPassByBillNo(string BillNo)
        {
            return this.service.GetNoPassByBillNo(BillNo);
        }

        public string GetBarcode(string BillNo)
        {
            return this.service.GetBarcode(BillNo);
        }

        public bool UpdateSendChkFlag(string StrBillNo, string SendPerson)
        {
            return this.service.UpdateSendChkFlag(StrBillNo, SendPerson);
        }

        public PpmRubberChkDetail GetEntity(string BillNo, string Barcode, string OrderID)
        {
            return this.service.GetEntity(BillNo, Barcode, OrderID);
        }
    }
}
