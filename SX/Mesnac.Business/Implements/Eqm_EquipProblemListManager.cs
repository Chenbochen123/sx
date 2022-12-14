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
    public class Eqm_EquipProblemListManager : BaseManager<Eqm_EquipProblemList>, IEqm_EquipProblemListManager
    {
		#region 属性注入与构造方法
		
        private IEqm_EquipProblemListService service;

        public Eqm_EquipProblemListManager()
        {
            this.service = new Eqm_EquipProblemListService();
            base.BaseService = this.service;
        }

		public Eqm_EquipProblemListManager(string connectStringKey)
        {
			this.service = new Eqm_EquipProblemListService(connectStringKey);
            base.BaseService = this.service;
        }

        public Eqm_EquipProblemListManager(NBear.Data.Gateway way)
        {
			this.service = new Eqm_EquipProblemListService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
