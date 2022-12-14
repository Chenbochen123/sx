using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    using NBear.Common;
    using NBear.Data;
    using Mesnac.Data.Components;
    using System.Data;
    public class BasEquipManager : BaseManager<BasEquip>, IBasEquipManager
    {
        #region 属性注入与构造方法

        private IBasEquipService service;

        public BasEquipManager()
        {
            this.service = new BasEquipService();
            base.BaseService = this.service;
        }

        public BasEquipManager(string connectStringKey)
        {
            this.service = new BasEquipService(connectStringKey);
            base.BaseService = this.service;
        }

        public BasEquipManager(NBear.Data.Gateway way)
        {
            this.service = new BasEquipService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region IBasEquipManager 成员
        #region 查询条件类定义
        public class QueryParams : BasEquipService.QueryParams
        {
        }
        #endregion
        public Data.Components.PageResult<BasEquip> GetTablePageDataBySql(BasEquipService.QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }

        public string GetNextEquipCode(string equipTypeCode)
        {
            return this.service.GetNextEquipCode(equipTypeCode);
        }
        #endregion

        public DataSet EquipStorageQuery(QueryParams queryParams)
        {
            return this.service.EquipStorageQuery(queryParams);
        }
        public DataSet EquipStorageQueryByCode(QueryParams queryParams)
        {
            return this.service.EquipStorageQueryByCode(queryParams);
        }
        public DataSet UpdateEquipStorage(QueryParams queryParams)
        {
            return this.service.UpdateEquipStorage(queryParams);
        }

        public DataSet getMiLanEquipNodeByWorkShopCode(string workshopCode)
        {
            return this.service.getMiLanEquipNodeByWorkShopCode(workshopCode);
        }
    }
}
