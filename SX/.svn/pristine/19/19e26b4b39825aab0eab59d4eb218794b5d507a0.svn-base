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
    public class PptOpenMixDataManager : BaseManager<PptOpenMixData>, IPptOpenMixDataManager
    {
		#region 属性注入与构造方法
		
        private IPptOpenMixDataService service;

        public PptOpenMixDataManager()
        {
            this.service = new PptOpenMixDataService();
            base.BaseService = this.service;
        }

		public PptOpenMixDataManager(string connectStringKey)
        {
			this.service = new PptOpenMixDataService(connectStringKey);
            base.BaseService = this.service;
        }

        public PptOpenMixDataManager(NBear.Data.Gateway way)
        {
			this.service = new PptOpenMixDataService(way);
            base.BaseService = this.service;
        }

        #endregion

        public DataSet GetOpenMixByBarCode(string barCode) { 
            return this.service.GetOpenMixByBarCode(barCode);
        }
    }
}
