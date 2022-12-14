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
    public class Eqm_MotorInfoManager : BaseManager<Eqm_MotorInfo>, IEqm_MotorInfoManager
    {
		#region 属性注入与构造方法
		
        private IEqm_MotorInfoService service;

        public Eqm_MotorInfoManager()
        {
            this.service = new Eqm_MotorInfoService();
            base.BaseService = this.service;
        }

		public Eqm_MotorInfoManager(string connectStringKey)
        {
			this.service = new Eqm_MotorInfoService(connectStringKey);
            base.BaseService = this.service;
        }

        public Eqm_MotorInfoManager(NBear.Data.Gateway way)
        {
			this.service = new Eqm_MotorInfoService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
