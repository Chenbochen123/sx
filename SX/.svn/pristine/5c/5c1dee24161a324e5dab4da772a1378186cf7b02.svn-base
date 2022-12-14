using System;
using System.Collections.Generic;
using System.Text;
using NBear.Common;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using System.Data;
    using Mesnac.Data.Components;
    public interface IPptPlanService : IBaseService<PptPlan>
    {

        /// <summary>
        /// 返回指定机台计划信息
        /// 孙宜建
        /// 2013-2-20
        /// </summary>
        /// <param name="group">机台编码</param>
        /// <param name="date">计划日期</param>
        /// <returns></returns>
        DataSet GetEquipPlan(string equipCode, string date);
        /// <summary>
        /// 带条件查询返回指定机台计划信息
        /// 袁洋
        /// 2013-3-21
        /// </summary>
        /// <param name="equipCode">机台编码</param>
        /// <param name="date">计划日期</param>
        /// <param name="shiftid">班次号</param>
        /// <param name="recipematerialcode">配方物料代码</param>
        /// <param name="recipename">配方名称</param>
        /// <returns></returns>
        DataSet GetEquipPlan(string equipCode, string date, string shiftid, string recipematerialcode, string recipename);
        /// <summary>
        /// 计划执行监控
        /// 2013-03-7
        /// 孙宜建
        /// </summary>
        /// <param name="equipCode">机台</param>
        /// <param name="date">日期</param>
        /// <returns></returns>
        DataSet GetPlanMonitor(string equipCode, string date);
        /// <summary>
        /// 返回新增计划号
        /// 孙宜建
        /// 2013-2-26
        /// </summary>
        /// <param name="date">日期XXXX-XX-XX</param>
        /// <param name="equipCode">机台编码</param>
        /// <param name="shiftid">班次</param>
        /// <returns></returns>
        string GetGetMaxPlanId(string date, string equipCode, string shiftid);
        /// <summary>
        /// 返回指定车间的设备单位时间产量
        /// 王锴
        /// 2014-3-25
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="workShopCode"></param>
        /// <returns></returns>
        DataSet GetEquipmentPruductionSummary(DateTime beginTime, DateTime endTime, string workShopCode, string shiftId, string equipmentCode);
        /// <summary>
        /// 插入计划时更改插入后的优先级
        /// sunyj
        /// 2013-2-27
        /// </summary>
        /// <param name="date">计划日期</param>
        /// <param name="equipCode">机台编号</param>
        /// <param name="shiftid">班次</param>
        /// <param name="priLevel">插入的优先级</param>
        bool UpdatePriLevel(string date, string equipCode, string shiftid, int priLevel);

        /// <summary>
        /// 批量下达计划
        /// 孙宜建 2013-3-1
        /// </summary>
        /// <param name="equipCode">机台</param>
        /// <param name="date">日期</param>
        /// <param name="shiftid">班次</param>
        /// <returns></returns>
        bool AllUpdatePlanState(string equipCode, string date, string shiftid);


        /// <summary>
        /// 获取药品的计划排产信息
        /// 孙宜建
        /// 2013-3-2
        /// </summary>
        /// <param name="date">日期</param>
        /// <param name="equipCode">机台</param>
        /// <param name="shiftId">班次</param>
        /// <returns></returns>
        DataSet GetXLPlanCreate(string date, string equipCode, string shiftId);
        /// <summary>
        /// 计划执行分析
        /// 孙宜建
        /// 2013-3-28
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        PageResult<PptPlan> GetTablePageDataBySql(Mesnac.Data.Implements.PptPlanService.QueryParams queryParams);
        /// <summary>
        /// 获取生产计划的领料单
        /// 2013-4-1
        /// 孙宜建
        /// </summary>
        /// <param name="date">计划日期</param>
        /// <param name="type"></param>
        /// <returns></returns>
        DataTable GetSumDayMater(string date, string type,string store);

        /// <summary>
        /// 获取小料称量查询中的计划信息
        /// 孙宜建
        /// 2013-4-2
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        PageResult<PptPlan> GetSmallPlanTablePageDataBySql(Implements.PptPlanService.QueryParams queryParams);


        /// <summary>
        /// 获取分页数据集
        /// 孙本强 @ 2013-04-03 13:33:12
        /// </summary>
        /// <param name="queryParams">查询参数</param>
        /// <returns>分页数据集</returns>
        /// <remarks></remarks>
        PageResult<PptPlan> GetPlanLotReportPageDataBySql(Mesnac.Data.Implements.PptPlanService.QueryParams queryParams);

        EntityArrayList<BasMaterial> GetPlanPptMaterial(Implements.PptPlanService.QueryParams queryParams);
        /// <summary>
        /// 批报表根据planID值获取主信息
        /// </summary>
        /// <param name="planID"></param>
        /// <returns></returns>
        DataSet GetRptPlanLotMain(string planID);
        DataSet GetRptPlanLotMainAvgAndSum(string planID);
        DataSet GetRptPlanLotMaterialDetailInfo(string planID);
        DataSet GetRptPlanLotRubsDetailInfo(string planID);
        DataSet GetRptPlanLotSumDetailInfo(string planID);
        DataSet GetPlanTotalReport(string byGroup, DateTime beginDate, DateTime endDate, string shiftID, string classID, string equipCode, string materCode);
        PageResult<PptPlan> GetTableTotalDataBySql(Implements.PptPlanService.QueryParams queryParams);
        DataSet CompoundQuery(Implements.PptPlanService.QueryParams queryParams);
        DataSet CompoundQueryMonth(Implements.PptPlanService.QueryParams queryParams);

    }
}
