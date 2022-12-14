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
    public class PstMaterialCarryoverManager : BaseManager<PstMaterialCarryover>, IPstMaterialCarryoverManager
    {
		#region 属性注入与构造方法
		
        private IPstMaterialCarryoverService service;

        public PstMaterialCarryoverManager()
        {
            this.service = new PstMaterialCarryoverService();
            base.BaseService = this.service;
        }

		public PstMaterialCarryoverManager(string connectStringKey)
        {
			this.service = new PstMaterialCarryoverService(connectStringKey);
            base.BaseService = this.service;
        }

        public PstMaterialCarryoverManager(NBear.Data.Gateway way)
        {
			this.service = new PstMaterialCarryoverService(way);
            base.BaseService = this.service;
        }

        #endregion

        public string GetInaccountDuration(string StorageID)
        {
            return this.service.GetInaccountDuration(StorageID);
        }

        public List<string> GetDurationFromPststorage(string StorageID)
        {
            return this.service.GetDurationFromPststorage(StorageID);
        }

        public string GetStorageDuration(string StorageID)
        {
            return this.service.GetStorageDuration(StorageID);
        }

        public bool CarryoverStorageDetail(string StorageID, string InaccountDuration)
        {
            return this.service.CarryoverStorageDetail(StorageID, InaccountDuration);
        }

        public bool UpdateStorageDuring(string StorageID)
        {
            return this.service.UpdateStorageDuring(StorageID);
        }
    }
}
