using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    public class QmtCheckItemTypeManager : BaseManager<QmtCheckItemType>, IQmtCheckItemTypeManager
    {
		#region 属性注入与构造方法
		
        private IQmtCheckItemTypeService service;

        public QmtCheckItemTypeManager()
        {
            this.service = new QmtCheckItemTypeService();
            base.BaseService = this.service;
        }

		public QmtCheckItemTypeManager(string connectStringKey)
        {
			this.service = new QmtCheckItemTypeService(connectStringKey);
            base.BaseService = this.service;
        }

        public QmtCheckItemTypeManager(NBear.Data.Gateway way)
        {
			this.service = new QmtCheckItemTypeService(way);
            base.BaseService = this.service;
        }

        #endregion

        public System.Data.DataSet GetDataByParas(QmtCheckItemTypeParams queryParams)
        {
            return this.service.GetDataByParas(queryParams);
        }

        public string GetNextTypeIDByParas(QmtCheckItemType qmtCheckItemType)
        {
            return this.service.GetNextTypeIDByParas(qmtCheckItemType);
        }
    }
}
