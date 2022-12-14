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
    public class Eqm_MainDailyManager : BaseManager<Eqm_MainDaily>, IEqm_MainDailyManager
    {
		#region 属性注入与构造方法
		
        private IEqm_MainDailyService service;

        public Eqm_MainDailyManager()
        {
            this.service = new Eqm_MainDailyService();
            base.BaseService = this.service;
        }

		public Eqm_MainDailyManager(string connectStringKey)
        {
			this.service = new Eqm_MainDailyService(connectStringKey);
            base.BaseService = this.service;
        }

        public Eqm_MainDailyManager(NBear.Data.Gateway way)
        {
			this.service = new Eqm_MainDailyService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
