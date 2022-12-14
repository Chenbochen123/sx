using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    public class QmtDealNotionManager : BaseManager<QmtDealNotion>, IQmtDealNotionManager
    {
		#region 属性注入与构造方法
		
        private IQmtDealNotionService service;

        public QmtDealNotionManager()
        {
            this.service = new QmtDealNotionService();
            base.BaseService = this.service;
        }

		public QmtDealNotionManager(string connectStringKey)
        {
			this.service = new QmtDealNotionService(connectStringKey);
            base.BaseService = this.service;
        }

        public QmtDealNotionManager(NBear.Data.Gateway way)
        {
			this.service = new QmtDealNotionService(way);
            base.BaseService = this.service;
        }

        #endregion

        public System.Data.DataSet GetDataByParas(QmtDealNotionParams queryParams)
        {
            return this.service.GetDataByParas(queryParams);
        }
    }
}
