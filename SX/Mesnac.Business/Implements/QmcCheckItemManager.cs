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
    public class QmcCheckItemManager : BaseManager<QmcCheckItem>, IQmcCheckItemManager
    {
		#region ����ע���빹�췽��
		
        private IQmcCheckItemService service;

        public QmcCheckItemManager()
        {
            this.service = new QmcCheckItemService();
            base.BaseService = this.service;
        }

		public QmcCheckItemManager(string connectStringKey)
        {
			this.service = new QmcCheckItemService(connectStringKey);
            base.BaseService = this.service;
        }

        public QmcCheckItemManager(NBear.Data.Gateway way)
        {
			this.service = new QmcCheckItemService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region ��ѯ�����ඨ��
        public class QueryParams : QmcCheckItemService.QueryParams
        {
        }
        #endregion

        public PageResult<QmcCheckItem> GetTablePageDataBySql(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }

        public string GetNextItemId()
        {
            return this.service.GetNextItemId();
        }
    }
}
