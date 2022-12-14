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
    public class BasFactoryInfoManager : BaseManager<BasFactoryInfo>, IBasFactoryInfoManager
    {
		#region ����ע���빹�췽��
		
        private IBasFactoryInfoService service;

        public BasFactoryInfoManager()
        {
            this.service = new BasFactoryInfoService();
            base.BaseService = this.service;
        }

		public BasFactoryInfoManager(string connectStringKey)
        {
			this.service = new BasFactoryInfoService(connectStringKey);
            base.BaseService = this.service;
        }

        public BasFactoryInfoManager(NBear.Data.Gateway way)
        {
			this.service = new BasFactoryInfoService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region ��ѯ�����ඨ��
        public class QueryParams : BasFactoryInfoService.QueryParams
        {
        }
        #endregion

        public PageResult<BasFactoryInfo> GetTablePageDataBySql(Mesnac.Data.Implements.BasFactoryInfoService.QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }
        public string GetNextFactoryCode()
        {
            return this.service.GetNextFactoryCode();
        }
    }
}
