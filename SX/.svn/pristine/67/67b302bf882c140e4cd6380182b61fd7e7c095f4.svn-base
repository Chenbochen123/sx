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
    public class Ppt_pmdownrecordManager : BaseManager<Ppt_pmdownrecord>, IPpt_pmdownrecordManager
    {
		#region 属性注入与构造方法
		
        private IPpt_pmdownrecordService service;

        public Ppt_pmdownrecordManager()
        {
            this.service = new Ppt_pmdownrecordService();
            base.BaseService = this.service;
        }

		public Ppt_pmdownrecordManager(string connectStringKey)
        {
			this.service = new Ppt_pmdownrecordService(connectStringKey);
            base.BaseService = this.service;
        }

        public Ppt_pmdownrecordManager(NBear.Data.Gateway way)
        {
			this.service = new Ppt_pmdownrecordService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
