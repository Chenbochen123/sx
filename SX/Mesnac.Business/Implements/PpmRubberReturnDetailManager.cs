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
    public class PpmRubberReturnDetailManager : BaseManager<PpmRubberReturnDetail>, IPpmRubberReturnDetailManager
    {
		#region ����ע���빹�췽��
		
        private IPpmRubberReturnDetailService service;

        public PpmRubberReturnDetailManager()
        {
            this.service = new PpmRubberReturnDetailService();
            base.BaseService = this.service;
        }

		public PpmRubberReturnDetailManager(string connectStringKey)
        {
			this.service = new PpmRubberReturnDetailService(connectStringKey);
            base.BaseService = this.service;
        }

        public PpmRubberReturnDetailManager(NBear.Data.Gateway way)
        {
			this.service = new PpmRubberReturnDetailService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region ��ѯ�����ඨ��
        public class QueryParams : PpmRubberReturnDetailService.QueryParams
        {
        }
        #endregion

        public PageResult<PpmRubberReturnDetail> GetTablePageDataBySql(QueryParams queryParams)
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
    }
}
