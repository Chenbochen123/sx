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
    public class PpmRubberStorageDetailManager : BaseManager<PpmRubberStorageDetail>, IPpmRubberStorageDetailManager
    {
		#region 属性注入与构造方法
		
        private IPpmRubberStorageDetailService service;

        public PpmRubberStorageDetailManager()
        {
            this.service = new PpmRubberStorageDetailService();
            base.BaseService = this.service;
        }

		public PpmRubberStorageDetailManager(string connectStringKey)
        {
			this.service = new PpmRubberStorageDetailService(connectStringKey);
            base.BaseService = this.service;
        }

        public PpmRubberStorageDetailManager(NBear.Data.Gateway way)
        {
			this.service = new PpmRubberStorageDetailService(way);
            base.BaseService = this.service;
        }

        #endregion

        public DataSet GetByInfo(string Barcode, string StorageID, string StoragePlaceID)
        {
            return this.service.GetByInfo(Barcode, StorageID, StoragePlaceID);
        }
    }
}
