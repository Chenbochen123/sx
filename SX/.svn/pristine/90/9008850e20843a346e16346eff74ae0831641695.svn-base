using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    /// <summary>
    /// IPmtEquipAbilityManager 接口定义
    /// 孙本强 @ 2013-04-03 11:49:36
    /// </summary>
    /// <remarks></remarks>
    public interface IPmtEquipAbilityManager : IBaseManager<PmtEquipAbility>
    {
        /// <summary>
        /// 获取分页数据集
        /// 孙本强 @ 2013-04-03 11:49:36
        /// </summary>
        /// <param name="queryParams">查询参数</param>
        /// <returns>分页数据集</returns>
        /// <remarks></remarks>
        PageResult<PmtEquipAbility> GetTablePageDataBySql(Mesnac.Data.Implements.PmtEquipAbilityService.QueryParams queryParams);
        /// <summary>
        /// 执行存储过程进行汇总
        /// 孙本强 @ 2013-04-03 11:49:37
        /// </summary>
        /// <param name="startDate">汇总开始日期</param>
        /// <param name="endDate">汇总结束日期</param>
        /// <param name="shiftID">当班次为“全部”时，传入参数为“0”</param>
        /// <remarks></remarks>
        void ExecProcEquipAbility(string startDate, string endDate, string shiftID, string workbarcode);
    }
}
