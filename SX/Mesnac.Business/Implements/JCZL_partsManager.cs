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
    public class JCZL_partsManager : BaseManager<JCZL_parts>, IJCZL_partsManager
    {
		#region 属性注入与构造方法
		
        private IJCZL_partsService service;

        public JCZL_partsManager()
        {
            this.service = new JCZL_partsService();
            base.BaseService = this.service;
        }

		public JCZL_partsManager(string connectStringKey)
        {
			this.service = new JCZL_partsService(connectStringKey);
            base.BaseService = this.service;
        }

        public JCZL_partsManager(NBear.Data.Gateway way)
        {
			this.service = new JCZL_partsService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
