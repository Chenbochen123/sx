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
    public class EqmSapSparePartManager : BaseManager<EqmSapSparePart>, IEqmSapSparePartManager
    {
		#region ����ע���빹�췽��
		
        private IEqmSapSparePartService service;

        public EqmSapSparePartManager()
        {
            this.service = new EqmSapSparePartService();
            base.BaseService = this.service;
        }

		public EqmSapSparePartManager(string connectStringKey)
        {
			this.service = new EqmSapSparePartService(connectStringKey);
            base.BaseService = this.service;
        }

        public EqmSapSparePartManager(NBear.Data.Gateway way)
        {
			this.service = new EqmSapSparePartService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region ��ѯ�����ඨ��
        public class QueryParams : EqmSapSparePartService.QueryParams
        {
        }
        #endregion
        public PageResult<EqmSapSparePart> GetTablePageDataBySql(Mesnac.Data.Implements.EqmSapSparePartService.QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }
        //��ȡ��һ�������
        public string GetNextSparePartStoreInCode(DateTime storeInDate)
        {
            return this.service.GetNextSparePartStoreInCode(storeInDate);
        }
    }
}
