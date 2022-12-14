using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    public class QmtCheckItemManager : BaseManager<QmtCheckItem>, IQmtCheckItemManager
    {
		#region 属性注入与构造方法
		
        private IQmtCheckItemService service;

        public QmtCheckItemManager()
        {
            this.service = new QmtCheckItemService();
            base.BaseService = this.service;
        }

		public QmtCheckItemManager(string connectStringKey)
        {
			this.service = new QmtCheckItemService(connectStringKey);
            base.BaseService = this.service;
        }

        public QmtCheckItemManager(NBear.Data.Gateway way)
        {
			this.service = new QmtCheckItemService(way);
            base.BaseService = this.service;
        }

        #endregion

        public System.Data.DataSet GetDataByParas(QmtCheckItemParams queryParams)
        {
            return this.service.GetDataByParas(queryParams);
        }

        public string GetNextItemCodeByParas(QmtCheckItem qmtCheckItem)
        {
            return this.service.GetNextItemCodeByParas(qmtCheckItem);
        }
    }
}
