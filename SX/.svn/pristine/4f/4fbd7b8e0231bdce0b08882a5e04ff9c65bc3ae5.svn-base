using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    public class FixDeptLevelManager : BaseManager<FixDeptLevel>, IFixDeptLevelManager
    {
		#region 属性注入与构造方法
		
        private IFixDeptLevelService service;

        public FixDeptLevelManager()
        {
            this.service = new FixDeptLevelService();
            base.BaseService = this.service;
        }

		public FixDeptLevelManager(string connectStringKey)
        {
			this.service = new FixDeptLevelService(connectStringKey);
            base.BaseService = this.service;
        }

        public FixDeptLevelManager(NBear.Data.Gateway way)
        {
			this.service = new FixDeptLevelService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
