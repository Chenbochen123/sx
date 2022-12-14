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
    public class Eqm_CleanRecordManager : BaseManager<Eqm_CleanRecord>, IEqm_CleanRecordManager
    {
		#region 属性注入与构造方法
		
        private IEqm_CleanRecordService service;

        public Eqm_CleanRecordManager()
        {
            this.service = new Eqm_CleanRecordService();
            base.BaseService = this.service;
        }

		public Eqm_CleanRecordManager(string connectStringKey)
        {
			this.service = new Eqm_CleanRecordService(connectStringKey);
            base.BaseService = this.service;
        }

        public Eqm_CleanRecordManager(NBear.Data.Gateway way)
        {
			this.service = new Eqm_CleanRecordService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
