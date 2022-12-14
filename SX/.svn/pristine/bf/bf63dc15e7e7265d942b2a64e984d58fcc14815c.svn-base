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
    public class Ppt_ClassUserManager : BaseManager<Ppt_ClassUser>, IPpt_ClassUserManager
    {
		#region 属性注入与构造方法
		
        private IPpt_ClassUserService service;

        public Ppt_ClassUserManager()
        {
            this.service = new Ppt_ClassUserService();
            base.BaseService = this.service;
        }

		public Ppt_ClassUserManager(string connectStringKey)
        {
			this.service = new Ppt_ClassUserService(connectStringKey);
            base.BaseService = this.service;
        }

        public Ppt_ClassUserManager(NBear.Data.Gateway way)
        {
			this.service = new Ppt_ClassUserService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
