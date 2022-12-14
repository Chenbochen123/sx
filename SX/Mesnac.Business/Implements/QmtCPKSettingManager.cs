using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    public class QmtCPKSettingManager : BaseManager<QmtCPKSetting>, IQmtCPKSettingManager
    {
		#region 属性注入与构造方法
		
        private IQmtCPKSettingService service;

        public QmtCPKSettingManager()
        {
            this.service = new QmtCPKSettingService();
            base.BaseService = this.service;
        }

		public QmtCPKSettingManager(string connectStringKey)
        {
			this.service = new QmtCPKSettingService(connectStringKey);
            base.BaseService = this.service;
        }

        public QmtCPKSettingManager(NBear.Data.Gateway way)
        {
			this.service = new QmtCPKSettingService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
