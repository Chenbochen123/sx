using System;
using System.Collections.Generic;
using System.Data;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Implements;
    /// <summary>
    /// IPmtRecipeWeightLogService 接口定义
    /// 孙本强 @ 2013-04-03 12:54:55
    /// </summary>
    /// <remarks></remarks>
    public interface IPmtRecipeWeightLogService : IBaseService<PmtRecipeWeightLog>
    {
        /// <summary>
        /// 获取称量信息的日志信息
        /// 孙本强 @ 2013-04-03 12:54:55
        /// </summary>
        /// <param name="recipe">The recipe.</param>
        /// <param name="weightType">Type of the weight.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        DataSet GetPmtRecipeWeightLog(string recipe, string weightType);
    }
}
