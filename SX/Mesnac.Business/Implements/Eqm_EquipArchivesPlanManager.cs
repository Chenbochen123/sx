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
    public class Eqm_EquipArchivesPlanManager : BaseManager<Eqm_EquipArchivesPlan>, IEqm_EquipArchivesPlanManager
    {
		#region 属性注入与构造方法
		
        private IEqm_EquipArchivesPlanService service;

        public Eqm_EquipArchivesPlanManager()
        {
            this.service = new Eqm_EquipArchivesPlanService();
            base.BaseService = this.service;
        }

		public Eqm_EquipArchivesPlanManager(string connectStringKey)
        {
			this.service = new Eqm_EquipArchivesPlanService(connectStringKey);
            base.BaseService = this.service;
        }

        public Eqm_EquipArchivesPlanManager(NBear.Data.Gateway way)
        {
			this.service = new Eqm_EquipArchivesPlanService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
