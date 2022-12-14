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
    public class PpmRubberCarryoverManager : BaseManager<PpmRubberCarryover>, IPpmRubberCarryoverManager
    {
		#region 属性注入与构造方法
		
        private IPpmRubberCarryoverService service;

        public PpmRubberCarryoverManager()
        {
            this.service = new PpmRubberCarryoverService();
            base.BaseService = this.service;
        }

		public PpmRubberCarryoverManager(string connectStringKey)
        {
			this.service = new PpmRubberCarryoverService(connectStringKey);
            base.BaseService = this.service;
        }

        public PpmRubberCarryoverManager(NBear.Data.Gateway way)
        {
			this.service = new PpmRubberCarryoverService(way);
            base.BaseService = this.service;
        }

        #endregion

        public string GetInaccountDuration(string StorageID)
        {
            return this.service.GetInaccountDuration(StorageID);
        }

        public List<string> GetDurationFromPpmStorage(string StorageID)
        {
            return this.service.GetDurationFromPpmStorage(StorageID);
        }

        public string GetStorageDuration(string StorageID)
        {
            return this.service.GetStorageDuration(StorageID);
        }

        public bool CarryoverStorageDetail(string StorageID, string InaccountDuration)
        {
            return this.service.CarryoverStorageDetail(StorageID, InaccountDuration);
        }
    }
}
