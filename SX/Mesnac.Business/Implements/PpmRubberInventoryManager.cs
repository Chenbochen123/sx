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
    public class PpmRubberInventoryManager : BaseManager<PpmRubberInventory>, IPpmRubberInventoryManager
    {
		#region ����ע���빹�췽��
		
        private IPpmRubberInventoryService service;

        public PpmRubberInventoryManager()
        {
            this.service = new PpmRubberInventoryService();
            base.BaseService = this.service;
        }

		public PpmRubberInventoryManager(string connectStringKey)
        {
			this.service = new PpmRubberInventoryService(connectStringKey);
            base.BaseService = this.service;
        }

        public PpmRubberInventoryManager(NBear.Data.Gateway way)
        {
			this.service = new PpmRubberInventoryService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region ��ѯ�����ඨ��
        public class QueryParams : PpmRubberInventoryService.QueryParams
        {
        }
        #endregion

        public PageResult<PpmRubberInventory> GetTablePageDataBySql(QueryParams queryParams)
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
