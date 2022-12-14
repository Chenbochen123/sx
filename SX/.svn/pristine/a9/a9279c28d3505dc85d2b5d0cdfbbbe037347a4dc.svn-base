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
    public class EqmSapSparePartManager : BaseManager<EqmSapSparePart>, IEqmSapSparePartManager
    {
		#region 属性注入与构造方法
		
        private IEqmSapSparePartService service;

        public EqmSapSparePartManager()
        {
            this.service = new EqmSapSparePartService();
            base.BaseService = this.service;
        }

		public EqmSapSparePartManager(string connectStringKey)
        {
			this.service = new EqmSapSparePartService(connectStringKey);
            base.BaseService = this.service;
        }

        public EqmSapSparePartManager(NBear.Data.Gateway way)
        {
			this.service = new EqmSapSparePartService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region 查询条件类定义
        public class QueryParams : EqmSapSparePartService.QueryParams
        {
        }
        #endregion
        public PageResult<EqmSapSparePart> GetTablePageDataBySql(Mesnac.Data.Implements.EqmSapSparePartService.QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }
        //获取下一个入库编号
        public string GetNextSparePartStoreInCode(DateTime storeInDate)
        {
            return this.service.GetNextSparePartStoreInCode(storeInDate);
        }
    }
}
