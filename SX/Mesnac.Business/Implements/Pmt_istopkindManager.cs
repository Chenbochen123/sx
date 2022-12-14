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
    public class Pmt_istopkindManager : BaseManager<Pmt_istopkind>, IPmt_istopkindManager
    {
		#region 属性注入与构造方法
		
        private IPmt_istopkindService service;

        public Pmt_istopkindManager()
        {
            this.service = new Pmt_istopkindService();
            base.BaseService = this.service;
        }

		public Pmt_istopkindManager(string connectStringKey)
        {
			this.service = new Pmt_istopkindService(connectStringKey);
            base.BaseService = this.service;
        }

        public Pmt_istopkindManager(NBear.Data.Gateway way)
        {
			this.service = new Pmt_istopkindService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
