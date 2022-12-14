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
    public class PpmStorageDetailManager : BaseManager<PpmStorageDetail>, IPpmStorageDetailManager
    {
		#region 属性注入与构造方法
		
        private IPpmStorageDetailService service;

        public PpmStorageDetailManager()
        {
            this.service = new PpmStorageDetailService();
            base.BaseService = this.service;
        }

		public PpmStorageDetailManager(string connectStringKey)
        {
			this.service = new PpmStorageDetailService(connectStringKey);
            base.BaseService = this.service;
        }

        public PpmStorageDetailManager(NBear.Data.Gateway way)
        {
			this.service = new PpmStorageDetailService(way);
            base.BaseService = this.service;
        }

        #endregion

        public DataSet GetByInfo(string Barcode, string StorageID, string StoragePlaceID)
        {
            return this.service.GetByInfo(Barcode, StorageID, StoragePlaceID);
        }

        public int GetOrderID(string Barcode)
        {
            return this.service.GetOrderID(Barcode);
        }
    }
}
