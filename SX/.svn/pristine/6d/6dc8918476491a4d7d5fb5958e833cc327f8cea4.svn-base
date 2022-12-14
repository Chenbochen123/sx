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
    public class Ppt_RunHuaYouManager : BaseManager<Ppt_RunHuaYou>, IPpt_RunHuaYouManager
    {
		#region 属性注入与构造方法
		
        private IPpt_RunHuaYouService service;

        public Ppt_RunHuaYouManager()
        {
            this.service = new Ppt_RunHuaYouService();
            base.BaseService = this.service;
        }

		public Ppt_RunHuaYouManager(string connectStringKey)
        {
			this.service = new Ppt_RunHuaYouService(connectStringKey);
            base.BaseService = this.service;
        }

        public Ppt_RunHuaYouManager(NBear.Data.Gateway way)
        {
			this.service = new Ppt_RunHuaYouService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
