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
    public class EqmSparePartManager : BaseManager<EqmSparePart>, IEqmSparePartManager
    {
		#region ����ע���빹�췽��
		
        private IEqmSparePartService service;

        public EqmSparePartManager()
        {
            this.service = new EqmSparePartService();
            base.BaseService = this.service;
        }

		public EqmSparePartManager(string connectStringKey)
        {
			this.service = new EqmSparePartService(connectStringKey);
            base.BaseService = this.service;
        }

        public EqmSparePartManager(NBear.Data.Gateway way)
        {
			this.service = new EqmSparePartService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region ��ѯ�����ඨ��
        public class QueryParams : EqmSparePartService.QueryParams
        {
        }
        #endregion
        public PageResult<EqmSparePart> GetTablePageDataBySql(Mesnac.Data.Implements.EqmSparePartService.QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }


        public string GetNextSparePartCode(string MajorTypeID, string MinorTypeID)
        {
            return this.service.GetNextSparePartCode(MajorTypeID, MinorTypeID);
        }

        public PageResult<EqmSparePart> GetSparePartBySearchKey(Mesnac.Data.Implements.EqmSparePartService.QueryParams queryParams)
        {
            return this.service.GetSparePartBySearchKey(queryParams);
        }
    }
}
