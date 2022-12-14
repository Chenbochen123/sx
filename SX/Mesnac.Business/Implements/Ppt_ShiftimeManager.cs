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
    public class Ppt_ShiftimeManager : BaseManager<Ppt_Shiftime>, IPpt_ShiftimeManager
    {
		#region 属性注入与构造方法
		
        private IPpt_ShiftimeService service;

        public Ppt_ShiftimeManager()
        {
            this.service = new Ppt_ShiftimeService();
            base.BaseService = this.service;
        }

		public Ppt_ShiftimeManager(string connectStringKey)
        {
			this.service = new Ppt_ShiftimeService(connectStringKey);
            base.BaseService = this.service;
        }

        public Ppt_ShiftimeManager(NBear.Data.Gateway way)
        {
			this.service = new Ppt_ShiftimeService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
