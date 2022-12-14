using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    using NBear.Data;
    public class PptLotService : BaseService<PptLot>, IPptLotService
    {
        #region 构造方法

        public PptLotService() : base() { }

        public PptLotService(string connectStringKey) : base(connectStringKey) { }

        public PptLotService(NBear.Data.Gateway way) : base(way) { }

        #endregion 构造方法


        public class QueryParams
        {
            public PageResult<PptLot> PageParams { get; set; }
            public string PmtRecipe { get; set; }
            public string EquipCode { get; set; }
            public string BeginTime { get; set; }
            public string EndTime { get; set; }
            public string ClassID { get; set; }
            public string ShiftID { get; set; }

            public string TypeFlag { get; set; }
        }


        public DataSet GetLotInfoByBarcode(string barcode)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"SELECT	Barcode, l.PlanDate, MaterCode, MaterName, equip.EquipName, 
	                                    SerialID, shift.ShiftName AS ShiftID, class.ClassName AS ClassID, 		
                                        recipe.LotDoneTime ,recipe.RecipeName , recipe.RecipeMaterialCode , P.PlanNum, 
                                        l.StartDatetime, DoneRtime, DoneAllRtime, BwbTime, SetWeight, l.RealWeight, 
	                                    ErrorSgn, ShelfBarcode, ShelfUpdate, TestResult, PjTemp, 
	                                    PjPower, PjEner, PjStatus, MixStatus, PolyDisTime, CBDisTime, 
	                                    OilDisTime, PowderDisTime, SerialBatchID, CBBatch, 
	                                    OilBatch, PolyBatch, PowderBatch, SmallBatch, UsedFlag, 
	                                    UsedDatetime, UsedPlanid, Workerbarcode, MemNote, 		
                                        WarningSgn, Shelfnum, LimitTime, Maxtime, LotEnergy, SDSTime
                                FROM		PptLot l
                                LEFT JOIN	PptShift shift		ON l.ShiftID = shift.ObjID
                                LEFT JOIN	PptClass class		ON l.ClassID = class.ObjID
                                LEFT JOIN	PmtRecipe recipe	ON l.EdtCode = recipe.RecipeVersionID 
								                                AND l.EquipCode = recipe.RecipeEquipCode
								                                AND l.MaterCode = recipe.RecipeMaterialCode
                                LEFT JOIN	BasEquip equip		ON l.EquipCode = equip.EquipCode
                                LEFT JOIN	PptPlan	 p			ON l.PlanID  = P.PlanID");
            sqlstr.AppendLine(@"WHERE 1=1");
            sqlstr.AppendLine(@"AND l.Barcode='" + barcode + "'");

            NBear.Data.CustomSqlSection css = this.GetBySql(sqlstr.ToString());
            return css.ToDataSet();
        }

        public PageResult<PptLot> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<PptLot> pageParams = queryParams.PageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"SELECT t1.Barcode FROM dbo.PptLot t1
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
            if (pageParams.PageSize < 0)
            {
                NBear.Data.CustomSqlSection css = this.GetBySql(sqlstr.ToString());
                pageParams.DataSet = css.ToDataSet();
                return pageParams;
            }
            else
            {
                sqlstr.AppendLine(@"ORDER BY t1.StartDatetime");
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
        public PageResult<PptLot> GetBarCodeScannPageDataBySql(QueryParams queryParams)
        {
            PageResult<PptLot> pageParams = queryParams.PageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"SELECT b.EquipName,s.ShiftName,c.ClassName,p.MaterName,p.SerialBatchID,p.StartDatetime,
            p.Barcode,w.WeightID,w.MaterName WMaterName,w.SetWeight,w.RealWeight,w.MaterBarcode,w.ErrorAllow,w.ErrorOut, CASE WHEN  w.WarningSgn='0' THEN '不报警' WHEN w.WarningSgn='1' THEN '报警' END WarningSgn
            ,CASE WHEN w.MaterQua='1' THEN '合格' WHEN w.MaterQua='2' THEN '不合格' WHEN w.MaterQua='3' THEN '检验' WHEN w.MaterQua='4' THEN '未达到停放时间' END MaterQua
            ,w.WeighTime,w.WeighType,CASE WHEN w.WeighState='0' THEN '手动' WHEN w.WeighState='1' THEN '自动' END WeighState
            FROM dbo.PptLot p INNER JOIN dbo.PptWeigh w
            ON p.Barcode = w.Barcode LEFT JOIN dbo.BasEquip b ON p.EdtCode=b.EquipCode
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

        public DataSet GetPptLot(QueryParams queryParams)
        {
            StoredProcedureSection sps = this.defaultGateway.FromStoredProcedure("GetPptLot");
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("BeginTime"), this.TypeToDbType(queryParams.BeginTime.GetType()), queryParams.BeginTime);
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("EndTime"), this.TypeToDbType(queryParams.EndTime.GetType()), queryParams.EndTime);
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("ShiftID"), this.TypeToDbType(queryParams.ShiftID.GetType()), queryParams.ShiftID);
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("EquipCode"), this.TypeToDbType(queryParams.EquipCode.GetType()), queryParams.EquipCode);
            //sps.AddInputParameter(this.defaultGateway.BuildDbParamName("TypeFlag"), this.TypeToDbType(queryParams.TypeFlag.GetType()), queryParams.TypeFlag);
            return sps.ToDataSet();
        }
    }
}
