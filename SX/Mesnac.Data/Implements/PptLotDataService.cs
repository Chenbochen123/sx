using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class PptLotDataService : BaseService<PptLotData>, IPptLotDataService
    {
        #region 构造方法

        public PptLotDataService() : base() { }

        public PptLotDataService(string connectStringKey) : base(connectStringKey) { }

        public PptLotDataService(NBear.Data.Gateway way) : base(way) { }

        #endregion 构造方法


        public class QueryParams
        {
            public QueryParams()
            {
                PageParams = new PageResult<PptLotData>();
                BeginTime = null;
                EndTime = null;
                ClassID = null;
                ShiftID = null;
                MaterCode = null;
                ShelfBarcode = null;
                StopTime = null;
                Barcode = null;
            }
            public string PmtRecipe { get; set; }
            public string EquipCode { get; set; }
            public string BeginTime { get; set; }
            public string EndTime { get; set; }
            public string ClassID { get; set; }
            public string ShiftID { get; set; }
            public string MaterCode { get; set; }
            public string ShelfBarcode { get; set; }
            public string StopTime { get; set; }
            public string Barcode { get; set; }
            public PageResult<PptLotData> PageParams { get; set; }
        }

        public DataSet GetLostInfoByBarcode(string barcode)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"SELECT t1.*,
                t2.RecipeModifyTime,t3.ItemName AS RecipeStateName,
                t4.ClassName,t5.ShiftName,t2.RecipeName
                FROM dbo.PptLotData t1

                LEFT JOIN dbo.PmtRecipe t2
                ON t2.RecipeEquipCode=t1.EquipCode 
                AND t2.RecipeMaterialCode=t1.MaterCode 
                AND t2.RecipeVersionID=t1.EdtCode

                LEFT JOIN dbo.SysCode t3 ON t2.RecipeState = t3.ItemCode AND t3.TypeID='PmtState'

                INNER JOIN dbo.PptClass t4 ON t1.ClassID=t4.ObjID
                INNER JOIN dbo.PptShift t5 ON t1.ShiftID=t5.ObjID
                ");
            sqlstr.AppendLine(@"WHERE 1=1");
            sqlstr.AppendLine(@"AND t1.Barcode='" + barcode + "'");

            NBear.Data.CustomSqlSection css = this.GetBySql(sqlstr.ToString());
            return css.ToDataSet();
        }

        public DataSet GetLotInfoByBarcode(string barcode)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"SELECT	l.*,shift.ShiftName,class.ClassName,
                                recipe.LotDoneTime ,recipe.RecipeName , recipe.RecipeMaterialCode,recipe.RecipeModifyTime,
                                equip.EquipName, P.PlanNum,rstate.ItemName AS RecipeStateName,
                                test.ItemName AS TestResultName, mixstatus.ItemName AS MixStatusName,
                                u.UserName,u.HRCode AS UserHRCode,u.RealName AS UserRealName,M.MaterialName
                                FROM		PptLotData l
                                LEFT JOIN	PptShift shift		ON l.ShiftID = shift.ObjID
                                LEFT JOIN	PptClass class		ON l.ClassID = class.ObjID
                                LEFT JOIN	PmtRecipe recipe	ON l.EdtCode = recipe.RecipeVersionID 
                                                                    AND l.EquipCode = recipe.RecipeEquipCode
                                                                    AND l.MaterCode = recipe.RecipeMaterialCode
                                LEFT JOIN	BasEquip equip		ON l.EquipCode = equip.EquipCode
                                LEFT JOIN	PptPlan	 p			ON l.PlanID  = P.PlanID
                                LEFT JOIN  BasMaterial M ON p.RecipeMaterialCode = M.MaterialCode
                                LEFT JOIN   SysCode  test ON l.TestResult=test.ItemCode AND test.TypeID='TestResult'
                                LEFT JOIN   SysCode  mixstatus ON l.MixStatus=mixstatus.ItemCode AND mixstatus.TypeID='MixStatus'
                                LEFT JOIN   SysCode rstate   ON recipe.RecipeState = rstate.ItemCode AND rstate.TypeID='PmtState'
                                LEFT JOIN   BasUser u ON l.Workerbarcode=u.HRCode
               ");
            sqlstr.AppendLine(@"WHERE 1=1");
            sqlstr.AppendLine(@"AND l.Barcode='" + barcode + "'");

            NBear.Data.CustomSqlSection css = this.GetBySql(sqlstr.ToString());
            return css.ToDataSet();
        }

        public PageResult<PptLotData> GetBarcodeTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<PptLotData> pageParams = queryParams.PageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"SELECT t1.Barcode FROM dbo.PptLotData t1
                                        LEFT JOIN dbo.PmtRecipe t2
                                        ON t2.RecipeEquipCode=t1.EquipCode 
                                        AND t2.RecipeMaterialCode=t1.MaterCode 
                                        AND t2.RecipeVersionID=t1.EdtCode ");
            sqlstr.AppendLine(@" WHERE 1=1");
            if (!string.IsNullOrWhiteSpace(queryParams.PmtRecipe))
            {
                sqlstr.AppendLine(@"AND t2.ObjID=" + queryParams.PmtRecipe);
            }
            if (!string.IsNullOrWhiteSpace(queryParams.EquipCode))
            {
                sqlstr.AppendLine(@"AND t1.EquipCode='" + queryParams.EquipCode + "'");
            }
            if (!string.IsNullOrWhiteSpace(queryParams.BeginTime))
            {
                sqlstr.AppendLine(@"AND t1.StartDatetime>='" + queryParams.BeginTime + "'");
            }
            if (!string.IsNullOrWhiteSpace(queryParams.BeginTime))
            {
                sqlstr.AppendLine(@"AND t1.StartDatetime<='" + queryParams.EndTime + "'");
            }
            if (!string.IsNullOrWhiteSpace(queryParams.ClassID))
            {
                sqlstr.AppendLine(@"AND t1.ClassID='" + queryParams.ClassID + "'");
            }
            if (!string.IsNullOrWhiteSpace(queryParams.ShiftID))
            {
                sqlstr.AppendLine(@"AND t1.ShiftID='" + queryParams.ShiftID + "'");
            }
            if (!string.IsNullOrWhiteSpace(queryParams.MaterCode))
            {
                sqlstr.AppendLine(@"AND t1.MaterCode='" + queryParams.MaterCode + "'");
            }
            sqlstr.AppendLine(@"ORDER BY t1.StartDatetime");
            if (pageParams.PageSize < 0)
            {
                NBear.Data.CustomSqlSection css = this.GetBySql(sqlstr.ToString());
                pageParams.DataSet = css.ToDataSet();
                return pageParams;
            }
            else
            {
                pageParams.QueryStr = sqlstr.ToString();
                return this.GetPageDataByReader(pageParams);
            }
        }

        public PageResult<PptLotData> GetAnalysisTechnology(QueryParams queryParams)
        {
            PageResult<PptLotData> pageParams = queryParams.PageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"SELECT 0 AS LotIndex,t1.PjEner AS MixingEner,
                                t1.SetWeight AS InSetWeight,t1.RealWeight AS InRealWeight,
                                t1.DoneRtime AS DoneRtime,t5.LotDoneTime AS StandTime,
                                t3.RealWeight AS OutRealWeight,t3.ShelfNum*t3.TotalWeight AS OutSetWeight
                                FROM dbo.PptLotData t1
                                LEFT JOIN dbo.BasMaterial t2 ON t1.MaterCode=t2.MaterialCode
                                LEFT JOIN dbo.PptShiftConfig t3 ON t1.ShelfBarcode=t3.Barcode
                                LEFT JOIN dbo.PptPlan t4	ON	t4.PlanID = t1.PlanID
                                LEFT JOIN dbo.PmtRecipe t5 ON t5.RecipeEquipCode = t4.RecipeEquipCode 
                                and t5.RecipeMaterialCode = t4.RecipeMaterialCode and t5.RecipeVersionID = t4.RecipeVersionID
                             ");
            sqlstr.AppendLine(@" WHERE 1=1");
            if (!string.IsNullOrWhiteSpace(queryParams.EquipCode))
            {
                sqlstr.AppendLine(@"AND t1.EquipCode='" + queryParams.EquipCode + "'");
            }
            try
            {
                if (!string.IsNullOrEmpty(queryParams.BeginTime))
                {
                    sqlstr.AppendLine("AND t1.StartDatetime  >='" + Convert.ToDateTime(queryParams.BeginTime).ToString("yyyy-MM-dd HH:mm:ss") + "'");
                }
            }
            catch { }
            try
            {
                if (!string.IsNullOrEmpty(queryParams.EndTime))
                {
                    sqlstr.AppendLine("AND t1.StartDatetime  <='" + Convert.ToDateTime(queryParams.EndTime).AddDays(1).ToString("yyyy-MM-dd HH:mm:ss") + "'");
                }
            }
            catch { }
            if (!string.IsNullOrWhiteSpace(queryParams.ClassID))
            {
                sqlstr.AppendLine(@"AND t1.ClassID='" + queryParams.ClassID + "'");
            }
            if (!string.IsNullOrWhiteSpace(queryParams.ShiftID))
            {
                sqlstr.AppendLine(@"AND t1.ShiftID='" + queryParams.ShiftID + "'");
            }
            if (!string.IsNullOrWhiteSpace(queryParams.ShelfBarcode))
            {
                sqlstr.AppendLine(@"AND t1.Barcode='" + queryParams.ShelfBarcode + "'");
            }
            if (!string.IsNullOrWhiteSpace(queryParams.MaterCode))
            {
                sqlstr.AppendLine(@"AND t1.MaterCode='" + queryParams.MaterCode + "'");
            }
            sqlstr.AppendLine(@"ORDER BY t1.StartDatetime");
            if (pageParams.PageSize < 0)
            {
                NBear.Data.CustomSqlSection css = this.GetBySql(sqlstr.ToString());
                pageParams.DataSet = css.ToDataSet();
                return pageParams;
            }
            else
            {
                pageParams.QueryStr = sqlstr.ToString();
                return this.GetPageDataByReader(pageParams);
            }
        }

        public PageResult<PptLotData> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<PptLotData> pageParams = queryParams.PageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"SELECT t1.*,shift.ShiftName , class.ClassName , test.ItemName AS TestResultName,
                            mixstatus.ItemName AS MixStatusName , equip.EquipName  ,T3.USER_NAME
                            FROM        dbo.PptLotData t1
                            LEFT JOIN   PptShift shift		    ON t1.ShiftID = shift.ObjID
                            LEFT JOIN   PptClass class		    ON t1.ClassID = class.ObjID
                            LEFT JOIN   dbo.BasEquip equip      ON t1.EquipCode=equip.EquipCode
                            LEFT JOIN   dbo.SysCode  test       ON t1.TestResult=test.ItemCode AND test.TypeID='TestResult'
                            LEFT JOIN   dbo.SysCode  mixstatus  ON t1.MixStatus=mixstatus.ItemCode AND mixstatus.TypeID='MixStatus'
                            LEFT JOIN SYS_USER T3 ON T3.USER_ID=T1.Workerbarcode
                            ");
            sqlstr.AppendLine(@" WHERE 1=1 ");

            if (!string.IsNullOrWhiteSpace(queryParams.BeginTime))
            {
                sqlstr.AppendLine(@" AND SUBSTRING(Barcode , 0,7) >='" + queryParams.BeginTime + "'");
            }
            if (!string.IsNullOrWhiteSpace(queryParams.EndTime))
            {
                sqlstr.AppendLine(@" AND SUBSTRING(Barcode , 0,7) <='" + queryParams.EndTime + "'");
            }
            if (!string.IsNullOrWhiteSpace(queryParams.EquipCode))
            {
                sqlstr.AppendLine(@" AND t1.EquipCode='" + queryParams.EquipCode + "'");
            }
            if (!string.IsNullOrWhiteSpace(queryParams.ClassID))
            {
                sqlstr.AppendLine(@" AND t1.ClassID='" + queryParams.ClassID + "'");
            }
            if (!string.IsNullOrWhiteSpace(queryParams.ShiftID))
            {
                sqlstr.AppendLine(@" AND t1.ShiftID='" + queryParams.ShiftID + "'");
            }
            if (!string.IsNullOrWhiteSpace(queryParams.ShelfBarcode))
            {
                sqlstr.AppendLine(@" AND t1.ShelfBarcode='" + queryParams.ShelfBarcode + "'");
            }
            if (!string.IsNullOrWhiteSpace(queryParams.MaterCode))
            {
                sqlstr.AppendLine(@" AND t1.MaterCode='" + queryParams.MaterCode + "'");
            }
            if (!string.IsNullOrWhiteSpace(queryParams.Barcode))
            {
                sqlstr.AppendLine(@" AND t1.Barcode='" + queryParams.Barcode + "'");
            }
            sqlstr.AppendLine(@" ORDER BY t1.StartDatetime");
            if (pageParams.PageSize < 0)
            {
                NBear.Data.CustomSqlSection css = this.GetBySql(sqlstr.ToString());
                pageParams.DataSet = css.ToDataSet();
                return pageParams;
            }
            else
            {
                pageParams.QueryStr = sqlstr.ToString();
                return this.GetPageDataByReader(pageParams);
            }
        }
        /// <summary>
        /// 获取条码漏扫信息
        /// 孙宜建
        /// 2013-03-30
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public PageResult<PptLotData> GetBarCodeScannPageDataBySql(QueryParams queryParams,string sqlwhere)
        {
            PageResult<PptLotData> pageParams = queryParams.PageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"SELECT b.EquipName,s.ShiftName,c.ClassName,p.MaterName,p.SerialBatchID,convert(varchar, p.StartDatetime, 120) StartDatetime,
            p.Barcode,w.WeightID,w.MaterName WMaterName,w.SetWeight,w.RealWeight,w.MaterBarcode,w.ErrorAllow,w.ErrorOut, CASE WHEN  w.WarningSgn='0' THEN '不报警' WHEN w.WarningSgn='1' THEN '报警' END WarningSgn
            ,CASE WHEN w.MaterQua='1' THEN '合格' WHEN w.MaterQua='2' THEN '不合格' WHEN w.MaterQua='3' THEN '检验' WHEN w.MaterQua='4' THEN '未达到停放时间' END MaterQua
            ,w.WeighTime,w.WeighType,CASE WHEN w.WeighState='0' THEN '手动' WHEN w.WeighState='1' THEN '自动' END WeighState
            FROM dbo.PptLotData p INNER JOIN dbo.PptWeighData w
            ON p.Barcode = w.Barcode LEFT JOIN dbo.BasEquip b ON p.EquipCode=b.EquipCode
            LEFT JOIN dbo.PptShift s ON p.ShiftID=s.ObjID
            LEFT JOIN dbo.PptClass c ON c.ObjID=p.ClassID
            WHERE w.MaterBarcode IS NULL ");

            if (!string.IsNullOrWhiteSpace(queryParams.EquipCode))
            {
                sqlstr.AppendLine(@"AND p.EquipCode='" + queryParams.EquipCode + "'");
            }
            if (!string.IsNullOrWhiteSpace(queryParams.BeginTime))
            {
                sqlstr.AppendLine(@"AND p.PlanDate='" + queryParams.BeginTime + "'");
            }
            if (!string.IsNullOrWhiteSpace(queryParams.ClassID))
            {
                sqlstr.AppendLine(@"AND p.ClassID='" + queryParams.ClassID + "'");
            }
            if (!string.IsNullOrEmpty(sqlwhere))
            {
                sqlstr.AppendLine(sqlwhere);
            }
            if (!string.IsNullOrWhiteSpace(queryParams.ShiftID))
            {
                sqlstr.AppendLine(@"AND p.ShiftID='" + queryParams.ShiftID + "'");
            }
            if (pageParams.PageSize < 0)
            {
                NBear.Data.CustomSqlSection css = this.GetBySql(sqlstr.ToString());
                pageParams.DataSet = css.ToDataSet();
                return pageParams;
            }
            else
            {
                sqlstr.AppendLine(@"ORDER BY p.EquipCode");
                pageParams.QueryStr = sqlstr.ToString();
                return this.GetPageDataByReader(pageParams);
            }
        }
        /// <summary>
        /// 获取主机手产量统计
        /// 孙宜建
        /// 2013-05-27
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public PageResult<PptLotData> GetTablePageHostStatisticsBySql(QueryParams queryParams)
        {
            PageResult<PptLotData> pageParams = queryParams.PageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendFormat(@"EXEC dbo.ProcHostStatistics @PlanBeginDate = '{0}',
    @PlanStopDate = '{1}'",queryParams.BeginTime, queryParams.StopTime);
            if (pageParams.PageSize < 0)
            {
                NBear.Data.CustomSqlSection css = this.GetBySql(sqlstr.ToString());
                pageParams.DataSet = css.ToDataSet();
                return pageParams;
            }
            else
            {
                pageParams.QueryStr = sqlstr.ToString();
                return this.GetPageDataByReader(pageParams);
            }
        }

        /// <summary>
        /// 获取班组产量统计
        /// 孙宜建
        /// 2013-05-28
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public PageResult<PptLotData> GetTablePageClassStatisticsBySql(QueryParams queryParams)
        {
            PageResult<PptLotData> pageParams = queryParams.PageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendFormat(@"EXEC dbo.ProcClassStatistics @PlanBeginDate = '{0}',
    @PlanStopDate = '{1}'", queryParams.BeginTime, queryParams.StopTime);
            if (pageParams.PageSize < 0)
            {
                NBear.Data.CustomSqlSection css = this.GetBySql(sqlstr.ToString());
                pageParams.DataSet = css.ToDataSet();
                return pageParams;
            }
            else
            {
                pageParams.QueryStr = sqlstr.ToString();
                return this.GetPageDataByReader(pageParams);
            }
        }
    }
}
