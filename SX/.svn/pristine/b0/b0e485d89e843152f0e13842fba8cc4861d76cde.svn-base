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
    public class Pmt_mstopkindManager : BaseManager<Pmt_mstopkind>, IPmt_mstopkindManager
    {
		#region 属性注入与构造方法
		
        private IPmt_mstopkindService service;

        public Pmt_mstopkindManager()
        {
            this.service = new Pmt_mstopkindService();
            base.BaseService = this.service;
        }

		public Pmt_mstopkindManager(string connectStringKey)
        {
			this.service = new Pmt_mstopkindService(connectStringKey);
            base.BaseService = this.service;
        }

        public Pmt_mstopkindManager(NBear.Data.Gateway way)
        {
			this.service = new Pmt_mstopkindService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
