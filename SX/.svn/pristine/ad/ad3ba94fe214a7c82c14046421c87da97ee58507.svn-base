using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    public class QmtCheckStandTypeManager : BaseManager<QmtCheckStandType>, IQmtCheckStandTypeManager
    {
		#region 属性注入与构造方法
		
        private IQmtCheckStandTypeService service;

        public QmtCheckStandTypeManager()
        {
            this.service = new QmtCheckStandTypeService();
            base.BaseService = this.service;
        }

		public QmtCheckStandTypeManager(string connectStringKey)
        {
			this.service = new QmtCheckStandTypeService(connectStringKey);
            base.BaseService = this.service;
        }

        public QmtCheckStandTypeManager(NBear.Data.Gateway way)
        {
			this.service = new QmtCheckStandTypeService(way);
            base.BaseService = this.service;
        }

        #endregion

        public System.Data.DataSet GetDataByParas(QmtCheckStandTypeParams queryParams)
        {
            return this.service.GetDataByParas(queryParams);
        }
    }
}
