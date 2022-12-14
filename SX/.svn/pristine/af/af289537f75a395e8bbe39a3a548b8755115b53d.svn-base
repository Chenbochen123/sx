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
    public class Ppt_pmpartsManager : BaseManager<Ppt_pmparts>, IPpt_pmpartsManager
    {
		#region 属性注入与构造方法
		
        private IPpt_pmpartsService service;

        public Ppt_pmpartsManager()
        {
            this.service = new Ppt_pmpartsService();
            base.BaseService = this.service;
        }

		public Ppt_pmpartsManager(string connectStringKey)
        {
			this.service = new Ppt_pmpartsService(connectStringKey);
            base.BaseService = this.service;
        }

        public Ppt_pmpartsManager(NBear.Data.Gateway way)
        {
			this.service = new Ppt_pmpartsService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
