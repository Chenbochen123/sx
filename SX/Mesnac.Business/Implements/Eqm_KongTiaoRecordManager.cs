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
    public class Eqm_KongTiaoRecordManager : BaseManager<Eqm_KongTiaoRecord>, IEqm_KongTiaoRecordManager
    {
		#region 属性注入与构造方法
		
        private IEqm_KongTiaoRecordService service;

        public Eqm_KongTiaoRecordManager()
        {
            this.service = new Eqm_KongTiaoRecordService();
            base.BaseService = this.service;
        }

		public Eqm_KongTiaoRecordManager(string connectStringKey)
        {
			this.service = new Eqm_KongTiaoRecordService(connectStringKey);
            base.BaseService = this.service;
        }

        public Eqm_KongTiaoRecordManager(NBear.Data.Gateway way)
        {
			this.service = new Eqm_KongTiaoRecordService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
