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
    using NBear.Common;
    using System.Data;
    public class BasWorkShopManager : BaseManager<BasWorkShop>, IBasWorkShopManager
    {
		#region 属性注入与构造方法
		
        private IBasWorkShopService service;

        public BasWorkShopManager()
        {
            this.service = new BasWorkShopService();
            base.BaseService = this.service;
        }

		public BasWorkShopManager(string connectStringKey)
        {
			this.service = new BasWorkShopService(connectStringKey);
            base.BaseService = this.service;
        }

        public BasWorkShopManager(NBear.Data.Gateway way)
        {
			this.service = new BasWorkShopService(way);
            base.BaseService = this.service;
        }

        #endregion
        #region 查询条件类定义
        public class QueryParams : BasWorkShopService.QueryParams
        {
        }
        #endregion
        public PageResult<BasWorkShop> GetTablePageDataBySql(Mesnac.Data.Implements.BasWorkShopService.QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }

        public string GetNextWorkShopCode()
        {
            return this.service.GetNextWorkShopCode();
        }
        public EntityArrayList<BasWorkShop> getAllMiLanWorkShop()
        {
            return this.service.getAllMiLanWorkShop();
        }

        public DataSet getAllMiLanWorkShopNode()
        {
            return this.service.getAllMiLanWorkShopNode();
        }
    }
}
