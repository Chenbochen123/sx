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
    public class TblWeightManager : BaseManager<TblWeight>, ITblWeightManager
    {
		#region 属性注入与构造方法
		
        private ITblWeightService service;

        public TblWeightManager()
        {
            this.service = new TblWeightService();
            base.BaseService = this.service;
        }

		public TblWeightManager(string connectStringKey)
        {
			this.service = new TblWeightService(connectStringKey);
            base.BaseService = this.service;
        }

        public TblWeightManager(NBear.Data.Gateway way)
        {
			this.service = new TblWeightService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
