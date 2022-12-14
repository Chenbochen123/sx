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
		#region 属性注入与构造方法
		
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
        #region IBasEquipManager 成员
        #region 查询条件类定义
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
