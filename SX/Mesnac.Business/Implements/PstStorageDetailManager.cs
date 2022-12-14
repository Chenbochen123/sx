using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    public class PstStorageDetailManager : BaseManager<PstStorageDetail>, IPstStorageDetailManager
    {
		#region 属性注入与构造方法
		
        private IPstStorageDetailService service;

        public PstStorageDetailManager()
        {
            this.service = new PstStorageDetailService();
            base.BaseService = this.service;
        }

		public PstStorageDetailManager(string connectStringKey)
        {
			this.service = new PstStorageDetailService(connectStringKey);
            base.BaseService = this.service;
        }

        public PstStorageDetailManager(NBear.Data.Gateway way)
        {
			this.service = new PstStorageDetailService(way);
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
