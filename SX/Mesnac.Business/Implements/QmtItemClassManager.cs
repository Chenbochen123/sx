using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    using Mesnac.Data.Components;
    public class QmtItemClassManager : BaseManager<QmtItemClass>, IQmtItemClassManager
    {
		#region 属性注入与构造方法
		
        private IQmtItemClassService service;

        public QmtItemClassManager()
        {
            this.service = new QmtItemClassService();
            base.BaseService = this.service;
        }

		public QmtItemClassManager(string connectStringKey)
        {
			this.service = new QmtItemClassService(connectStringKey);
            base.BaseService = this.service;
        }

        public QmtItemClassManager(NBear.Data.Gateway way)
        {
			this.service = new QmtItemClassService(way);
            base.BaseService = this.service;
        }

        #endregion
        #region 查询条件类定义
        public class QueryParams : QmtItemClassService.QueryParams
        {
        }
        #endregion
        public PageResult<QmtItemClass> GetTablePageDataBySql(Mesnac.Data.Implements.QmtItemClassService.QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }


        public int GetItemClassNextPrimaryKeyValue()
        {
            return this.service.GetItemClassNextPrimaryKeyValue();
        }
    }
}
