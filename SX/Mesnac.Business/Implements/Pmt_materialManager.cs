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
    public class Pmt_materialManager : BaseManager<Pmt_material>, IPmt_materialManager
    {
		#region 属性注入与构造方法
		
        private IPmt_materialService service;

        public Pmt_materialManager()
        {
            this.service = new Pmt_materialService();
            base.BaseService = this.service;
        }

		public Pmt_materialManager(string connectStringKey)
        {
			this.service = new Pmt_materialService(connectStringKey);
            base.BaseService = this.service;
        }

        public Pmt_materialManager(NBear.Data.Gateway way)
        {
			this.service = new Pmt_materialService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
