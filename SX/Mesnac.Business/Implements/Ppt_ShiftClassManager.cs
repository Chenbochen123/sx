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
    public class Ppt_ShiftClassManager : BaseManager<Ppt_ShiftClass>, IPpt_ShiftClassManager
    {
		#region 属性注入与构造方法
		
        private IPpt_ShiftClassService service;

        public Ppt_ShiftClassManager()
        {
            this.service = new Ppt_ShiftClassService();
            base.BaseService = this.service;
        }

		public Ppt_ShiftClassManager(string connectStringKey)
        {
			this.service = new Ppt_ShiftClassService(connectStringKey);
            base.BaseService = this.service;
        }

        public Ppt_ShiftClassManager(NBear.Data.Gateway way)
        {
			this.service = new Ppt_ShiftClassService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
