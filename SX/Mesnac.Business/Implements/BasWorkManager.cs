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
    public class BasWorkManager : BaseManager<BasWork>, IBasWorkManager
    {
		#region ����ע���빹�췽��
		
        private IBasWorkService service;

        public BasWorkManager()
        {
            this.service = new BasWorkService();
            base.BaseService = this.service;
        }

		public BasWorkManager(string connectStringKey)
        {
			this.service = new BasWorkService(connectStringKey);
            base.BaseService = this.service;
        }

        public BasWorkManager(NBear.Data.Gateway way)
        {
			this.service = new BasWorkService(way);
            base.BaseService = this.service;
        }

        #endregion
        #region ��ѯ�����ඨ��
        public class QueryParams : BasWorkService.QueryParams
        {
        }
        #endregion
        public PageResult<BasWork> GetTablePageDataBySql(Mesnac.Data.Implements.BasWorkService.QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }
        public string GetNextWorkPositionCode() {
            return this.service.GetNextWorkPositionCode();
        }
    }
}
