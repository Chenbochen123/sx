using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    using System.Data;
    public class EqmMixerFaultService : BaseService<EqmMixerFault>, IEqmMixerFaultService
    {
		#region 构造方法

        public EqmMixerFaultService() : base(){ }

        public EqmMixerFaultService(string connectStringKey) : base(connectStringKey){ }

        public EqmMixerFaultService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public class QueryParams
        {
            public string objID { get; set; }
            public string faultName { get; set; }
            public string faultType { get; set; }
            public string equipCode { get; set; }
            public string AlarmState { get; set; }
            public string faultBeginDate { get; set; }
            public string faultEndDate { get; set; }
            public string workShopCode { get; set; }
            public PageResult<EqmMixerFault> pageParams { get; set; }
        }

        /// <summary>
        /// 分页方法
        ///    and faultcode <> '8'  由闫志旭于2015.3.30应张恒要求所加
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public PageResult<EqmMixerFault> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<EqmMixerFault> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@" SELECT a.ObjID, a.FaultCode, a.FaultName, a.FaultPosition, 
                                        a.AlarmState, a.FaultDate, FaultType, c.EquipName as EquipCode, 
                                        b.WorkShopName as WorkShopCode, a.Remark, a.DeleteFlag  
                                 FROM EqmMixerFault a 
                                 LEFT JOIN BasWorkShop  b   ON  b.ObjID = a.WorkShopCode
                                 LEFT JOIN BasEquip     c   ON  c.EquipCode = a.EquipCode
                                 WHERE 1=1   and faultcode <> '8' 
                            ");
            if (!string.IsNullOrEmpty(queryParams.objID))
            {
                sqlstr.AppendLine(" AND a.ObjID = " + queryParams.objID);
            }
            if (!string.IsNullOrEmpty(queryParams.faultName))
            {
                sqlstr.AppendLine(" AND a.FaultName like '%" + queryParams.faultName + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.faultType))
            {
                sqlstr.AppendLine(" AND a.FaultType like '%" + queryParams.faultType + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.AlarmState))
            {
                sqlstr.AppendLine(" AND a.AlarmState like '%" + queryParams.AlarmState + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.equipCode))
            {
                sqlstr.AppendLine(" AND a.EquipCode ='" + queryParams.equipCode + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.workShopCode))
            {
                sqlstr.AppendLine(" AND a.WorkShopCode ='" + queryParams.workShopCode + "'");
            }
            try
            {
                if (!string.IsNullOrEmpty(queryParams.faultBeginDate))
                {
                    sqlstr.AppendLine("AND a.FaultDate  >='" + Convert.ToDateTime(queryParams.faultBeginDate).ToString("yyyy-MM-dd HH:mm:ss") + "'");
                }
            }
            catch { }
            try
            {
                if (!string.IsNullOrEmpty(queryParams.faultEndDate))
                {
                    sqlstr.AppendLine("AND a.FaultDate  <='" + Convert.ToDateTime(queryParams.faultEndDate).ToString("yyyy-MM-dd HH:mm:ss") + "'");
                }
            }
            catch { }
            pageParams.QueryStr = sqlstr.ToString();
            if (pageParams.PageSize < 0)
            {
                NBear.Data.CustomSqlSection css = this.GetBySql(sqlstr.ToString());
                pageParams.DataSet = css.ToDataSet();
                return pageParams;
            }
            else
            {
                return this.GetPageDataBySql(pageParams);
            }
        }

        /// <summary>
        /// 根据属性字段对密炼机故障进行分组的统计方法
        /// </summary>
        /// <param name="columnName">属性字段</param>
        /// <param name="faultBeginDate">开始时间</param>
        /// <param name="faultEndDate">截止时间</param>
        /// <param name="count">取多少条</param>
        /// <returns></returns>
        public DataSet GetChartGroupAnalysis(string columnName, DateTime faultBeginDate, DateTime faultEndDate, int count)
        {
            DataSet ds = new DataSet();
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"    SELECT  ");
            if( count != 0)
            {
                sqlstr.AppendLine("TOP " + count); 
            }
            switch (columnName)
            {
                case "故障类型":
                    sqlstr.AppendLine(@" FaultType AS AnalysisName , COUNT(*) AS Count FROM EqmMixerFault WHERE 1=1 ");
                    break;
                case "所属设备":
                    sqlstr.AppendLine(@" b.EquipName AS AnalysisName , COUNT(*) AS Count FROM EqmMixerFault a
                        LEFT JOIN BasEquip     b   ON  b.EquipCode = a.EquipCode
                        WHERE 1=1 ");
                    break;
                case "所属车间":
                    sqlstr.AppendLine(@" b.WorkShopName AS AnalysisName , COUNT(*) AS Count FROM EqmMixerFault a
                        LEFT JOIN BasWorkShop  b   ON  b.ObjID = a.WorkShopCode
                        WHERE 1=1 ");
                    break;
                default:
                    break;
            }
            if (faultBeginDate != Convert.ToDateTime("0001-01-01 0:00:00"))
            {
                sqlstr.AppendLine(" AND FaultDate >= '" + faultBeginDate + "'");
            }
            if (faultEndDate != Convert.ToDateTime("0001-01-01 0:00:00"))
            {
                sqlstr.AppendLine(" AND FaultDate <= '" + faultEndDate + "'");
            }
            switch (columnName)
            {
                case "故障类型":
                    sqlstr.AppendLine(" Group By FaultType ORDER BY Count DESC");
                    break;
                case "所属设备":
                    sqlstr.AppendLine(" Group By b.EquipName ORDER BY Count DESC");
                    break;
                case "所属车间":
                    sqlstr.AppendLine(" Group By b.WorkShopName ORDER BY Count DESC");
                    break;
                default:
                    break;
            }
            ds = this.GetBySql(sqlstr.ToString()).ToDataSet();
            return ds;
        }
    }
}
