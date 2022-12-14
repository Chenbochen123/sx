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
    public class Ppt_plantimeManager : BaseManager<Ppt_plantime>, IPpt_plantimeManager
    {
		#region 属性注入与构造方法
		
        private IPpt_plantimeService service;

        public Ppt_plantimeManager()
        {
            this.service = new Ppt_plantimeService();
            base.BaseService = this.service;
        }

		public Ppt_plantimeManager(string connectStringKey)
        {
			this.service = new Ppt_plantimeService(connectStringKey);
            base.BaseService = this.service;
        }

        public Ppt_plantimeManager(NBear.Data.Gateway way)
        {
			this.service = new Ppt_plantimeService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
