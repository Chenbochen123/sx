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
    public class Ppt_MaterfirstEquipManager : BaseManager<Ppt_MaterfirstEquip>, IPpt_MaterfirstEquipManager
    {
		#region 属性注入与构造方法
		
        private IPpt_MaterfirstEquipService service;

        public Ppt_MaterfirstEquipManager()
        {
            this.service = new Ppt_MaterfirstEquipService();
            base.BaseService = this.service;
        }

		public Ppt_MaterfirstEquipManager(string connectStringKey)
        {
			this.service = new Ppt_MaterfirstEquipService(connectStringKey);
            base.BaseService = this.service;
        }

        public Ppt_MaterfirstEquipManager(NBear.Data.Gateway way)
        {
			this.service = new Ppt_MaterfirstEquipService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
