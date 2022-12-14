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
    public class Pmt_equipclassManager : BaseManager<Pmt_equipclass>, IPmt_equipclassManager
    {
		#region 属性注入与构造方法
		
        private IPmt_equipclassService service;

        public Pmt_equipclassManager()
        {
            this.service = new Pmt_equipclassService();
            base.BaseService = this.service;
        }

		public Pmt_equipclassManager(string connectStringKey)
        {
			this.service = new Pmt_equipclassService(connectStringKey);
            base.BaseService = this.service;
        }

        public Pmt_equipclassManager(NBear.Data.Gateway way)
        {
			this.service = new Pmt_equipclassService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
