using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    public class QmtQrigImportLogMasterManager : BaseManager<QmtQrigImportLogMaster>, IQmtQrigImportLogMasterManager
    {
		#region 属性注入与构造方法
		
        private IQmtQrigImportLogMasterService service;

        public QmtQrigImportLogMasterManager()
        {
            this.service = new QmtQrigImportLogMasterService();
            base.BaseService = this.service;
        }

		public QmtQrigImportLogMasterManager(string connectStringKey)
        {
			this.service = new QmtQrigImportLogMasterService(connectStringKey);
            base.BaseService = this.service;
        }

        public QmtQrigImportLogMasterManager(NBear.Data.Gateway way)
        {
			this.service = new QmtQrigImportLogMasterService(way);
            base.BaseService = this.service;
        }

        #endregion

        public System.Data.DataSet GetQrigDetailInfo(string guid)
        {
            return this.service.GetQrigDetailInfo(guid);
        }
    }
}
