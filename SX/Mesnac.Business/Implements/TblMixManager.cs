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
    public class TblMixManager : BaseManager<TblMix>, ITblMixManager
    {
		#region 属性注入与构造方法
		
        private ITblMixService service;

        public TblMixManager()
        {
            this.service = new TblMixService();
            base.BaseService = this.service;
        }

		public TblMixManager(string connectStringKey)
        {
			this.service = new TblMixService(connectStringKey);
            base.BaseService = this.service;
        }

        public TblMixManager(NBear.Data.Gateway way)
        {
			this.service = new TblMixService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
