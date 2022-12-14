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
    public class PstMaterialInventoryDetailManager : BaseManager<PstMaterialInventoryDetail>, IPstMaterialInventoryDetailManager
    {
		#region ����ע���빹�췽��
		
        private IPstMaterialInventoryDetailService service;

        public PstMaterialInventoryDetailManager()
        {
            this.service = new PstMaterialInventoryDetailService();
            base.BaseService = this.service;
        }

		public PstMaterialInventoryDetailManager(string connectStringKey)
        {
			this.service = new PstMaterialInventoryDetailService(connectStringKey);
            base.BaseService = this.service;
        }

        public PstMaterialInventoryDetailManager(NBear.Data.Gateway way)
        {
			this.service = new PstMaterialInventoryDetailService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region ��ѯ�����ඨ��
        public class QueryParams : PstMaterialInventoryDetailService.QueryParams
        {
        }
        #endregion

        public PageResult<PstMaterialInventoryDetail> GetTablePageDataBySql(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }

        public int GetByStorage(string billNo, string storageID, string inventoryDate)
        {
            return this.service.GetByStorage(billNo, storageID, inventoryDate);
        }

        public DataSet GetByBillNo(string BillNo)
        {
            return this.service.GetByBillNo(BillNo);
        }
    }
}
