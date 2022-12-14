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
    public class IncSapOrderReloadManager : BaseManager<IncSapOrderReload>, IIncSapOrderReloadManager
    {
		#region 属性注入与构造方法
		
        private IIncSapOrderReloadService service;

        public IncSapOrderReloadManager()
        {
            this.service = new IncSapOrderReloadService();
            base.BaseService = this.service;
        }

		public IncSapOrderReloadManager(string connectStringKey)
        {
			this.service = new IncSapOrderReloadService(connectStringKey);
            base.BaseService = this.service;
        }

        public IncSapOrderReloadManager(NBear.Data.Gateway way)
        {
			this.service = new IncSapOrderReloadService(way);
            base.BaseService = this.service;
        }

        #endregion
        #region 查询条件类定义
        public class QueryParams : IncSapOrderReloadService.QueryParams
        {
        }
        #endregion
        public PageResult<IncSapOrderReload> GetTablePageDataBySql(Mesnac.Data.Implements.IncSapOrderReloadService.QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }

        public bool IsExistData(string MesOrderCode, string MesOrderType)
        {
            return this.service.IsExistData(MesOrderCode, MesOrderType);
        }
    }
}
