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
    public class BasEquipTypeManager : BaseManager<BasEquipType>, IBasEquipTypeManager
    {
		#region ����ע���빹�췽��
		
        private IBasEquipTypeService service;

        public BasEquipTypeManager()
        {
            this.service = new BasEquipTypeService();
            base.BaseService = this.service;
        }

		public BasEquipTypeManager(string connectStringKey)
        {
			this.service = new BasEquipTypeService(connectStringKey);
            base.BaseService = this.service;
        }

        public BasEquipTypeManager(NBear.Data.Gateway way)
        {
			this.service = new BasEquipTypeService(way);
            base.BaseService = this.service;
        }

        #endregion
        #region ��ѯ�����ඨ��
        public class QueryParams : BasEquipTypeService.QueryParams
        {
        }
        #endregion

        public PageResult<BasEquipType> GetTablePageDataBySql(Mesnac.Data.Implements.BasEquipTypeService.QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }
        public string GetNextEquipTypeCode()
        {
            return this.service.GetNextEquipTypeCode();
        }
    }
}
