using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    public class FixEquipModelMixManager : BaseManager<FixEquipModelMix>, IFixEquipModelMixManager
    {
		#region 属性注入与构造方法
		
        private IFixEquipModelMixService service;

        public FixEquipModelMixManager()
        {
            this.service = new FixEquipModelMixService();
            base.BaseService = this.service;
        }

		public FixEquipModelMixManager(string connectStringKey)
        {
			this.service = new FixEquipModelMixService(connectStringKey);
            base.BaseService = this.service;
        }

        public FixEquipModelMixManager(NBear.Data.Gateway way)
        {
			this.service = new FixEquipModelMixService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
