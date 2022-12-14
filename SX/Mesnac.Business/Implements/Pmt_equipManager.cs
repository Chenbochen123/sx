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
    public class Pmt_equipManager : BaseManager<Pmt_equip>, IPmt_equipManager
    {
		#region 属性注入与构造方法
		
        private IPmt_equipService service;

        public Pmt_equipManager()
        {
            this.service = new Pmt_equipService();
            base.BaseService = this.service;
        }

		public Pmt_equipManager(string connectStringKey)
        {
			this.service = new Pmt_equipService(connectStringKey);
            base.BaseService = this.service;
        }

        public Pmt_equipManager(NBear.Data.Gateway way)
        {
			this.service = new Pmt_equipService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
