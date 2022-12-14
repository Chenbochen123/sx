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
    public class JCZL_WorkShopManager : BaseManager<JCZL_WorkShop>, IJCZL_WorkShopManager
    {
		#region 属性注入与构造方法
		
        private IJCZL_WorkShopService service;

        public JCZL_WorkShopManager()
        {
            this.service = new JCZL_WorkShopService();
            base.BaseService = this.service;
        }

		public JCZL_WorkShopManager(string connectStringKey)
        {
			this.service = new JCZL_WorkShopService(connectStringKey);
            base.BaseService = this.service;
        }

        public JCZL_WorkShopManager(NBear.Data.Gateway way)
        {
			this.service = new JCZL_WorkShopService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
