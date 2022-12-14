using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    using System.Data;
    public class BasEquipPartRelationManager : BaseManager<BasEquipPartRelation>, IBasEquipPartRelationManager
    {
		#region ����ע���빹�췽��
		
        private IBasEquipPartRelationService service;

        public BasEquipPartRelationManager()
        {
            this.service = new BasEquipPartRelationService();
            base.BaseService = this.service;
        }

		public BasEquipPartRelationManager(string connectStringKey)
        {
			this.service = new BasEquipPartRelationService(connectStringKey);
            base.BaseService = this.service;
        }

        public BasEquipPartRelationManager(NBear.Data.Gateway way)
        {
			this.service = new BasEquipPartRelationService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region IBasEquipManager ��Ա
        #region ��ѯ�����ඨ��
        public class QueryParams : BasEquipPartRelationService.QueryParams
        {
        }
        #endregion
        public Data.Components.PageResult<BasEquipPartRelation> GetTablePageDataBySql(BasEquipPartRelationService.QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }

        public string GetNextEquipPartRelationCode()
        {
            return this.service.GetNextEquipPartRelationCode();
        }

        public DataSet GetEquipPartsByEquipCode(string equipCode)
        {
            return this.service.GetEquipPartsByEquipCode(equipCode);
        }

        #endregion
    }
}
