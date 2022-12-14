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
    public class PstMaterialChkDetailManager : BaseManager<PstMaterialChkDetail>, IPstMaterialChkDetailManager
    {
		#region 属性注入与构造方法
		
        private IPstMaterialChkDetailService service;

        public PstMaterialChkDetailManager()
        {
            this.service = new PstMaterialChkDetailService();
            base.BaseService = this.service;
        }

		public PstMaterialChkDetailManager(string connectStringKey)
        {
			this.service = new PstMaterialChkDetailService(connectStringKey);
            base.BaseService = this.service;
        }

        public PstMaterialChkDetailManager(NBear.Data.Gateway way)
        {
			this.service = new PstMaterialChkDetailService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region 查询条件类定义
        public class QueryParams : PstMaterialChkDetailService.QueryParams
        {
        }
        #endregion

        public PageResult<PstMaterialChkDetail> GetTablePageDataBySql(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }

        public PageResult<PstMaterialChkDetail> GetCheckSequence(QueryParams queryParams)
        {
            return this.service.GetCheckSequence(queryParams);
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

        public PstMaterialChkDetail GetEntity(string BillNo, string Barcode, string OrderID)
        {
            return this.service.GetEntity(BillNo, Barcode, OrderID);
        }

        public DataSet GetPstMaterialCheckDetailQueryInfoByParams(IPstMaterialCheckDetailQueryParams paras)
        {
            return this.service.GetPstMaterialCheckDetailQueryInfoByParams(paras);
        }

        public DataSet GetAddLedgerCheckDetail(string BillNo, string Barcode, string OrderID)
        {
            return this.service.GetAddLedgerCheckDetail(BillNo, Barcode, OrderID);
        }

        public string GetLLBarcode(string Barcode)
        {
            return this.service.GetLLBarcode(Barcode);
        }

        public string GetChkResult(string Barcode)
        {
            return this.service.GetChkResult(Barcode);
        }
    }
}
