using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    public class BasEquipPartInfoManager : BaseManager<BasEquipPartInfo>, IBasEquipPartInfoManager
    {
		#region ����ע���빹�췽��
		
        private IBasEquipPartInfoService service;

        public BasEquipPartInfoManager()
        {
            this.service = new BasEquipPartInfoService();
            base.BaseService = this.service;
        }

		public BasEquipPartInfoManager(string connectStringKey)
        {
			this.service = new BasEquipPartInfoService(connectStringKey);
            base.BaseService = this.service;
        }

        public BasEquipPartInfoManager(NBear.Data.Gateway way)
        {
			this.service = new BasEquipPartInfoService(way);
            base.BaseService = this.service;
        }

        #endregion
        #region IBasEquipManager ��Ա
        #region ��ѯ�����ඨ��
        public class QueryParams : BasEquipPartInfoService.QueryParams
        {
        }
        #endregion
        public Data.Components.PageResult<BasEquipPartInfo> GetTablePageDataBySql(BasEquipPartInfoService.QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }

        public string GetNextEquipPartInfoCode(string equipTypeCode)
        {
            return this.service.GetNextEquipPartInfoCode(equipTypeCode);
        }

        #endregion
    }
}
