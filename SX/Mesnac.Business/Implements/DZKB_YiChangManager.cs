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
    public class DZKB_YiChangManager : BaseManager<DZKB_YiChang>, IDZKB_YiChangManager
    {
		#region 属性注入与构造方法
		
        private IDZKB_YiChangService service;

        public DZKB_YiChangManager()
        {
            this.service = new DZKB_YiChangService();
            base.BaseService = this.service;
        }

		public DZKB_YiChangManager(string connectStringKey)
        {
			this.service = new DZKB_YiChangService(connectStringKey);
            base.BaseService = this.service;
        }

        public DZKB_YiChangManager(NBear.Data.Gateway way)
        {
			this.service = new DZKB_YiChangService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
