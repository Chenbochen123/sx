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
    public class QmcFactoryNonCheckManager : BaseManager<QmcFactoryNonCheck>, IQmcFactoryNonCheckManager
    {
		#region ����ע���빹�췽��
		
        private IQmcFactoryNonCheckService service;

        public QmcFactoryNonCheckManager()
        {
            this.service = new QmcFactoryNonCheckService();
            base.BaseService = this.service;
        }

		public QmcFactoryNonCheckManager(string connectStringKey)
        {
			this.service = new QmcFactoryNonCheckService(connectStringKey);
            base.BaseService = this.service;
        }

        public QmcFactoryNonCheckManager(NBear.Data.Gateway way)
        {
			this.service = new QmcFactoryNonCheckService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region ��ѯ�����ඨ��
        public class QueryParams : QmcFactoryNonCheckService.QueryParams
        {
        }
        #endregion

        public PageResult<QmcFactoryNonCheck> GetTablePageDataBySql(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }
    }
}
