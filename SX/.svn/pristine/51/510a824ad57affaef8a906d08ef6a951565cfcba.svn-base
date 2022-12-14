using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    public interface IQmtCheckAssessMasterService : IBaseService<QmtCheckAssessMaster>
    {
        DataSet GetCheckRubberAssessQSQueryByParas(IQmtCheckRubberAssessQSQueryParams paras);

        DataSet GetCheckRubberAssessQSReportByParas(IQmtCheckRubberAssessQSReportParams paras);

        DataSet GetCheckRubberAssessQualityCPKReportByParas(IQmtCheckRubberAssessQualityCPKReportParams paras);

        DataSet GetCheckRubberAssessQualitiedRateReportByParas(IQmtCheckRubberAssessQualitiedRateReportParams paras);
    }

    public interface IQmtCheckRubberAssessQSQueryParams
    {
        string CheckPlanSDate { get; set; }
        string CheckPlanEDate { get; set; }
        string CheckShiftClass { get; set; }
        string CheckShiftId { get; set; }
        string WorkShopCode { get; set; }
    }

    public interface IQmtCheckRubberAssessQSReportParams
    {
        string CheckPlanDate { get; set; }
        string CheckShiftClass { get; set; }
        string CheckShiftId { get; set; }
        string WorkShopCode { get; set; }
    }

    public interface IQmtCheckRubberAssessQualityCPKReportParams
    {
        string MaterCode { get; set; }
        string BeginPlanDate { get; set; }
        string EndPlanDate { get; set; }
        string ZJSID { get; set; }
        string EquipCode { get; set; }
        string OtherMaterCodes { get; set; }
    }

    public interface IQmtCheckRubberAssessQualitiedRateReportParams
    {
        string BeginPlanDate { get; set; }
        string EndPlanDate { get; set; }
        string WorkBar { get; set; }
        string ShiftID { get; set; }
    }

}
