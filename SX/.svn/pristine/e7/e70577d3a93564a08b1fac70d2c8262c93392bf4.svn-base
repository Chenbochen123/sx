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
    public class SysUserCtrlManager : BaseManager<SysUserCtrl>, ISysUserCtrlManager
    {
		#region 属性注入与构造方法
		
        private ISysUserCtrlService service;

        public SysUserCtrlManager()
        {
            this.service = new SysUserCtrlService();
            base.BaseService = this.service;
        }

		public SysUserCtrlManager(string connectStringKey)
        {
			this.service = new SysUserCtrlService(connectStringKey);
            base.BaseService = this.service;
        }

        public SysUserCtrlManager(NBear.Data.Gateway way)
        {
			this.service = new SysUserCtrlService(way);
            base.BaseService = this.service;
        }

        #endregion
        public string GetItemCtrl(string TypeID)
        {
            return this.service.GetItemCtrl(TypeID);
        }
    }
}
