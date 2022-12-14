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
    public class Pmt_ikindManager : BaseManager<Pmt_ikind>, IPmt_ikindManager
    {
		#region 属性注入与构造方法
		
        private IPmt_ikindService service;

        public Pmt_ikindManager()
        {
            this.service = new Pmt_ikindService();
            base.BaseService = this.service;
        }

		public Pmt_ikindManager(string connectStringKey)
        {
			this.service = new Pmt_ikindService(connectStringKey);
            base.BaseService = this.service;
        }

        public Pmt_ikindManager(NBear.Data.Gateway way)
        {
			this.service = new Pmt_ikindService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
