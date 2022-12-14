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
    public class PpmRubberReturnManager : BaseManager<PpmRubberReturn>, IPpmRubberReturnManager
    {
		#region ����ע���빹�췽��
		
        private IPpmRubberReturnService service;

        public PpmRubberReturnManager()
        {
            this.service = new PpmRubberReturnService();
            base.BaseService = this.service;
        }

		public PpmRubberReturnManager(string connectStringKey)
        {
			this.service = new PpmRubberReturnService(connectStringKey);
            base.BaseService = this.service;
        }

        public PpmRubberReturnManager(NBear.Data.Gateway way)
        {
			this.service = new PpmRubberReturnService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region ��ѯ�����ඨ��
        public class QueryParams : PpmRubberReturnService.QueryParams
        {
        }
        #endregion

        public PageResult<PpmRubberReturn> GetTablePageDataBySql(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }

        public string GetBillNo()
        {
            return this.service.GetBillNo();
        }
    }
}
