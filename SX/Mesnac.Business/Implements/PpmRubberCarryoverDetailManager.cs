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
    public class PpmRubberCarryoverDetailManager : BaseManager<PpmRubberCarryoverDetail>, IPpmRubberCarryoverDetailManager
    {
		#region 属性注入与构造方法
		
        private IPpmRubberCarryoverDetailService service;

        public PpmRubberCarryoverDetailManager()
        {
            this.service = new PpmRubberCarryoverDetailService();
            base.BaseService = this.service;
        }

		public PpmRubberCarryoverDetailManager(string connectStringKey)
        {
			this.service = new PpmRubberCarryoverDetailService(connectStringKey);
            base.BaseService = this.service;
        }

        public PpmRubberCarryoverDetailManager(NBear.Data.Gateway way)
        {
			this.service = new PpmRubberCarryoverDetailService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
