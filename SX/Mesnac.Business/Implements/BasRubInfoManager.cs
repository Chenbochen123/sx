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
    public class BasRubInfoManager : BaseManager<BasRubInfo>, IBasRubInfoManager
    {
		#region ����ע���빹�췽��
		
        private IBasRubInfoService service;

        public BasRubInfoManager()
        {
            this.service = new BasRubInfoService();
            base.BaseService = this.service;
        }

		public BasRubInfoManager(string connectStringKey)
        {
			this.service = new BasRubInfoService(connectStringKey);
            base.BaseService = this.service;
        }

        public BasRubInfoManager(NBear.Data.Gateway way)
        {
			this.service = new BasRubInfoService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region ��ѯ�����ඨ��
        public class QueryParams : BasRubInfoService.QueryParams
        {
        }
        #endregion

        public PageResult<BasRubInfo> GetTablePageDataBySql(Mesnac.Data.Implements.BasRubInfoService.QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }

        public string GetNextRubInfoCode()
        {
            return this.service.GetNextRubInfoCode();
        }
    }
}
