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
    public class JCZL_SubFacManager : BaseManager<JCZL_SubFac>, IJCZL_SubFacManager
    {
		#region 属性注入与构造方法
		
        private IJCZL_SubFacService service;

        public JCZL_SubFacManager()
        {
            this.service = new JCZL_SubFacService();
            base.BaseService = this.service;
        }

		public JCZL_SubFacManager(string connectStringKey)
        {
			this.service = new JCZL_SubFacService(connectStringKey);
            base.BaseService = this.service;
        }

        public JCZL_SubFacManager(NBear.Data.Gateway way)
        {
			this.service = new JCZL_SubFacService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
