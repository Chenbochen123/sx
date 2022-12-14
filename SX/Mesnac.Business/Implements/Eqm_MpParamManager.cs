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
    public class Eqm_MpParamManager : BaseManager<Eqm_MpParam>, IEqm_MpParamManager
    {
		#region 属性注入与构造方法
		
        private IEqm_MpParamService service;

        public Eqm_MpParamManager()
        {
            this.service = new Eqm_MpParamService();
            base.BaseService = this.service;
        }

		public Eqm_MpParamManager(string connectStringKey)
        {
			this.service = new Eqm_MpParamService(connectStringKey);
            base.BaseService = this.service;
        }

        public Eqm_MpParamManager(NBear.Data.Gateway way)
        {
			this.service = new Eqm_MpParamService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
