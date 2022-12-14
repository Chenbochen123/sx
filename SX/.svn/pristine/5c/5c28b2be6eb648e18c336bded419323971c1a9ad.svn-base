using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    /// <summary>
    /// IPmtEquipAbilityService 接口定义
    /// 孙本强 @ 2013-04-03 13:00:39
    /// </summary>
    /// <remarks></remarks>
    public interface IPmtEquipAbilityService : IBaseService<PmtEquipAbility>
    {
        /// <summary>
        /// 获取分页数据集
        /// 孙本强 @ 2013-04-03 13:00:40
        /// </summary>
        /// <param name="queryParams">查询参数</param>
        /// <returns>分页数据集</returns>
        /// <remarks></remarks>
        PageResult<PmtEquipAbility> GetTablePageDataBySql(Mesnac.Data.Implements.PmtEquipAbilityService.QueryParams queryParams);

        /// <summary>
        /// 执行存储过程进行汇总
        /// 孙本强 @ 2013-04-03 13:00:40
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="shiftID">The shift ID.</param>
        /// <remarks></remarks>
        void ExecProcEquipAbility(string startDate, string endDate, string shiftID, string workbarcode);
    }
}
