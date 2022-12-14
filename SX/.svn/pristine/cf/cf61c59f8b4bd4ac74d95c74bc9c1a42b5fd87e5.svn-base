using System;
using System.Collections.Generic;
using System.Text;
using NBear.Common;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    using System.Data;
    using Mesnac.Data.Components;
    public class PptPlanManager : BaseManager<PptPlan>, IPptPlanManager
    {
        #region 属性注入与构造方法

        private IPptPlanService service;

        public PptPlanManager()
        {
            this.service = new PptPlanService();
            base.BaseService = this.service;
        }

        public PptPlanManager(string connectStringKey)
        {
            this.service = new PptPlanService(connectStringKey);
            base.BaseService = this.service;
        }

        public PptPlanManager(NBear.Data.Gateway way)
        {
            this.service = new PptPlanService(way);
            base.BaseService = this.service;
        }

        #endregion
        #region 查询条件类定义
        public class QueryParams : PptPlanService.QueryParams
        {
        }
        public PageResult<PptPlan> GetTablePageDataBySql(Mesnac.Data.Implements.PptPlanService.QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }
        #endregion

        /// <summary>
        /// 返回指定机台计划信息
        /// 孙宜建
        /// 2013-2-20
        /// </summary>
        /// <param name="group">机台编码</param>
        /// <param name="date">计划日期</param>
        /// <returns></returns>
        public DataSet GetEquipPlan(string equipCode, string date)
        {
            return service.GetEquipPlan(equipCode, date);
        }
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
        public DataSet GetEquipPlan(string equipCode, string date, string shiftid, string recipematerialcode, string recipename)
        {
            return service.GetEquipPlan(equipCode, date, shiftid, recipematerialcode, recipename);
        }
        /// <summary>
        /// 返回指定车间的设备单位时间产量
        /// 王锴
        /// 2014-3-25
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="workShopCode"></param>
        /// <returns></returns>
        public DataSet GetEquipmentPruductionSummary(DateTime beginTime, DateTime endTime, string workShopCode, string shiftId, string equipmentCode)
        {
            return service.GetEquipmentPruductionSummary(beginTime, endTime, workShopCode, shiftId, equipmentCode);
        }
        /// <summary>
        /// 计划执行监控
        /// 2013-03-7
        /// 孙宜建
        /// </summary>
        /// <param name="equipCode">机台</param>
        /// <param name="date">日期</param>
        /// <returns></returns>
        public DataSet GetPlanMonitor(string equipCode, string date)
        {
            return service.GetPlanMonitor(equipCode, date);
        }
        /// <summary>
        /// 返回新增计划号
        /// 孙宜建
        /// 2013-2-26
        /// </summary>
        /// <param name="date">日期XXXX-XX-XX</param>
        /// <param name="equipCode">机台编码</param>
        /// <param name="shiftid">班次</param>
        /// <returns></returns>
        public string GetGetMaxPlanId(string date, string equipCode, string shiftid)
        {
            return this.service.GetGetMaxPlanId(date, equipCode, shiftid);
        }

        /// <summary>
        /// 插入计划时更改插入后的优先级
        /// sunyj
        /// 2013-2-27
        /// </summary>
        /// <param name="date">计划日期</param>
        /// <param name="equipCode">机台编号</param>
        /// <param name="shiftid">班次</param>
        /// <param name="priLevel">插入的优先级</param>
        public bool UpdatePriLevel(string date, string equipCode, string shiftid, int priLevel)
        {
            return this.service.UpdatePriLevel(date, equipCode, shiftid, priLevel);
        }

        /// <summary>
        /// 批量下达计划
        /// 孙宜建 2013-3-1
        /// </summary>
        /// <param name="equipCode">机台</param>
        /// <param name="date">日期</param>
        /// <param name="shiftid">班次</param>
        /// <returns></returns>
        public bool AllUpdatePlanState(string equipCode, string date, string shiftid)
        {
            return this.service.AllUpdatePlanState(equipCode, date, shiftid);
        }
        /// <summary>
        /// 获取药品的计划排产信息
        /// 孙宜建
        /// 2013-3-2
        /// </summary>
        /// <param name="date">日期</param>
        /// <param name="equipCode">机台</param>
        /// <param name="shiftId">班次</param>
        /// <returns></returns>
        public DataSet GetXLPlanCreate(string date, string equipCode, string shiftId)
        {
            return this.service.GetXLPlanCreate(date, equipCode, shiftId);
        }

        #region IPptPlanManager 成员

        /// <summary>
        /// 获取生产计划的领料单
        /// 2013-4-1
        /// 孙宜建
        /// </summary>
        /// <param name="date">计划日期</param>
        /// <param name="type"></param>
        /// <returns></returns>
        public DataTable GetSumDayMater(string date, string type,string store)
        {
            return this.service.GetSumDayMater(date, type,store);
        }

        /// <summary>
        /// 获取小料称量查询中的计划信息
        /// 孙宜建
        /// 2013-4-2
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public PageResult<PptPlan> GetSmallPlanTablePageDataBySql(PptPlanService.QueryParams queryParams)
        {
            return this.service.GetSmallPlanTablePageDataBySql(queryParams);
        }

        #endregion


        /// <summary>
        /// 获取分页数据集
        /// 孙本强 @ 2013-04-03 13:34:04
        /// </summary>
        /// <param name="queryParams">查询参数</param>
        /// <returns>分页数据集</returns>
        /// <remarks></remarks>
        public PageResult<PptPlan> GetPlanLotReportPageDataBySql(QueryParams queryParams)
        {
            return this.service.GetPlanLotReportPageDataBySql(queryParams);
        }


        public EntityArrayList<BasMaterial> GetPlanPptMaterial(PptPlanManager.QueryParams queryParams)
        {
            return this.service.GetPlanPptMaterial(queryParams);
        }


        public DataSet GetRptPlanLotMain(string planID) {
            return this.service.GetRptPlanLotMain(planID);
        }
        public DataSet GetRptPlanLotMainAvgAndSum(string planID)
        {
            return this.service.GetRptPlanLotMainAvgAndSum(planID);
        }
        public  DataSet GetRptPlanLotMaterialDetailInfo(string planID) {
            return this.service.GetRptPlanLotMaterialDetailInfo(planID);
        }
        public DataSet GetRptPlanLotRubsDetailInfo(string planID)
        {
            return this.service.GetRptPlanLotRubsDetailInfo(planID);
        }
        public DataSet GetRptPlanLotSumDetailInfo(string planID)
        {
            return this.service.GetRptPlanLotSumDetailInfo(planID);
        }
        public DataSet GetPlanTotalReport(string byGroup, DateTime beginDate, DateTime endDate, string shiftID, string classID, string equipCode, string materCode)
        {
            return this.service.GetPlanTotalReport(byGroup, beginDate, endDate, shiftID, classID, equipCode, materCode);
        }

        public PageResult<PptPlan> GetTableTotalDataBySql(QueryParams queryParams)
        {
            return this.service.GetTableTotalDataBySql(queryParams);
        }

        public DataSet CompoundQuery(QueryParams queryParams)
        {
            return this.service.CompoundQuery(queryParams);
        }
        public DataSet CompoundQueryMonth(QueryParams queryParams)
        {
            return this.service.CompoundQueryMonth(queryParams);
        }
      
    }
}
