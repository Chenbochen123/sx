using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    public class QmtCheckAssessDetailManager : BaseManager<QmtCheckAssessDetail>, IQmtCheckAssessDetailManager
    {
		#region 属性注入与构造方法
		
        private IQmtCheckAssessDetailService service;

        public QmtCheckAssessDetailManager()
        {
            this.service = new QmtCheckAssessDetailService();
            base.BaseService = this.service;
        }

		public QmtCheckAssessDetailManager(string connectStringKey)
        {
			this.service = new QmtCheckAssessDetailService(connectStringKey);
            base.BaseService = this.service;
        }

        public QmtCheckAssessDetailManager(NBear.Data.Gateway way)
        {
			this.service = new QmtCheckAssessDetailService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
