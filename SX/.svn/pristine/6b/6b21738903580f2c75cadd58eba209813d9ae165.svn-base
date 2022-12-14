using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    public class PptShiftManager : BaseManager<PptShift>, IPptShiftManager
    {
		#region 属性注入与构造方法
		
        private IPptShiftService service;

        public PptShiftManager()
        {
            this.service = new PptShiftService();
            base.BaseService = this.service;
        }

		public PptShiftManager(string connectStringKey)
        {
			this.service = new PptShiftService(connectStringKey);
            base.BaseService = this.service;
        }

        public PptShiftManager(NBear.Data.Gateway way)
        {
			this.service = new PptShiftService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
