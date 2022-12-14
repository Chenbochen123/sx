using System;
using System.Collections.Generic;
using System.Data;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Implements;
    /// <summary>
    /// IPmtRecipeMixingLogService 接口定义
    /// 孙本强 @ 2013-04-03 12:57:57
    /// </summary>
    /// <remarks></remarks>
    public interface IPmtRecipeMixingLogService : IBaseService<PmtRecipeMixingLog>
    {
        /// <summary>
        /// 获取密炼信息日志信息
        /// 孙本强 @ 2013-04-03 12:57:58
        /// </summary>
        /// <param name="recipe">The recipe.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        DataSet GetPmtRecipeMixingLog(string recipe);
    }
}
