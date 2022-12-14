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
    public class QmcPropertyManager : BaseManager<QmcProperty>, IQmcPropertyManager
    {
		#region ����ע���빹�췽��
		
        private IQmcPropertyService service;

        public QmcPropertyManager()
        {
            this.service = new QmcPropertyService();
            base.BaseService = this.service;
        }

		public QmcPropertyManager(string connectStringKey)
        {
			this.service = new QmcPropertyService(connectStringKey);
            base.BaseService = this.service;
        }

        public QmcPropertyManager(NBear.Data.Gateway way)
        {
			this.service = new QmcPropertyService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region ��ѯ�����ඨ��
        public class QueryParams : QmcPropertyService.QueryParams
        {
        }
        #endregion

        public PageResult<QmcProperty> GetTablePageDataBySql(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }

        public string GetNextPropertyId()
        {
            return this.service.GetNextPropertyId();
        }
    }
}
