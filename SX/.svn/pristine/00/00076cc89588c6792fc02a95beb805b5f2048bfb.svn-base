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
    public class Ppt_curvedataManager : BaseManager<Ppt_curvedata>, IPpt_curvedataManager
    {
		#region 属性注入与构造方法
		
        private IPpt_curvedataService service;

        public Ppt_curvedataManager()
        {
            this.service = new Ppt_curvedataService();
            base.BaseService = this.service;
        }

		public Ppt_curvedataManager(string connectStringKey)
        {
			this.service = new Ppt_curvedataService(connectStringKey);
            base.BaseService = this.service;
        }

        public Ppt_curvedataManager(NBear.Data.Gateway way)
        {
			this.service = new Ppt_curvedataService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
