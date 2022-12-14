using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    /// <summary>
    /// 密炼
    /// 孙本强 @ 2013-04-03 13:02:39
    /// </summary>
    /// <remarks></remarks>
    public class PmtRecipeMixingLogService : BaseService<PmtRecipeMixingLog>, IPmtRecipeMixingLogService
    {
        #region 构造方法

        /// <summary>
        /// 类 PmtRecipeMixingLogService 构造函数
        /// 孙本强 @ 2013-04-03 13:02:39
        /// </summary>
        /// <remarks></remarks>
        public PmtRecipeMixingLogService() : base() { }

        /// <summary>
        /// 类 PmtRecipeMixingLogService 构造函数
        /// 孙本强 @ 2013-04-03 13:02:39
        /// </summary>
        /// <param name="connectStringKey">The connect string key.</param>
        /// <remarks></remarks>
        public PmtRecipeMixingLogService(string connectStringKey) : base(connectStringKey) { }

        /// <summary>
        /// 类 PmtRecipeMixingLogService 构造函数
        /// 孙本强 @ 2013-04-03 13:02:39
        /// </summary>
        /// <param name="way">The way.</param>
        /// <remarks></remarks>
        public PmtRecipeMixingLogService(NBear.Data.Gateway way) : base(way) { }

        #endregion 构造方法

        /// <summary>
        /// 获取密炼信息日志信息
        /// 孙本强 @ 2013-04-03 12:57:58
        /// 孙本强 @ 2013-04-03 13:02:39
        /// </summary>
        /// <param name="recipe">The recipe.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public DataSet GetPmtRecipeMixingLog(string recipe)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(@"SELECT t1.*,t2.ShowName AS ActionName,t3.ShowName AS TermName,'0' as isDifference FROM dbo.PmtRecipeMixingLog t1
                            LEFT JOIN dbo.PmtAction t2 ON t1.ActionCode = t2.ActionCode
                            LEFT JOIN dbo.PmtTerm t3 ON t1.TermCode=t3.TermCode");
            sql.AppendLine(" WHERE t1.LogObjID=").Append(recipe).AppendLine(" ORDER BY t1.MixingStep");
            DataSet ds = new DataSet();
            ds = this.GetBySql(sql.ToString()).ToDataSet();
            return ds;
        }
    }
}
