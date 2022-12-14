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
    public class Ppt_shiftNumManager : BaseManager<Ppt_shiftNum>, IPpt_shiftNumManager
    {
		#region 属性注入与构造方法
		
        private IPpt_shiftNumService service;

        public Ppt_shiftNumManager()
        {
            this.service = new Ppt_shiftNumService();
            base.BaseService = this.service;
        }

		public Ppt_shiftNumManager(string connectStringKey)
        {
			this.service = new Ppt_shiftNumService(connectStringKey);
            base.BaseService = this.service;
        }

        public Ppt_shiftNumManager(NBear.Data.Gateway way)
        {
			this.service = new Ppt_shiftNumService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
