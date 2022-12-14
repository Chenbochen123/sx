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
    public class PstMaterialReturnManager : BaseManager<PstMaterialReturn>, IPstMaterialReturnManager
    {
		#region 属性注入与构造方法
		
        private IPstMaterialReturnService service;

        public PstMaterialReturnManager()
        {
            this.service = new PstMaterialReturnService();
            base.BaseService = this.service;
        }

		public PstMaterialReturnManager(string connectStringKey)
        {
			this.service = new PstMaterialReturnService(connectStringKey);
            base.BaseService = this.service;
        }

        public PstMaterialReturnManager(NBear.Data.Gateway way)
        {
			this.service = new PstMaterialReturnService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region 查询条件类定义
        public class QueryParams : PstMaterialReturnService.QueryParams
        {
        }
        #endregion

        public PageResult<PstMaterialReturn> GetTablePageDataBySql(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }

        public string GetBillNo()
        {
            return this.service.GetBillNo();
        }
    }
}
