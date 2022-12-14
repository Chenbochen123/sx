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
    public class BasRubTyrePartManager : BaseManager<BasRubTyrePart>, IBasRubTyrePartManager
    {
		#region ����ע���빹�췽��
		
        private IBasRubTyrePartService service;

        public BasRubTyrePartManager()
        {
            this.service = new BasRubTyrePartService();
            base.BaseService = this.service;
        }

		public BasRubTyrePartManager(string connectStringKey)
        {
			this.service = new BasRubTyrePartService(connectStringKey);
            base.BaseService = this.service;
        }

        public BasRubTyrePartManager(NBear.Data.Gateway way)
        {
			this.service = new BasRubTyrePartService(way);
            base.BaseService = this.service;
        }

        #endregion
        #region ��ѯ�����ඨ��
        public class QueryParams : BasRubTyrePartService.QueryParams
        {
        }
        #endregion

        public PageResult<BasRubTyrePart> GetTablePageDataBySql(Mesnac.Data.Implements.BasRubTyrePartService.QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }
        public string GetNextTyrePartCode()
        {
            return this.service.GetNextTyrePartCode();
        }
    }
}
