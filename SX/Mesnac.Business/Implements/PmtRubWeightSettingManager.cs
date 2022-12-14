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
    public class PmtRubWeightSettingManager : BaseManager<PmtRubWeightSetting>, IPmtRubWeightSettingManager
    {
		#region ����ע���빹�췽��
		
        private IPmtRubWeightSettingService service;

        public PmtRubWeightSettingManager()
        {
            this.service = new PmtRubWeightSettingService();
            base.BaseService = this.service;
        }

		public PmtRubWeightSettingManager(string connectStringKey)
        {
			this.service = new PmtRubWeightSettingService(connectStringKey);
            base.BaseService = this.service;
        }

        public PmtRubWeightSettingManager(NBear.Data.Gateway way)
        {
			this.service = new PmtRubWeightSettingService(way);
            base.BaseService = this.service;
        }

        #endregion 
        #region ��ѯ�����ඨ��
        public class QueryParams : PmtRubWeightSettingService.QueryParams
        {
        }
        #endregion
        public PageResult<PmtRubWeightSetting> GetTablePageDataBySql(Mesnac.Data.Implements.PmtRubWeightSettingService.QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }
    }
}
