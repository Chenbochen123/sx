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
    public class Pmt_WeightManager : BaseManager<Pmt_Weight>, IPmt_WeightManager
    {
		#region 属性注入与构造方法
		
        private IPmt_WeightService service;

        public Pmt_WeightManager()
        {
            this.service = new Pmt_WeightService();
            base.BaseService = this.service;
        }

		public Pmt_WeightManager(string connectStringKey)
        {
			this.service = new Pmt_WeightService(connectStringKey);
            base.BaseService = this.service;
        }

        public Pmt_WeightManager(NBear.Data.Gateway way)
        {
			this.service = new Pmt_WeightService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
