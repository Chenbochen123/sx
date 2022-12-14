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
    public class TblMaterManager : BaseManager<TblMater>, ITblMaterManager
    {
		#region 属性注入与构造方法
		
        private ITblMaterService service;

        public TblMaterManager()
        {
            this.service = new TblMaterService();
            base.BaseService = this.service;
        }

		public TblMaterManager(string connectStringKey)
        {
			this.service = new TblMaterService(connectStringKey);
            base.BaseService = this.service;
        }

        public TblMaterManager(NBear.Data.Gateway way)
        {
			this.service = new TblMaterService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
