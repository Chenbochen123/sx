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
using System.Data;
    public class PptMixingDataManager : BaseManager<PptMixingData>, IPptMixingDataManager
    {
		#region 属性注入与构造方法
		
        private IPptMixingDataService service;

        public PptMixingDataManager()
        {
            this.service = new PptMixingDataService();
            base.BaseService = this.service;
        }

		public PptMixingDataManager(string connectStringKey)
        {
			this.service = new PptMixingDataService(connectStringKey);
            base.BaseService = this.service;
        }

        public PptMixingDataManager(NBear.Data.Gateway way)
        {
			this.service = new PptMixingDataService(way);
            base.BaseService = this.service;
        }

        #endregion

        public DataSet GetMixDataByBarCode(string barCode)
        {
            return this.service.GetMixDataByBarCode(barCode);
        }
    }
}
