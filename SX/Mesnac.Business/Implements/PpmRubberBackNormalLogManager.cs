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
    public class PpmRubberBackNormalLogManager : BaseManager<PpmRubberBackNormalLog>, IPpmRubberBackNormalLogManager
    {
		#region 属性注入与构造方法
		
        private IPpmRubberBackNormalLogService service;

        public PpmRubberBackNormalLogManager()
        {
            this.service = new PpmRubberBackNormalLogService();
            base.BaseService = this.service;
        }

		public PpmRubberBackNormalLogManager(string connectStringKey)
        {
			this.service = new PpmRubberBackNormalLogService(connectStringKey);
            base.BaseService = this.service;
        }

        public PpmRubberBackNormalLogManager(NBear.Data.Gateway way)
        {
			this.service = new PpmRubberBackNormalLogService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
