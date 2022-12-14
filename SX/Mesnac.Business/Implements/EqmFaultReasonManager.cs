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
    public class EqmFaultReasonManager : BaseManager<EqmFaultReason>, IEqmFaultReasonManager
    {
		#region ����ע���빹�췽��
		
        private IEqmFaultReasonService service;

        public EqmFaultReasonManager()
        {
            this.service = new EqmFaultReasonService();
            base.BaseService = this.service;
        }

		public EqmFaultReasonManager(string connectStringKey)
        {
			this.service = new EqmFaultReasonService(connectStringKey);
            base.BaseService = this.service;
        }

        public EqmFaultReasonManager(NBear.Data.Gateway way)
        {
			this.service = new EqmFaultReasonService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region IEqmFaultReasonManager ��Ա

        public System.Data.DataSet GetDataByParas( EqmFaultReasonParams queryParams )
        {
            return this.service.GetDataByParas( queryParams );
        }
        #endregion

        #region ��ѯ�����ඨ��
        public class QueryParams : EqmFaultReasonService.QueryParams
        {
        }
        #endregion

        public PageResult<EqmFaultReason> GetEqmFaultReasonBySearchKey(Mesnac.Data.Implements.EqmFaultReasonService.QueryParams queryParams)
        {
            return this.service.GetEqmFaultReasonBySearchKey(queryParams);
        }
    }
}
