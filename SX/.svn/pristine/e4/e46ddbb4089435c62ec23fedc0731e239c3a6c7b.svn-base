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
    public class PpmRubberBackReasonManager : BaseManager<PpmRubberBackReason>, IPpmRubberBackReasonManager
    {
		#region 属性注入与构造方法
		
        private IPpmRubberBackReasonService service;

        public PpmRubberBackReasonManager()
        {
            this.service = new PpmRubberBackReasonService();
            base.BaseService = this.service;
        }

		public PpmRubberBackReasonManager(string connectStringKey)
        {
			this.service = new PpmRubberBackReasonService(connectStringKey);
            base.BaseService = this.service;
        }

        public PpmRubberBackReasonManager(NBear.Data.Gateway way)
        {
			this.service = new PpmRubberBackReasonService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
