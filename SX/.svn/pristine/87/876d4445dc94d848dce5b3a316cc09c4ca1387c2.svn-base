using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    public class PstStorgaeDetailManager : BaseManager<PstStorgaeDetail>, IPstStorgaeDetailManager
    {
		#region 属性注入与构造方法
		
        private IPstStorgaeDetailService service;

        public PstStorgaeDetailManager()
        {
            this.service = new PstStorgaeDetailService();
            base.BaseService = this.service;
        }

		public PstStorgaeDetailManager(string connectStringKey)
        {
			this.service = new PstStorgaeDetailService(connectStringKey);
            base.BaseService = this.service;
        }

        public PstStorgaeDetailManager(NBear.Data.Gateway way)
        {
			this.service = new PstStorgaeDetailService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
