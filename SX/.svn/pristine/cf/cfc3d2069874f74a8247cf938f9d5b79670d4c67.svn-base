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
    public class Qmt_QuaStandDetailManager : BaseManager<Qmt_QuaStandDetail>, IQmt_QuaStandDetailManager
    {
		#region 属性注入与构造方法
		
        private IQmt_QuaStandDetailService service;

        public Qmt_QuaStandDetailManager()
        {
            this.service = new Qmt_QuaStandDetailService();
            base.BaseService = this.service;
        }

		public Qmt_QuaStandDetailManager(string connectStringKey)
        {
			this.service = new Qmt_QuaStandDetailService(connectStringKey);
            base.BaseService = this.service;
        }

        public Qmt_QuaStandDetailManager(NBear.Data.Gateway way)
        {
			this.service = new Qmt_QuaStandDetailService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
