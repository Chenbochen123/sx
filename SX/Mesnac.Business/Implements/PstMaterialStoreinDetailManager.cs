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
    public class PstMaterialStoreinDetailManager : BaseManager<PstMaterialStoreinDetail>, IPstMaterialStoreinDetailManager
    {
		#region ����ע���빹�췽��
		
        private IPstMaterialStoreinDetailService service;

        public PstMaterialStoreinDetailManager()
        {
            this.service = new PstMaterialStoreinDetailService();
            base.BaseService = this.service;
        }

		public PstMaterialStoreinDetailManager(string connectStringKey)
        {
			this.service = new PstMaterialStoreinDetailService(connectStringKey);
            base.BaseService = this.service;
        }

        public PstMaterialStoreinDetailManager(NBear.Data.Gateway way)
        {
			this.service = new PstMaterialStoreinDetailService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region ��ѯ�����ඨ��
        public class QueryParams : PstMaterialStoreinDetailService.QueryParams
        {
        }
        #endregion

        public PageResult<PstMaterialStoreinDetail> GetTablePageDataBySql(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }

        public DataSet GetByBillNo(string BillNo)
        {
            return this.service.GetByBillNo(BillNo);
        }

        public DataSet GetByOtherBillNo(string BillNo, string Barcode, string OrderID)
        {
            return this.service.GetByOtherBillNo(BillNo, Barcode, OrderID);
        }

        public DataSet GetFromChkdetail(string BillNo)
        {
            return this.service.GetFromChkdetail(BillNo);
        }

        public bool UpdateStorage(string BillNo, string StorageID, string StoragePlaceID)
        {
            return this.service.UpdateStorage(BillNo, StorageID, StoragePlaceID);
        }

        public PstMaterialStoreinDetail GetStoreinDetail(string BillNo, string Barcode)
        {
            return this.service.GetStoreinDetail(BillNo, Barcode);
        }
    }
}
