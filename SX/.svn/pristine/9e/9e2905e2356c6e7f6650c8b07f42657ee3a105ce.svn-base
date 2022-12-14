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
    public class Ppt_GeLiJiManager : BaseManager<Ppt_GeLiJi>, IPpt_GeLiJiManager
    {
		#region 属性注入与构造方法
		
        private IPpt_GeLiJiService service;

        public Ppt_GeLiJiManager()
        {
            this.service = new Ppt_GeLiJiService();
            base.BaseService = this.service;
        }

		public Ppt_GeLiJiManager(string connectStringKey)
        {
			this.service = new Ppt_GeLiJiService(connectStringKey);
            base.BaseService = this.service;
        }

        public Ppt_GeLiJiManager(NBear.Data.Gateway way)
        {
			this.service = new Ppt_GeLiJiService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
