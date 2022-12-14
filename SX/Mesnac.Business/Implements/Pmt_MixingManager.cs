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
    public class Pmt_MixingManager : BaseManager<Pmt_Mixing>, IPmt_MixingManager
    {
		#region 属性注入与构造方法
		
        private IPmt_MixingService service;

        public Pmt_MixingManager()
        {
            this.service = new Pmt_MixingService();
            base.BaseService = this.service;
        }

		public Pmt_MixingManager(string connectStringKey)
        {
			this.service = new Pmt_MixingService(connectStringKey);
            base.BaseService = this.service;
        }

        public Pmt_MixingManager(NBear.Data.Gateway way)
        {
			this.service = new Pmt_MixingService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
