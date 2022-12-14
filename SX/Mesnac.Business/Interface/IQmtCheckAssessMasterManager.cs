using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;

    using Mesnac.Data.Interface;

    public interface IQmtCheckAssessMasterManager : IBaseManager<QmtCheckAssessMaster>
    {
        DataSet GetCheckRubberAssessQSQueryByParas(IQmtCheckRubberAssessQSQueryParams paras);

        DataSet GetCheckRubberAssessQSReportByParas(IQmtCheckRubberAssessQSReportParams paras);

        DataSet GetCheckRubberAssessQualityCPKReportByParas(IQmtCheckRubberAssessQualityCPKReportParams paras);

        DataSet GetCheckRubberAssessQualitiedRateReportByParas(IQmtCheckRubberAssessQualitiedRateReportParams paras);
    }
}
