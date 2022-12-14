using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    public class FixYesNoManager : BaseManager<FixYesNo>, IFixYesNoManager
    {
		#region 属性注入与构造方法
		
        private IFixYesNoService service;

        public FixYesNoManager()
        {
            this.service = new FixYesNoService();
            base.BaseService = this.service;
        }

		public FixYesNoManager(string connectStringKey)
        {
			this.service = new FixYesNoService(connectStringKey);
            base.BaseService = this.service;
        }

        public FixYesNoManager(NBear.Data.Gateway way)
        {
			this.service = new FixYesNoService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
