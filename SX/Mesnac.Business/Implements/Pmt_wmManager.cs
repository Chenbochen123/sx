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
    public class Pmt_wmManager : BaseManager<Pmt_wm>, IPmt_wmManager
    {
		#region 属性注入与构造方法
		
        private IPmt_wmService service;

        public Pmt_wmManager()
        {
            this.service = new Pmt_wmService();
            base.BaseService = this.service;
        }

		public Pmt_wmManager(string connectStringKey)
        {
			this.service = new Pmt_wmService(connectStringKey);
            base.BaseService = this.service;
        }

        public Pmt_wmManager(NBear.Data.Gateway way)
        {
			this.service = new Pmt_wmService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
