using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    public class QmtCheckAssessLotManager : BaseManager<QmtCheckAssessLot>, IQmtCheckAssessLotManager
    {
		#region 属性注入与构造方法
		
        private IQmtCheckAssessLotService service;

        public QmtCheckAssessLotManager()
        {
            this.service = new QmtCheckAssessLotService();
            base.BaseService = this.service;
        }

		public QmtCheckAssessLotManager(string connectStringKey)
        {
			this.service = new QmtCheckAssessLotService(connectStringKey);
            base.BaseService = this.service;
        }

        public QmtCheckAssessLotManager(NBear.Data.Gateway way)
        {
			this.service = new QmtCheckAssessLotService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
