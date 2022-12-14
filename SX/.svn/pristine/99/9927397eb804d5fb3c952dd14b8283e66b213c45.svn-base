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
    public class Eqm_ImportantBJrecordManager : BaseManager<Eqm_ImportantBJrecord>, IEqm_ImportantBJrecordManager
    {
		#region 属性注入与构造方法
		
        private IEqm_ImportantBJrecordService service;

        public Eqm_ImportantBJrecordManager()
        {
            this.service = new Eqm_ImportantBJrecordService();
            base.BaseService = this.service;
        }

		public Eqm_ImportantBJrecordManager(string connectStringKey)
        {
			this.service = new Eqm_ImportantBJrecordService(connectStringKey);
            base.BaseService = this.service;
        }

        public Eqm_ImportantBJrecordManager(NBear.Data.Gateway way)
        {
			this.service = new Eqm_ImportantBJrecordService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
