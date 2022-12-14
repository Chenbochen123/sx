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
    public class Qmt_QuaStandMasterManager : BaseManager<Qmt_QuaStandMaster>, IQmt_QuaStandMasterManager
    {
		#region 属性注入与构造方法
		
        private IQmt_QuaStandMasterService service;

        public Qmt_QuaStandMasterManager()
        {
            this.service = new Qmt_QuaStandMasterService();
            base.BaseService = this.service;
        }

		public Qmt_QuaStandMasterManager(string connectStringKey)
        {
			this.service = new Qmt_QuaStandMasterService(connectStringKey);
            base.BaseService = this.service;
        }

        public Qmt_QuaStandMasterManager(NBear.Data.Gateway way)
        {
			this.service = new Qmt_QuaStandMasterService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
