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
    public class Eqm_MixCleanRecordManager : BaseManager<Eqm_MixCleanRecord>, IEqm_MixCleanRecordManager
    {
		#region 属性注入与构造方法
		
        private IEqm_MixCleanRecordService service;

        public Eqm_MixCleanRecordManager()
        {
            this.service = new Eqm_MixCleanRecordService();
            base.BaseService = this.service;
        }

		public Eqm_MixCleanRecordManager(string connectStringKey)
        {
			this.service = new Eqm_MixCleanRecordService(connectStringKey);
            base.BaseService = this.service;
        }

        public Eqm_MixCleanRecordManager(NBear.Data.Gateway way)
        {
			this.service = new Eqm_MixCleanRecordService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
