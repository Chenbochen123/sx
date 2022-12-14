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
    public class QmtCheckAssessMasterManager : BaseManager<QmtCheckAssessMaster>, IQmtCheckAssessMasterManager
    {
		#region 属性注入与构造方法
		
        private IQmtCheckAssessMasterService service;

        public QmtCheckAssessMasterManager()
        {
            this.service = new QmtCheckAssessMasterService();
            base.BaseService = this.service;
        }

		public QmtCheckAssessMasterManager(string connectStringKey)
        {
			this.service = new QmtCheckAssessMasterService(connectStringKey);
            base.BaseService = this.service;
        }

        public QmtCheckAssessMasterManager(NBear.Data.Gateway way)
        {
			this.service = new QmtCheckAssessMasterService(way);
            base.BaseService = this.service;
        }

        #endregion

        public DataSet GetCheckRubberAssessQSQueryByParas(IQmtCheckRubberAssessQSQueryParams paras)
        {
            return this.service.GetCheckRubberAssessQSQueryByParas(paras);
        }

        public DataSet GetCheckRubberAssessQSReportByParas(IQmtCheckRubberAssessQSReportParams paras)
        {
            return this.service.GetCheckRubberAssessQSReportByParas(paras);
        }

        public DataSet GetCheckRubberAssessQualityCPKReportByParas(IQmtCheckRubberAssessQualityCPKReportParams paras)
        {
            return this.service.GetCheckRubberAssessQualityCPKReportByParas(paras);
        }

        public DataSet GetCheckRubberAssessQualitiedRateReportByParas(IQmtCheckRubberAssessQualitiedRateReportParams paras)
        {
            return this.service.GetCheckRubberAssessQualitiedRateReportByParas(paras);
        }


    }
}
