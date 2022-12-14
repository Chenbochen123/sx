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
    /// PmtRecipeWeightLogService 实现类
    /// 孙本强 @ 2013-04-03 13:02:24
    /// </summary>
    /// <remarks></remarks>
    public class PmtRecipeWeightLogService : BaseService<PmtRecipeWeightLog>, IPmtRecipeWeightLogService
    {
        #region 构造方法

        /// <summary>
        /// 类 PmtRecipeWeightLogService 构造函数
        /// 孙本强 @ 2013-04-03 13:02:24
        /// </summary>
        /// <remarks></remarks>
        public PmtRecipeWeightLogService() : base() { }

        /// <summary>
        /// 类 PmtRecipeWeightLogService 构造函数
        /// 孙本强 @ 2013-04-03 13:02:24
        /// </summary>
        /// <param name="connectStringKey">The connect string key.</param>
        /// <remarks></remarks>
        public PmtRecipeWeightLogService(string connectStringKey) : base(connectStringKey) { }

        /// <summary>
        /// 类 PmtRecipeWeightLogService 构造函数
        /// 孙本强 @ 2013-04-03 13:02:24
        /// </summary>
        /// <param name="way">The way.</param>
        /// <remarks></remarks>
        public PmtRecipeWeightLogService(NBear.Data.Gateway way) : base(way) { }

        #endregion 构造方法


        /// <summary>
        /// 获取称量信息的日志信息
        /// 孙本强 @ 2013-04-03 12:54:55
        /// 孙本强 @ 2013-04-03 13:02:24
        /// </summary>
        /// <param name="recipe">The recipe.</param>
        /// <param name="weightType">Type of the weight.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public DataSet GetPmtRecipeWeightLog(string recipe, string weightType)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(@"SELECT t1.*,t2.ShowName AS ActionName,t3.MaterialName AS MaterialShowName,'0' as isDifference FROM dbo.PmtRecipeWeightLog t1
                            LEFT JOIN dbo.PmtWeightAction t2 ON t1.ActCode = t2.ActionCode
                            LEFT JOIN dbo.BasMaterial t3 ON t1.MaterialCode=t3.MaterialCode");
            sql.AppendLine(" WHERE t1.LogObjID=").Append(recipe);
            sql.AppendLine(" AND t1.WeightType=").Append(weightType).AppendLine(" ORDER BY t1.WeightID");
            DataSet ds = new DataSet();
            ds = this.GetBySql(sql.ToString()).ToDataSet();
            return ds;
        }
    }
}
