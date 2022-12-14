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
    public class Pst_mmshopcheckManager : BaseManager<Pst_mmshopcheck>, IPst_mmshopcheckManager
    {
		#region 属性注入与构造方法
		
        private IPst_mmshopcheckService service;

        public Pst_mmshopcheckManager()
        {
            this.service = new Pst_mmshopcheckService();
            base.BaseService = this.service;
        }

		public Pst_mmshopcheckManager(string connectStringKey)
        {
			this.service = new Pst_mmshopcheckService(connectStringKey);
            base.BaseService = this.service;
        }

        public Pst_mmshopcheckManager(NBear.Data.Gateway way)
        {
			this.service = new Pst_mmshopcheckService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
