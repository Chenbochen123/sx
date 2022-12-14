using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    public class QmtCheckMasterManager : BaseManager<QmtCheckMaster>, IQmtCheckMasterManager
    {
		#region 属性注入与构造方法
		
        private IQmtCheckMasterService service;

        public QmtCheckMasterManager()
        {
            this.service = new QmtCheckMasterService();
            base.BaseService = this.service;
        }

		public QmtCheckMasterManager(string connectStringKey)
        {
			this.service = new QmtCheckMasterService(connectStringKey);
            base.BaseService = this.service;
        }

        public QmtCheckMasterManager(NBear.Data.Gateway way)
        {
			this.service = new QmtCheckMasterService(way);
            base.BaseService = this.service;
        }

        #endregion

        public PageResult<QmtCheckMaster> GetCheckSummaryQueryByParas(IQmtCheckMasterSummaryQueryParams paras)
        {
            return this.service.GetCheckSummaryQueryByParas(paras);
        }

        public DataSet GetMasterBatchReportByParas(IQmtCheckMasterBatchReportParams paras)
        {
            return this.service.GetMasterBatchReportByParas(paras);
        }

        public DataSet GetCheckRubberQSQueryByParas(IQmtCheckRubberQSQueryParams paras)
        {
            return this.service.GetCheckRubberQSQueryByParas(paras);
        }

        public DataSet GetCheckRubberQSReportByParas(IQmtCheckRubberQSReportParams paras)
        {
            return this.service.GetCheckRubberQSReportByParas(paras);
        }

        public DataSet GetCheckRubberQualitiedRateReportByParas(IQmtCheckRubberQualitiedRateReportParams paras)
        {
            return this.service.GetCheckRubberQualitiedRateReportByParas(paras);
        }

        public DataSet GetCheckRubberQualityMonthReportByParas(IQmtCheckRubberQualityMonthReportParams paras)
        {
            return this.service.GetCheckRubberQualityMonthReportByParas(paras);
        }

        public DataSet GetCheckRubberCardQueryByParas(IQmtCheckRubberCardQueryParams paras)
        {
            return this.service.GetCheckRubberCardQueryByParas(paras);
        }

        public DataSet GetCheckRubberCardReportByParas(IQmtCheckRubberCardReportParams paras)
        {
            return this.service.GetCheckRubberCardReportByParas(paras);
        }

        public DataSet GetCheckRubberQualityReportByParas(IQmtCheckRubberQualityReportParams paras)
        {
            return this.service.GetCheckRubberQualityReportByParas(paras);
        }

        public DataSet GetCheckRubberQualityReportViewByParas(IQmtCheckRubberQualityReportViewParams paras)
        {
            return this.service.GetCheckRubberQualityReportViewByParas(paras);
        }

        public DataSet GetCheckRubberQualityCPKReportByParas(IQmtCheckRubberQualityCPKReportParams paras)
        {
            return this.service.GetCheckRubberQualityCPKReportByParas(paras);
        }

        public DataSet GetCheckRubberQualifiedRateMonthReportByParas(IQmtRubberQualifiedRateMonthReportParams paras)
        {
            return this.service.GetCheckRubberQualifiedRateMonthReportByParas(paras);
        }

        public DataSet GetCheckRubberQualityCPKDailyReportByParas(IQmtRubberQualityCPKDailyReportParams paras)
        {
            return this.service.GetCheckRubberQualityCPKDailyReportByParas(paras);
        }

        public DataSet GetCheckRubberQualityAvgDailyReportByParas(IQmtRubberQualityAvgDailyReportParams paras)
        {
            return this.service.GetCheckRubberQualityAvgDailyReportByParas(paras);
        }

        public DataSet GetCheckRubberQualityWorkshopCPKReportByParas(IQmtRubberQualityWorkshopCPKReportParams paras)
        {
            return this.service.GetCheckRubberQualityWorkshopCPKReportByParas(paras);
        }

        public DataSet GetCheckRubberQualityEquipCPKReportByParas(IQmtRubberQualityEquipCPKReportParams paras)
        {
            return this.service.GetCheckRubberQualityEquipCPKReportByParas(paras);
        }

        public DataSet GetCheckRubberQualityCPKRateReportByParas(IQmtRubberQualityCPKRateReportParams paras)
        {
            return this.service.GetCheckRubberQualityCPKRateReportByParas(paras);
        }

        public DataSet GetCheckRubberLBEquipDataReportByParas(IQmtRubberLBEquipDataReportParams paras)
        {
            return this.service.GetCheckRubberLBEquipDataReportByParas(paras);
        }

        public DataSet GetCheckRubberQualityZJSCPKReportByParas(IQmtRubberQualityZJSCPKReportParams paras)
        {
            return this.service.GetCheckRubberQualityZJSCPKReportByParas(paras);
        }
    }
}
