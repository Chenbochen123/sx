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
    public class Pmt_XLbagSetManager : BaseManager<Pmt_XLbagSet>, IPmt_XLbagSetManager
    {
		#region 属性注入与构造方法
		
        private IPmt_XLbagSetService service;

        public Pmt_XLbagSetManager()
        {
            this.service = new Pmt_XLbagSetService();
            base.BaseService = this.service;
        }

		public Pmt_XLbagSetManager(string connectStringKey)
        {
			this.service = new Pmt_XLbagSetService(connectStringKey);
            base.BaseService = this.service;
        }

        public Pmt_XLbagSetManager(NBear.Data.Gateway way)
        {
			this.service = new Pmt_XLbagSetService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
