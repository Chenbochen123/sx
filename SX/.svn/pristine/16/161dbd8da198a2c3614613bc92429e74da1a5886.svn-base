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
    public class BasProductPlaceManager : BaseManager<BasProductPlace>, IBasProductPlaceManager
    {
		#region 属性注入与构造方法
		
        private IBasProductPlaceService service;

        public BasProductPlaceManager()
        {
            this.service = new BasProductPlaceService();
            base.BaseService = this.service;
        }

		public BasProductPlaceManager(string connectStringKey)
        {
			this.service = new BasProductPlaceService(connectStringKey);
            base.BaseService = this.service;
        }

        public BasProductPlaceManager(NBear.Data.Gateway way)
        {
			this.service = new BasProductPlaceService(way);
            base.BaseService = this.service;
        }

        #endregion

        public DataSet GetProductPlace(string FactoryID)
        {
            return this.service.GetProductPlace(FactoryID);
        }
    }
}
