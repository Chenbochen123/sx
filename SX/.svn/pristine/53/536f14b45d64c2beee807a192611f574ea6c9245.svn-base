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
    public class Pst_MinstockManager : BaseManager<Pst_Minstock>, IPst_MinstockManager
    {
		#region 属性注入与构造方法
		
        private IPst_MinstockService service;

        public Pst_MinstockManager()
        {
            this.service = new Pst_MinstockService();
            base.BaseService = this.service;
        }

		public Pst_MinstockManager(string connectStringKey)
        {
			this.service = new Pst_MinstockService(connectStringKey);
            base.BaseService = this.service;
        }

        public Pst_MinstockManager(NBear.Data.Gateway way)
        {
			this.service = new Pst_MinstockService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
