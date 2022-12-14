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
    public class BasEquipFacManager : BaseManager<BasEquipFac>, IBasEquipFacManager
    {
		#region 属性注入与构造方法
		
        private IBasEquipFacService service;

        public BasEquipFacManager()
        {
            this.service = new BasEquipFacService();
            base.BaseService = this.service;
        }

		public BasEquipFacManager(string connectStringKey)
        {
			this.service = new BasEquipFacService(connectStringKey);
            base.BaseService = this.service;
        }

        public BasEquipFacManager(NBear.Data.Gateway way)
        {
			this.service = new BasEquipFacService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
