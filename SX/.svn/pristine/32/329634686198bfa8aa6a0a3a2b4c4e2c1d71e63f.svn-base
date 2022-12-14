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
    public class Pmt_XLAutoCreateManager : BaseManager<Pmt_XLAutoCreate>, IPmt_XLAutoCreateManager
    {
		#region 属性注入与构造方法
		
        private IPmt_XLAutoCreateService service;

        public Pmt_XLAutoCreateManager()
        {
            this.service = new Pmt_XLAutoCreateService();
            base.BaseService = this.service;
        }

		public Pmt_XLAutoCreateManager(string connectStringKey)
        {
			this.service = new Pmt_XLAutoCreateService(connectStringKey);
            base.BaseService = this.service;
        }

        public Pmt_XLAutoCreateManager(NBear.Data.Gateway way)
        {
			this.service = new Pmt_XLAutoCreateService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
