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
    public class Pst_mminjarManager : BaseManager<Pst_mminjar>, IPst_mminjarManager
    {
		#region 属性注入与构造方法
		
        private IPst_mminjarService service;

        public Pst_mminjarManager()
        {
            this.service = new Pst_mminjarService();
            base.BaseService = this.service;
        }

		public Pst_mminjarManager(string connectStringKey)
        {
			this.service = new Pst_mminjarService(connectStringKey);
            base.BaseService = this.service;
        }

        public Pst_mminjarManager(NBear.Data.Gateway way)
        {
			this.service = new Pst_mminjarService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
