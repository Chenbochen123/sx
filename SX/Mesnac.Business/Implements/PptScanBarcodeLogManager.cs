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
    public class PptScanBarcodeLogManager : BaseManager<PptScanBarcodeLog>, IPptScanBarcodeLogManager
    {
		#region 属性注入与构造方法
		
        private IPptScanBarcodeLogService service;

        public PptScanBarcodeLogManager()
        {
            this.service = new PptScanBarcodeLogService();
            base.BaseService = this.service;
        }

		public PptScanBarcodeLogManager(string connectStringKey)
        {
			this.service = new PptScanBarcodeLogService(connectStringKey);
            base.BaseService = this.service;
        }

        public PptScanBarcodeLogManager(NBear.Data.Gateway way)
        {
			this.service = new PptScanBarcodeLogService(way);
            base.BaseService = this.service;
        }

        #endregion


        #region IPptScanBarcodeLogManager 成员
        #region 查询条件类定义
        public class QueryParams : PptScanBarcodeLogService.QueryParams
        {
        }
        #endregion
        public Data.Components.PageResult<PptScanBarcodeLog> GetTablePageDataBySql(PptScanBarcodeLogService.QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }
        #endregion
    }
}
