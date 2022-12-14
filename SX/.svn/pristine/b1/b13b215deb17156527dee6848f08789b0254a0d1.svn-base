using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    using NBear.Common;
    public class PptBarBomDataManager : BaseManager<PptBarBomData>, IPptBarBomDataManager
    {
		#region 属性注入与构造方法
		
        private IPptBarBomDataService service;

        public PptBarBomDataManager()
        {
            this.service = new PptBarBomDataService();
            base.BaseService = this.service;
        }

		public PptBarBomDataManager(string connectStringKey)
        {
			this.service = new PptBarBomDataService(connectStringKey);
            base.BaseService = this.service;
        }

        public PptBarBomDataManager(NBear.Data.Gateway way)
        {
			this.service = new PptBarBomDataService(way);
            base.BaseService = this.service;
        }

        #endregion

        public DataTable GetBarBomInfo(string barcode)
        {
            return this.service.GetBarBomInfo(barcode);
        }

        public DataTable GetBatchInfo(string barcode)
        {
            return this.service.GetBatchInfo(barcode);
        }
        public DataTable GetUseNodeByCurrentBarcode(string currentBarcode)
        {
            return this.service.GetUseNodeByCurrentBarcode(currentBarcode);
        }
    }
}
