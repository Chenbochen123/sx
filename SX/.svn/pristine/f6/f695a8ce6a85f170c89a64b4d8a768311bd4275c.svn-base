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
    public class EQM_EnergyManageManager : BaseManager<EQM_EnergyManage>, IEQM_EnergyManageManager
    {
		#region 属性注入与构造方法
		
        private IEQM_EnergyManageService service;

        public EQM_EnergyManageManager()
        {
            this.service = new EQM_EnergyManageService();
            base.BaseService = this.service;
        }

		public EQM_EnergyManageManager(string connectStringKey)
        {
			this.service = new EQM_EnergyManageService(connectStringKey);
            base.BaseService = this.service;
        }

        public EQM_EnergyManageManager(NBear.Data.Gateway way)
        {
			this.service = new EQM_EnergyManageService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
