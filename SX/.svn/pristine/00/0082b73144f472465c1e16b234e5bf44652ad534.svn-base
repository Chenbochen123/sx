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
    public class Pmt_recipetypeManager : BaseManager<Pmt_recipetype>, IPmt_recipetypeManager
    {
		#region 属性注入与构造方法
		
        private IPmt_recipetypeService service;

        public Pmt_recipetypeManager()
        {
            this.service = new Pmt_recipetypeService();
            base.BaseService = this.service;
        }

		public Pmt_recipetypeManager(string connectStringKey)
        {
			this.service = new Pmt_recipetypeService(connectStringKey);
            base.BaseService = this.service;
        }

        public Pmt_recipetypeManager(NBear.Data.Gateway way)
        {
			this.service = new Pmt_recipetypeService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
