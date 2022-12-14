using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    public interface IQmtCheckMasterService : IBaseService<QmtCheckMaster>
    {
        PageResult<QmtCheckMaster> GetCheckSummaryQueryByParas(IQmtCheckMasterSummaryQueryParams paras);

        DataSet GetMasterBatchReportByParas(IQmtCheckMasterBatchReportParams paras);

        DataSet GetCheckRubberQSQueryByParas(IQmtCheckRubberQSQueryParams paras);

        DataSet GetCheckRubberQSReportByParas(IQmtCheckRubberQSReportParams paras);

        DataSet GetCheckRubberQualitiedRateReportByParas(IQmtCheckRubberQualitiedRateReportParams paras);

        DataSet GetCheckRubberQualityMonthReportByParas(IQmtCheckRubberQualityMonthReportParams paras);

        DataSet GetCheckRubberCardQueryByParas(IQmtCheckRubberCardQueryParams paras);

        DataSet GetCheckRubberCardReportByParas(IQmtCheckRubberCardReportParams paras);

        DataSet GetCheckRubberQualityReportByParas(IQmtCheckRubberQualityReportParams paras);

        DataSet GetCheckRubberQualityReportViewByParas(IQmtCheckRubberQualityReportViewParams paras);

        DataSet GetCheckRubberQualityCPKReportByParas(IQmtCheckRubberQualityCPKReportParams paras);

        DataSet GetCheckRubberQualifiedRateMonthReportByParas(IQmtRubberQualifiedRateMonthReportParams paras);

        DataSet GetCheckRubberQualityCPKDailyReportByParas(IQmtRubberQualityCPKDailyReportParams paras);

        DataSet GetCheckRubberQualityAvgDailyReportByParas(IQmtRubberQualityAvgDailyReportParams paras);

        DataSet GetCheckRubberQualityWorkshopCPKReportByParas(IQmtRubberQualityWorkshopCPKReportParams paras);

        DataSet GetCheckRubberQualityEquipCPKReportByParas(IQmtRubberQualityEquipCPKReportParams paras);

        DataSet GetCheckRubberQualityCPKRateReportByParas(IQmtRubberQualityCPKRateReportParams paras);

        DataSet GetCheckRubberLBEquipDataReportByParas(IQmtRubberLBEquipDataReportParams paras);

        DataSet GetCheckRubberQualityZJSCPKReportByParas(IQmtRubberQualityZJSCPKReportParams paras);
    }

    public interface IQmtCheckMasterSummaryQueryParams
    {
        string PlanSDate { get; set; }
        string PlanEDate { get; set; }
        string WorkShopId { get; set; }
        string ShiftId { get; set; }
        string ShiftClass { get; set; }
        string ZJSID { get; set; }
        string MaterCode { get; set; }
        string StandCode { get; set; }
        string JudgeResult { get; set; }
        string EquipCode { get; set; }

        PageResult<QmtCheckMaster> PageParams { get; set; }

    }

    public interface IQmtCheckMasterBatchReportParams
    {
        string PlanDate { get; set; }
        string ShiftId { get; set; }
        string MaterCode { get; set; }
        string EquipCode { get; set; }
        string StartSerialId { get; set; }
        string EndSerialId { get; set; }
        string StandCode { get; set; }
    }

    public interface IQmtCheckRubberQSQueryParams
    {
        string CheckPlanSDate { get; set; }
        string CheckPlanEDate { get; set; }
        string CheckShiftClass { get; set; }
        string CheckShiftId { get; set; }
        string WorkShopCode { get; set; }
    }

    public interface IQmtCheckRubberQSReportParams
    {
        string CheckPlanDate { get; set; }
        string CheckShiftClass { get; set; }
        string CheckShiftId { get; set; }
        string WorkShopCode { get; set; }
    }

    public interface IQmtCheckRubberQualitiedRateReportParams
    {
        string BeginPlanDate { get; set; }
        string EndPlanDate { get; set; }
        string WorkBar { get; set; }
        string ShiftID { get; set; }
    
    }

    public interface IQmtCheckRubberQualityMonthReportParams
    {
        //string PlanMonth { get; set; }
        string BeginPlanDate { get; set; }
        string EndPlanDate { get; set; }
        string CheckTypeCode { get; set; }
        string WorkBar { get; set; }
        string ShiftID { get; set; }
    }

    public interface IQmtCheckRubberCardQueryParams
    {
        string PlanDate { get; set; }
        string ZJSID { get; set; }
        string MaterCode { get; set; }
        string EquipCode { get; set; }
        string ShiftId { get; set; }
        string StandCode { get; set; }
    }

    public interface IQmtCheckRubberCardReportParams
    {
        string PlanDate { get; set; }
        string Barcode { get; set; }
    }

    public interface IQmtCheckRubberQualityReportParams
    {
        string BeginCheckPlanDate { get; set; }
        string EndCheckPlanDate { get; set; }
        string WorkShopCode { get; set; }
        string CheckTypeCode { get; set; }
    }

    public interface IQmtCheckRubberQualityReportViewParams
    {
        string RubTypeCode { get; set; }
        string CheckPlanDate { get; set; }
        string ShiftCheckId { get; set; }
        string WorkShopCode { get; set; }
        string JudgeResult { get; set; }
        string CheckTypeCode { get; set; }
    }

    public interface IQmtCheckRubberQualityCPKReportParams
    {
        string MaterCode { get; set; }
        string BeginPlanDate { get; set; }
        string EndPlanDate { get; set; }
        string ZJSID { get; set; }
        string EquipCode { get; set; }
        string OtherMaterCodes { get; set; }
    }

    /// <summary>
    /// 胶料合格率月报表查询参数接口
    /// </summary>
    public interface IQmtRubberQualifiedRateMonthReportParams
    {
        string BeginPlanDate { get; set; }
        string EndPlanDate { get; set; }
        string RubTypeCode { get; set; }
        string StandCode { get; set; }
        string ZJSID { get; set; }
    }

    /// <summary>
    /// 胶料CPK统计日报表查询参数接口
    /// </summary>
    public interface IQmtRubberQualityCPKDailyReportParams
    {
        string BeginPlanDate { get; set; }
        string EndPlanDate { get; set; }
        string MaterCode { get; set; }
        string StandCode { get; set; }
    }

    /// <summary>
    /// 胶料质检均值统计日报表查询参数接口
    /// </summary>
    public interface IQmtRubberQualityAvgDailyReportParams
    {
        string BeginPlanDate { get; set; }
        string EndPlanDate { get; set; }
        string MaterCode { get; set; }
    }

    /// <summary>
    /// 胶料质检车间CPK统计日报表查询参数接口
    /// </summary>
    public interface IQmtRubberQualityWorkshopCPKReportParams
    {
        string BeginPlanDate { get; set; }
        string EndPlanDate { get; set; }
    }

    /// <summary>
    /// 胶料质检机台CPK统计日报表查询参数接口
    /// </summary>
    public interface IQmtRubberQualityEquipCPKReportParams
    {
        string BeginPlanDate { get; set; }
        string EndPlanDate { get; set; }
    }

    /// <summary>
    /// CPK合格率统计日报表查询参数接口
    /// </summary>
    public interface IQmtRubberQualityCPKRateReportParams
    {
        string BeginPlanDate { get; set; }
        string EndPlanDate { get; set; }
        string MaterCode { get; set; }
        string StandCode { get; set; }
    }

    /// <summary>
    /// 胶料硫变仪稳定性统计日报表查询参数接口
    /// </summary>
    public interface IQmtRubberLBEquipDataReportParams
    {
        string BeginPlanDate { get; set; }
        string WorkShopCode { get; set; }
        string ItemType { get; set; }
    }

    /// <summary>
    /// 胶料质检主机手CPK统计日报表查询参数接口
    /// </summary>
    public interface IQmtRubberQualityZJSCPKReportParams
    {
        string BeginPlanDate { get; set; }
        string EndPlanDate { get; set; }
    }


}
