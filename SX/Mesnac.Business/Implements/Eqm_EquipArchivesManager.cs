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
    public class Eqm_EquipArchivesManager : BaseManager<Eqm_EquipArchives>, IEqm_EquipArchivesManager
    {
		#region 属性注入与构造方法
		
        private IEqm_EquipArchivesService service;

        public Eqm_EquipArchivesManager()
        {
            this.service = new Eqm_EquipArchivesService();
            base.BaseService = this.service;
        }

		public Eqm_EquipArchivesManager(string connectStringKey)
        {
			this.service = new Eqm_EquipArchivesService(connectStringKey);
            base.BaseService = this.service;
        }

        public Eqm_EquipArchivesManager(NBear.Data.Gateway way)
        {
			this.service = new Eqm_EquipArchivesService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
