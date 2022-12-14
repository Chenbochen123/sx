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
    public class Ppt_EquipState2Manager : BaseManager<Ppt_EquipState2>, IPpt_EquipState2Manager
    {
		#region 属性注入与构造方法
		
        private IPpt_EquipState2Service service;

        public Ppt_EquipState2Manager()
        {
            this.service = new Ppt_EquipState2Service();
            base.BaseService = this.service;
        }

		public Ppt_EquipState2Manager(string connectStringKey)
        {
			this.service = new Ppt_EquipState2Service(connectStringKey);
            base.BaseService = this.service;
        }

        public Ppt_EquipState2Manager(NBear.Data.Gateway way)
        {
			this.service = new Ppt_EquipState2Service(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
