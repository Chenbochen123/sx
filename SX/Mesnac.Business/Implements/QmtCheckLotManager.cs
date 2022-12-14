using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    public class QmtCheckLotManager : BaseManager<QmtCheckLot>, IQmtCheckLotManager
    {
		#region 属性注入与构造方法
		
        private IQmtCheckLotService service;

        public QmtCheckLotManager()
        {
            this.service = new QmtCheckLotService();
            base.BaseService = this.service;
        }

		public QmtCheckLotManager(string connectStringKey)
        {
			this.service = new QmtCheckLotService(connectStringKey);
            base.BaseService = this.service;
        }

        public QmtCheckLotManager(NBear.Data.Gateway way)
        {
			this.service = new QmtCheckLotService(way);
            base.BaseService = this.service;
        }

        #endregion

        public DataSet GetCheckLotResultByParas(IQmtCheckLotParams paras)
        {
            return this.service.GetCheckLotResultByParas(paras);
        }
    }
}
