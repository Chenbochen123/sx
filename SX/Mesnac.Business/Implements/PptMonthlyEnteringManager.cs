using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    using Mesnac.Data.Components;
    public class PptMonthlyEnteringManager : BaseManager<PptMonthlyEntering>, IPptMonthlyEnteringManager
    {
		#region ����ע���빹�췽��
		
        private IPptMonthlyEnteringService service;

        public PptMonthlyEnteringManager()
        {
            this.service = new PptMonthlyEnteringService();
            base.BaseService = this.service;
        }

		public PptMonthlyEnteringManager(string connectStringKey)
        {
			this.service = new PptMonthlyEnteringService(connectStringKey);
            base.BaseService = this.service;
        }

        public PptMonthlyEnteringManager(NBear.Data.Gateway way)
        {
			this.service = new PptMonthlyEnteringService(way);
            base.BaseService = this.service;
        }

        #endregion
        public class QueryParams : PptMonthlyEnteringService.QueryParams
        {
        }

        #region IPptMonthlyEnteringManager ��Ա
        public PageResult<PptMonthlyEntering> GetPptMonthlyEnteringPageDataBySql(PptMonthlyEnteringService.QueryParams queryParams)
        {
            return this.service.GetPptMonthlyEnteringPageDataBySql(queryParams);
        }
        #endregion
    }
}
