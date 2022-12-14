using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    public class PptProcedureManager : BaseManager<PptProcedure>, IPptProcedureManager
    {
		#region 属性注入与构造方法
		
        private IPptProcedureService service;

        public PptProcedureManager()
        {
            this.service = new PptProcedureService();
            base.BaseService = this.service;
        }

		public PptProcedureManager(string connectStringKey)
        {
			this.service = new PptProcedureService(connectStringKey);
            base.BaseService = this.service;
        }

        public PptProcedureManager(NBear.Data.Gateway way)
        {
			this.service = new PptProcedureService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
