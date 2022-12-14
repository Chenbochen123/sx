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
    public class Eqm_MeasureManager : BaseManager<Eqm_Measure>, IEqm_MeasureManager
    {
		#region 属性注入与构造方法
		
        private IEqm_MeasureService service;

        public Eqm_MeasureManager()
        {
            this.service = new Eqm_MeasureService();
            base.BaseService = this.service;
        }

		public Eqm_MeasureManager(string connectStringKey)
        {
			this.service = new Eqm_MeasureService(connectStringKey);
            base.BaseService = this.service;
        }

        public Eqm_MeasureManager(NBear.Data.Gateway way)
        {
			this.service = new Eqm_MeasureService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
