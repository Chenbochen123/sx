using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    using NBear.Common;
    public class PstMaterialInventoryManager : BaseManager<PstMaterialInventory>, IPstMaterialInventoryManager
    {
		#region ����ע���빹�췽��
		
        private IPstMaterialInventoryService service;

        public PstMaterialInventoryManager()
        {
            this.service = new PstMaterialInventoryService();
            base.BaseService = this.service;
        }

		public PstMaterialInventoryManager(string connectStringKey)
        {
			this.service = new PstMaterialInventoryService(connectStringKey);
            base.BaseService = this.service;
        }

        public PstMaterialInventoryManager(NBear.Data.Gateway way)
        {
			this.service = new PstMaterialInventoryService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region ��ѯ�����ඨ��
        public class QueryParams : PstMaterialInventoryService.QueryParams
        {
        }
        #endregion

        public PageResult<PstMaterialInventory> GetTablePageDataBySql(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }

        public string GetBillNo()
        {
            return this.service.GetBillNo();
        }

        public bool UpdateChkResultFlag(string StrBillNo, string UserID)
        {
            return this.service.UpdateChkResultFlag(StrBillNo, UserID);
        }
    }
}
