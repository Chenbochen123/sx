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
    public class QmtCheckDetailManager : BaseManager<QmtCheckDetail>, IQmtCheckDetailManager
    {
		#region 属性注入与构造方法
		
        private IQmtCheckDetailService service;

        public QmtCheckDetailManager()
        {
            this.service = new QmtCheckDetailService();
            base.BaseService = this.service;
        }

		public QmtCheckDetailManager(string connectStringKey)
        {
			this.service = new QmtCheckDetailService(connectStringKey);
            base.BaseService = this.service;
        }

        public QmtCheckDetailManager(NBear.Data.Gateway way)
        {
			this.service = new QmtCheckDetailService(way);
            base.BaseService = this.service;
        }

        #endregion

        public DataSet GetCheckRubberQualityReportDetailByParas(IQmtCheckRubberQualityReportDetailParams paras)
        {
            return this.service.GetCheckRubberQualityReportDetailByParas(paras);
        }
    }
}
