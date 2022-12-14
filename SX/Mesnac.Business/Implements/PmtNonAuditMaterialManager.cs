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
    public class PmtNonAuditMaterialManager : BaseManager<PmtNonAuditMaterial>, IPmtNonAuditMaterialManager
    {
		#region 属性注入与构造方法
		
        private IPmtNonAuditMaterialService service;

        public PmtNonAuditMaterialManager()
        {
            this.service = new PmtNonAuditMaterialService();
            base.BaseService = this.service;
        }

		public PmtNonAuditMaterialManager(string connectStringKey)
        {
			this.service = new PmtNonAuditMaterialService(connectStringKey);
            base.BaseService = this.service;
        }

        public PmtNonAuditMaterialManager(NBear.Data.Gateway way)
        {
			this.service = new PmtNonAuditMaterialService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region 查询条件类定义
        public class QueryParams : PmtNonAuditMaterialService.QueryParams
        {
        }
        #endregion

        public PageResult<PmtNonAuditMaterial> GetTablePageDataBySql(Mesnac.Data.Implements.PmtNonAuditMaterialService.QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }
    }
}
